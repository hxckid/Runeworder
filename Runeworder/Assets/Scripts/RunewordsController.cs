using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunewordsController : MonoBehaviour
{
    public UserRunes_SO userRunes;
    public RunewordsDB_SO runewordsDB;
    public GameObject runewordPrefab;
    public GameObject uiParent;

    [SerializeField] List<GameObject> runewordsToShow;
    [SerializeField] List<ListController> sortedLc = new List<ListController>();
    private void Start()
    {
        RuneController.OnRuneToggleChanged += RuneStateChanged;
        runewordsToShow = new List<GameObject>();
        runewordsToShow.Clear();
        UpdateRunewordsList(4);
    }

    private void RuneStateChanged(Runes rune, bool isOn)
    {
        UpdateRunewordsList(4);
    }

    public void UpdateRunewordsList(int type)
    {
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
                lc.hasRunes = 0;
                for (int i = 0; i < rw.runes.Count; i++)
                {
                    lc.runes[i].text = rw.runes[i].ToString();
                    lc.runes[i].color = Color.gray;
                    foreach (var rune in userRunes.hasRunes)
                    {
                        if (rune == rw.runes[i])
                        {
                            lc.runes[i].color = Color.green;
                            lc.hasRunes++;
                        }
                    }
                }
                lc.reqLevel.text = $"Level: {rw.reqLevel}";
                lc.type.text = $"{rw.runewordType}";
                lc.runeword = rw;

                GameObject inst = Instantiate(runewordPrefab, uiParent.transform);
                runewordsToShow.Add(inst);
            }
            else if (type == 5 && rw.gameVersion == "Ressurected")
            {
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
                    lc.runes[i].text = rw.runes[i].ToString();
                    lc.runes[i].color = Color.gray;
                    foreach (var rune in userRunes.hasRunes)
                    {
                        if (rune == rw.runes[i])
                            lc.runes[i].color = Color.green;
                    }
                }
                lc.reqLevel.text = $"Level: {rw.reqLevel}";
                lc.type.text = $"{rw.runewordType}";
                lc.runeword = rw;

                GameObject inst = Instantiate(runewordPrefab, uiParent.transform);
                runewordsToShow.Add(inst);
            }
        }
    }

    public void SortRunewordsBy(string type)
    {
        sortedLc.Clear();

        foreach (var rw in runewordsToShow)
        {
            sortedLc.Add(rw.GetComponent<ListController>());
        }

        switch (type)
        {
            case "Name":
                sortedLc.Sort((a, b) => a.runewordName.text.CompareTo(b.runewordName.text));
                foreach (var lc in sortedLc)
                {
                    var inst = Instantiate(lc.gameObject, uiParent.transform);
                }
                break;
            case "Runes":
                sortedLc.Sort((a, b) => a.hasRunes.CompareTo(b.hasRunes));
                
                break;
            case "Level":
                break;
            default:
                break;
        }
    }
}
