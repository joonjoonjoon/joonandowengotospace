using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class WobblyPlane : MonoBehaviour {
    public float power = 3.0f;
    public float scale = 1.0f;
    private Vector2 perlin1 = new Vector2(0f, 0f);
    public Gradient gradient;
    public Mesh originalMesh;
    public bool flatBottom;
    public Color sideColor;

    private float perlinStartX;
    private float perlinStartY;

    void Start()
    {
        MakeSomeNoise();
    }

    void OnEnable()
    {
        perlinStartX = Random.Range(0.0f, 100.0f);
        perlinStartY = Random.Range(0.0f, 100.0f);

        MakeSomeNoise();
    }

    void Update()
    {
        /*if (Application.isPlaying)
        {
            perlinStartX += 0.01f;
            perlinStartY+=0.01f;
            MakeSomeNoise();
        }*/
    }
        
        
    void MakeSomeNoise()
    {
        perlin1 = new Vector2(perlinStartX, perlinStartY);
        var perlin2 = new Vector2(Random.Range(0f,100f),Random.Range(0f,100f));
        var perlinOffset = Random.value;
        Mesh sm = originalMesh;
        Mesh lm = new Mesh();

        Vector3[] vertices = sm.vertices;
        Vector3[] normals = sm.normals;
        int[] tris = sm.triangles;
        Vector3[] originalVertices = null;
        if (originalMesh != null)
        {
            originalVertices = originalMesh.vertices;
        }
        Color[] colors = new Color[vertices.Length];
       
        for (int i = 0; i < vertices.Length; i++)
        {
            float xCoord = perlin1.x + vertices[i].x * scale;
            float yCoord = perlin1.y + vertices[i].z * scale;

            
            var y = (Mathf.PerlinNoise(xCoord, yCoord) - 0.5f);
           
            vertices[i].y = (y * power);

            if(originalVertices != null)
            {
                vertices[i].y += originalVertices[i].y;
                if (flatBottom && originalVertices[i].y < 0)
                    vertices[i].y = originalVertices[i].y;
            }

            colors[i] = gradient.Evaluate(y + perlinOffset*Mathf.PerlinNoise(perlin2.x ,perlin2.y));
        }

        for (int i = 0; i < normals.Length; i++)
        {
            if(normals[i].y < 0.1f)
            {
                colors[i] = sideColor;
            }
        }

        lm.vertices = vertices;
        lm.triangles = tris; 

        lm.colors = colors;
        lm.RecalculateBounds();
        lm.RecalculateNormals();

        MeshFilter mf = GetComponent<MeshFilter>();
        mf.mesh = lm;           

        MeshCollider mc = GetComponent<MeshCollider>();
        if(mc != null)
            mc.sharedMesh = lm;
    }
}
