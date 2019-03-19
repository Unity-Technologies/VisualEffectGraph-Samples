using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipHoloTableZoomControl : MonoBehaviour
{
    [Header("Input")]
    public string ZoomAxisName = "LeftTrigger";
    public KeyCode ZoomKey = KeyCode.LeftShift;

    [Header("Configuration")]
    public Cinemachine.CinemachineFreeLook m_Camera;
    public UnityEngine.Rendering.Volume m_PostProcessVolume; 
    public Vector2 ZoomInOutFov = new Vector2(40,75);
    public float dampen = 14f;

    float m_CachedValue = 0.0f;

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
    }
}
