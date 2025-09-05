using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
                OnLongPress();
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

    void OnLongPress()
    {
        OnLongTapRune?.Invoke(rune);

        //// Метод для длинного тапа - показываем информацию о руне
        //if (runeInfoPanel != null)
        //{
        //    // Находим главный Canvas
        //    Canvas mainCanvas = FindFirstObjectByType<Canvas>();
        //    if (mainCanvas != null)
        //    {
        //        GameObject runeInfoPanelInstance = Instantiate(runeInfoPanel, mainCanvas.transform);
        //        var runeInfoPanelComponent = runeInfoPanelInstance.GetComponent<RuneInfoPanel>();
        //        if (runeInfoPanelComponent != null)
        //        {
        //            runeInfoPanelComponent.ShowRuneInfo(rune);
        //        }
        //        else
        //        {
        //            Debug.LogWarning("Компонент RuneInfoPanel не найден на объекте!");
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogWarning("Canvas не найден в сцене!");
        //    }
        //}
        //else
        //{
        //    Debug.LogWarning("RuneInfoPanelPrefab не назначен в RuneController!");
        //}
    }
}
