using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickURL : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    public string URL = "https://unity.com";
    public Texture2D linkCursor;
    public Vector2 hotSpotPosition = Vector2.zero;

    public void OnPointerDown(PointerEventData eventData)
    {
        Application.OpenURL(URL);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(linkCursor, hotSpotPosition, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
    }
}
