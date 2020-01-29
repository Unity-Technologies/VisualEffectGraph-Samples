using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class HighlightEntry : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public Image MenuEntryBackgroundImage;
    public Color Normal = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public Color Selected = new Color(2.0f, 2.0f, 2.0f, 1.0f);

    public void OnEnable()
    {
        MenuEntryBackgroundImage.color = Normal;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        MenuEntryBackgroundImage.color = Normal;
    }

    public void OnSelect(BaseEventData eventData)
    {
        MenuEntryBackgroundImage.color = Selected;
    }
}
