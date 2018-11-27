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

    }
    public void DisableRing()
    {

    }
    public void RotateRing(int _sign)
    {
        float l_angles = 360 / m_ringAudioSources.Count;

        if (_sign > 0)
        {

        }
        else if (_sign < 0)
        {

        }
        StartCoroutine(DoRotation(l_angles));


    }
    IEnumerator DoRotation(float _angles)
    {
        yield return null;
    }
}
