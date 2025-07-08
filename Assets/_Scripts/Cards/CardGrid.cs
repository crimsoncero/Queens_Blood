using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public enum TileEffectEnum
{
    None,
    Pawn,
    Effect,
    PawnAndEffect,
    Center,
}

[Serializable]
public class CardGrid
{
    public const int WIDTH = 5;
    public const int HEIGHT = 5;

    public enum Orientation
    {
        Normal,
        Rotated,
    }
    
    [SerializeField] private TileEffectEnum[] _grid = new TileEffectEnum[WIDTH * HEIGHT]
    {
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.None, TileEffectEnum.None,TileEffectEnum.None,
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.None, TileEffectEnum.None,TileEffectEnum.None,
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.Center, TileEffectEnum.None,TileEffectEnum.None,
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.None, TileEffectEnum.None,TileEffectEnum.None,
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.None, TileEffectEnum.None,TileEffectEnum.None
    };
    
    private TileEffectEnum this[int i, int j]
    {
        get => _grid[i * HEIGHT + j];
        set => _grid[i * HEIGHT + j] = value;
    }

    public TileEffectEnum this[int i, int j, Orientation o = Orientation.Normal]
    {
        get => o == Orientation.Normal ? this[i, j] : this[WIDTH - 1 - i, HEIGHT - 1 - j];
        set
        {
            if (o == Orientation.Normal)
            {
                this[i, j] = value;
            }
            else
            {
                this[WIDTH - 1 - i, HEIGHT - 1 - j] = value;
            }
        }
    }
    
    public CardGrid()
    {
        
    }
}
