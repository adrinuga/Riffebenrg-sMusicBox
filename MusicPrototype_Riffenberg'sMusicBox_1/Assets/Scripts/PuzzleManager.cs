using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;




public class PuzzleManager : MonoBehaviour {

    public static PuzzleManager m_instance = null;

    public UnityEvent m_startLvlEvent;


    [SerializeField] GameManager.PuzzleType m_PuzzleType;

    [SerializeField] private BallMovement m_Ball;
    [SerializeField] private GridScript m_Grid;

    //MovingWallsPuzzle
    [SerializeField] private Transform m_LastPosition;
    [SerializeField] private AudioMixer m_AudioMixer;

    [SerializeField] private Slider[] m_Sliders;
    [SerializeField] private int m_NumberOfWallPositions;

    private Node m_LastNode;


    //RythmPuzzle
    [SerializeField] private BallRhythmMovement m_BallRythm;

    //SimonSaysPuzzle
    [SerializeField] private Transform[] SimonSaysTransforms;
    [SerializeField] private AudioSource[] SimonSaysAudioSources;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_FinalAudio;
    private bool[] m_CurrentSimonSaysVisited;


    //Change Scene
    [SerializeField] private int m_sceneToChange;
    private AsyncOperation m_async;


    void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        switch (m_PuzzleType)
        {
            case GameManager.PuzzleType.puzzleR:
                m_LastNode = m_Grid.GetNodeContainingPosition(m_LastPosition.position);

                break;

            case GameManager.PuzzleType.puzzleH:

                //Save the final node
                m_LastNode = m_Grid.GetNodeContainingPosition(m_LastPosition.position);


                    break;

            case GameManager.PuzzleType.puzzleM:
                m_CurrentSimonSaysVisited = new bool[SimonSaysTransforms.Length];
                for (int i = 0; i < SimonSaysTransforms.Length; i++)
                {
                    m_CurrentSimonSaysVisited[i] = false;
                }
                m_AudioSource.Play();
                break;
        }

        StartCoroutine(LoadScene());
    }

	
	// Update is called once per frame
	void Update () {
		switch(m_PuzzleType)
        {
            case GameManager.PuzzleType.puzzleR:
                if (m_BallRythm.m_CurrentNode == m_LastNode)
                {
                    //Puzzle finished
                    ActivateScene();
                }
                break;

            case GameManager.PuzzleType.puzzleH:

                //Update the music effects
                for (int i = 0; i < m_Sliders.Length; i++)
                {
                    m_AudioMixer.SetFloat("EQ" + i, m_Sliders[i].value);

                }



                if (m_Ball.m_CurrentNode == m_LastNode)
                {
                    //Puzzle finished
                    ActivateScene();
                }
                break;

            case GameManager.PuzzleType.puzzleM:

                if(m_Ball.m_CurrentNode.isSimonSays)
                {
                    for (int i = 0; i < SimonSaysTransforms.Length; i++)
                    {
                        if (m_Grid.GetNodeContainingPosition(SimonSaysTransforms[i].position) == m_Ball.m_CurrentNode)
                        {
                            if(i > 0)
                            {
                                if (m_CurrentSimonSaysVisited[i - 1])
                                {
                                    if (!SimonSaysAudioSources[i].isPlaying && !m_CurrentSimonSaysVisited[i])
                                    {
                                        SimonSaysAudioSources[i].Play();
                                        if(m_AudioSource.isPlaying)
                                        {
                                            m_AudioSource.Stop();
                                        }
                                    }

                                    m_CurrentSimonSaysVisited[i] = true;

                                }
                                else
                                {
                                    ResetPlayerPosition();
                                }
                            }
                            else if(i == 0)
                            {
                                if (!SimonSaysAudioSources[i].isPlaying && !m_CurrentSimonSaysVisited[i])
                                {
                                    SimonSaysAudioSources[i].Play();
                                    if (m_AudioSource.isPlaying)
                                    {
                                        m_AudioSource.Stop();
                                    }
                                }

                                m_CurrentSimonSaysVisited[i] = true;
                            }
                        }
                    }


                    
                }

                int l_SimonSaysVisitedNodes = 0;
                for (int i = 0; i < SimonSaysTransforms.Length; i++)
                {
                    if (m_Grid.GetNodeContainingPosition(SimonSaysTransforms[i].position).hasBeenVisited)
                    {
                        l_SimonSaysVisitedNodes++;
                    }

                }
                if (l_SimonSaysVisitedNodes == SimonSaysTransforms.Length - 1 && m_Ball.transform.position == m_Grid.GetNodeContainingPosition(SimonSaysTransforms[SimonSaysTransforms.Length - 1].position).worldPosition)
                {
                    //Puzzle finished
                    ActivateScene();
                }
                break;
        }


	}

    public void ResetPlayerPosition()
    {
        if(m_PuzzleType == GameManager.PuzzleType.puzzleM)
        {
            for (int j = 0; j < SimonSaysTransforms.Length; j++)
            {
                m_CurrentSimonSaysVisited[j] = false;
                m_Ball.ResetPosition();
            }
        }
    }
    public void StartLevel()
    {
        print("EMPIEZA EL LEVEL");
    }

    IEnumerator LoadScene()
    {
        m_async = SceneManager.LoadSceneAsync(m_sceneToChange);
        m_async.allowSceneActivation = false;
        yield return null;
    }

    public void ActivateScene()
    {
        if (m_async.progress >= 0.9f)
        {
            GameManager.m_instance.AddCompletedPuzzle(m_PuzzleType);
            GameManager.m_instance.SaveInfo(transform.root.position, transform.root.rotation, m_sceneToChange);
            Debug.Log("Changed to scene: " + m_sceneToChange);
            m_async.allowSceneActivation = true;
        }
    }
}
