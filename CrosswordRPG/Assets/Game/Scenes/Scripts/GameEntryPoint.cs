using UnityEngine;

public class GameEntryPoint : EntryPoint
{
    private GameLooping _loop;

    [SerializeField] private Transform _joystickContent;
    [SerializeField] private Transform _gridContent;
    [SerializeField] private GameMenu _menu;
    [SerializeField] private Environment _environment;

    protected override void Initialize()
    {
        var joystick = new Joystick(_joystickContent);
        var grid = new Grid(_gridContent);
        joystick.OnInputEvent += grid.OpenWord;

        _menu.Initialize();

        _environment.Initialize();

        _loop = new GameLooping(joystick, grid);
        _loop.OnEnemyDisplayEvent += _menu.DisplayEnemy;
        _loop.OnCompleteEvent += _menu.OpenPopup<WinPopup>;

        _menu.OnHUDOpenEvent += _loop.OnEnable;
        _loop.Begin();

    }


    private void Update()
    {
        if (_loop != null) _loop.Update();
    }
}