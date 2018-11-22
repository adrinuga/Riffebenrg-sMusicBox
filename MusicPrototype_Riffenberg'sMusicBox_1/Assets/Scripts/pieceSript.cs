using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pieceSript : MonoBehaviour
{

    public GridScript theGrid;
    [SerializeField] private Transform[] m_Positions;
    private Node[] m_Nodes;
    private int m_CurrentPositionIndex = 0;

    // Use this for initializations
    void Start()
    {
        m_Nodes = new Node[m_Positions.Length];
        for (int i = 0; i < m_Positions.Length; i++)
        {
            m_Nodes[i] = theGrid.GetNodeContainingPosition(m_Positions[i].position);
            //Destroy(m_Positions[i].gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveWallToNextPosition()
    {
        if(m_CurrentPositionIndex < m_Nodes.Length - 1)
        {
            m_CurrentPositionIndex++;
        }
        transform.position = m_Nodes[m_CurrentPositionIndex].worldPosition;
        theGrid.GenerateGrid();

    }
    public void MoveWallToPreviousPosition()
    {
        if (m_CurrentPositionIndex > 0)
        {
            m_CurrentPositionIndex--;
        }
        transform.position = m_Nodes[m_CurrentPositionIndex].worldPosition;
        theGrid.GenerateGrid();
    }
}
