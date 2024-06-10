using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    private GameLooping _loop;

    [SerializeField] private Transform _joystickContent;
    [SerializeField] private Transform _gridContent;
    [SerializeField] private HUD _hud;

    private void Start()
    {
        var joystick = new Joystick(_joystickContent);
        var grid = new Grid(_gridContent);
        joystick.OnInputEvent += grid.OpenWord;
        _loop = new GameLooping(joystick, grid, _hud);
    }

    private void Update()
    {
        _loop.Update();
    }
}