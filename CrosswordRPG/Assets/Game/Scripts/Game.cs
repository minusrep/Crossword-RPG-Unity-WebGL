using UnityEngine;
using Yandex;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    public int Level => _yandexSDK.Data.Level > _levelCount  ? Random.Range(1, _levelCount + 1) : _yandexSDK.Data.Level;
    public int Count => _yandexSDK.Data.Level;
    public Audio Audio { get; private set; }
    public IAdvertisement Advertisement => _yandexSDK;
    private YandexSDK _yandexSDK;
    private int _levelCount;

    public void Initialize(YandexSDK yandexSDK)
    {
        if (Instance != null) 
        {
            Destroy(gameObject);
            return;
        }  

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _levelCount = Resources.LoadAll("Levels").Length;
        _yandexSDK = yandexSDK;
        Audio = new Audio();
    }
    public void Complete()
    {
        _yandexSDK.Data.Level++;
        _yandexSDK.SaveData();
        _yandexSDK.UpdateLeaderboard(_yandexSDK.Data.Level);
    }
}