using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSamplesCMCameraControl : MonoBehaviour
{
    [Header("Input")]
    public string ZoomAxisName = "LeftTrigger";
    public KeyCode ZoomKey = KeyCode.LeftShift;

    [Header("Configuration")]
    public Cinemachine.CinemachineFreeLook m_Camera;
    public UnityEngine.Rendering.Volume m_PostProcessVolume; 
    public Vector2 ZoomInOutFov = new Vector2(40,75);
    public float dampen = 14f;
    public int MouseRotateButton = 1;

    float m_CachedValue = 0.0f;

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
    }

    float GetInputAxis(string axisName)
    {
        if (Input.GetMouseButton(MouseRotateButton) && !menuVisible)
        {
            return Input.GetAxis(axisName);
        }
        else
            return 0;
    }


    // Update is called once per frame
    void Update()
    {
        if(m_PostProcessVolume != null && m_Camera != null)
        {
            float value = Mathf.Clamp01(Input.GetAxisRaw(ZoomAxisName) + (Input.GetKey(ZoomKey)? 1 : 0));
            m_CachedValue = Mathf.Lerp(m_CachedValue, value, dampen * Time.deltaTime);

            if (m_CachedValue < 0.001f) // Handle Dead zone
                m_CachedValue = 0;

            m_PostProcessVolume.weight = value;
            m_Camera.m_Lens.FieldOfView = Mathf.Lerp(ZoomInOutFov.y, ZoomInOutFov.x, m_CachedValue);
        }

        if(Input.GetMouseButton(MouseRotateButton))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
