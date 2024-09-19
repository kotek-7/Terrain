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
    /// 地形情報を生成します。
    /// </summary>
    /// <param name="xLength">x軸方向の生成範囲</param>
    /// <param name="zLength">z軸方向の生成範囲</param>
    /// <param name="mapScale">地図のスケール、大きいほどズームされる</param>
    /// <param name="heightScale">高さのスケール、大きいほどy軸方向の範囲が大きくなる</param>
    /// <returns>生成したTerrain型の地形情報</returns>
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
    /// Minecraft風の地形情報を生成します。
    /// </summary>
    /// <param name="xLength">x軸方向の生成範囲</param>
    /// <param name="zLength">z軸方向の生成範囲</param>
    /// <param name="mapScale">地図のスケール、大きいほどズームされる</param>
    /// <param name="heightScale">高さのスケール、大きいほどy軸方向の範囲が大きくなる</param>
    /// <returns>生成したTerrain型の地形情報</returns>
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
    /// 地形情報を元にワールドにブロックを配置します。
    /// </summary>
    /// <param name="terrain">元となる地形情報</param>
    /// <param name="blockSize">1ブロック分の大きさ</param>
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
    /// 指定した座標にブロックを1つ設置します。
    /// </summary>
    /// <param name="x">x座標</param>
    /// <param name="y">y座標</param>
    /// <param name="z">z座標</param>
    void SetBlock(float x, float y, float z)
    {
        Debug.Log($"Set block at ({x}, {y}, {z})");
        Instantiate(block, new Vector3(x, y, z), Quaternion.identity);
    }
}
