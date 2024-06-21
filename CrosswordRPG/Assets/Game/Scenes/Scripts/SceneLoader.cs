using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static LoadingScreen LoadingScreen
    {
        get
        {
            if (_loadingScreen == null) CreateLoadingScreen();
            return _loadingScreen;
        }
    }
    private static LoadingScreen _loadingScreen;

    public static void LoadScene(SceneID sceneID)
    {
        var operation = SceneManager.LoadSceneAsync((int)sceneID);
        LoadingScreen.Invoke();
        LoadingScreen.StartCoroutine(LoadingRoutine(operation));
    }

    private static IEnumerator LoadingRoutine(AsyncOperation operation)
    {
        yield return new WaitUntil(() => operation.isDone);
        LoadingScreen.Hide();
    }

    private static void CreateLoadingScreen()
    {
        _loadingScreen = Instantiate(Resources.Load<LoadingScreen>("Prefabs/LoadingScreen"));
        _loadingScreen.Initialize();
        _loadingScreen.OnAnimationOver();
    }
}