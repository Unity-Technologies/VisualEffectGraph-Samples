using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class MagicLampHandler : MonoBehaviour
{
    public PlayableDirector TimelineDirector;

    private bool isActivated = false;

    float m_Interp = 0.0f;
    bool m_Reverse = true;

    void OnMouseDown()
    {
        m_Reverse = !m_Reverse;
    }

    void Update()
    {
        m_Interp += Time.deltaTime * (m_Reverse ? -1.0f : 1.0f);
        m_Interp = Mathf.Clamp(m_Interp, 0.0f, (float)TimelineDirector.duration);

        TimelineDirector.time = m_Interp;
        TimelineDirector.Evaluate();
    }

}
