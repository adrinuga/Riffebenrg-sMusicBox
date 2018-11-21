using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager m_instance = null;
    public List<InteractableObject> m_interactableObjects;
    public PlayerLookingNav m_playerNav;

	// Use this for initialization
	void Awake ()
    {
		if(m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
	}
    void Start()
    {
        m_interactableObjects = new List<InteractableObject>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public InteractableObject  GetInteractableObject(Transform _object)
    {
        InteractableObject l_iObject = null;
        foreach(InteractableObject i in m_interactableObjects)
        {
            if (i.ReturnObject() == _object)
            {
                l_iObject = i;
            }
            
        }
        return l_iObject;
    }

}
