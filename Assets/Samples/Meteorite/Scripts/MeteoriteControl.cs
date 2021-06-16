using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.VFX;

public class MeteoriteControl : MonoBehaviour
{
    public float initDelay = 3;
    public float loopDelay = 10;
    public PlayableDirector Director;
    float m_Time;
    bool m_isMenuOpen;


    // Start is called before the first frame update
    void Start()
    {
        m_Time = initDelay;

    }

    private void OnEnable()
    {
        if(SampleLoader.instance != null)
            SampleLoader.instance.onMenuToggle += Instance_onMenuToggle;
    }
    private void OnDisable()
    {
        if (SampleLoader.instance != null)
            SampleLoader.instance.onMenuToggle -= Instance_onMenuToggle;
    }

    private void Instance_onMenuToggle(bool isOpen)
    {
        m_isMenuOpen = isOpen;

    }

    // Update is called once per frame
    void Update()
    {
        m_Time = m_Time - Time.deltaTime;

        if (Input.anyKey && !Input.GetMouseButton(1) && Director.time==0 && !m_isMenuOpen )
        {
            m_Time = -1;

        }
        if (m_Time<0)
        {
            m_Time = loopDelay;
            Director?.Play();
        }
    }

    

}
