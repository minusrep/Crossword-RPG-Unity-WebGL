using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yandex;

public class WinPopup : Popup
{
    [SerializeField] private Button _home;
    [SerializeField] private Button _continue;


    public override void Initialize(IPopupController controller)
    {
        base.Initialize(controller);
        _home.onClick.AddListener(Home);
        _continue.onClick.AddListener(Continue);
        _continue.onClick.AddListener(Game.Instance.Advertisement.RateGame);
        _continue.onClick.AddListener(Game.Instance.Advertisement.ShowFullScreenAdv);
    }

    private void Start()
    {
        if (Game.Instance != null) 
            _continue.GetComponentInChildren<TextMeshProUGUI>().text = $"Уровень {Game.Instance.Count + 1}";
    }


    private void Home()
    {
        SceneLoader.LoadScene(SceneID.MENU);
    }

    private void Continue()
    {
        SceneLoader.LoadScene(SceneID.GAME);
    }

    
}