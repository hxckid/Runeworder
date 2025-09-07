using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public List<TextMeshProUGUI> textMeshText;
    public Text langText;
    public Dropdown verDrop;
    
    UserData userData;
    string json;
    string key = "UserData";
    GameObject txt;

    public delegate void LanguageHandler(Languages languages, string ver);
    public static event LanguageHandler OnLanguageChanged;
    
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
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Belarusian:
            case SystemLanguage.Russian:
            case SystemLanguage.Ukrainian:
                ChangeLanguage();
                break;
        }
    }

    public void ChangeLanguage()
    {
        currentLanguage = (currentLanguage == Languages.Ru) ? Languages.En : Languages.Ru;
        string currentVersion = verDrop.options[verDrop.value].text;

        switch (currentLanguage)
        {
            case Languages.En:
                langText.text = "ENG";
                OnLanguageChanged?.Invoke(Languages.En, currentVersion);
                buttonsText[0].text = "Reset";
                buttonsText[1].text = "Show Runewords";
                buttonsText[2].text = "Back to Runes";
                buttonsText[3].text = "Custom";
                buttonsText[4].text = "Weapons";
                buttonsText[5].text = "Armors";
                buttonsText[6].text = "Helms";
                buttonsText[7].text = "Shields";
                buttonsText[8].text = "Patch 2.6";
                buttonsText[9].text = "↕ Name";
                buttonsText[10].text = "↕ Runes";
                buttonsText[11].text = "↕ Level";
                for (int i = 12; i < 28; i++)
                    buttonsText[i].text = Enum.GetName(typeof(RunewordWeaponBases), i-12);
                buttonsText[12].text = "Amazon";
                buttonsText[19].text = "Melee";
                buttonsText[20].text = "Missile";
                buttonsText[27].text = "All";
                buttonsText[28].text = "We are searching for:";
                buttonsText[29].text = "All";
                buttonsText[30].text = "Horadric Cube Recipes";
                buttonsText[31].text = "Runewords";
                buttonsText[32].text = $"Current Version: {Application.version}";
                buttonsText[33].text = $"Back";
                buttonsText[34].text = $"Horadric Cube Recipes will be added soon!\n\n" +
                    $"But right now you can help us leaving a feedback on Google Play Store and drop there some stars! " +
                    $"We highly appreciate your trust and your feedback let us growth to ubers level!" +
                    $"We wish you a best possible loot and a good day!\n\nSee you in Sanctuary!\n\n\n\n\nSincerely yours, Sisyphean Labor Team.";
                buttonsText[35].text = "We are searching for:";
                buttonsText[36].text = "Name:";
                buttonsText[37].text = "Sockets:";
                buttonsText[38].text = "Type:";
                buttonsText[39].text = "Find";
                buttonsText[40].text = "Back";
                buttonsText[41].text = "Both Ladder and Non";
                buttonsText[42].text = "Non-Ladder Only";
                buttonsText[43].text = "Ladder Only";
                buttonsText[44].text = "Ladder Runewords Settings:";
                buttonsText[45].text = "Back";
                buttonsText[46].text = "Support the Developer";
                buttonsText[47].text = $"We, Sisyphean Labour team, would like to thank you for your trust and support in using our application!\n\n" +
                    $"We would be grateful if you could take a moment to leave a rating or review on the Google Play Store. Your feedback helps others " +
                    $"discover our app and supports its ongoing development. Thank you!";
                buttonsText[48].text = "Google Play Store";
                buttonsText[49].text = "Our e-mail: sisyfeanlabor@gmail.com";
                buttonsText[50].text = "Patch 2.4";
                buttonsText[51].text = "LoD >= 1.11";
                buttonsText[52].text = "LoD >= 1.10";
                buttonsText[53].text = "Original";
                buttonsText[54].text = "All";
                buttonsText[55].text = "Completed Only";
                buttonsText[56].text = "Tip: Long-press any Rune to see its recipe and details";

                textMeshText[0].text = "Cube Recipes";
                break;

            case Languages.Ru:
                langText.text = "РУС";
                OnLanguageChanged?.Invoke(Languages.Ru, currentVersion);
                buttonsText[0].text = "Сброс";
                buttonsText[1].text = "Найти рунворды";
                buttonsText[2].text = "Назад к рунам";
                buttonsText[3].text = "Ручной";
                buttonsText[4].text = "Оружие";
                buttonsText[5].text = "Броня";
                buttonsText[6].text = "Шлемы";
                buttonsText[7].text = "Щиты";
                buttonsText[8].text = "Патч 2.6";
                buttonsText[9].text = "↕ Имя";
                buttonsText[10].text = "↕ Руны";
                buttonsText[11].text = "↕ Уровень";
                buttonsText[12].text = "Амазонки"; 
                buttonsText[13].text = "Топоры";
                buttonsText[14].text = "Когти";
                buttonsText[15].text = "Дубины";
                buttonsText[16].text = "Кинжалы";
                buttonsText[17].text = "Молоты";
                buttonsText[18].text = "Булавы";
                buttonsText[19].text = "Ближнего боя";
                buttonsText[20].text = "Дальнего боя";
                buttonsText[21].text = "Древковое";
                buttonsText[22].text = "Скипетры";
                buttonsText[23].text = "Копья";
                buttonsText[24].text = "Посохи";
                buttonsText[25].text = "Мечи";
                buttonsText[26].text = "Жезлы";
                buttonsText[27].text = "Все оружие";
                buttonsText[28].text = "Мы ищем рунное слово:";
                buttonsText[29].text = "Все";
                buttonsText[30].text = "Рецепты Куба";
                buttonsText[31].text = "Рунворды";
                buttonsText[32].text = $"Текущая версия: {Application.version}";
                buttonsText[33].text = $"Назад";
                buttonsText[34].text = $"Мы уже в процессе добавления рецептов для куба!\n\n" +
                    $"Но прямо сейчас вы можете помочь нам, оставив отзыв в Google Play Store и поставив нам несколько звездочек! " +
                    $"Мы очень ценим ваше доверие, и ваши отзывы позволяют нам расти до убер уровня!" +
                    $" Желаем вам наилучшего лута и хорошего дня!\n\nДо встречи в Санктуарии!\n\n\n\n\nИскренне Ваша, команда Sisyphean Labor.";
                buttonsText[35].text = "Мы ищем:";
                buttonsText[36].text = "Название:";
                buttonsText[37].text = "Гнезда:";
                buttonsText[38].text = "Тип:";
                buttonsText[39].text = "Найти";
                buttonsText[40].text = "Назад";
                buttonsText[41].text = "Ладдер и Не-Ладдер";
                buttonsText[42].text = "Только Не-Ладдер";
                buttonsText[43].text = "Только Ладдер";
                buttonsText[44].text = "Настройки для Ладдера:";
                buttonsText[45].text = "Назад";
                buttonsText[46].text = "Поддержать разработчика";
                buttonsText[47].text = $"Мы хотели бы поблагодарить Вас за доверие и поддержку нашего приложения и " +
                    $"были бы очень признательны, если бы у вас нашлось время поставить нам оценку или написать отзыв в магазине Google Play. " +
                    $"\n\nВаш отзыв поможет другим узнать об этом приложении и поддержит его развитие. Спасибо!";
                buttonsText[48].text = "Google Play Store";
                buttonsText[49].text = "Наша почта: sisyfeanlabor@gmail.com";
                buttonsText[50].text = "Патч 2.4";
                buttonsText[51].text = "LoD >= 1.11";
                buttonsText[52].text = "LoD >= 1.10";
                buttonsText[53].text = "Оригинал";
                buttonsText[54].text = "Все";
                buttonsText[55].text = "Только Завершенные";
                buttonsText[56].text = "Подсказка: Долгое нажатие на руну покажет её рецепт и свойства";

                textMeshText[0].text = "Рецепты Куба";
                break;
        }
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
                            switch (currentLanguage)
                            {
                                case Languages.En:
                                    quitText.GetComponent<Text>().text = "Press Back again to Quit";
                                    break;
                                case Languages.Ru:
                                    quitText.GetComponent<Text>().text = "Нажмите Назад для выхода";
                                    break;
                            }
                            txt = Instantiate(quitText, runesTab.transform);
                            Destroy(txt, 4f);
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
                        GameObject go = FindFirstObjectByType<TooltipController>().gameObject;
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

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
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
