using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ListController : MonoBehaviour
{
    public Runeword_SO runeword;
    public Image background;
    public Text runewordName;
    public List<Text> runes;
    public Text reqLevel;
    public Text type;
    public Button btn;

    public Canvas parent;
    public GameObject tooltip;
    public GameObject tooltipRune;

    Color ressurectedColor;

    private void Awake()
    {
        btn.onClick.AddListener(ShowTooltip);
        parent = FindObjectOfType<Canvas>();
        ressurectedColor = new Color32(236,140,24,255);
    }

    private void ShowTooltip()
    {
        AppManager.instance.gameState = GameState.Tooltip;
        var inst = Instantiate(tooltip, parent.transform);
        TooltipController tc = inst.GetComponent<TooltipController>();
        for (int i = 0; i < runeword.sprites.Count; i++)
        {
            tooltipRune.GetComponent<Image>().sprite = runeword.sprites[i];
            Instantiate(tooltipRune, tc.runeIcons.transform);
        }
        tc.rwName.text = runeword.runewordName;
        tc.rwSeq.text = runeword.runesSequence;
        tc.rwStats.text = runeword.statsDesc;
        tc.rwType.text = runeword.subType;
        tc.rwLevel.text = $"Required Level: {runeword.reqLevel}";
        if (runeword.gameVersion != "Ressurected")
            tc.rwLadder.text = runeword.isLadder ? "Diablo 2 LoD Ladder Item" : "Non Ladder Item";
        else
            tc.rwLadder.text = runeword.isLadder ? "Ressurected Ladder Item" : "Non Ladder Item";
        tc.rwClass.text = runeword.classItem != Classes.Any ? $"Class Specified: {runeword.classItem}" : "Class Specified: Any";
        tc.rwVersion.color = Color.white;
        switch (runeword.gameVersion)
        {
            case "Ressurected":
                tc.rwVersion.color = ressurectedColor;
                break;
            case "1.10":
            case "1.11":
                tc.rwVersion.color = new Color32(222, 222, 150, 255);
                break;
        }

        if (runeword.gameVersion == "1.10" || runeword.gameVersion == "1.11")
            tc.rwVersion.text = $"Game version: {runeword.gameVersion}+ LoD";
        else
            tc.rwVersion.text = $"Game version: {runeword.gameVersion}";

        tc.rwItem.text = $"{runeword.recomendedItems}";
    }
}
