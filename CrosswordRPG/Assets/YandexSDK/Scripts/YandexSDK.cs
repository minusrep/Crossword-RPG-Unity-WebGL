using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;
using System;

namespace Yandex
{
    public class YandexSDK : MonoBehaviour, IAdvertisement
    {
        [DllImport("__Internal")]
        private static extern void ShowFullScreenAdvExtern();
        [DllImport("__Internal")]
        private static extern void RateGameExtern();
        [DllImport("__Internal")]
        private static extern void ShowRewardedAdvExtern();
        [DllImport("__Internal")]
        private static extern void UpdateLeaderboardExtern(int value);
        [DllImport("__Internal")]
        private static extern void SaveDataExtern(string data);
        [DllImport("__Internal")]
        private static extern void LoadDataExtern();
        [DllImport("__Internal")]
        private static extern string GetLanguageExtern();


        public event Action OnAdvShowEvent;
        public event Action OnAdvCloseEvent;
        public event Action OnRewardedEvent;

        public bool IsInitialized { get; private set; }
        public Data Data { get; private set; }

        private bool _isEditor;
        private string _path;

        public IEnumerator Initialize()
        {
#if UNITY_EDITOR
            _isEditor = true;
            _path = Application.streamingAssetsPath + "/save.json";
#else
            _isEditor = false;
#endif
            DontDestroyOnLoad(this);
            if (!IsInitialized) LoadData();
            yield return new WaitUntil(() => IsInitialized);
        }
        public void SetData(string value)
        {
            if (IsInitialized) return;
            Data = JsonUtility.FromJson<Data>(value);
            IsInitialized = true;
        }

        public void SaveData()
        {
            var json = JsonUtility.ToJson(Data);

            if (_isEditor) 
            {
                File.WriteAllText(_path, json);
                Debug.Log("Data saved");
            } 
            else
            {
                SaveDataExtern(json);
            }
        }

        public void ShowFullScreenAdv()
        {
            OnAdvShow();
            if (_isEditor) 
            {
                OnAdvClose();
                Debug.Log("Fullscreen adv");
            } 
            else ShowFullScreenAdvExtern();
        }


        public void ShowRewardedAdv()
        {
            if (_isEditor) 
            {
                OnRewarded();
                OnAdvClose();
                Debug.Log("Rewarded adv");
            } 
            else ShowRewardedAdvExtern();
        }

        public void OnAdvShow() 
        {
            Time.timeScale = 0f;
            OnAdvShowEvent?.Invoke();
        }
        public void OnAdvClose() 
        {
            Time.timeScale = 1f;
            OnAdvCloseEvent?.Invoke();
        } 
        public void OnRewarded() => OnRewardedEvent?.Invoke();

        public void RateGame()
        {
            if (_isEditor) Debug.Log("Rate game");
            else RateGameExtern();
        }

        public string GetLanguage()
        {
            if (_isEditor) return "tr";
            else return GetLanguageExtern();
        }

        public void UpdateLeaderboard(int value)
        {
            if (_isEditor) Debug.Log($"Value: {value}");
            else UpdateLeaderboardExtern(value);
        }

        private void LoadData()
        {
            if (_isEditor)
            {
                if (!File.Exists(_path)) 
                {
                    Data = new Data();
                    SaveData();
                }
                Debug.Log($"Data loaded");
                SetData(File.ReadAllText(_path));
            }
                
            else LoadDataExtern();
        }
    }
}
