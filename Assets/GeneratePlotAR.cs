using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GeneratePlotAR : MonoBehaviour
{
    [Header("Plot Dimensions")]
    [SerializeField] private int xSize;
    [SerializeField] private int zSize;
    [SerializeField] private GameObject pointer;
    [SerializeField] private GameObject gridPoint;
    [SerializeField] private TextMeshPro pointerText;

    [Header("Plot Properties")]
    [SerializeField] private float thresholdDistance;
    [SerializeField] private TextMeshPro tmp;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minZ;
    [SerializeField] private float maxZ;

    [Header("System Properties")]
    [SerializeField] private TotalEnergyCalculator tec;
    [SerializeField] private DihedralAngleCalculator dac;

    private MeshCollider mc;
    private Mesh plotMesh;
    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;
    private bool[] checkVertices;

    private float maxY = 1000f;
    private List<GameObject> spheres = new List<GameObject>();
    private List<float> energyValues = new List<float>();

    // used to create initial plot
    public void CreatePlot(out Vector3[] vertices, out int[] triangles, out Vector2[] uvs)
    {
        vertices = new Vector3[(2 * xSize + 1) * (2 * zSize + 1)];
        for (int i = 0, z = -zSize; z <= zSize; z++)
        {
            for (int x = -xSize; x <= xSize; x++)
            {
                vertices[i] = new Vector3(x, 0, z);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6 * 4];

        int vert = 0;
        int tris = 0;
        for (int z = -zSize; z < zSize; z++)
        {
            for (int x = -xSize; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + 1 + 2 * xSize;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + 1 + 2 * xSize;
                triangles[tris + 5] = vert + 2 + 2 * xSize;

                vert++;
                tris += 6;
            }
            vert++;
        }

        uvs = new Vector2[vertices.Length];
        for (int i = 0, z = 0; z <= 2 * zSize; z++)
        {
            for (int x = 0; x <= 2 * xSize; x++)
            {
                uvs[i] = new Vector2(((float)x - (-xSize)) / xSize - (-xSize), ((float)z - (-zSize)) / zSize - (-zSize));
                i++;
            }
        }
        checkVertices = new bool[vertices.Length];
    }

    // To Update the mesh values at each frame
    private void UpdateMesh(Mesh mesh, Vector3[] vertices, int[] triangles, Vector2[] uvs)
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }

    //Do Something when plot is touched
    private void StartTouch()
    {
        float x = (((dac.CalculateDihedralAngle() + 180f) - minX) / (maxX - minX)) * xSize;
        float z = ((Vector3.Distance(dac.atom1.position, dac.atom4.position) - minZ) / (maxZ - minZ)) * zSize;
        for (int i = 0; i < vertices.Length; i++)
        {
            if (Vector3.Distance(vertices[i], new Vector3(x, 0, z)) <= thresholdDistance)
            {
                Debug.Log(Vector3.Distance(vertices[i], new Vector3(x, 0, z)));
                if (tec.totalEnergy > maxY)
                {
                    maxY = tec.totalEnergy;
                }
                pointer.transform.localPosition = vertices[i];
                if (!checkVertices[i])
                {
                    vertices[i].y = tec.totalEnergy * 12.5f / maxY;
                    GameObject sphere = Instantiate(gridPoint, pointer.transform.position, Quaternion.identity, gameObject.transform);
                    spheres.Add(sphere);
                    sphere.transform.localPosition = new Vector3(sphere.transform.localPosition.x, vertices[i].y, sphere.transform.localPosition.z);
                    energyValues.Add(tec.totalEnergy);
                    checkVertices[i] = true;
                }
                pointerText.text = tec.totalEnergyText.text;
            }
            i++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        plotMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = plotMesh;

        CreatePlot(out vertices, out triangles, out uvs);

        mc = GetComponent<MeshCollider>();
        mc.sharedMesh = plotMesh;
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = maxY.ToString();
        pointerText.transform.parent.LookAt(Camera.main.transform.position);
        StartTouch();
        UpdateMesh(plotMesh, vertices, triangles, uvs);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (vertices == null)
        {
            return;
        }
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(gameObject.transform.position + vertices[i], 0.5f);
        }
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}