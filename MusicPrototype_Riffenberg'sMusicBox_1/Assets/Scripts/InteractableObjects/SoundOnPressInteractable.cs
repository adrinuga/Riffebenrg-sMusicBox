using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnPressInteractable :InteractableObject {

    void Start()
    {
        GameManager.m_instance.m_interactableObjects.Add(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void OnClick()
    {
        if (GameManager.m_instance.m_playerNav.m_BoxOn)
        {
        }
    }
    public override void MouseOver()
    {
        if (GameManager.m_instance.m_playerNav.m_BoxOn)
        {

        }
    }
    public override Transform ReturnObject()
    {
        return this.transform;
    }
}
