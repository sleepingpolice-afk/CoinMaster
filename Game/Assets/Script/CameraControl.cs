using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public SpriteRenderer backgroundRenderer;

    void Start()
    {
        if (backgroundRenderer == null)
        {
            Debug.LogError("Background Renderer not assigned!");
            return;
        }

        Camera cam = Camera.main;
        cam.orthographic = true;

        // Get the background size in world units
        float bgWidth = backgroundRenderer.bounds.size.x;
        float bgHeight = backgroundRenderer.bounds.size.y;

        float screenAspect = cam.aspect;

        // Calculate the camera size needed to fit the background
        float targetOrthoSize = bgHeight / 2f;

        // Check if background is wider than the screen (relative to height)
        if (bgWidth / bgHeight > screenAspect)
        {
            // Background is wider than screen - adjust by width
            targetOrthoSize = (bgWidth / screenAspect) / 2f;
        }

        cam.orthographicSize = targetOrthoSize;
    }
}
