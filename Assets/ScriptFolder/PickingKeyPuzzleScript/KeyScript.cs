using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class KeyScript : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rb;
    private TargetJoint2D joint;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Create joint at the point clicked
                joint = gameObject.AddComponent<TargetJoint2D>();
                joint.autoConfigureTarget = false;
                joint.anchor = rb.transform.InverseTransformPoint(mouseWorldPos);
                joint.target = mouseWorldPos;
                joint.dampingRatio = 1f;
                joint.frequency = 5f;
            }
        }

        if (Input.GetMouseButton(0) && joint != null)
        {
            Vector2 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            joint.target = mouseWorldPos;
        }

        if (Input.GetMouseButtonUp(0) && joint != null)
        {
            Destroy(joint);
        }
    }
}
