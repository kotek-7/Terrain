using System.Collections.Generic;

/// <summary>
/// �n�`����\���N���X�ł��B
/// </summary>
public class Terrain
{
    /// <summary>
    /// �u���b�N�̍������i�[����2����List�ł��B
    /// </summary>
    public IReadOnlyList<IReadOnlyList<float>> BlockHeights2D { get; private set; }

    public Terrain(IReadOnlyList<IReadOnlyList<float>> blockHeights2D)
    {
        BlockHeights2D = blockHeights2D;
    }
}