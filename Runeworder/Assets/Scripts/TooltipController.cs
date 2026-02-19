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
    public Toggle star;

    private Runeword_SO currentRuneword;

    private void Start()
    {
        if (star != null)
        {
            // Remove any existing listeners first to avoid duplicates
            star.onValueChanged.RemoveListener(OnStarToggleChanged);
            star.onValueChanged.AddListener(OnStarToggleChanged);
        }
        else
        {
            Debug.LogError("[TooltipController] Star toggle is NULL! Please assign it in the Tooltip prefab.");
        }
    }

    public void InitializeStarToggle(Runeword_SO runeword)
    {
        if (AppManager.instance == null)
        {
            Debug.LogError("[TooltipController] AppManager.instance is NULL!");
            return;
        }
        currentRuneword = runeword;
        if (star != null && currentRuneword != null)
        {
            // Temporarily remove listener to avoid triggering OnStarToggleChanged when setting initial value
            star.onValueChanged.RemoveListener(OnStarToggleChanged);
            bool isFavorite = AppManager.instance.IsRunewordFavorite(currentRuneword.name);
            star.isOn = isFavorite;
            // Re-add listener after setting initial value
            star.onValueChanged.AddListener(OnStarToggleChanged);
        }
    }

    private void OnStarToggleChanged(bool isOn)
    {
        if (AppManager.instance == null)
        {
            Debug.LogError("[TooltipController] AppManager.instance is NULL in OnStarToggleChanged!");
            return;
        }
        if (currentRuneword != null)
        {
            AppManager.instance.ToggleFavoriteRuneword(currentRuneword.name);
            
            // Notify RunewordsController to refresh the list if needed
            if (RunewordsController.instance != null)
            {
                RunewordsController.instance.RefreshCurrentList();
            }
        }
    }

    public void DestroyTooltip()
    {
        if (star != null)
        {
            star.onValueChanged.RemoveListener(OnStarToggleChanged);
        }
        AppManager.instance.gameState = GameState.Runewords;
        Destroy(gameObject);
    }

}
