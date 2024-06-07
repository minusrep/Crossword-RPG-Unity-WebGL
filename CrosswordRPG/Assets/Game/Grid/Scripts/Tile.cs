using TMPro;
using UnityEngine;

public class Tile 
{
    public char Value
    {
        get => _value;
        set
        {
            _value = value;
            _valueInfo.text = _value.ToString();
        }
    }
    public TileState State
    {
        get => _state;
        set
        {
            _state = value;
            switch (_state)
            {
                case TileState.Open:
                    _animator.SetTrigger("Open");
                    break;
                case TileState.Close:
                    _animator.SetTrigger("Close");
                    break;
                case TileState.Empty:
                    _animator.SetTrigger("Empty");
                    break;
            }
        }
    }

    private char _value;
    private TileState _state;
    private TextMeshProUGUI _valueInfo;
    private GameObject _gameObject;
    private Animator _animator;


    public Tile(GameObject gameObject, char value, TileState state)
    {
        _gameObject = gameObject;
        _valueInfo = _gameObject.GetComponentInChildren<TextMeshProUGUI>();
        _animator = _gameObject.GetComponent<Animator>();

        Value = value;
        State = state;
    }
   

    public void Destroy()
    {
        Game.Destroy(_gameObject);
    }
}