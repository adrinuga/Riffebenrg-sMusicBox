using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : MonoBehaviour
{
    public Outline m_ringOutline;
    public int m_ringAudioIndex;
    public List<AudioSource> m_ringAudioSources = new List<AudioSource>();
    public AudioSource 
        m_actualSource,
        m_ringEffectSource
        ;
    public AudioClip
        m_rotateClip,
        m_StopRotatingClip
        ;

    [SerializeField] private float m_rotSpeed = 60.0f;

    public Animation m_ringBarAnim;
    public AnimationClip 
        m_barAnimationOpen,
        m_barAnimationClose;

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
        if (m_actualSource != null)
        {
            m_actualSource.mute = true;
           
        }

        float l_angles = 360 / (m_ringAudioSources.Count + 1);

        if (_sign > 0)
        {
            if(m_ringAudioIndex >= m_ringAudioSources.Count)
            {
                
                m_ringAudioIndex = 0;
               
            }
            else
            {
                m_ringAudioIndex ++;
                
            }

        }
        else if (_sign < 0)
        {
            if (m_ringAudioIndex <= 0)
            {
                m_ringAudioIndex = m_ringAudioSources.Count;

            }
            else
            {
                m_ringAudioIndex--;
                
            }

        }

        if (m_ringAudioIndex != 0)
        {
            m_actualSource = m_ringAudioSources[m_ringAudioIndex - 1];
            
        }
        else
        {
            m_actualSource = null;
        }

        StartCoroutine(DoRotation(l_angles*_sign));

    }
    IEnumerator DoRotation(float _angles)
    {
        m_ringEffectSource.clip = m_rotateClip;

        GameManager.m_instance.m_finalPuzzle.m_canAct = false;
        float l_rotCounting = 0f;

        Quaternion targetRotation = GetTargetQuaternion(transform.rotation, Quaternion.Euler(0, -_angles, 0));

        while (l_rotCounting < Mathf.Abs(_angles))
        {
            if (!m_ringEffectSource.isPlaying)
            {
                m_ringEffectSource.Play();

            }

            l_rotCounting += m_rotSpeed * Time.deltaTime;
            transform.RotateAround(transform.position, -Vector3.forward, Mathf.Sign(_angles) * m_rotSpeed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = targetRotation;

        GameManager.m_instance.m_finalPuzzle.m_canAct = true;

        m_ringEffectSource.clip = m_StopRotatingClip;
        m_ringEffectSource.Play();

        if (m_actualSource != null)
        {
            m_actualSource.mute = false;
        }
    }

    private Quaternion GetTargetQuaternion(Quaternion initial, Quaternion rotation)
    {
        return initial * rotation;
    }
}
