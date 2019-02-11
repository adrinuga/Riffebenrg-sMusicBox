using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SafeOut : InteractableObject {

    [SerializeField] Transform m_puzzlesInside;
    [SerializeField] UnityEvent m_OpenBoxEvent;
    [SerializeField] private Outline m_objectOutline;
    [SerializeField] private Animation m_objectAnimation;
    [SerializeField]
    private AnimationClip
        m_bringCloseAnimation,
        m_putDownAnimation,
        m_openBoxAnimation
        ;



 

    // Use this for initialization
    void Start ()
    {
        GameManager.m_instance.m_interactableObjects.Add(this);

        if(GameManager.m_instance.m_beforeSceneInfo.m_lastIndexScene != 0)
        {
            transform.root.position = GameManager.m_instance.m_beforeSceneInfo.m_boxPos;
            transform.root.rotation = GameManager.m_instance.m_beforeSceneInfo.m_boxRot;
            m_puzzlesInside.gameObject.SetActive(true);
            transform.gameObject.SetActive(false);

            GameManager.m_instance.m_SafePrevious = transform.root;

            this.gameObject.SetActive(false);
            m_objectOutline.enabled = false;
        }

	}
	
	// Update is called once per frame
	void Update ()
    {
        m_objectOutline.enabled = false;
    }
    public override void OnClick()
    {
        if (!GameManager.m_instance.m_playerNav.m_BoxOn)
        {
            GameManager.m_instance.m_playerNav.BringObjectClose(transform.root, m_bringCloseAnimation.length);
            m_puzzlesInside.gameObject.SetActive(true);
            transform.gameObject.SetActive(false);
            m_objectOutline.enabled = false;
           
            m_objectAnimation.CrossFade(m_bringCloseAnimation.name);
            
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
    public void SetBoxOut()
    {
        GameManager.m_instance.m_playerNav.LeaveObjectDown(m_putDownAnimation.length);
        m_puzzlesInside.gameObject.SetActive(false);
        transform.gameObject.SetActive(true);
       
        m_objectAnimation.CrossFade(m_putDownAnimation.name);
    }
    public void OpenBox()
    {
        m_objectAnimation.clip = m_openBoxAnimation;
        m_objectAnimation.Play();
    }
}
