using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAdjust : MonoBehaviour
{
    public float referenceOrthographicSize = 5f;
    public float referenceAspect = 16f / 9f;

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        UpdateCameraSize();
    }

    void Update()
    {
        UpdateCameraSize();
    }

    void UpdateCameraSize()
    {
        float targetAspect = referenceAspect;
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Camera mainCam = cam;
        if (scaleHeight < 1.0f)
        {
            Rect rect = mainCam.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            mainCam.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = mainCam.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            mainCam.rect = rect;
        }

        mainCam.orthographicSize = referenceOrthographicSize;
    }
}
