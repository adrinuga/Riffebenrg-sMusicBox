using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCompleted : InteractableObject {


    [SerializeField] private Outline m_objectOutline;
    [SerializeField] private AudioSource m_PuzzleSolvedAudioSource;
    [SerializeField] private GameManager.PuzzleType m_PuzzleType;
    [SerializeField] private Light m_Light;


    // Use this for initialization
    void Start()
    {
        switch (m_PuzzleType)
        {
            case GameManager.PuzzleType.puzzleM:
                if(GameManager.m_instance.m_puzzleCompletedM)
                {
                    GameManager.m_instance.m_interactableObjects.Add(this);
                    m_Light.enabled = true;
                }
                break;
            case GameManager.PuzzleType.puzzleH:
                if (GameManager.m_instance.m_puzzleCompletedH)
                {
                    GameManager.m_instance.m_interactableObjects.Add(this);
                    m_Light.enabled = true;
                }
                break;
            case GameManager.PuzzleType.puzzleR:
                if (GameManager.m_instance.m_puzzleCompletedR)
                {
                    GameManager.m_instance.m_interactableObjects.Add(this);
                    m_Light.enabled = true;
                }
                break;

        }
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
            if(!m_PuzzleSolvedAudioSource.isPlaying)
            {
                m_PuzzleSolvedAudioSource.Play();
            }

        }
        
        if (!m_PuzzleSolvedAudioSource.isPlaying)
        {
            m_PuzzleSolvedAudioSource.Play();
        }
    }
    public override void MouseOver()
    {
        m_objectOutline.enabled = true;

    }
    public override Transform ReturnObject()
    {
        return this.transform;
    }
}
