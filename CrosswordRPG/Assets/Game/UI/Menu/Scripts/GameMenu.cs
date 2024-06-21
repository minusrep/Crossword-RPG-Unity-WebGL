using System;
using System.Linq;
using UnityEngine;

public class GameMenu : BaseMenu
{
    public event Action OnHUDOpenEvent;
    private HUD _hud => (HUD)_popups.First(x => x is HUD);

    public override void Initialize()
    {
        base.Initialize();
        OpenPopup<HUD>();
    }

    public override void OpenPopup()
    {
        base.OpenPopup();
        if (_current is HUD) OnHUDOpenEvent?.Invoke();
    }

    public void DisplayEnemy(int currentHealth, int maxHealth, bool fx) => 
        _hud.DisplayEnemy(currentHealth, maxHealth, fx); 
}