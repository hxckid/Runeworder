using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class AppManager : MonoBehaviour
{
    public UserRunes_SO userRunes;

    UserData userData;
    string json;
    string key = "UserData";

    private void OnEnable()
    {
        RuneController.OnRuneToggleChanged += SaveUserData;
    }

    private void SaveUserData(Runes rune, bool isOn)
    {
        userData = new UserData(userRunes.hasRunes);
        json = JsonUtility.ToJson(userData);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    private void Awake()
    {
        json = PlayerPrefs.GetString(key);
        userData = JsonUtility.FromJson<UserData>(json);
        userRunes.hasRunes.Clear();
        userRunes.hasRunes = new List<Runes>(userData.runes);
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