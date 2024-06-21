using UnityEngine;
using UnityEngine.UI;

public class Environment : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _enemy;


    public void Initialize()
    {
        var background = (int) Game.Instance.Level / 5;
        var enemy = Resources.Load<Sprite>($"Enemies/enemy-{Game.Instance.Level}");
        _background.sprite = Resources.Load<Sprite>($"Backgrounds/background-{background}");
        _enemy.sprite = enemy;
    }
}
