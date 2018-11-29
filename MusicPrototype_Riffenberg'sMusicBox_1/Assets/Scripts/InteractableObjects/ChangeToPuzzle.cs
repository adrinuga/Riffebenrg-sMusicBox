using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToPuzzle : InteractableObject {

    [SerializeField] private int m_sceneToChange;
    [SerializeField] private Outline m_objectOutline;
    [SerializeField] Animation m_puzzleAnim;
    [SerializeField] AnimationClip m_changeSceneAnim;

    AsyncOperation m_async;

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
        if (GameManager.m_instance.m_playerNav.m_BoxOn)
        {
            Debug.Log("changeScene");


            StartCoroutine(Load());

        }
    }
    public override void MouseOver()
    {
        if (GameManager.m_instance.m_playerNav.m_BoxOn)
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
        Debug.LogWarning("ASYNC LOAD STARTED - " +
           "DO NOT EXIT PLAY MODE UNTIL SCENE LOADS... UNITY WILL CRASH");
        m_async = SceneManager.LoadSceneAsync(m_sceneToChange);
        m_async.allowSceneActivation = false;

        //m_puzzleAnim.clip = m_changeSceneAnim;
        ////m_puzzleAnim.Play();
        ////while (m_puzzleAnim.isPlaying)
        ////{
        yield return null;

        //}
        ActivateScene();

        

    }
    public void ActivateScene()
    {
        GameManager.m_instance.SaveInfo();
        Debug.Log("goChange");
        m_async.allowSceneActivation = true;
    }
}
