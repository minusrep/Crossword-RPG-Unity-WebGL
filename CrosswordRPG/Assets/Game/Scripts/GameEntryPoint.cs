using System.Collections;
using System.Linq;
using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    private Joystick _joystick;
    private Grid _grid;

    [SerializeField] private Transform _joystickContent;
    [SerializeField] private Transform _gridContent;

    private void Start()
    {
        _joystick = new Joystick(_joystickContent);
        _grid = new Grid(_gridContent);

        _joystick.OnInputEvent += _grid.OpenWord;
        _grid.OnWordOpenEvent += OnWordOpen;
        StartCoroutine(GameRoutine());
    }

    private void Update()
    {
        _joystick.Update();
    }


    private IEnumerator GameRoutine()
    {
        var level = Resources.Load<Level>("Levels/Level_0");
        BeginLevel(level);
        yield return new WaitUntil(() => _grid.Complete);
        level = Resources.Load<Level>("Levels/Level_1");
        BeginLevel(level);
    }

    private void BeginLevel(Level level)
    {
        _grid.LoadLevel(level);
        _joystick.Initialize(level.Letters.ToArray());
    }

    private void OnWordOpen(int value)
    {
        _joystick.ClearSelected();
    }

}