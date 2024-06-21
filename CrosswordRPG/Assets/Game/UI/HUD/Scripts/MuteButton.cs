using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    private Button _button;

    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _mute;
    [SerializeField] private Sprite _unmute;
    [SerializeField] private bool _music;

    public void Initialize(UnityAction call)
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(call);
        _button.onClick.AddListener(UpdateIcon);
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        var state = _music ? Game.Instance.Audio.MuteMusic : Game.Instance.Audio.MuteSounds;
        _icon.sprite = state ? _mute : _unmute;
    }
    
    public void AddListener(UnityAction call)
    {
        _button.onClick.AddListener(call);
    }
}