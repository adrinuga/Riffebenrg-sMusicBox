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

    private bool l_UpsideDown = false;

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
        Debug.Log("enable");
    }
    public void DisableRing()
    {
        Debug.Log("disable");
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
        GameManager.m_instance.m_finalPuzzle.m_canAct = false;
        float l_originalRot = transform.eulerAngles.x;
        float l_rotCounting = 0f;

        l_originalRot = Mathf.FloorToInt(l_originalRot);

        //Debug.Log(l_originalRot);




        //if (Mathf.Sign( l_originalRot)<0)
        //{
        //    _angles *= -1;
        //}

        //if (l_originalRot <= -180.0f)
        //    l_originalRot += 360.0f;



        Debug.Log(transform.eulerAngles);

        //Quaternion l_finalRot = Quaternion.Euler(l_finalRotAngles, transform.rotation.y, transform.rotation.z);

        while (l_rotCounting < Mathf.Abs(_angles))
        {
            l_rotCounting += m_rotSpeed * Time.deltaTime;
            transform.RotateAround(transform.position, -Vector3.forward, Mathf.Sign(_angles) * m_rotSpeed * Time.deltaTime);
            yield return null;
        }
        //transform.rotation = Quaternion.Slerp(transform.rotation, l_finalRot, Time.deltaTime * m_rotSpeed);
        //if (transform.rotation == l_finalRot) { yield return null; }

        //transform.Rotate(new Vector3 (l_finalRotAngles, 0,0));

        

        float l_anglesAfterRot = transform.localEulerAngles.x;




        //if (l_anglesAfterRot > 180)
        //{
        //    // l_anglesAfterRot -= 360;
        //    l_finalRotAngles  = 0;

        //}


        //Debug.Log(l_anglesAfterRot);

        //if (Mathf.Floor(l_originalRot)==0)
        //{
        //    l_originalRot = (transform.localEulerAngles.y + transform.localEulerAngles.z) - 360;

        //    if (l_originalRot < 0) { l_originalRot = 0; }
        //}
        Debug.Log("Rot before sum " + l_originalRot);

        float l_finalRotAngles = l_originalRot + _angles;

        Debug.Log("Final rot after sum " + l_finalRotAngles);

        if (_angles > 0)
        {

            if (l_finalRotAngles >= 180)
            {
                l_UpsideDown = true;
            }
            if (l_finalRotAngles >= 360)
            {
                l_UpsideDown = false;
            }

            if (l_UpsideDown)
            {
                l_finalRotAngles -= 180;
            }
        }
        else if (_angles < 0)
        {
            if (l_finalRotAngles <= -180)
            {
                l_UpsideDown = true;
            }
            if (l_finalRotAngles <= -360)
            {
                l_UpsideDown = false;
            }

            if (l_UpsideDown)
            {
                l_finalRotAngles -= 180;
            }
        }
        Debug.Log("Final rot after sign change " + l_finalRotAngles);


        //transform.localEulerAngles = new Vector3(l_finalRotAngles, 0, 90);

        // transform.rotation = Quaternion.AngleAxis(l_finalRotAngles, -Vector3.forward);


        transform.eulerAngles = new Vector3(l_finalRotAngles, transform.eulerAngles.y, transform.eulerAngles.z);
        //transform.RotateAround(Vector3.zero, Vector3.right, l_finalRotAngles);

        //transform.localRotation = Quaternion.Euler(l_finalRotAngles, transform.eulerAngles.y, 0);

        GameManager.m_instance.m_finalPuzzle.m_canAct = true;

        //Quaternion qTo = Quaternion.AngleAxis(l_originalRot + _angles, Vector3.right);
        //transform.rotation = qTo;

        if (m_actualSource != null)
        {
            m_actualSource.mute = false;
        }

       
    }
}
