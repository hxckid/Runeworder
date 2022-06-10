using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsBehaviour : MonoBehaviour
{
    public List<Button> buttons;

    public Color32 normal;
    public Color32 highlighted;

    public void MarkButtonAsActive(int number)
    {
        foreach (var btn in buttons)
        {
            btn.gameObject.GetComponent<Image>().color = normal;
        }

        buttons[number].gameObject.GetComponent<Image>().color = highlighted;

        if (number == 5 || number == 6)
        {
            buttons[5].gameObject.GetComponent<Image>().color = highlighted;
            buttons[6].gameObject.GetComponent<Image>().color = highlighted;
        }
    }
}
