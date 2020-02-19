using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[InitializeOnLoad]
public class CreateBlankMesh
{
    // Start is called before the first frame update
    static CreateBlankMesh()
    {
        GameObject old = GameObject.Find("Blank");

        if (old != null)
            Object.DestroyImmediate(old);

        int sideLen = 256;
        int size = (int)Mathf.Pow(sideLen, 2);

        Vector3[] newVertices = new Vector3[size];
        Vector2[] newUv = new Vector2[size];

        for (int i = 0; i < size; i++)
        {
            newVertices[i] = new Vector3(0F, 0F, 0F);
            newUv[i] = new Vector2(0F, 0F);
        }

        int[] newTriangles = fillTri(sideLen);
    
        Mesh mesh = new Mesh();
        mesh.vertices = newVertices;
        mesh.uv = newUv;
        mesh.triangles = newTriangles;

        GameObject obj = new GameObject("Blank", typeof(MeshFilter), typeof(MeshRenderer));

        obj.transform.localScale = new Vector3(256,1,256);

        obj.GetComponent<MeshFilter>().mesh = mesh;
        obj.GetComponent<MeshRenderer>().material = Resources.Load("Materials/Stylize_Grass",
            typeof(Material)) as Material;
    }

    static int[] fillTri(int side)
    {
        int sqrCount = (int)Mathf.Pow((side - 1), 2);
        int ptCount =  sqrCount * 6;

        int[] tmp = new int[ptCount];

        int pos = 0;

        //Define the number of squares in the mesh
        for (int i = 0; i < sqrCount; i++)
        {
            //Each square in the mesh is defined by two triangles, and each
            //triangle is composed of three points
            //Define the two triangles in each square in the square mesh
            int sqrRow = (int)(i / (side - 1));
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
