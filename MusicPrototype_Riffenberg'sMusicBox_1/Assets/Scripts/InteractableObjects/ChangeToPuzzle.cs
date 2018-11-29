using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToPuzzle : InteractableObject {

    [SerializeField] private int m_sceneToChange;
    [SerializeField] private Outline m_objectOutline;
    [SerializeField] Animation m_puzzleAnim;
    [SerializeField] AnimationClip m_changeSceneAnim;

    public GameManager.PuzzleType m_sceneChangeType;

    private bool m_completed;

    AsyncOperation m_async;

    // Use this for initialization
    void Start()
    {
        GameManager.m_instance.m_interactableObjects.Add(this);

        if (GameManager.m_instance.m_beforeSceneInfo.m_lastIndexScene != 0)
        {



            switch(m_sceneChangeType)
            {
            case (GameManager.PuzzleType.puzzleM):

                    if (GameManager.m_instance.m_puzzleCompletedM)
                    {
                        m_completed = true;
                    }

               

                break;
            case (GameManager.PuzzleType.puzzleH):

                    if (GameManager.m_instance.m_puzzleCompletedH)
                    {
                        m_completed = true;
                    }
                   

                break;
            case (GameManager.PuzzleType.puzzleR):

                    if (GameManager.m_instance.m_puzzleCompletedR)
                    {
                        m_completed = true;
                    }
                break;
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        m_objectOutline.enabled = false;
    }
    public override void OnClick()
    {
        if (GameManager.m_instance.m_playerNav.m_BoxOn && !m_completed)
        {
            Debug.Log("changeScene");


            StartCoroutine(Load());

        }
    }
    public override void MouseOver()
    {
        if (GameManager.m_instance.m_playerNav.m_BoxOn && !m_completed)
        {

            m_objectOutline.enabled = true;
            Debug.Log("outline");

        }
    }
    public override Transform ReturnObject()
    {
        return this.transform;
    }
    IEnumerator Load()
    {
        ////Debug.LogWarning("ASYNC LOAD STARTED - " +
        //   "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
        m_async = SceneManager.LoadSceneAsync(m_sceneToChange);
        m_async.allowSceneActivation = false;
        //m_puzzleAnim.clip = m_changeSceneAnim;
        ////m_puzzleAnim.Play();
        ////while (m_puzzleAnim.isPlaying)
        ////{
        ///



        // TO DO CHECK IF THE SCENE IS LOADED
        yield return null;

        //}
        ActivateScene();

        

    }
    public void ActivateScene()
    {
        GameManager.m_instance.SaveInfo(transform.root.position,transform.root.rotation,m_sceneToChange);
        Debug.Log("goChange");
        m_async.allowSceneActivation = true;
    }
}
