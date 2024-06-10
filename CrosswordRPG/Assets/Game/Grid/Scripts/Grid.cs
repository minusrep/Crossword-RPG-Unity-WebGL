using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid
{
    public event Action<int> OnWordOpenEvent;
    public bool Complete
    {
        get
        {
            var result = true;
            foreach (var word in _words)
                if (!word.IsOpen) result = false;
            return result;
        }
    }
    public int OpenCount
    {
        get
        {
            var count = 0;
            foreach(var tile in _tiles)
                if (tile.State == TileState.Open) count++;
            return count;
        }
    }

    private Transform _parent;
    private GameObject _prefab;

    private Tile[,] _matrix;
    private List<Tile> _tiles;
    private List<Word> _words;


    public Grid(Transform parent)
    {
        _words = new List<Word>();
        _tiles = new List<Tile>();
        _parent = parent;
        _prefab = Resources.Load<GameObject>("Prefabs/Tile");
    }
    

    public void LoadLevel(Level level)
    {
        Clear();
        InstantiateTiles(level, 0.67f);
    }

    public void OpenWord(string value)
    {
        foreach(var word in _words)
            if (word.Value == value && !word.IsOpen)
            {
                word.Open();
                OnWordOpenEvent?.Invoke(word.Count);
            }
    }

    private void InstantiateTiles(Level level, float tileSize = 1f)
    {
        var size = level.Size;
        _matrix = new Tile[size.x, size.y];
        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                var position = Vector3.zero;
                position.x = (x - size.x / 2f + 0.5f) * tileSize + _parent.transform.position.x;
                position.y = (y * -1f + size.y / 2f - 0.5f) * tileSize + _parent.transform.position.y;
                position.z = 0f;

                var gameObject = Game.Instantiate(_prefab, position, Quaternion.identity, _parent);
                var state = level.Value[_tiles.Count] != '#' ? TileState.Close : TileState.Empty;
                var tile = new Tile(gameObject, level.Value[_tiles.Count], state);
                _tiles.Add(tile);
                _matrix[x, y] = tile;
            }
        }
        FindWords(level);
    }

    private void FindWords(Level level)
    {
        FindWords(level, true);
        FindWords(level, false);
    }

    private void FindWords(Level level, bool horizontal)
    {
        var line = horizontal ? _matrix.GetLength(1) : _matrix.GetLength(0);
        var column = horizontal ? _matrix.GetLength(0) : _matrix.GetLength(1);
        for(int y = 0; y < line; y++)
        {
            var tiles = new List<Tile>();
            var value = string.Empty;
            for(int x = 0; x < column; x++)
            {
                var tile = horizontal ?  _matrix[x, y] : _matrix[y, x];
                if (tile.State != TileState.Empty)
                {
                    value += tile.Value;
                    tiles.Add(tile);
                    if (level.Words.Contains(value))
                        _words.Add(new Word(tiles));
                }
                else if (tiles.Count > 0)
                {
                    tiles = new List<Tile>();
                    value = string.Empty;
                }
            }
        }
    }


    private void Clear()
    {
        _tiles.ForEach(tile => tile.Destroy());
        _tiles.Clear();
        _words.Clear();
    }
}
