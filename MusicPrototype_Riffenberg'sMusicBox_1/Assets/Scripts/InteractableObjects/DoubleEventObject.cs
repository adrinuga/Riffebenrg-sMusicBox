using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoubleEventObject : InteractableObject {

    [SerializeField] private UnityEvent 
        m_eventOne,
        m_eventTwo
        ;
    private bool m_eventOneTriggered = false;

    [SerializeField] private AudioSource m_effectSource;

    [SerializeField] private Outline m_objectOutline;
    
    






    // Use this for initialization
    void Start()
    {
        GameManager.m_instance.m_interactableObjects.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        m_objectOutline.enabled = false;
    }
    public override void OnClick()
    {
        if (!GameManager.m_instance.m_playerNav.m_BoxOn)
        {
            if (!m_eventOneTriggered)
            {
                m_eventOne.Invoke();
                m_eventOneTriggered = true;
            }
            else
            {
                m_eventTwo.Invoke();
                m_eventOneTriggered = false;
            }

            if (m_effectSource != null)
                m_effectSource.Play();

          
            
            m_objectOutline.enabled = false;
            
           

        }
    }
    public override void MouseOver()
    {
        if (!GameManager.m_instance.m_playerNav.m_BoxOn)
        {
            m_objectOutline.enabled = true;
        }

    }
    public override Transform ReturnObject()
    {
        return this.transform;
    }
   
}
