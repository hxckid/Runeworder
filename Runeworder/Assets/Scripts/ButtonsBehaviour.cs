using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsBehaviour : MonoBehaviour
{
    public List<Button> buttons;

    public Color32 normal;
    public Color32 highlighted;

    public void HighlightAndRenameButton(int number)
    {
        foreach (var btn in buttons)
        {
            btn.gameObject.GetComponent<Image>().color = normal;
        }

        buttons[number].gameObject.GetComponent<Image>().color = highlighted;

        if (number >= 5 && number <= 10)
        {
            buttons[11].gameObject.GetComponent<Image>().color = highlighted;
            Text buttonText = buttons[number].GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttons[11].GetComponentInChildren<Text>().text = buttonText.text;
            }
        }
    }
}

