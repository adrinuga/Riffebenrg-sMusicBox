using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingWall : MonoBehaviour
{

    [SerializeField] private GridScript m_Grid;
    [SerializeField] private PuzzleManager m_PuzzleManager;
    [SerializeField] private Slider m_Slider;
    [SerializeField] private Transform[] m_Positions;
    private Node[] m_Nodes;
    private int m_CurrentPositionIndex = 0;


    private float speed = 10f;
    private bool isMoving = false;

    // Use this for initializations
    void Start()
    {
        m_Nodes = new Node[m_Positions.Length];
        for (int i = 0; i < m_Positions.Length; i++)
        {
            m_Nodes[i] = m_Grid.GetNodeContainingPosition(m_Positions[i].position);
            //Destroy(m_Positions[i].gameObject);
        }
        m_CurrentPositionIndex = (int)m_Slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if((int)m_Slider.value != m_CurrentPositionIndex)
        {
            MoveWall((int)m_Slider.value);
        }


        transform.position = Vector3.MoveTowards(transform.position, m_Nodes[m_CurrentPositionIndex].worldPosition, speed * Time.deltaTime);
        if(transform.position == m_Nodes[m_CurrentPositionIndex].worldPosition)
        {
            m_Grid.GenerateGrid();
            isMoving = false;

        }
        else
        {
            isMoving = true;
        }


    }
    public void MoveWall(int _NewPosition)
    {
        if (_NewPosition < m_Positions.Length && _NewPosition >= 0)
        {
            if(isMoving)
            {
                transform.position = m_Nodes[m_CurrentPositionIndex].worldPosition;
                m_CurrentPositionIndex = _NewPosition;
            }
            else
            {
                m_CurrentPositionIndex = _NewPosition;
            }

        }
    }
}
