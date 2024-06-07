﻿using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class Level : ScriptableObject
{
    public string Value
    {
        get
        {
            var result = _value.Replace("\n", "");
            return result;
        }
    }
    public Vector2Int Size => _size;
    public IReadOnlyList<string> Words => _words;
    public IReadOnlyList<char> Letters => _letters;

    [SerializeField][TextArea] private string _value;
    [SerializeField] private Vector2Int _size;
    [SerializeField] private List<string> _words;
    [SerializeField] private List<char> _letters;
}