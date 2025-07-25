using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FlashlightCone2D : MonoBehaviour
{
    public float viewDistance = 6f;
    public float viewAngle = 90f;
    public int rayCount = 100;
    public LayerMask obstacleMask;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    void Awake()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void LateUpdate()
    {
        GenerateCone();
    }

    void GenerateCone()
    {
        float angleStep = viewAngle / rayCount;
        float angleStart = -viewAngle / 2f;

        vertices = new Vector3[rayCount + 2];
        triangles = new int[rayCount * 3];

        Vector3 origin = transform.position;
        vertices[0] = Vector3.zero;

        for (int i = 0; i <= rayCount; i++)
        {
            float angle = angleStart + angleStep * i;
            float angleRad = (angle + transform.eulerAngles.z) * Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

            RaycastHit2D hit = Physics2D.Raycast(origin, dir, viewDistance, obstacleMask);
            Vector3 vertex = hit.collider == null ? origin + dir * viewDistance : (Vector3)hit.point;

            vertices[i + 1] = transform.InverseTransformPoint(vertex);

            if (i < rayCount)
            {
                int triIndex = i * 3;
                triangles[triIndex] = 0;
                triangles[triIndex + 1] = i + 1;
                triangles[triIndex + 2] = i + 2;
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);
    }

}
