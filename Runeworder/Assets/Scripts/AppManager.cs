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
    public List<Text> buttonsText;
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
        switch (currentLanguage)
        {
            case Languages.En:
                OnLanguageChanged?.Invoke(Languages.En);
                foreach (var button in buttonsText)
                {
                    button.font = enFont;
                }
                buttonsText[0].fontSize = 80;
                buttonsText[0].text = "Reset";
                buttonsText[1].fontSize = 70;
                buttonsText[1].text = "Show Runewords";
                buttonsText[2].fontSize = 70;
                buttonsText[2].text = "Back to Runes";
                buttonsText[3].fontSize = 58;
                buttonsText[3].text = "All";
                buttonsText[4].fontSize = 58;
                buttonsText[4].text = "Weapons";
                buttonsText[5].fontSize = 58;
                buttonsText[5].text = "Armors";
                buttonsText[6].fontSize = 58;
                buttonsText[6].text = "Helms";
                buttonsText[7].fontSize = 58;
                buttonsText[7].text = "Shields";
                buttonsText[8].fontSize = 48;
                buttonsText[8].text = "Patch 2.4";
                buttonsText[9].fontSize = 62;
                buttonsText[9].text = "Name";
                buttonsText[10].fontSize = 62;
                buttonsText[10].text = "Runes";
                buttonsText[11].fontSize = 62;
                buttonsText[11].text = "Level";
                break;
            case Languages.Ru:
                OnLanguageChanged?.Invoke(Languages.Ru);
                foreach (var button in buttonsText)
                {
                    button.font = ruFont;
                }
                buttonsText[0].fontSize = 64;
                buttonsText[0].text = "Сбросить";
                buttonsText[1].fontSize = 56;
                buttonsText[1].text = "К рунвордам";
                buttonsText[2].fontSize = 52;
                buttonsText[2].text = "Назад к рунам";
                buttonsText[3].fontSize = 46;
                buttonsText[3].text = "Все";
                buttonsText[4].fontSize = 46;
                buttonsText[4].text = "Оружие";
                buttonsText[5].fontSize = 46;
                buttonsText[5].text = "Броня";
                buttonsText[6].fontSize = 46;
                buttonsText[6].text = "Шлемы";
                buttonsText[7].fontSize = 46;
                buttonsText[7].text = "Щиты";
                buttonsText[8].fontSize = 40;
                buttonsText[8].text = "Патч 2.4";
                buttonsText[9].fontSize = 46;
                buttonsText[9].text = "Имя";
                buttonsText[10].fontSize = 46;
                buttonsText[10].text = "Руны";
                buttonsText[11].fontSize = 46;
                buttonsText[11].text = "Уровень";
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
