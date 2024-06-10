using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private Profile _enemy;
    [SerializeField] private Profile _player;
    [SerializeField] private CameraShake _cameraShake;

    public void DisplayPlayer(int health, int maxHealth, bool fx = true)
    {
        _player.DisplayDamage(health, maxHealth, fx);
        if (fx) _cameraShake.Invoke(0.2f, 0.2f);
    }

    public void DisplayEnemy(int health, int maxHealth, bool fx = true) =>
        _enemy.DisplayDamage(health, maxHealth, fx);
}