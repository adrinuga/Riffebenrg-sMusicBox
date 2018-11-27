using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : MonoBehaviour
{
    public Outline m_ringOutline;
    public int m_ringAudioIndex;
    public List<AudioSource> m_ringAudioSources = new List<AudioSource>();
    

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void EnableRing() 
    {
        m_ringOutline.enabled = true;
    }
    public void DisableRing()
    {
        m_ringOutline.enabled = false;
    }
    public void RotateRing(int _sign)
    {
        float l_angles = 360 / m_ringAudioSources.Count;

        if (_sign > 0)
        {
            if(m_ringAudioIndex >= m_ringAudioSources.Count-1)
            {
                m_ringAudioIndex = 0;
            }
            else
            {
                m_ringAudioIndex++;
            }


        }
        else if (_sign < 0)
        {
            if (m_ringAudioIndex <= 0)
            {
                m_ringAudioIndex = m_ringAudioSources.Count-1 ;
            }
            else
            {
                m_ringAudioIndex++;
            }

        }
        StartCoroutine(DoRotation(l_angles,_sign));


    }
    IEnumerator DoRotation(float _angles, int _direction)
    {
        yield return null;
    }
}
