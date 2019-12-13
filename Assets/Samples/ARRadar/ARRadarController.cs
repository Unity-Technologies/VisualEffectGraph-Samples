using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public static class ARRShaderPropertyToID
{
    public static int galaxyMapInitialize = Shader.PropertyToID("Initialize");
}

public class ARRadarController : MonoBehaviour {

    public AnimationCurve curve;
    private float m_value = 0f;
    public float showSpeed = 0.7f;
    public float hideSpeed = 1.2f;
    public bool toggle;

    private Light m_light;
    private float m_lightIntensity = 0f;
    private VisualEffect m_vf;

    private void Awake()
    {
        m_light = GetComponentInChildren<Light>();

        if (m_light != null)
            m_lightIntensity = m_light.intensity;

        m_vf = GetComponent<VisualEffect>();
    }

    private void OnEnable()
    {
        toggle = true;
    }

    IEnumerator Start ()
    {
       // m_vf = GetComponent<VisualEffect>();

        while (true)
        {
            if (m_vf == null)
            {
                Debug.LogWarning("No Visual Effect component found; the ARRadarController script needs to be attached to the ARRadar effect.");
                break;
            }

            if (toggle)
            {
                if (m_value < 1f)                    
                    m_value += Time.deltaTime * showSpeed * curve.Evaluate(m_value);

                if (m_value > 1f)
                    m_value = 1f;
            }
            else
            {
                if (m_value > 0f)
                    m_value -= Time.deltaTime * hideSpeed * curve.Evaluate(m_value);

                if (m_value < 0f)
                    m_value = 0f;
            }

            if (m_light != null)
                m_light.intensity = Mathf.Lerp(0f, m_lightIntensity, m_value);

            m_vf.SetFloat(ARRShaderPropertyToID.galaxyMapInitialize, m_value);

            if (Input.GetKeyDown(KeyCode.Space))
                toggle = !toggle;

            yield return null;
        }
	}
	
}
