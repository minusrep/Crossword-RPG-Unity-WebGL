using UnityEngine;
using UnityEngine.UI;

public class HUD : Popup
{
    [SerializeField] private Profile _enemy;
    [SerializeField] private Button _settings;
    [SerializeField] private Button _hint;


    public void DisplayEnemy(int health, int maxHealth, bool fx = true) =>
        _enemy.DisplayDamage(health, maxHealth, fx);

    public override void Initialize(IPopupController controller)
    {
        base.Initialize(controller);
        _settings.onClick.AddListener(controller.OpenPopup<GameSettingsPopup>);
        _hint.onClick.AddListener(Game.Instance.Advertisement.ShowRewardedAdv);

    }
}