using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



public class PuzzleManager : MonoBehaviour {

    public static PuzzleManager m_instance = null;




    private enum TypeOfPuzzles {Rhythm, MovingWalls, SimonSays }
    [SerializeField] TypeOfPuzzles m_PuzzleType;

    [SerializeField] private BallMovement m_Ball;
    [SerializeField] private GridScript m_Grid;

    //MovingWallsPuzzle
    [SerializeField] private Transform m_LastPosition;
    [SerializeField] private AudioMixer m_AudioMixer;

    [SerializeField] private Slider[] m_Sliders;
    [SerializeField] private int m_NumberOfWallPositions;

    private Node m_LastNode;


    //RythmPuzzle

    //SimonSaysPuzzle
    [SerializeField] private Transform[] SimonSaysTransforms;
    [SerializeField] private AudioSource[] SimonSaysAudioSources;
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_FinalAudio;
    private bool[] m_CurrentSimonSaysVisited;


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
            case TypeOfPuzzles.Rhythm:

                break;

            case TypeOfPuzzles.MovingWalls:

                //Save the final node
                m_LastNode = m_Grid.GetNodeContainingPosition(m_LastPosition.position);


                    break;

            case TypeOfPuzzles.SimonSays:
                m_CurrentSimonSaysVisited = new bool[SimonSaysTransforms.Length];
                for (int i = 0; i < SimonSaysTransforms.Length; i++)
                {
                    m_CurrentSimonSaysVisited[i] = false;
                }
                m_AudioSource.Play();
                break;
        }
    }

	
	// Update is called once per frame
	void Update () {
		switch(m_PuzzleType)
        {
            case TypeOfPuzzles.Rhythm:

                break;

            case TypeOfPuzzles.MovingWalls:

                //Update the music effects
                for (int i = 0; i < m_Sliders.Length; i++)
                {
                    m_AudioMixer.SetFloat("P" + i, m_Sliders[i].value);

                }



                if (m_Ball.m_CurrentNode == m_LastNode)
                {
                    //Puzzle finished
                    Debug.Break();
                }
                break;

            case TypeOfPuzzles.SimonSays:

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
                if (l_SimonSaysVisitedNodes == SimonSaysTransforms.Length)
                {
                    //Puzzle finished
                    Debug.Break();
                }
                break;
        }


	}

    public void ResetPlayerPosition()
    {
        if(m_PuzzleType == TypeOfPuzzles.SimonSays)
        {
            for (int j = 0; j < SimonSaysTransforms.Length; j++)
            {
                m_CurrentSimonSaysVisited[j] = false;
                m_Ball.ResetPosition();
            }
        }
    }

}
