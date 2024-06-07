using System.Collections.Generic;
using UnityEngine;

public class Word 
{
    public string Value
    {
        get
        {
            var value = string.Empty;
            _tiles.ForEach(tile => value += tile.Value);
            return value;
        }
    }
    public bool IsOpen
    {
        get
        {
            var value = true;
            foreach (var tile in _tiles)
                if (tile.State != TileState.Open) value = false;
            return value;
        }
    }

    public int Count => _tiles.Count;

    private List<Tile> _tiles;

    public Word(List<Tile> tiles)
    {
        _tiles = tiles;
    }

    public void Open()
    {
        if (IsOpen) Debug.Log($"{Value} already open");
        else 
            foreach(var tile in _tiles)
                if(tile.State != TileState.Open) tile.State = TileState.Open;
    }
}