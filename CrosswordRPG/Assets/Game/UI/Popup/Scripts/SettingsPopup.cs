using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : Popup
{
    [SerializeField] private MuteButton _muteMusic;
    [SerializeField] private MuteButton _muteVolume;
    [SerializeField] private Button _close;

    public override void Initialize(IPopupController controller)
    {
        base.Initialize(controller);
        _close.onClick.AddListener(controller.OpenPopup);
        _muteMusic.Initialize(Game.Instance.Audio.ChangeMuteMusic);
        _muteVolume.Initialize(Game.Instance.Audio.ChangeMuteSounds);

        _muteMusic.AddListener(Game.Instance.Audio.InvokeUI);
        _muteVolume.AddListener(Game.Instance.Audio.InvokeUI);
    }
}