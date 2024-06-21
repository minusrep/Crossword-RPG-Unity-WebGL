using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public bool Active => gameObject.activeSelf;
    private Animator _animator;

    public void Initialize()
    {
        DontDestroyOnLoad(gameObject);
        _animator = GetComponent<Animator>();
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