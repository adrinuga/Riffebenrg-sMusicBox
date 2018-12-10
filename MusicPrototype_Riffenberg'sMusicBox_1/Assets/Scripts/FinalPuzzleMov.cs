using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinalPuzzleMov : MonoBehaviour
{
    [SerializeField] private List<RingScript> m_ringList = new List<RingScript>();
    [SerializeField] private  int[] m_finalCombination;

    [HideInInspector] public bool m_canAct = true;

    private RingScript m_actualRing;
    [SerializeField] private float m_timeBetweenBarAnims;
    [SerializeField] private UnityEvent m_waitBarsEvent, m_returnBarsEvent, m_succesEvent;

    void Start()
    {
        GameManager.m_instance.m_finalPuzzle = this;
        this.enabled = false;
    }

	// Use this for initialization

    void OnEnable()
    {
        m_actualRing = m_ringList[0];
        m_actualRing.EnableRing();
    }
    void OnDisable()
    {
        m_actualRing.DisableRing();
    }

	
	// Update is called once per frame
	void Update ()
    {
        if (m_canAct)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (m_ringList.IndexOf(m_actualRing) > 0)
                {
                   
                    m_actualRing.DisableRing();
                    m_actualRing = m_ringList[m_ringList.IndexOf(m_actualRing) - 1];
                    m_actualRing.EnableRing();
                }
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (m_ringList.IndexOf(m_actualRing) < m_ringList.Count - 1)
                {
                    
                    m_actualRing.DisableRing();
                    m_actualRing = m_ringList[m_ringList.IndexOf(m_actualRing) + 1];
                    m_actualRing.EnableRing();
                }
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                m_actualRing.RotateRing(-1);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                m_actualRing.RotateRing(1);
            }
        }
    }
    public void CheckFinalCombination()
    {
        int l_ringsRightCount = 0;
        List<RingScript> l_correctRings = new List<RingScript>();
        foreach(RingScript ring in m_ringList)
        {
            if(ring.m_ringAudioIndex == 0)
            {
                l_ringsRightCount = 0;

                break;
                
            }
            else
            {
                if (ring.m_ringAudioIndex == m_finalCombination[m_ringList.IndexOf(ring)])
                {
                    ring.m_ringBarAnim.clip = ring.m_barAnimationOpen;
                    ring.m_ringBarAnim.Play();
                    l_ringsRightCount++;
                    l_correctRings.Add(ring);
                }
            }
            

        }
        bool l_open = false;
        if (l_ringsRightCount >= m_ringList.Count)
        {
            l_open = true;
        }
        if (l_ringsRightCount > 0)
        {
            StartCoroutine(AnimAfterBars(l_open, l_correctRings));
        }
        


    }
    IEnumerator AnimAfterBars(bool _open, List <RingScript> _animatedRings)
    {
        m_waitBarsEvent.Invoke();

        bool l_allRingsStopped = false;

        while (!l_allRingsStopped)
        {
            foreach (RingScript ring in _animatedRings)
            {
                if (ring.m_ringBarAnim.isPlaying)
                {
                    break;
                }
                if(_animatedRings[_animatedRings.Count-1] == ring)
                {
                    l_allRingsStopped = true;
                }

            }
            yield return null;
        }
        //if each animation is done

       

        

        if (_open)
        {
            //play box animation success and open

            yield return new WaitForSeconds(m_timeBetweenBarAnims);

            m_succesEvent.Invoke();
        }
        else
        {
            if(_animatedRings.Count > 0)
            {
                foreach(RingScript ring in _animatedRings)
                {
                    ring.m_ringBarAnim.clip = ring.m_barAnimationClose;
                    ring.m_ringBarAnim.Play();

                    m_returnBarsEvent.Invoke();
                }
            }
            //play bars back to original 
        }


    }
    public void MuteAllRingsSources()
    {
        foreach(RingScript r in m_ringList)
        {
            foreach(AudioSource a in r.m_ringAudioSources)
            {
                a.mute = true;
            }
        }
    }
    public void UnmuteSelectedSources()
    {
        foreach(RingScript r in m_ringList)
        {
            if(r.m_actualSource != null)
            {
                r.m_actualSource.mute = false;
            }
        }
    }
}
