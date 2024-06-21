using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class MainPopup : Popup
{
    [SerializeField] private Button _start;
    [SerializeField] private Button _settings;

    public override void Initialize(IPopupController controller)
    {
        base.Initialize(controller);
        _start.onClick.AddListener(StartLevel);
        _start.onClick.AddListener(Game.Instance.Audio.InvokeUI);
        _start.GetComponentInChildren<TextMeshProUGUI>().text = $"Уровень {Game.Instance.Count}";

        _settings.onClick.AddListener(controller.OpenPopup<SettingsPopup>);
    }


    private void StartLevel()
    {
        SceneLoader.LoadScene(SceneID.GAME);
    }
}