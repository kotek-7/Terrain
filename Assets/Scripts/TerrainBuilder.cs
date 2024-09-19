using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class TerrainBuilder : MonoBehaviour
{
    public GameObject block;

    void Start()
    {
        // Terrain terrain = GenerateTerrain(64, 64, 20f, 8f);
        Terrain terrain = GenerateMinecraftTerrain(64, 64, 20f, 8f);
        BuildTerrain(terrain, 1f);
    }

    /// <summary>
    /// �n�`���𐶐����܂��B
    /// </summary>
    /// <param name="xLength">x�������̐����͈�</param>
    /// <param name="zLength">z�������̐����͈�</param>
    /// <param name="mapScale">�n�}�̃X�P�[���A�傫���قǃY�[�������</param>
    /// <param name="heightScale">�����̃X�P�[���A�傫���ق�y�������͈̔͂��傫���Ȃ�</param>
    /// <returns>��������Terrain�^�̒n�`���</returns>
    public Terrain GenerateTerrain(int xLength, int zLength, float mapScale, float heightScale)
    {
        var resultBlockHeights = new List<List<float>>();

        for (int xIndex = 0; xIndex < xLength; xIndex++)
        {
            resultBlockHeights.Add(new List<float>());
            for (int zIndex = 0; zIndex < zLength; zIndex++)
            {
                float xInput = xIndex / mapScale;
                float zInput = zIndex / mapScale;

                float rawHeight = Mathf.PerlinNoise(xInput, zInput);
                float height = rawHeight * heightScale;

                resultBlockHeights[xIndex].Add(height);
            }
        }

        return new Terrain(resultBlockHeights);
    }

    /// <summary>
    /// Minecraft���̒n�`���𐶐����܂��B
    /// </summary>
    /// <param name="xLength">x�������̐����͈�</param>
    /// <param name="zLength">z�������̐����͈�</param>
    /// <param name="mapScale">�n�}�̃X�P�[���A�傫���قǃY�[�������</param>
    /// <param name="heightScale">�����̃X�P�[���A�傫���ق�y�������͈̔͂��傫���Ȃ�</param>
    /// <returns>��������Terrain�^�̒n�`���</returns>
    public Terrain GenerateMinecraftTerrain(int xLength, int zLength, float mapScale, float heightScale)
    {
        var resultBlockHeights2D = new List<List<float>>();

        for (int xIndex = 0; xIndex < xLength; xIndex++)
        {
            resultBlockHeights2D.Add(new List<float>());
            for (int zIndex = 0; zIndex < zLength; zIndex++)
            {
                float xInput = xIndex / mapScale;
                float zInput = zIndex / mapScale;

                float rawHeight = Mathf.PerlinNoise(xInput, zInput);
                float height = Mathf.Round(rawHeight * heightScale);

                resultBlockHeights2D[xIndex].Add(height);
            }
        }

        return new Terrain(resultBlockHeights2D);
    }

    /// <summary>
    /// �n�`�������Ƀ��[���h�Ƀu���b�N��z�u���܂��B
    /// </summary>
    /// <param name="terrain">���ƂȂ�n�`���</param>
    /// <param name="blockSize">1�u���b�N���̑傫��</param>
    public void BuildTerrain(Terrain terrain, float blockSize)
    {
        for (int xIndex = 0; xIndex < terrain.BlockHeights2D.Count; xIndex++)
        {
            var blockHeights1D = terrain.BlockHeights2D[xIndex]; 
            for (int zIndex = 0; zIndex < blockHeights1D.Count; zIndex++)
            {
                var height = blockHeights1D[zIndex];    

                float x = xIndex * blockSize;
                float z = zIndex * blockSize;
                float y = height;

                SetBlock(x, y, z);
            }
        }
    }

    /// <summary>
    /// �w�肵�����W�Ƀu���b�N��1�ݒu���܂��B
    /// </summary>
    /// <param name="x">x���W</param>
    /// <param name="y">y���W</param>
    /// <param name="z">z���W</param>
    void SetBlock(float x, float y, float z)
    {
        Debug.Log($"Set block at ({x}, {y}, {z})");
        Instantiate(block, new Vector3(x, y, z), Quaternion.identity);
    }
}
