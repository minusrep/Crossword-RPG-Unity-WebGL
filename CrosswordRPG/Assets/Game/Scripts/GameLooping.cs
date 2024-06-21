using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GameLooping
{
    public event Action OnCompleteEvent;
    public event Action<int, int, bool> OnEnemyDisplayEvent;

    private Joystick _joystick;
    private Grid _grid;
    private Level _currentLevel;

    public GameLooping(Joystick joystick, Grid grid)
    {
        _joystick = joystick;
        _grid = grid;
        _grid.OnWordOpenEvent += OnWordOpen;
        Game.Instance.Advertisement.OnRewardedEvent += _grid.OpenTile;
    }

    public void OnEnable()
    {
        _grid.Update();
        Debug.Log("UPDATED");
    }

    public void Update()
    {
        _joystick.Update();
    }

    public void Begin()
    {
        Game.Instance.StartCoroutine(GameRoutine());
    }

    private IEnumerator GameRoutine()
    {
        _currentLevel = Resources.Load<Level>($"Levels/Level_{Game.Instance.Level}");
        BeginLevel(_currentLevel);
        yield return new WaitUntil(() => _grid.Complete);
        OnCompleteEvent?.Invoke();
        Game.Instance.Complete();
    }


    private void BeginLevel(Level level)
    {
        _grid.LoadLevel(level);
        _joystick.Initialize(level.Letters.ToArray());
        OnEnemyDisplayEvent?.Invoke(_currentLevel.Count - _grid.OpenCount, _currentLevel.Count, false);
    }

    private void OnWordOpen(int value)
    {
        _joystick.ClearSelected();
        Game.Instance.Audio.InvokeWin();
        OnEnemyDisplayEvent?.Invoke(_currentLevel.Count - _grid.OpenCount, _currentLevel.Count, true);
    }
}