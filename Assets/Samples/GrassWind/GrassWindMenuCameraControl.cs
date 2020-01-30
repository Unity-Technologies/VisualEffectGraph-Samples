using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassWindMenuCameraControl : MonoBehaviour
{
    CinemachineCore.AxisInputDelegate m_AxisDelegateBackup;

    private void OnEnable()
    {
        m_AxisDelegateBackup = CinemachineCore.GetInputAxis;

        CinemachineCore.GetInputAxis = GetInputAxis;
        if(SampleLoader.instance != null)
        {
            SampleLoader.instance.onMenuToggle += OnSamplesMenuToggle;
        }
    }

    private void OnDisable()
    {
        CinemachineCore.GetInputAxis = m_AxisDelegateBackup;

        if (SampleLoader.instance != null)
        {
            SampleLoader.instance.onMenuToggle -= OnSamplesMenuToggle;
        }
    }

    bool menuVisible = false;
    void OnSamplesMenuToggle(bool visible)
    {
        menuVisible = visible;
        ShowCursor(menuVisible);
    }

    private void Start()
    {
        ShowCursor(false);
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
