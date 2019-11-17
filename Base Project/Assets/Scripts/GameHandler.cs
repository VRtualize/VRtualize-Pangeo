using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class Startup
{
    static Startup()
    {
        //If there exists an old GameObject named Mesh, delete it before building a new one
        GameObject oldMesh = GameObject.Find("Mesh");
        if (oldMesh != null)
            Object.DestroyImmediate(oldMesh);

        //Testing cache call
        UsgsMapResources res = new UsgsMapResources();
        Cache cache = new Cache();
        cache.setMesh(res);
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

        /*
        vertices[0] = new Vector3(0,1);
        vertices[1] = new Vector3(1,1);
        vertices[2] = new Vector3(0,0);
        vertices[3] = new Vector3(1,0);

        uv[0] = new Vector2(0,1);
        uv[1] = new Vector2(1,1);
        uv[2] = new Vector2(0,0);
        uv[3] = new Vector2(1,0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 2;
        triangles[4] = 1;
        triangles[5] = 3;
        */

        //Create a new Mesh to render
        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        //Assign the Mesh to the rendering engine and transform into position
        GameObject Obj = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
        Obj.transform.localScale = new Vector3(1, 1, 1);
        Obj.transform.rotation = Quaternion.Euler(90, 0, 0);
        Obj.transform.position = new Vector3(0, 0.1F, 0);

        Obj.GetComponent<MeshFilter>().mesh = mesh;

        Obj.GetComponent<MeshRenderer>().material = mat;

    }

    static Vector3[] fillVert(int len, int side, List<float> ElevPts)
    {
        Vector3[] tmp = new Vector3[len];
        int pos = 0;
        for(int i = 0; i < side; i++)
        {
            //Debug.Log("i =" + i);
            for(int j = 0; j < side; j++)
            {
                tmp[pos] = new Vector3(i, (side - j), (-1) * ElevPts[pos]);
                pos++;
            }
        }
        return tmp;
    }

    static Vector2[] fillUv(int len, int side)
    {
        Vector2[] tmp = new Vector2[len];
        int pos = 0;
        for (int i = 0; i < side; i++)
        {
            for (int j = 0; j < side; j++)
            {
                tmp[pos] = new Vector3(i, (side - j));
                pos++;
            }
        }
        return tmp;
    }

    static int[] fillTri(int ptCount, int side)
    {
        int[] tmp = new int[ptCount];
        int pos = 0;
        int sqrCount = ptCount / 6;
        for (int i = 0; i < sqrCount; i++)
        {
            int sqrRow = (int) (i / (side - 1));
            tmp[pos++] = i + sqrRow;
            tmp[pos++] = i + 1 + sqrRow;
            tmp[pos++] = i + side + sqrRow;
            tmp[pos++] = i + side + sqrRow;
            tmp[pos++] = i + 1 + sqrRow;
            tmp[pos++] = i + side + 1 + sqrRow;
        }
        return tmp;
    }
}
