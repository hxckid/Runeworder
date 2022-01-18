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

    private void Awake()
    {
        btn.onClick.AddListener(ShowTooltip);
        parent = FindObjectOfType<Canvas>();
    }

    private void ShowTooltip()
    {
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
        tc.rwLadder.text = runeword.isLadder ? "Ladder Item: Yes" : "Ladder Item: No";
        tc.rwClass.text = runeword.classItem != Classes.Any ? $"Class Specified: {runeword.classItem}" : "Class Specified: Any";
        tc.rwVersion.text = $"Game version: {runeword.gameVersion}";

        
    }
}
