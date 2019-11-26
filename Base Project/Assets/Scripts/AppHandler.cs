using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppHandler : MonoBehaviour
{
    //Initialize the environment
    void Awake()
    {
        int tileSize = 212;
        int size = 10;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
                BuildNewTile(i * tileSize, 0.1F, j * tileSize, "Tile " + (i*size + j) );
        }
        //BuildNewTile(0, 0.1F, 0, "Tile 0");
        //BuildNewTile(0, 0.1F, 256, "Tile 1");
    }

    //build a tile from location data
    void BuildNewTile(float x, float y, float z, string name)
    {
        //Testing cache call
        UsgsMapResources res = new UsgsMapResources();
        Cache cache = new Cache();
        cache.setMesh(res, (int) x, (int) z);
        List<float> ElevList = cache.getMesh();
        
        //Assign the GameObject material from the Resources folder
        Material mat = Resources.Load("Materials/Stylize_Grass", typeof(Material)) as Material;

        //Perform array calculations
        int length = ElevList.Count;
        int side = (int) Mathf.Sqrt(length);
        int sqrPtCount = (int) (Mathf.Pow( (side - 1), 2) * 6);

        //Create empty vertices, uv, and triangles arrays and set their values
        Vector3[] vertices = fillVert(length, side, ElevList);
        Vector2[] uv = fillUv(length, side);
        int[] triangles = fillTri(sqrPtCount, side);

        //Create a new Mesh to render
        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();


        //Assign the Mesh to the rendering engine and transform into position
        GameObject Obj = new GameObject(name, typeof(MeshFilter), typeof(MeshRenderer));
        Obj.transform.localScale = new Vector3(1, 1, 1);
        Obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        Obj.transform.position = new Vector3(x - (x/side), y, (side - z) - 212 + (z/side));

        //Graphics.DrawMeshNow(mesh, Vector3.zero, Quaternion.identity, 0);

        Obj.GetComponent<MeshFilter>().mesh = mesh;

        Obj.GetComponent<MeshRenderer>().material = mat;
        
        //for (int i = 0; i < vertices.Length; i++)
        //    Debug.Log(vertices[i]);
    }

    Vector3[] fillVert(int len, int side, List<float> ElevPts)
    {
        Vector3[] tmp = new Vector3[len];
        float pointSep = 10.29F;
        int pos = 0;
        for(int i = 0; i < side; i++)
        {
            //Debug.Log("i =" + i);
            for(int j = 0; j < side; j++)
            {
                tmp[pos] = new Vector3(i, ElevPts[pos]/pointSep, (side -j));
                pos++;
            }
        }
        return tmp;
    }

    Vector2[] fillUv(int len, int side)
    {
        Vector2[] tmp = new Vector2[len];
        int pos = 0;
        for (int i = 0; i < side; i++)
        {
            for (int j = 0; j < side; j++)
            {
                tmp[pos] = new Vector2(i, (side - j));
                pos++;
            }
        }
        return tmp;
    }

    int[] fillTri(int ptCount, int side)
    {
        int[] tmp = new int[ptCount];
        int pos = 0;
        int sqrCount = ptCount / 6;
        for (int i = 0; i < sqrCount; i++)
        {
            int sqrRow = (int) (i / (side - 1));
            tmp[pos++] = i + side + sqrRow;
            tmp[pos++] = i + 1 + sqrRow;
            tmp[pos++] = i + sqrRow;
            tmp[pos++] = i + side + 1 + sqrRow;
            tmp[pos++] = i + 1 + sqrRow;
            tmp[pos++] = i + side + sqrRow;
        }
        return tmp;
    }
}
