using System;
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

    private void Start()
    {
        RuneController.OnRuneToggleChanged += RuneStateChanged;
        runewordsToShow = new List<GameObject>();
        runewordsToShow.Clear();
        UpdateRunewordsList();
    }

    private void RuneStateChanged(Runes rune, bool isOn)
    {
        UpdateRunewordsList();
    }

    private void UpdateRunewordsList()
    {
        int odd = 1;
        foreach (var rw in runewordsDB.runewords)
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
            }
            lc.reqLevel.text = $"Req Lvl: {rw.reqLevel}";
            lc.type.text = $"{rw.runewordType}";

            GameObject inst = Instantiate(runewordPrefab, uiParent.transform);
            runewordsToShow.Add(inst);
        }
    }
}
