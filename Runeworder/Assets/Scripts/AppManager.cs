using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum Languages { En, Ru }

public enum GameState { Runes, Runewords, Tooltip }

[Serializable]
public class AppManager : MonoBehaviour
{
    public static AppManager instance = null;

    public UserRunes_SO userRunes;
    public GameState gameState;
    public GameObject runewordsTab;
    public GameObject runesTab;
    public GameObject quitText;
    public GameObject runesBtn;
    public GameObject runewordsBtn;
    public Languages currentLanguage;
    public Canvas canvas;

    public Font enFont;
    public Font ruFont;
    Text[] links;

    UserData userData;
    string json;
    string key = "UserData";
    GameObject txt;

    public delegate void LanguageHandler(Languages languages);
    public static event LanguageHandler OnLanguageChanged;

    private void OnValidate()
    {
        //links = canvas.GetComponentsInChildren<Text>();

        switch (currentLanguage)
        {
            case Languages.En:
                OnLanguageChanged?.Invoke(Languages.En);
                //foreach (var text in links)
                //{
                //    text.font = enFont;
                //}
                break;
            case Languages.Ru:
                OnLanguageChanged?.Invoke(Languages.Ru);
                //foreach (var text in links)
                //{
                //    text.font = ruFont;
                //}
                break;
        }
    }

    private void OnEnable()
    {
        if (instance == null)
            instance = this;
        else if (instance == this)
            Destroy(gameObject);

        RuneController.OnRuneToggleChanged += SaveUserData;
        json = PlayerPrefs.GetString(key);
        userData = JsonUtility.FromJson<UserData>(json);
        userRunes.hasRunes.Clear();
        userRunes.hasRunes = new List<RunesEn>(userData.runes);
        gameState = GameState.Runes;
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Home))
            {
                AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call<bool>("moveTaskToBack", true);
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                switch (instance.gameState) {
                    case GameState.Runes:
                        if (txt != null)
                            Application.Quit();
                        else
                        {
                            txt = Instantiate(quitText, runesTab.transform);
                            Destroy(txt, 2f);
                        }
                        break;
                    case GameState.Runewords:
                        runewordsTab.SetActive(false);
                        runesTab.SetActive(true);
                        runewordsBtn.SetActive(true);
                        runesBtn.SetActive(false);
                        instance.gameState = GameState.Runes;
                        break;
                    case GameState.Tooltip:
                        GameObject go = FindObjectOfType<TooltipController>().gameObject;
                        Destroy(go);
                        instance.gameState = GameState.Runewords;
                        break;
                }
            }
        }
    }

    private void SaveUserData(RunesEn rune, bool isOn)
    {
        userData = new UserData(userRunes.hasRunes);
        json = JsonUtility.ToJson(userData);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public void SetGameState(string state)
    {
        switch (state)
        {
            case "Runes":
                AppManager.instance.gameState = GameState.Runes;
                break;
            case "Runewords":
                AppManager.instance.gameState = GameState.Runewords;
                break;
            case "Tooltip":
                AppManager.instance.gameState = GameState.Tooltip;
                break;
        }
    }
}

[Serializable]
public class UserData
{
    [SerializeField] public List<RunesEn> runes;

    public UserData(List<RunesEn> list)
    {
        runes = new List<RunesEn>(list);
    }
}
