using System.Collections.Generic;

/// <summary>
/// 地形情報を表すクラスです。
/// </summary>
public class Terrain
{
    /// <summary>
    /// ブロックの高さを格納する2次元Listです。
    /// </summary>
    public IReadOnlyList<IReadOnlyList<float>> BlockHeights2D { get; private set; }

    public Terrain(IReadOnlyList<IReadOnlyList<float>> blockHeights2D)
    {
        BlockHeights2D = blockHeights2D;
    }
}