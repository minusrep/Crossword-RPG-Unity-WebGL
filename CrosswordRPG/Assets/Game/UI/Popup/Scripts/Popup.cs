using UnityEngine;

public abstract class Popup : MonoBehaviour
{
    public bool Active => gameObject.activeSelf;

    private Animator _animator;

    public virtual void Initialize(IPopupController controller)
    {
        _animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public void Invoke()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        _animator.SetTrigger("Hide");
    }

    public void OnAnimationOver()
    {
        gameObject.SetActive(false);
    }
}