using UnityEngine;

public class MenuEntryPoint : EntryPoint
{
    [SerializeField] private Menu _menu;

    protected override void Initialize()
    {
        _menu.Initialize();
    }
}
