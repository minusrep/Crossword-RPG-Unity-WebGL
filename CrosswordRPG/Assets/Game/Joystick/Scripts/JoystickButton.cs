using TMPro;
using UnityEngine;

public class JoystickButton
{
    public bool Selected
    {
        get => _selected;
        set
        {
            _selected = value;
            if (_selected) _animator.SetTrigger("Select");
            else _animator.SetTrigger("Idle");
        }
    }

    public char Value
    {
        get => _value;
        set
        {
            _value = value;
            _valueInfo.text = _value.ToString();
        }
    }

    public GameObject GameObject => _gameObject;

    private bool _selected;
    private char _value;

    private GameObject _gameObject;
    private TextMeshPro _valueInfo;
    private Animator _animator;
    private LineRenderer _lineRenderer;

    public JoystickButton(GameObject gameObject, char value)
    {
        _gameObject = gameObject;
        _valueInfo = _gameObject.GetComponentInChildren<TextMeshPro>();
        _animator = _gameObject.GetComponent<Animator>();
        _lineRenderer = _gameObject.GetComponent<LineRenderer>();
        Value = value;
    }

    public void Clear()
    {
        Selected = false;
        _lineRenderer.positionCount = 0;
    }

    public void Connect(Vector3 position)
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, _gameObject.transform.position);
        _lineRenderer.SetPosition(1, position);
    }

    public void Connect(JoystickButton button) => Connect(button.GameObject.transform.position);

    public void Destroy()
    {
        Game.Destroy(_gameObject);
    }
}