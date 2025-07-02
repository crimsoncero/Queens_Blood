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
    public const int GRID_WIDTH = 5;
    public const int GRID_HEIGHT = 5;

    public enum Orientation
    {
        Normal,
        Rotated,
    }
    
    [SerializeField] private TileEffectEnum[] _grid = new TileEffectEnum[GRID_WIDTH * GRID_HEIGHT]
    {
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.None, TileEffectEnum.None,TileEffectEnum.None,
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.None, TileEffectEnum.None,TileEffectEnum.None,
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.Center, TileEffectEnum.None,TileEffectEnum.None,
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.None, TileEffectEnum.None,TileEffectEnum.None,
        TileEffectEnum.None ,TileEffectEnum.None, TileEffectEnum.None, TileEffectEnum.None,TileEffectEnum.None
    };
    
    private TileEffectEnum this[int i, int j]
    {
        get => _grid[i * GRID_HEIGHT + j];
        set => _grid[i * GRID_HEIGHT + j] = value;
    }

    public TileEffectEnum this[int i, int j, Orientation o = Orientation.Normal]
    {
        get => o == Orientation.Normal ? this[i, j] : this[GRID_WIDTH - 1 - i, GRID_HEIGHT - 1 - j];
        set
        {
            if (o == Orientation.Normal)
            {
                this[i, j] = value;
            }
            else
            {
                this[GRID_WIDTH - 1 - i, GRID_HEIGHT - 1 - j] = value;
            }
        }
    }
    
    public CardGrid()
    {
        
    }
}
