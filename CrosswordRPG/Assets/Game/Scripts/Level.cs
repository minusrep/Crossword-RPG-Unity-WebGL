using System.Collections.Generic;
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
    
    public int Count
    {
        get
        {
            var count = 0;
            foreach (var element in Value)
                if (element != '#') count++;
            return count;
        }
    }

    public Vector2Int Size
    {
        get
        {
            var size = new Vector2Int(0, 1);
            foreach(var element in _value)
                if (element == '\n') size.y++;
            foreach (var element in _value)
                if (element == '\n') break;
                    else size.x++;
            return size;
        }
    }
    public IReadOnlyList<string> Words => _words;
    public IReadOnlyList<char> Letters => _letters;

    [SerializeField][TextArea] private string _value;
    [SerializeField] private List<string> _words;
    [SerializeField] private List<char> _letters;
}