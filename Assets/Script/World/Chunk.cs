using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public Vector3 chunkPosition;

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;
    public float QueeTime = 5;
    public Block[,,] Blocks;

    public Material m_Grass;
    public Material m_material;
    public Material m_Dev;

    void Start()
    {

    }


    public void MakeMesh(VoxelDataItem[,,] voxelData)
    {
        chunkPosition = transform.position;
        Blocks = new Block[World.ChunkSize + 1, World.ChunkSize + 1, World.ChunkSize + 1];

        for (int x = 0; x < World.ChunkSize + 1; x++)
        {
            for (int y = 0; y < World.ChunkSize + 1; y++)
            {
                for (int z = 0; z < World.ChunkSize + 1; z++)
                {
                    Blocks[x, y, z] = new Block(x, y, z, voxelData[x, y, z].density, voxelData[x, y, z].typeBlock);
                    Blocks[x, y, z].CurrentChunk = transform.position;//Set this chunk to the tile
                    /*if (Blocks[x, y, z].HaveTree)
                    {
                        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                        obj.transform.position = new Vector3(Blocks[x, y, z].x, Blocks[x, y, z].y, Blocks[x, y, z].z);
                        obj.transform.SetParent(transform, true);
                    }*/
                }
            }
        }

        World.Instance.pendingUpdateMesh.Enqueue(chunkPosition);
    }

    public void UpdateMeshData(MeshDataThread meshDataThread)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<Color> color = new List<Color>();

        for (int i = 0; i < meshDataThread.vertices.Length; i++)
        {
            vertices.Add(meshDataThread.vertices[i].Vertice);
            color.Add(meshDataThread.vertices[i].VertColor);
        }


        meshFilter.mesh = new Mesh();

        meshFilter.mesh.vertices = vertices.ToArray();
        meshFilter.mesh.triangles = meshDataThread.triangles;
        meshFilter.mesh.colors = color.ToArray();

        meshFilter.mesh.RecalculateNormals();

        meshCollider.sharedMesh = meshFilter.mesh;

        meshDataThread.vertices = null;
        meshDataThread.triangles = null;
        meshDataThread.uvs = null;
    }


    public void UpdateMeshChunk()
    {
        World.Instance.pendingUpdateMesh.Enqueue(chunkPosition);
    }

    private void OnDestroy()
    {
        Blocks = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(World.ChunkSize / 2, World.ChunkSize / 2, World.ChunkSize / 2), new Vector3(World.ChunkSize, World.ChunkSize, World.ChunkSize));
    }
}
