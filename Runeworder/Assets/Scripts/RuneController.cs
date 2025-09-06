using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RuneController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Image background;
    public Image checkmark;
    public Text runeName;
    public RunesEn rune;

    public delegate void RuneHandler(RunesEn rune, bool isOn);
    public static event RuneHandler OnRuneToggleChanged;
    public delegate void RuneLongTapHandler(RunesEn rune);
    public static event RuneLongTapHandler OnLongTapRune;

    Toggle toggle;
    private float longPressTime = 0.5f;
    private float pressStartTime;
    private bool isLongPress = false;
    private bool isPressed = false;
    private bool toggleStateBeforePress = false;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged();
        });
    }

    void Update()
    {
        if (isPressed && !isLongPress)
        {
            if (Time.time - pressStartTime >= longPressTime)
            {
                isLongPress = true;
                OnLongTap();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressStartTime = Time.time;
        isPressed = true;
        isLongPress = false;
        toggleStateBeforePress = toggle.isOn;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        if (isLongPress)
        {
            // Если был длинный тап, отменяем клик
            eventData.eligibleForClick = false;
        }
        isLongPress = false;
    }

    void ToggleValueChanged()
    {
        // Не срабатываем если это был длинный тап
        if (!isLongPress)
        {
            OnRuneToggleChanged?.Invoke(rune, toggle.isOn);
        }
    }

    void OnLongTap()
    {
        OnLongTapRune?.Invoke(rune);
    }

    public void OnTooltipRuneTap(Image runeImage)
    {
        Enum.TryParse<RunesEn>(runeImage.sprite.name.Split('_')[1], out RunesEn runeTapped);
        OnLongTapRune?.Invoke(runeTapped);
    }
}
