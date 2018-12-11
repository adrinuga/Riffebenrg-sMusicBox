using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;




public class PuzzleManager : MonoBehaviour {

    public static PuzzleManager m_instance = null;

    [SerializeField] GameManager.PuzzleType m_PuzzleType;

    [SerializeField] private BallMovement m_Ball;
    [SerializeField] private GridScript m_Grid;

    //MovingWallsPuzzle
    [SerializeField] private Transform m_LastPosition;
    [SerializeField] private AudioMixer m_AudioMixer;

    [SerializeField] private Slider[] m_Sliders;

    private Node m_LastNode;

    public bool m_Completed = false;


    //RythmPuzzle
    [SerializeField] private BallRhythmMovement m_BallRythm;

    //SimonSaysPuzzle
    [SerializeField] private Transform[] SimonSaysTransforms;
    [SerializeField] private AudioSource[] SimonSaysAudioSources;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioSource m_FinalAudioSource;
    [SerializeField] private AudioClip m_FinalAudio;
    [SerializeField] private Animation m_IntroAnimation;
    private bool[] m_CurrentSimonSaysVisited;
    private bool m_PlayingFinalAudio;


    //Change Scene
    [SerializeField] private int m_sceneToChange;
    [SerializeField] private Image m_FadeImage;
    [SerializeField] private Animator m_FadeAnimator;
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
                //m_AudioSource.Play();
                //m_IntroAnimation.Play();
                StartCoroutine(PlayAfterFade());
                break;
        }

        StartCoroutine(LoadScene());
    }

	
	// Update is called once per frame
	void Update () {
        if (!m_Completed)
        {
            switch (m_PuzzleType)
            {
                case GameManager.PuzzleType.puzzleR:
                    if (m_BallRythm.m_CurrentNode == m_LastNode)
                    {
                        //Puzzle finished
                        m_Completed = true;
                        CompletePuzzle();
                        //ActivateScene();

                    }
                    break;

                case GameManager.PuzzleType.puzzleH:

                    //Update the music effects
                    for (int i = 0; i < m_Sliders.Length; i++)
                    {
                        m_AudioMixer.SetFloat("EQ" + i, m_Sliders[i].value);

                    }



                    if (m_Ball.transform.position == m_LastNode.worldPosition)
                    {
                        //Puzzle finished
                        m_Completed = true;
                        CompletePuzzle();
                        //ActivateScene();
                    }
                    break;

                case GameManager.PuzzleType.puzzleM:

                    if (m_Ball.m_CurrentNode.isSimonSays)
                    {
                        for (int i = 0; i < SimonSaysTransforms.Length; i++)
                        {
                            if (m_Grid.GetNodeContainingPosition(SimonSaysTransforms[i].position) == m_Ball.m_CurrentNode)
                            {
                                if (i > 0)
                                {
                                    if (m_CurrentSimonSaysVisited[i - 1])
                                    {
                                        if (!SimonSaysAudioSources[i].isPlaying && !m_CurrentSimonSaysVisited[i])
                                        {
                                            SimonSaysAudioSources[i].Play();
                                            if (m_AudioSource.isPlaying)
                                            {
                                                m_AudioSource.Stop();
                                                //m_IntroAnimation.Stop();
                                            }

                                        }

                                        m_CurrentSimonSaysVisited[i] = true;

                                    }
                                    else
                                    {
                                        ResetPlayerPosition();
                                    }
                                }
                                else if (i == 0)
                                {
                                    if (!SimonSaysAudioSources[i].isPlaying && !m_CurrentSimonSaysVisited[i])
                                    {
                                        SimonSaysAudioSources[i].Play();
                                        if (m_AudioSource.isPlaying)
                                        {
                                            m_AudioSource.Stop();
                                            //m_IntroAnimation.Stop();
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
                        m_Completed = true;
                        CompletePuzzle();
                        //ActivateScene();
                    }
                    break;
            }
        }

	}

    public void ResetPlayerPosition()
    {
        if(m_PuzzleType == GameManager.PuzzleType.puzzleM)
        {
            for (int j = 0; j < SimonSaysTransforms.Length; j++)
            {
                m_CurrentSimonSaysVisited[j] = false;
            }
            if(!m_IntroAnimation.isPlaying && !m_AudioSource.isPlaying)
            {
                m_IntroAnimation.Play();
                m_AudioSource.Play();
            }

        }
        m_Ball.ResetPosition();

    }
    public void StartLevel()
    {
    }

    public void ActivateScene()
    {
        if(m_async!= null)
        {
            StartCoroutine(ChangeScene());
        }
    }

    IEnumerator PlayAfterFade()
    {
        yield return new WaitUntil(() => m_FadeImage.color.a == 0);
        m_AudioSource.Play();
        m_IntroAnimation.Play();

    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(4f);

        m_async = SceneManager.LoadSceneAsync(m_sceneToChange);
        m_async.allowSceneActivation = false;
        yield return null;
    }

    IEnumerator ChangeScene()
    {
        m_FadeAnimator.SetBool("Fade", true);
        yield return new WaitUntil(() => m_FadeImage.color.a == 1);

        while (m_async.progress < 0.9f)
        {
            yield return null;
        }
       /* if(m_Completed)
        {
            /*GameManager.m_instance.AddCompletedPuzzle(m_PuzzleType);
            if(m_FinalAudio != null && !m_PlayingFinalAudio)
            {
                //m_AudioMixer.
                Debug.Log("Final not null");
                m_AudioSource.clip = m_FinalAudio;
                m_AudioSource.Play();
                m_PlayingFinalAudio = true;
            }
        }
        if (m_FinalAudio != null)
        {
            while (m_AudioSource.isPlaying)
            {
                //Debug.Log("Playing");
                yield return null;
            }
        }*/
        //m_PlayingFinalAudio = false;
        GameManager.m_instance.ResetLists();
        Debug.Log("Changed to scene: " + m_sceneToChange);
        m_async.allowSceneActivation = true;

    }

    private void CompletePuzzle()
    {
        if (m_PuzzleType == GameManager.PuzzleType.puzzleH)
        {
            m_Sliders[0].interactable = false;
        }
        GameManager.m_instance.AddCompletedPuzzle(m_PuzzleType);
        if (m_FinalAudio != null && !m_PlayingFinalAudio)
        {
            if(m_AudioMixer != null)
            m_AudioMixer.ClearFloat("EQ" + 0);

            m_AudioSource.clip = m_FinalAudio;
            m_AudioSource.Play();
            m_PlayingFinalAudio = true;
        }
    }
}
