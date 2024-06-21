using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HealthBar
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private Image _fill;

    public void Display(int health, int maxHealth)
    {
        _title.text = $"{health}/{maxHealth}";
        _fill.fillAmount = (float)health / maxHealth;
    }
}