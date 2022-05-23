using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RunewordsController : MonoBehaviour
{
    public UserRunes_SO userRunes;
    public RunewordsDB_SO runewordsDBEng;
    public RunewordsDB_SO runewordsDBRus;
    public GameObject runewordPrefab;
    public GameObject uiParent;

    [SerializeField] List<GameObject> runewordsToShow;
    int lastPressed = 0;
    bool toggle = false;
    [SerializeField] RunewordsDB_SO runewordsDB;

    private void Start()
    {
        runewordsToShow = new List<GameObject>();
        runewordsToShow.Clear();
        AppManager.OnLanguageChanged += Localize;
        Localize(AppManager.instance.currentLanguage);
    }

    private void Localize(Languages lang)
    {
        switch (lang)
        {
            case Languages.En:
                runewordsDB = runewordsDBEng;
                Text[] en = runewordPrefab.GetComponentsInChildren<Text>();
                foreach (var t in en)
                {
                    t.font = AppManager.instance.latin;
                }
                en[0].fontSize = 55;
                en[1].fontSize = 48;
                en[2].fontSize = 48;
                en[3].fontSize = 48;
                en[4].fontSize = 48;
                en[5].fontSize = 48;
                en[6].fontSize = 48;
                en[7].fontSize = 45;
                en[8].fontSize = 45;
                break;
            case Languages.Ru:
                runewordsDB = runewordsDBRus;
                Text[] ru = runewordPrefab.GetComponentsInChildren<Text>();
                foreach (var t in ru)
                {
                    t.font = AppManager.instance.cyrillic;
                }
                ru[0].fontSize = 44;
                ru[1].fontSize = 36;
                ru[2].fontSize = 36;
                ru[3].fontSize = 36;
                ru[4].fontSize = 36;
                ru[5].fontSize = 36;
                ru[6].fontSize = 36;
                ru[7].fontSize = 36;
                ru[8].fontSize = 40;
                break;
        }
        UpdateRunewordsList(lastPressed);
    }

    public void UpdateRunewordsList(int type)
    {
        lastPressed = type;

        if (runewordsToShow.Count > 0)
        {
            foreach (var rw in runewordsToShow)
            {
                Destroy(rw);
            }
            runewordsToShow.Clear();
        }

        int odd = 1;
        foreach (var rw in runewordsDB.runewords)
        {
            if (rw.runewordType == (RunewordType)type || type == 4)
            {
                rw.hasRunes = 0;
                ListController lc = runewordPrefab.GetComponent<ListController>();
                if (odd % 2 == 0)
                    lc.background.color = Color.red;
                else
                    lc.background.color = Color.white;
                odd++;
                lc.runewordName.text = rw.runewordName;
                foreach (var rune in lc.runes)
                {
                    rune.text = "";
                }
                for (int i = 0; i < rw.runes.Count; i++)
                {
                    switch (AppManager.instance.currentLanguage)
                    {
                        case Languages.En:
                            lc.runes[i].text = rw.runes[i].ToString();
                            lc.reqLevel.text = $"Level: {rw.reqLevel}";
                            lc.type.text = $"{rw.runewordType}";
                            break;
                        case Languages.Ru:
                            RunesRu rus = (RunesRu)rw.runes[i];
                            lc.runes[i].text = rus.ToString();
                            lc.reqLevel.text = $"Уровень: {rw.reqLevel}";
                            switch (rw.runewordType)
                            {
                                case RunewordType.Weapons:
                                    lc.type.text = $"Оружие";
                                    break;
                                case RunewordType.BodyArmor:
                                    lc.type.text = $"Броня";
                                    break;
                                case RunewordType.Helms:
                                    lc.type.text = $"Шлемы";
                                    break;
                                case RunewordType.Shields:
                                    lc.type.text = $"Щиты";
                                    break;
                            }
                            break;
                    }
                    
                    lc.runes[i].color = Color.gray;
                    foreach (var rune in userRunes.hasRunes)
                    {
                        if (rune == rw.runes[i])
                        {
                            lc.runes[i].color = Color.green;
                            rw.hasRunes++;
                        }
                    }
                }

                lc.runeword = rw;

                GameObject inst = Instantiate(runewordPrefab, uiParent.transform);
                runewordsToShow.Add(inst);
            }
            else if (type == 5 && rw.gameVersion == "Ressurected")
            {
                rw.hasRunes = 0;
                ListController lc = runewordPrefab.GetComponent<ListController>();
                if (odd % 2 == 0)
                    lc.background.color = Color.red;
                else
                    lc.background.color = Color.white;
                odd++;
                lc.runewordName.text = rw.runewordName;
                foreach (var rune in lc.runes)
                {
                    rune.text = "";
                }
                for (int i = 0; i < rw.runes.Count; i++)
                {
                    switch (AppManager.instance.currentLanguage)
                    {
                        case Languages.En:
                            lc.runes[i].text = rw.runes[i].ToString();
                            lc.reqLevel.text = $"Level: {rw.reqLevel}";
                            lc.type.text = $"{rw.runewordType}";
                            break;
                        case Languages.Ru:
                            RunesRu rus = (RunesRu)rw.runes[i];
                            lc.runes[i].text = rus.ToString();
                            lc.reqLevel.text = $"Уровень: {rw.reqLevel}";
                            switch (rw.runewordType)
                            {
                                case RunewordType.Weapons:
                                    lc.type.text = $"Оружие";
                                    break;
                                case RunewordType.BodyArmor:
                                    lc.type.text = $"Броня";
                                    break;
                                case RunewordType.Helms:
                                    lc.type.text = $"Шлемы";
                                    break;
                                case RunewordType.Shields:
                                    lc.type.text = $"Щиты";
                                    break;
                            }
                            break;
                    }

                    lc.runes[i].color = Color.gray;
                    foreach (var rune in userRunes.hasRunes)
                    {
                        if (rune == rw.runes[i])
                        {
                            lc.runes[i].color = Color.green;
                            rw.hasRunes++;
                        }
                    }
                }
                lc.runeword = rw;

                GameObject inst = Instantiate(runewordPrefab, uiParent.transform);
                runewordsToShow.Add(inst);
            }
        }
    }

    public void SortRunewordsBy(string type)
    {
        if (toggle)
        {
            switch (type)
            {
                case "Name":
                    runewordsDB.runewords.Sort((a, b) => a.runewordName.CompareTo(b.runewordName));
                    break;
                case "Runes":
                    runewordsDB.runewords.Sort((a, b) =>
                    {
                        int result = a.hasRunes.CompareTo(b.hasRunes);
                        return result == 0 ? a.runewordName.CompareTo(b.runewordName) : result;
                    });
                    break;
                case "Level":
                    runewordsDB.runewords.Sort((a, b) =>
                    {
                        int result = a.reqLevel.CompareTo(b.reqLevel);
                        return result == 0 ? a.runewordName.CompareTo(b.runewordName) : result;
                    });
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case "Name":
                    runewordsDB.runewords.Sort((b, a) => a.runewordName.CompareTo(b.runewordName));
                    break;
                case "Runes":
                    runewordsDB.runewords.Sort((b, a) =>
                    {
                        int result = a.hasRunes.CompareTo(b.hasRunes);
                        return result == 0 ? b.runewordName.CompareTo(a.runewordName) : result;
                    });
                    break;
                case "Level":
                    runewordsDB.runewords.Sort((b, a) =>
                    {
                        int result = a.reqLevel.CompareTo(b.reqLevel);
                        return result == 0 ? b.runewordName.CompareTo(a.runewordName) : result;
                    });
                    break;
            }
        }
        toggle = !toggle;
        UpdateRunewordsList(lastPressed);
    }
}