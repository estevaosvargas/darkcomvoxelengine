using UnityEngine;
using System.Collections;

[System.Serializable]
public class Block
{
    public int x;
    public int y;
    public int z;

    public TypeBlock Type;
    public BiomeType TileBiome;

    public float density;

    public Vector3 CurrentChunk;

    public Block(int _x, int _y, int _z, float _density, TypeBlock _Type)
    {
        x = _x;
        y = _y;
        z = _z;

        Type = _Type;

        density = _density;

        /*if (y <= 0)
        {
            
        }
        else
        {
            float thisHeight = VoxelStruct.GetTerrainHeight(x, z);

            // Set the value of this point in the terrainMap.
            density = (float)y - thisHeight;

            if (density <= 0.6f && density >= 0.5f)
            {
                isSurface = true;

                System.Random rand = new System.Random(World.Instance.seed + x * y * z);

                if (rand.Next(0,10) <= 3)
                {
                    HaveTree = true;
                }

                SetBlock(BlockType.Grass);
            }
            else if (density < 0.5f)
            {
                FastNoise noise = new FastNoise(0);

                noise.SetFrequency(0.2f);
                noise.SetInterp(FastNoise.Interp.Quintic);

                density = noise.GetPerlin(x, y, z);

                if (density <= 0.5f)
                {
                    SetBlock(BlockType.DirtRoad);
                }
                else
                {
                    SetBlock(BlockType.Air);
                }
            }
            else
            {
                SetBlock(BlockType.Air);
            }
        }*/
    }

    public void RemoveBlock()
    {
        SetBlock(TypeBlock.Air);
        World.Instance.GetChunk(CurrentChunk).UpdateMeshChunk();
    }

    public void PlaceBlock(TypeBlock blockType)
    {
        SetBlock(blockType);
        World.Instance.GetChunk(CurrentChunk).UpdateMeshChunk();
    }

    float getHight()
    {
        FastNoise noise = new FastNoise(0);

        noise.SetFrequency(0.01f);
        noise.SetInterp(FastNoise.Interp.Quintic);

        return y - noise.GetPerlin(x, z, 0) * 20;
    }


    //Valus to determine whear what biome is on the positions
    public TypeBlock SetUpBiome(int x, int y, int z, float sample, float sample2)
    {
        if ((int)sample2 == 0)
        {
            //sem nemhum
            TileBiome = BiomeType.ForestNormal;
        }
        else if ((int)sample2 == 1)
        {
            //Jungle
            TileBiome = BiomeType.Jungle;
        }
        else if ((int)sample2 == 2)
        {
            //Oceano Normal
            TileBiome = BiomeType.ForestNormal;
        }
        else if ((int)sample2 == 3)
        {
            //Deserto
            TileBiome = BiomeType.Montahas;
        }
        else if ((int)sample2 == 4)
        {
            //sem nemhum
            TileBiome = BiomeType.Plain;
        }
        else if ((int)sample2 == 5)
        {
            //sem nemhum
            TileBiome = BiomeType.Snow;
        }
        else if ((int)sample2 == 6)
        {
            //sem nemhum
            TileBiome = BiomeType.Jungle;
        }
        else if ((int)sample2 == 7)
        {
            //sem nemhum
            TileBiome = BiomeType.Desert;
        }
        else if ((int)sample2 == -4)
        {
            //sem nemhum
            TileBiome = BiomeType.ForestNormal_Dense;
        }
        else if ((int)sample2 == 8)
        {
            //sem nemhum
            TileBiome = BiomeType.ForestNormal_Dense;
        }
        else if ((int)sample2 == -8)
        {
            //sem nemhum
            TileBiome = BiomeType.ForestNormal_Dense;
        }
        else if ((int)sample2 == -2)
        {
            //sem nemhum
            TileBiome = BiomeType.ForestNormal_Dense;
        }
        else
        {
            //Debug.Log("BiomeNum : " + (int)sample2);
            TileBiome = BiomeType.ForestNormal;
        }

        return Biome.GetBiome(x, y, z, this);
    }

    public Block[] GetNeighboors(bool diagonals = false)
    {
        Block[] neighbors;

        if (diagonals)
        {
            neighbors = new Block[8];

            neighbors[0] = World.Instance.GetTileAt(x, y,z + 1);//cima
            neighbors[1] = World.Instance.GetTileAt(x + 1, y, z);//direita
            neighbors[2] = World.Instance.GetTileAt(x, y, z - 1);//baixo
            neighbors[3] = World.Instance.GetTileAt(x - 1, y, z);//esquerda

            neighbors[4] = World.Instance.GetTileAt(x + 1, y, z - 1);//corn baixo direita
            neighbors[5] = World.Instance.GetTileAt(x - 1, y, z + 1);//corn cima esquerda
            neighbors[6] = World.Instance.GetTileAt(x + 1, y, z + 1);//corn cima direita
            neighbors[7] = World.Instance.GetTileAt(x - 1, y, z - 1);//corn baixo esuqerda

        }
        else
        {
            neighbors = new Block[6];

            neighbors[0] = World.Instance.GetTileAt(x, y, z - 1);//Atras
            neighbors[1] = World.Instance.GetTileAt(x, y, z + 1);//Frente
            neighbors[2] = World.Instance.GetTileAt(x, y + 1, z);//Cima
            neighbors[3] = World.Instance.GetTileAt(x, y - 1, z);//Baixo
            neighbors[4] = World.Instance.GetTileAt(x - 1, y, z);//esquerda
            neighbors[5] = World.Instance.GetTileAt(x + 1, y, z);//direita
        }

        return neighbors;
    }

    public void SetBlock(TypeBlock blockType)
    {
        Type = blockType;
    }
}
