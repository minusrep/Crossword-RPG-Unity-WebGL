using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsPopup : SettingsPopup
{
    [SerializeField] private Button _home;
    [SerializeField] private TextMeshProUGUI _level;

    public override void Initialize(IPopupController controller)
    {
        base.Initialize(controller);
        _home.onClick.AddListener(Home);
        _home.onClick.AddListener(Game.Instance.Audio.InvokeUI);
        _level.text = $"Уровень {Game.Instance.Count}";
    }

    private void Home()
    {
        SceneLoader.LoadScene(SceneID.MENU);
    }
}