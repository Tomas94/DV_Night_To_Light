using UnityEngine;


[RequireComponent(typeof(MeshCollider))]

public class VisionCone : MonoBehaviour
{
    public float coneAngle = 45f;
    public float coneLength = 10f;

    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
    }

    private void Start()
    {
        GenerateConeMesh();
        CreateCollider();
    }

    private void GenerateConeMesh()
    {
        Mesh coneMesh = new Mesh();

        Vector3[] vertices = new Vector3[3];
        vertices[0] = Vector3.zero;
        vertices[1] = Quaternion.Euler(0f, -coneAngle * 0.5f, 0f) * Vector3.forward * coneLength;
        vertices[2] = Quaternion.Euler(0f, coneAngle * 0.5f, 0f) * Vector3.forward * coneLength;

        int[] triangles = new int[3] { 0, 1, 2 };

        coneMesh.vertices = vertices;
        coneMesh.triangles = triangles;

        meshFilter.mesh = coneMesh;
    }

    private void CreateCollider()
    {
        meshCollider.sharedMesh = meshFilter.mesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") {
            Debug.Log("en rango de vision");
        }
    }
}
