﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToPuzzle : MonoBehaviour, InteractableObject {

    [SerializeField] private int m_sceneToChange;

    // Use this for initialization
    void Start()
    {
        GameManager.m_instance.m_interactableObjects.Add(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        if (GameManager.m_instance.m_playerNav.m_BoxOn)
        {
        }
    }
    public void MouseOver()
    {
        if (GameManager.m_instance.m_playerNav.m_BoxOn)
        {
        }
    }
    public Transform ReturnObject()
    {
        return this.transform;
    }
}