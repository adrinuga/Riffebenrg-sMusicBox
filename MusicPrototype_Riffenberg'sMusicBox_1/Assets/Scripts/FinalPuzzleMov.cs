using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzleMov : MonoBehaviour
{
    [SerializeField] private List<RingScript> m_ringList = new List<RingScript>();
    [SerializeField] private  int[] m_finalCombination;

    [HideInInspector] public bool m_canAct = true;

    private RingScript m_actualRing;

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
                    Debug.Log("UpRing");
                    m_actualRing.DisableRing();
                    m_actualRing = m_ringList[m_ringList.IndexOf(m_actualRing) - 1];
                    m_actualRing.EnableRing();
                }
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (m_ringList.IndexOf(m_actualRing) < m_ringList.Count - 1)
                {
                    Debug.Log("DownRing");
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
        foreach(RingScript ring in m_ringList)
        {
            if(ring.m_ringAudioIndex == 0)
            {
                break;
                
            }
            else
            {
                if (ring.m_ringAudioIndex == m_finalCombination[m_ringList.IndexOf(ring)])
                {
                }
            }
            

        }

    }
}
