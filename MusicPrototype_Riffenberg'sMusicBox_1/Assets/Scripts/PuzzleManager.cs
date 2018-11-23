using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour {

    public static PuzzleManager m_instance = null;


    private enum TypeOfPuzzles {Rythm, MovingWalls, SimonSays }
    [SerializeField] TypeOfPuzzles m_PuzzleType;

    [SerializeField] private BallMovement m_Ball;
    [SerializeField] private GridScript m_Grid;

    //MovingWallsPuzzle
    [SerializeField] private Transform m_LasPosition;
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
                m_Grid.GetNodeContainingPosition(m_LastNode.worldPosition);
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


                if(m_Ball.m_CurrentNode == m_LastNode)
                {
                    print("PUZZLE FINISHED");
                    Debug.Break();
                }
                break;

            case TypeOfPuzzles.SimonSays:

                break;
        }
	}
}
