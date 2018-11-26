using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;



public class PuzzleManager : MonoBehaviour {

    public static PuzzleManager m_instance = null;




    private enum TypeOfPuzzles {Rythm, MovingWalls, SimonSays }
    [SerializeField] TypeOfPuzzles m_PuzzleType;

    [SerializeField] private BallMovement m_Ball;
    [SerializeField] private GridScript m_Grid;

    //MovingWallsPuzzle
    [SerializeField] private Transform m_LasPosition;
    [SerializeField] private AudioMixer m_AudioMixer;

    [SerializeField] private Slider[] m_Sliders;
    [SerializeField] private int m_NumberOfWallPositions;

    private Node m_LastNode;


    //RythmPuzzle

    //SimonSaysPuzzle


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
            case TypeOfPuzzles.Rythm:

                break;

            case TypeOfPuzzles.MovingWalls:

                //Save the final node
                m_LastNode = m_Grid.GetNodeContainingPosition(m_LasPosition.position);


                    break;

            case TypeOfPuzzles.SimonSays:

                break;
        }
    }

	
	// Update is called once per frame
	void Update () {
		switch(m_PuzzleType)
        {
            case TypeOfPuzzles.Rythm:

                break;

            case TypeOfPuzzles.MovingWalls:

                //Update the music effects
                for (int i = 0; i < m_Sliders.Length; i++)
                {
                    m_AudioMixer.SetFloat("P" + i, m_Sliders[i].value);

                }



                if (m_Ball.m_CurrentNode == m_LastNode)
                {

                    Debug.Break();
                }
                break;

            case TypeOfPuzzles.SimonSays:

                break;
        }
	}
}
