using UnityEngine;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private Image _icon;
    [SerializeField] private HealthBar _healthBar;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void DisplayDamage(int health, int maxHealth, bool fx = true)
    {
        if (fx) 
        { 
            if (_animator != null) _animator.SetTrigger("Damage");
        }
        _healthBar.Display(health, maxHealth);
    }
}
