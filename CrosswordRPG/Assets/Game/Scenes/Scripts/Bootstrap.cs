using System.Collections;
using UnityEngine;
using Yandex;

public class Bootstrap : EntryPoint
{
    protected override void Initialize()
    {

    }

    protected override IEnumerator InitializeRoutine()
    {
        var player = new Data();
        var yandex = new GameObject("YandexSDK").AddComponent<YandexSDK>();
        yield return yandex.Initialize();
        var game = new GameObject("Game").AddComponent<Game>();
        game.Initialize(yandex);
        SceneLoader.LoadScene(SceneID.MENU);
    }
}