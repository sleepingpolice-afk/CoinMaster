using UnityEngine;


// I dont have idea how to do this, so this is fully ai prompted, tapi gaguna buat logic gamenya so okelah
public class BackgroundScaler : MonoBehaviour
{
    void Start()
    {
        ScaleAndCenterToCoverScreen();
    }

    void ScaleAndCenterToCoverScreen()
    {
        SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>(false);

        if (renderers.Length == 0)
        {
            Debug.LogWarning("No SpriteRenderers found in children of " + gameObject.name + ". Cannot scale background.");
            return;
        }

        if (Camera.main == null)
        {
            Debug.LogError("Main Camera is not found. Cannot scale background.");
            return;
        }

        if (!Camera.main.orthographic)
        {
            Debug.LogWarning("Main Camera is not orthographic. BackgroundScaler assumes an orthographic camera for screen size calculation.");
        }

        Vector3 initialParentPosition = transform.position;
        Vector3 initialParentLocalScale = transform.localScale;
        transform.localScale = Vector3.one;

        Bounds groupBaseWorldBounds = new Bounds();
        bool firstValidBoundSet = false;

        foreach (SpriteRenderer sr in renderers)
        {
            if (sr.sprite != null && sr.gameObject.activeInHierarchy && sr.enabled)
            {
                if (!firstValidBoundSet)
                {
                    groupBaseWorldBounds = sr.bounds;
                    firstValidBoundSet = true;
                }
                else
                {
                    groupBaseWorldBounds.Encapsulate(sr.bounds);
                }
            }
        }

        if (!firstValidBoundSet)
        {
            Debug.LogWarning("No active SpriteRenderers with sprites found in children of " + gameObject.name + ". Cannot scale.");
            transform.position = initialParentPosition;
            transform.localScale = initialParentLocalScale;
            return;
        }


        float groupBaseWidth = groupBaseWorldBounds.size.x;
        float groupBaseHeight = groupBaseWorldBounds.size.y;

        float camOrthographicSize = Camera.main.orthographicSize;
        float screenWorldHeight = camOrthographicSize * 2f;
        float screenWorldWidth = screenWorldHeight * Camera.main.aspect;

        float targetParentScaleX = 0f;
        if (groupBaseWidth > 0.0001f)
        {
            targetParentScaleX = screenWorldWidth / groupBaseWidth;
        }

        float targetParentScaleY = 0f;
        if (groupBaseHeight > 0.0001f)
        {
            targetParentScaleY = screenWorldHeight / groupBaseHeight;
        }

        float finalAbsoluteParentScale;
        if (targetParentScaleX > 0f && targetParentScaleY > 0f)
        {
            finalAbsoluteParentScale = Mathf.Max(targetParentScaleX, targetParentScaleY);
        }
        else if (targetParentScaleX > 0f)
        {
            finalAbsoluteParentScale = targetParentScaleX;
        }
        else if (targetParentScaleY > 0f)
        {
            finalAbsoluteParentScale = targetParentScaleY;
        }
        else
        {
            Debug.LogWarning("Group of sprites has no discernible size (width and height are effectively zero). Setting parent scale to 1.");
            finalAbsoluteParentScale = 1f;
        }

        if (finalAbsoluteParentScale <= 0.0001f)
        {
            finalAbsoluteParentScale = 1f;
        }

        transform.localScale = new Vector3(finalAbsoluteParentScale, finalAbsoluteParentScale, 1f);
        Vector3 unscaledGroupCenterOffsetFromParentPivot = groupBaseWorldBounds.center - initialParentPosition;
        Vector3 scaledGroupCenterOffsetFromParentPivot = unscaledGroupCenterOffsetFromParentPivot * finalAbsoluteParentScale;
        Vector3 targetWorldCenterForGroup = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, initialParentPosition.z);
        transform.position = targetWorldCenterForGroup - scaledGroupCenterOffsetFromParentPivot;
    }
}