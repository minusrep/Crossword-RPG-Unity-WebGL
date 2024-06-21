using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class EntryPoint : MonoBehaviour
{
    protected SceneID SceneID => (SceneID)SceneManager.GetActiveScene().buildIndex;

    private IEnumerator Start()
    {
        if (SceneID != SceneID.BOOTSTRAP && Game.Instance == null) 
        { 
            SceneLoader.LoadScene(SceneID.BOOTSTRAP);
            Debug.Log("BOOTSTRAP");
        }
        else
        {
            Debug.Log("Initialize");
            yield return InitializeRoutine();
            Initialize();
        }

        yield return null;
    }

    protected abstract void Initialize();
    protected virtual IEnumerator InitializeRoutine()
    {
        yield return null;
    }
}
