using UnityEngine;
using Random = UnityEngine.Random;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] public float m_PositionJitterScale;
    
    [SerializeField] public float m_RotationJitterScale;

    [SerializeField] public float m_IntensityJitterScale;

    [SerializeField] public float m_Timescale;
    
    private Vector3 m_InitialPosition;
    private float m_InitialIntensity;
    private Quaternion m_InitialRotation;

    private float m_XSeed;
    private float m_YSeed;
    private float m_ZSeed;

    private Light m_Light;
    
    void Start()
    {
        Random.InitState(gameObject.GetInstanceID());
        m_XSeed = Random.value*248;
        m_YSeed = Random.value*248;
        m_ZSeed = Random.value*248;

        m_Light = GetComponent<Light>();
        m_InitialIntensity = m_Light.intensity;
        m_InitialPosition = transform.position;
        m_InitialRotation = transform.rotation;
    }

    void Update()
    {
        float x = Time.time * m_Timescale + m_XSeed;
        float y = Time.time * m_Timescale + m_YSeed;
        float z = Time.time * m_Timescale + m_ZSeed;

        Vector3 Noise = PerlinNoise3D(new Vector3(x, y, z), 2, 1);
        Noise = Noise * 2 - Vector3.one;

        transform.SetPositionAndRotation(m_InitialPosition + Noise * m_PositionJitterScale, m_InitialRotation * Quaternion.Euler(Noise * m_RotationJitterScale)); //Nice!

        m_Light.intensity = m_InitialIntensity + Noise.x * m_IntensityJitterScale;
    }

    private Vector3 PerlinNoise3D(Vector3 uv, int Octaves, float freq)
    {
        Vector3 output = Vector3.zero;
        for (int i = 0; i < Octaves; i++)
        {
            output.x += Mathf.PerlinNoise1D(uv.x * freq * (i + 1));
            output.y += Mathf.PerlinNoise1D(uv.y * freq * (i + 1));
            output.z += Mathf.PerlinNoise1D(uv.z * freq * (i + 1));
            
        }

        return output;
    }
}
