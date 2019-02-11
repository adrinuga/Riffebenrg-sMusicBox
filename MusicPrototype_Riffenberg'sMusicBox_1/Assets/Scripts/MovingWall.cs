using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingWall : MonoBehaviour
{

    [SerializeField] private GridScript m_Grid;
    [SerializeField] private Slider m_Slider;
    [SerializeField] private Transform[] m_Positions;
    [SerializeField] private float m_Speed = 50f;

    private Node[] m_PositionsInNodes;
    private List<int> m_Path = new List<int>();
    private Node m_TargetNode;
    private int m_CurrentPositionIndex = 0;
    private int m_PreviousSliderPos;



    // Use this for initializations
    void Start()
    {
        //Saves the Node list and destroy the GameObjects used to indicate the new positions of the walls
        m_PositionsInNodes = new Node[m_Positions.Length];
        for (int i = 0; i < m_Positions.Length; i++)
        {
            m_PositionsInNodes[i] = m_Grid.GetNodeContainingPosition(m_Positions[i].position);
            //Destroy(m_Positions[i].gameObject);
        }
    }

    void Update()
    {
        //Checks if the slider value has changed and depending on the way it changed sets a path for the wall
        //That's because if slider is moved too fast unity cant catch all the values
        BuildPath();

        if (m_TargetNode != null)
        {
            //Update the position following the path
            transform.position = Vector3.MoveTowards(transform.position, m_TargetNode.worldPosition, m_Speed * Time.deltaTime);

            //if has arrived to the destination sets a new target and updates the grid
            if (transform.position == m_TargetNode.worldPosition)
            {
                m_Grid.GenerateGrid();
                m_CurrentPositionIndex++;
                if (m_CurrentPositionIndex < m_Path.Count)
                {
                    m_TargetNode = m_Grid.GetNodeContainingPosition(m_PositionsInNodes[m_Path[m_CurrentPositionIndex]].worldPosition);
                }

                //If has arrived at the end of the path resets
                else
                {
                    m_CurrentPositionIndex = 0;
                    m_Path.Clear();
                }
            }
        }

        //Update PreviousSliderPosition for the next update
        m_PreviousSliderPos = (int)m_Slider.value;
    }

    private void BuildPath()
    {
        if ((int)m_Slider.value != m_PreviousSliderPos)
        {
            if (m_PreviousSliderPos < (int)m_Slider.value)
            {
                for (int i = Mathf.Abs(m_PreviousSliderPos - (int)m_Slider.value); i > 0; i--)
                {
                    if (m_Slider.value - i >= 0 && (m_Slider.value - i) + 1 < m_Positions.Length)
                    {
                        m_Path.Add(((int)m_Slider.value - i) + 1);
                    }
                }
            }

            if (m_PreviousSliderPos > (int)m_Slider.value)
            {
                for (int i = 0; i <= Mathf.Abs(m_PreviousSliderPos - (int)m_Slider.value); i++)
                {
                    if (m_PreviousSliderPos - i >= 0 && m_PreviousSliderPos - i < m_Positions.Length)
                    {
                        m_Path.Add(m_PreviousSliderPos - i);
                    }
                }
            }
            //Initialize targetNode
            if (m_Path.Count >= 1)
            {
                m_TargetNode = m_Grid.GetNodeContainingPosition(m_PositionsInNodes[m_Path[m_CurrentPositionIndex]].worldPosition);
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    if (m_TargetNode != null)
    //        Gizmos.DrawCube(m_TargetNode.worldPosition, Vector3.one);
    //}

}