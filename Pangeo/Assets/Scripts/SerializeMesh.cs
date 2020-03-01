#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System;

namespace TheTide.utils
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter))]
    public class SerializeMesh : MonoBehaviour
    {
        [HideInInspector] [SerializeField] Vector2[] uv;
        [HideInInspector] [SerializeField] Vector3[] verticies;
        [HideInInspector] [SerializeField] int[] triangles;
        [HideInInspector] [SerializeField] bool serialized = false;
        // Use this for initialization

        void Awake()
        {
            if (serialized)
            {
                GetComponent<MeshFilter>().mesh = Rebuild();
            }
        }

        async void Start()
        {
            float i = Convert.ToSingle(name.Substring(name.IndexOf('x') + 1, name.IndexOf('y') - name.IndexOf('x') - 1))*256;
            float j = Convert.ToSingle(name.Substring(name.IndexOf('y') + 1))*256;

            GetComponent<MeshRenderer>().material = await TileBuilder.GetMaterial(i, j);
            GetComponent<MeshFilter>().mesh = await TileBuilder.GetMesh(i, j);
            transform.localScale = Vector3.one;

            if (serialized) return;

            Serialize();
        }

        public void Serialize()
        {
            var mesh = GetComponent<MeshFilter>().mesh;

            uv = mesh.uv;
            verticies = mesh.vertices;
            triangles = mesh.triangles;

            serialized = true;
        }

        public Mesh Rebuild()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = verticies;
            mesh.triangles = triangles;
            mesh.uv = uv;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();

            return mesh;
        }

        public void UpdateMesh(Vector3[] newVerticies, Vector2[] newUv)
        {
            var mesh = GetComponent<MeshFilter>().mesh;
            mesh.vertices = newVerticies;
            mesh.uv = newUv;

            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(SerializeMesh))]
    class SerializeMeshEditor : Editor
    {
        SerializeMesh obj;

        void OnSceneGUI()
        {
            obj = (SerializeMesh)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Rebuild"))
            {
                if (obj)
                {
                    obj.gameObject.GetComponent<MeshFilter>().mesh = obj.Rebuild();
                }
            }

            if (GUILayout.Button("Serialize"))
            {
                if (obj)
                {
                    obj.Serialize();
                }
            }
        }
    }
#endif
}
