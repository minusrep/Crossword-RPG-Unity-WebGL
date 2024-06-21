using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Joystick 
{
    public event Action<string> OnInputEvent;

    private GameObject _prefab;
    private Transform _parent;
    private List<JoystickButton> _buttons;
    private List<JoystickButton> _selectedButtons;
    private bool _isDragging;
    private char[] _letters;


    public Joystick(Transform parent)
    {
        _parent = parent;
        _prefab = Resources.Load<GameObject>("Prefabs/JoystickButton");
        _buttons = new List<JoystickButton>();
        _selectedButtons = new List<JoystickButton>();
    }

    public void Initialize(char[] letters)
    {
        Clear();
        InstantiateButtons(letters);
    }


    public void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("JoystickRandomize"))
                Randomize();
        }

        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("JoystickButton"))
            {
                _isDragging = true;
                var button = FindJoystickButton(hit.collider.gameObject);
                if (!_selectedButtons.Contains(button))
                {
                    if (_selectedButtons.Count > 0)
                        _selectedButtons?.Last().Connect(button);
                    _selectedButtons.Add(button);
                    button.Selected = true;

                    var value = string.Empty;
                    _selectedButtons.ForEach(button => value += button.Value.ToString());
                    OnInputEvent?.Invoke(value);
                    Game.Instance.Audio.InvokeJoystick();
                }
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            ClearSelected();
        }

        if (_isDragging)
        {
            _selectedButtons.Last().Connect(mousePosition);
        }
    }


    public void ClearSelected()
    {
        _selectedButtons.ForEach(button => button.Clear());
        _selectedButtons.Clear();
        _isDragging = false;
    }

    public void Randomize()
    {
        char[] randomArray = new char[_letters.Length];
        var letters = (char[]) _letters.Clone();

        var random = new System.Random();
        for (int i = _letters.Length; i >= 1; i--)
        {
            var number = random.Next(1, i + 1) - 1;
            randomArray[i - 1] = letters[number];
            letters[number] = letters[i - 1];
        }

        for (int i = 0; i < randomArray.Length; i++)
            _buttons[i].Value = randomArray[i];
    }

    private JoystickButton FindJoystickButton(GameObject gameObject)
    {
        foreach(var button in _buttons)
            if (button.GameObject == gameObject) 
                return button;
        throw new KeyNotFoundException("JoystickButton not found");
    }


    private void InstantiateButtons(char[] letters, float radius = 1f)
    {
        _letters = letters;
        var center = _parent.transform.position;
        var count = letters.Length;
        for(int i = 0; i < count; i++)
        {
            float angle = (360f / count) * i;
            var position = CirclePosition(center, radius, angle);
            var gameObject = Game.Instantiate(_prefab, position, Quaternion.identity, _parent);
            var button = new JoystickButton(gameObject, letters[i]);
            _buttons.Add(button);
        }
        Randomize();
    }
    
    private Vector3 CirclePosition(Vector3 center, float radius, float angle)
    {
        var position = Vector3.zero;
        position.x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        position.y = center.y + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        position.z = center.z;
        return position;
    }
    private void Clear()
    {
        _isDragging = false;
        _buttons.ForEach(button => button.Destroy());
        _buttons.Clear();
        _selectedButtons.Clear();
    }
}
