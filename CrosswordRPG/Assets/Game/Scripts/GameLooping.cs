using System.Collections;
using System.Linq;
using UnityEngine;

public class GameLooping
{
    private Joystick _joystick;
    private Grid _grid;
    private HUD _hud;
    private Level _currentLevel;
    private Player _player;

    public GameLooping(Joystick joystick, Grid grid, HUD hud)
    {
        _joystick = joystick;
        _grid = grid;
        _hud = hud;
        _grid.OnWordOpenEvent += OnWordOpen;
        _player = new Player { Health = 10, MaxHealth = 10 };

        Game.Instance.StartCoroutine(GameRoutine());
        Game.Instance.StartCoroutine(DamageRoutine());
    }

    public void Update()
    {
        _joystick.Update();
    }

    private IEnumerator GameRoutine()
    {
        _currentLevel = Resources.Load<Level>("Levels/Level_0");
        BeginLevel(_currentLevel);
        yield return new WaitUntil(() => _grid.Complete);
        _currentLevel = Resources.Load<Level>("Levels/Level_1");
        BeginLevel(_currentLevel);
    }

    private IEnumerator DamageRoutine()
    {
        yield return new WaitForSeconds(Random.Range(5, 10f));
        _player.Health -= Random.Range(1, 4);
        _hud.DisplayPlayer(_player.Health, _player.MaxHealth);
        if (_player.Health > 0) Game.Instance.StartCoroutine(DamageRoutine());
    }


    private void BeginLevel(Level level)
    {
        _grid.LoadLevel(level);
        _joystick.Initialize(level.Letters.ToArray());
        _hud.DisplayEnemy(_currentLevel.Count, _currentLevel.Count, false);
        _hud.DisplayPlayer(_player.Health, _player.MaxHealth, false);
    }

    private void OnWordOpen(int value)
    {
        _joystick.ClearSelected();
        _hud.DisplayEnemy(_currentLevel.Count - _grid.OpenCount, _currentLevel.Count);
    }
}