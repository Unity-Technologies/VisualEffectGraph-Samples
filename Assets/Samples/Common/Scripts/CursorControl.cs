using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    public bool DefaultCursorVisible = false;

    private void OnEnable()
    {
        if (SampleLoader.instance != null)
        {
            SampleLoader.instance.onMenuToggle += OnSamplesMenuToggle;
        }
    }

    private void OnDisable()
    {
        if (SampleLoader.instance != null)
        {
            SampleLoader.instance.onMenuToggle -= OnSamplesMenuToggle;
        }
    }

    bool menuVisible = false;
    void OnSamplesMenuToggle(bool visible)
    {
        menuVisible = visible;
        ShowCursor(menuVisible ? true : DefaultCursorVisible);
    }

    private void Start()
    {
        ShowCursor(DefaultCursorVisible);
    }

    void ShowCursor(bool visible)
    {
        if (visible)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    float GetInputAxis(string axisName)
    {
        if (!menuVisible)
        {
            return Input.GetAxis(axisName);
        }
        else
            return 0;
    }
}
