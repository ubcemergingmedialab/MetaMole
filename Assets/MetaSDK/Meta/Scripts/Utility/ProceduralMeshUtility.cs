using UnityEngine;

namespace Meta.Utility
{
    public static class ProceduralMeshUtility
    {
        public static void BuildSlice(Mesh mesh, float arcDegrees, int arcVertCount)
        {
            int triCount = arcVertCount - 1;
            int vertCount = arcVertCount + 1;
            var verts = new Vector3[vertCount];
            var tris = new int[triCount * 3];
            var uvs = new Vector2[vertCount];
            var colors = new Color[vertCount];
            int ti = 0;

            verts[0] = Vector3.zero;
            uvs[0] = Vector2.zero;
            colors[0] = Color.white;

            for (int vi = 1; vi < vertCount; vi++)
            {
                float arcProg = (vi - 1f) / (vertCount - 2f);
                float angle = arcDegrees * arcProg / 180f * Mathf.PI;

                verts[vi] = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
                uvs[vi] = new Vector2(arcProg, 1);
                colors[vi] = Color.white;

                if (vi == 1)
                {
                    continue;
                }

                tris[ti++] = 0;
                tris[ti++] = vi;
                tris[ti++] = vi - 1;
            }

            mesh.Clear();
            mesh.vertices = verts;
            mesh.triangles = tris;
            mesh.uv = uvs;
            mesh.colors = colors;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }

        public static void BuildRingArc(Mesh mesh, float arcDegrees, float inner, int arcVertexCount, float startDegree)
        {
            int triCount = (arcVertexCount - 1) * 2;
            int vertCount = arcVertexCount * 2;
            var verts = new Vector3[vertCount];
            var tris = new int[triCount * 3];
            var uvs = new Vector2[vertCount];
            var colors = new Color[vertCount];
            int vi = 0;
            int ti = 0;

            for (int arcI = 0; arcI < arcVertexCount; arcI++)
            {
                float arcProg = arcI / (arcVertexCount - 1f);
                float angle = (startDegree + arcDegrees * arcProg) / 180f * Mathf.PI;

                verts[vi] = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);
                uvs[vi] = new Vector2(arcProg, 1);
                colors[vi] = Color.white;
                vi++;

                verts[vi] = verts[vi - 1] * inner;
                uvs[vi] = new Vector2(arcProg, 0);
                colors[vi] = Color.white;
                vi++;

                if (arcI == 0)
                {
                    continue;
                }

                tris[ti++] = vi - 1;
                tris[ti++] = vi - 2;
                tris[ti++] = vi - 3;

                tris[ti++] = vi - 2;
                tris[ti++] = vi - 4;
                tris[ti++] = vi - 3;
            }

            mesh.Clear();
            mesh.vertices = verts;
            mesh.triangles = tris;
            mesh.uv = uvs;
            mesh.colors = colors;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }
    }
}