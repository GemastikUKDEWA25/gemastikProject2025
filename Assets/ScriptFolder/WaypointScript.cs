using UnityEngine;

public class WaypointScript : MonoBehaviour
{
    public Transform target;      // The world object you're tracking
    public Camera mainCam;        // Your camera
    public float edgeOffset = 0.1f; // How far from edge (0–0.5 = % of screen)

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetScreenPos = mainCam.WorldToViewportPoint(target.position);

        bool isOffScreen = targetScreenPos.x < 0 || targetScreenPos.x > 1 ||
                           targetScreenPos.y < 0 || targetScreenPos.y > 1 || targetScreenPos.z < 0;

        Vector3 clampedViewportPos = targetScreenPos;

        if (isOffScreen)
        {
            clampedViewportPos.x = Mathf.Clamp(clampedViewportPos.x, edgeOffset, 1f - edgeOffset);
            clampedViewportPos.y = Mathf.Clamp(clampedViewportPos.y, edgeOffset, 1f - edgeOffset);

            // If behind camera, flip the Z so it's in front
            if (clampedViewportPos.z < 0) clampedViewportPos.z = 1f;
        }

        // Convert to world position in front of camera
        Vector3 worldPos = mainCam.ViewportToWorldPoint(new Vector3(
            clampedViewportPos.x,
            clampedViewportPos.y,
            mainCam.nearClipPlane + 1f // Push it just in front
        ));

        transform.position = worldPos;

        // Optional: face the camera
        transform.rotation = Quaternion.LookRotation(mainCam.transform.forward);
    }
}
