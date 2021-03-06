using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipController : MonoBehaviour
{
    public GameObject runeIcons;
    public Text rwName;
    public Text rwSeq;
    public Text rwStats;
    public Text rwType;
    public Text rwLevel;
    public Text rwLadder;
    public Text rwClass;
    public Text rwVersion;
    public Text rwItem;
    public Text bestItemLabel;

    public void DestroyTooltip()
    {
        AppManager.instance.gameState = GameState.Runewords;
        Destroy(gameObject);
    }

}
