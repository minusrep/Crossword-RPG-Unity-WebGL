using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class BaseMenu : MonoBehaviour, IPopupController
{
    protected Popup _lastPopup;
    protected Popup _current;
    [SerializeField] protected List<Popup> _popups;

    public virtual void Initialize()
    {
        _popups.ForEach(x => x.Initialize(this));
    }

    public virtual void OpenPopup<T>() where T : Popup
    {
        var founded = _popups.First(x => x is T);
        if (founded == null) return;
        HideAll();
        founded.Invoke();
        _current = founded;
    }

    public virtual void OpenPopup()
    {
        var popup = _lastPopup ? _lastPopup : _popups.First();
        HideAll();
        popup.Invoke();
        _current = popup;
    }

    private void HideAll()
    {
        foreach (var popup in _popups)
        {
            if (popup.Active) _lastPopup = popup;
            popup.Hide();
        }
        Game.Instance.Audio.InvokeUI();
    }
}