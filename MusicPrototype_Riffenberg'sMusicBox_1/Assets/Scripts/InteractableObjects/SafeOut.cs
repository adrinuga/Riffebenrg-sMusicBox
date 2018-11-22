using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SafeOut : MonoBehaviour,InteractableObject {

    [SerializeField] Transform m_puzzlesInside;

 

    // Use this for initialization
    void Start ()
    {
        GameManager.m_instance.m_interactableObjects.Add(this);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void OnClick()
    {
        if (!GameManager.m_instance.m_playerNav.m_BoxOn)
        {
            GameManager.m_instance.m_playerNav.BringObjectClose(transform.root);
            m_puzzlesInside.gameObject.SetActive(true);
            transform.gameObject.SetActive(false);

        }
    }
    public void MouseOver()
    {
        if (!GameManager.m_instance.m_playerNav.m_BoxOn)
        {

        }

    }
    public Transform ReturnObject()
    {
        return this.transform;
    }
    public void SetBoxOut()
    {
        m_puzzlesInside.gameObject.SetActive(false);
        transform.gameObject.SetActive(true);
    }
}
