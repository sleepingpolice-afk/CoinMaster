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
        float currentAspect = (float)Screen.width / Screen.height;

        if (currentAspect < referenceAspect)
        {
            // Screen is narrower than reference → increase vertical size to maintain horizontal space
            cam.orthographicSize = referenceOrthographicSize * (referenceAspect / currentAspect);
        }
        else
        {
            // Screen is wider or equal → keep default vertical size
            cam.orthographicSize = referenceOrthographicSize;
        }
    }
}
