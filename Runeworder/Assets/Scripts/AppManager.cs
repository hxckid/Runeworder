using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    UserData userData;
    string json;
    string key = "UserData";
    int doubleBack = 0;

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
        userRunes.hasRunes = new List<Runes>(userData.runes);
        gameState = GameState.Runes;
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Home))
            {
                AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
                activity.Call<bool>("moveTaskToBack", true);
                Application.Quit();
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                switch (instance.gameState) {
                    case GameState.Runes:
                        doubleBack++;
                        var txt = Instantiate(quitText, runesTab.transform);
                        StartCoroutine(CheckQiut(txt));
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

    private IEnumerator CheckQiut(GameObject txt)
    {
        yield return new WaitForSeconds(2f);
        if (doubleBack >= 2)
        {
            Application.Quit();
        }
        else
        {
            doubleBack = 0;
            Destroy(txt);
        }
    }

    private void SaveUserData(Runes rune, bool isOn)
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
    [SerializeField] public List<Runes> runes;

    public UserData(List<Runes> list)
    {
        runes = new List<Runes>(list);
    }
}
