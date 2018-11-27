using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : MonoBehaviour
{
    public Outline m_ringOutline;
    public int m_ringAudioIndex;
    public List<AudioSource> m_ringAudioSources = new List<AudioSource>();
    private AudioSource m_actualSource;
    [SerializeField] private float m_rotSpeed = 60.0f;

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
        if (m_actualSource != null)
        {
            m_actualSource.mute = true;
           
        }

        float l_angles = 360 / m_ringAudioSources.Count;

        if (_sign > 0)
        {
            if(m_ringAudioIndex >= m_ringAudioSources.Count )
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
                m_ringAudioIndex = m_ringAudioSources.Count ;

            }
            else
            {
                m_ringAudioIndex--;
                
            }

            


        }
        //Debug.Log(m_ringAudioIndex + " " + _sign);

        if (m_ringAudioIndex != 0)
        {
            m_actualSource = m_ringAudioSources[m_ringAudioIndex - 1];
            
        }
        else
        {
            m_actualSource = null;
        }


        //Debug.Log(m_ringAudioIndex + " " + _sign);
        StartCoroutine(DoRotation(l_angles*_sign));


    }
    IEnumerator DoRotation(float _angles)
    {

        float l_originalRot = transform.rotation.eulerAngles.x;
        float l_rotCounting = 0f;

        Quaternion l_finalRot = Quaternion.Euler(0, l_originalRot + _angles, 0);
        Debug.Log(l_originalRot + _angles);
        while (l_rotCounting < Mathf.Abs(_angles))
        {
            l_rotCounting += m_rotSpeed * Time.deltaTime;
            transform.RotateAround(transform.position, - Vector3.forward, Mathf.Sign(_angles) * m_rotSpeed * Time.deltaTime);
            yield return null;
        }

        transform.eulerAngles = new Vector3(l_originalRot + _angles, transform.eulerAngles.y, transform.eulerAngles.z);

        //Quaternion qTo = Quaternion.AngleAxis(l_originalRot + _angles, Vector3.right);
        //transform.rotation = qTo;

        if (m_actualSource != null)
        {
            m_actualSource.mute = false;
        }

       
    }
}
