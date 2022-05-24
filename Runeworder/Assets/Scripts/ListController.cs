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
        Text[] txts = tc.GetComponentsInChildren<Text>();
        for (int i = 0; i < runeword.sprites.Count; i++)
        {
            tooltipRune.GetComponent<Image>().sprite = runeword.sprites[i];
            Instantiate(tooltipRune, tc.runeIcons.transform);
        }
        tc.rwName.text = runeword.runewordName;
        tc.rwSeq.text = runeword.runesSequence;
        tc.rwStats.text = runeword.statsDesc;
        tc.rwType.text = runeword.subType;

        if (AppManager.instance.currentLanguage == Languages.Ru)
        {
            for (int i = 0; i < 33; i++)
            {
                tc.rwSeq.text = tc.rwSeq.text.Replace($"{Enum.GetName(typeof(RunesEn), i)}", $"{Enum.GetName(typeof(RunesRu), i)}");
            }

            foreach (var txt in txts)
            {
                txt.font = AppManager.instance.cyrillic;
            }
            int sockets = int.Parse(tc.rwType.text[0].ToString());

            switch (sockets)
            {
                case 2:
                case 3:
                case 4:
                    if (tc.rwType.text.Contains("Socket"))
                        tc.rwType.text = tc.rwType.text.Replace("Socket", "Гнезда");
                    break;
                case 5:
                case 6:
                    if (tc.rwType.text.Contains("Socket"))
                        tc.rwType.text = tc.rwType.text.Replace("Socket", "Гнезд");
                    break;

            }

            if (tc.rwType.text.Contains("BodyArmor"))
                tc.rwType.text = tc.rwType.text.Replace("BodyArmor", "Броня");
            if (tc.rwType.text.Contains("Helms"))
                tc.rwType.text = tc.rwType.text.Replace("Helms", "Шлемы");
            if (tc.rwType.text.Contains("Shields"))
                tc.rwType.text = tc.rwType.text.Replace("Shields", "Щиты");
            txts[0].fontSize = 90;
            txts[1].fontSize = 56;
            txts[2].fontSize = 44;
            txts[3].fontSize = 44;
            txts[4].fontSize = 44;
            txts[5].fontSize = 44;
            txts[6].fontSize = 44;
            txts[7].fontSize = 36;
            txts[8].fontSize = 36;
            txts[9].fontSize = 36;
            txts[10].fontSize = 56;

            tc.rwLevel.text = $"Требуемый уровень: {runeword.reqLevel}";
            if (runeword.gameVersion != "Ressurected")
                tc.rwLadder.text = runeword.isLadder ? "Ладдерный предмет Diablo 2 LoD " : "Не Ладдерный Предмет";
            else
                tc.rwLadder.text = runeword.isLadder ? "Ладдерный предмет Ressurected" : "Не Ладдерный Предмет";
            tc.rwClass.text = runeword.classItem != Classes.Any ? $"Класс: {runeword.classItem}" : "Класс: Любой";
            if (runeword.gameVersion == "1.10" || runeword.gameVersion == "1.11")
                tc.rwVersion.text = $"Версия игры: {runeword.gameVersion}+ LoD";
            else
                tc.rwVersion.text = $"Версия игры: Для всех версий";
        }
        
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

        tc.rwItem.text = $"{runeword.recomendedItems}";
    }
}
