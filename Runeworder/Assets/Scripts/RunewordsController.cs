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
    public RunewordsDB_SO workflowDB;
    public GameObject runewordPrefab;
    public GameObject uiParent;

    List<GameObject> runewordsToShow;
    string lastPressed = string.Empty;
    bool toggle = false;
    RunewordsDB_SO currentDB;

    private void Start()
    {
        runewordsToShow = new List<GameObject>();
        runewordsToShow.Clear();
        AppManager.OnLanguageChanged += SetFont;
        SetFont(AppManager.instance.currentLanguage);
        lastPressed = "All";
    }

    private void SetFont(Languages lang)
    {
        switch (lang)
        {
            case Languages.En:
                currentDB = runewordsDBEng;
                Text[] en = runewordPrefab.GetComponentsInChildren<Text>();
                foreach (var t in en)
                {
                    t.font = AppManager.instance.latin;
                }
                en[0].fontSize = 55; //Name
                en[1].fontSize = 44; //Rune
                en[2].fontSize = 44; //Rune
                en[3].fontSize = 44; //Rune
                en[4].fontSize = 44; //Rune
                en[5].fontSize = 44; //Rune
                en[6].fontSize = 44; //Rune 
                en[7].fontSize = 44; //Lvl
                en[8].fontSize = 44; //Type
                break;
            case Languages.Ru:
                currentDB = runewordsDBRus;
                Text[] ru = runewordPrefab.GetComponentsInChildren<Text>();
                foreach (var t in ru)
                {
                    t.font = AppManager.instance.cyrillic;
                }
                ru[0].fontSize = 40; //Name
                ru[1].fontSize = 36; //Rune
                ru[2].fontSize = 36; //Rune
                ru[3].fontSize = 36; //Rune
                ru[4].fontSize = 36; //Rune
                ru[5].fontSize = 36; //Rune
                ru[6].fontSize = 36; //Rune
                ru[7].fontSize = 34; //Lvl
                ru[8].fontSize = 36; //Type
                break;
        }
    }

    public void FilterRunewords(string type)
    {
        ClearWorkflowDB();
        switch (type)
        {
            case "Armors":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.runewordType == RunewordType.BodyArmor)
                    {
                        workflowDB.runewords.Add(rw);
                    }
                }
                break;
            case "Helms":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.runewordType == RunewordType.Helms)
                    {
                        workflowDB.runewords.Add(rw);
                    }
                }
                break;
            case "Shields":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.runewordType == RunewordType.Shields)
                    {
                        workflowDB.runewords.Add(rw);
                    }
                }
                break;
            case "All":
                foreach (var rw in currentDB.runewords)
                {
                    workflowDB.runewords.Add(rw);
                }
                break;
            case "Resurrected":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.gameVersion == "Resurrected")
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "Weapons":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.runewordType == RunewordType.Weapons)
                        workflowDB.runewords.Add(rw);
                }
                break;
            case "Amazon Spears":
                foreach (var rw in currentDB.runewords)
                {
                    if (rw.subType.Contains("Amazon Spears"))
                        workflowDB.runewords.Add(rw);
                }
                break;
        }
        lastPressed = type;
        FillRunewordList();
    }

    private void ClearWorkflowDB()
    {
        workflowDB.runewords.Clear();
    }

    private void ClearRunewordList()
    {
        if (runewordsToShow.Count > 0)
        {
            foreach (var rw in runewordsToShow)
            {
                Destroy(rw);
            }
            runewordsToShow.Clear();
        }
    }

    private void FillRunewordList()
    {
        ClearRunewordList();
        int odd = 1;
        foreach (var rw in workflowDB.runewords)
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
                        lc.reqLevel.text = $"�������: {rw.reqLevel}";
                        switch (rw.runewordType)
                        {
                            case RunewordType.Weapons:
                                lc.type.text = $"������";
                                break;
                            case RunewordType.BodyArmor:
                                lc.type.text = $"�����";
                                break;
                            case RunewordType.Helms:
                                lc.type.text = $"�����";
                                break;
                            case RunewordType.Shields:
                                lc.type.text = $"����";
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

                lc.runeword = rw;
            }
            GameObject inst = Instantiate(runewordPrefab, uiParent.transform);
            runewordsToShow.Add(inst);
        }
    }

    public void SortRunewordsBy(string type)
    {
        if (toggle)
        {
            switch (type)
            {
                case "Name":
                    currentDB.runewords.Sort((b, a) => a.runewordName.CompareTo(b.runewordName));
                    break;
                case "Runes":
                    currentDB.runewords.Sort((a, b) =>
                    {
                        int result = a.hasRunes.CompareTo(b.hasRunes);
                        return result == 0 ? a.runewordName.CompareTo(b.runewordName) : result;
                    });
                    break;
                case "Level":
                    currentDB.runewords.Sort((a, b) =>
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
                    currentDB.runewords.Sort((a, b) => a.runewordName.CompareTo(b.runewordName));
                    break;
                case "Runes":
                    currentDB.runewords.Sort((b, a) =>
                    {
                        int result = a.hasRunes.CompareTo(b.hasRunes);
                        return result == 0 ? b.runewordName.CompareTo(a.runewordName) : result;
                    });
                    break;
                case "Level":
                    currentDB.runewords.Sort((b, a) =>
                    {
                        int result = a.reqLevel.CompareTo(b.reqLevel);
                        return result == 0 ? b.runewordName.CompareTo(a.runewordName) : result;
                    });
                    break;
            }
        }
        toggle = !toggle;
        FilterRunewords(lastPressed);
    }
}