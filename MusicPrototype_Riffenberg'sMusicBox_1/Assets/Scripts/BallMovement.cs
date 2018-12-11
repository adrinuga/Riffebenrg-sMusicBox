using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    public Node m_CurrentNode;
	public Vector3 m_NextPosition, m_SpawnPosition;

	[SerializeField] private GridScript m_GameGrid;
	[SerializeField] private float m_Speed;

    private Vector2 m_Direction;
    private Node m_LastVisitedNode;
    private bool m_CanMove = true;


	void Start () 
	{
        m_SpawnPosition = transform.position;
        m_CurrentNode = m_GameGrid.GetNodeContainingPosition(transform.position);
        transform.position = m_CurrentNode.worldPosition;
        m_NextPosition = m_CurrentNode.worldPosition;


    }

    void Update()
    {
        if (!PuzzleManager.m_instance.m_Completed)
        {
            m_CurrentNode = m_GameGrid.GetNodeContainingPosition(transform.position);

            if (transform.position == m_NextPosition && m_CanMove)
            {
                GetInput();
                Move();

                CheckPreviousNode();
                SetPreviousNode();
            }
            transform.position = Vector3.MoveTowards(transform.position, m_NextPosition, m_Speed * Time.deltaTime);

            if (!m_CurrentNode.isTransitable)
            {
                print("Reset");
                PuzzleManager.m_instance.ResetPlayerPosition();
            }
        }
    }

    public void ResetPosition()
    {
        m_CurrentNode = m_GameGrid.GetNodeContainingPosition(m_SpawnPosition);
        transform.position = m_CurrentNode.worldPosition;
        m_NextPosition = m_CurrentNode.worldPosition;
        m_LastVisitedNode = null;

        m_Direction.x = 0;
        m_Direction.y = 0;

        m_CanMove = false;
        CancelInvoke();
        Invoke("ChangeCanMoveState", .5f);
    }
    private void GetInput()
    {
        m_Direction.x = Input.GetAxisRaw("Horizontal");
        m_Direction.y = Input.GetAxisRaw("Vertical");
    }
    private void Move()
    {
        if (m_Direction.x < 0)
        {
            Node l_NextNode = m_GameGrid.GetNode(m_CurrentNode.gridPositionX - 1, m_CurrentNode.gridPositionY);
            if (l_NextNode != null)
            {
                if (l_NextNode.isTransitable)
                {
                    m_NextPosition = l_NextNode.worldPosition;
                    m_Direction.y = 0;
                }
                else
                {
                    m_Direction.x = 0;
                }
            }
        }
        else if (m_Direction.x > 0)
        {
            Node l_NextNode = m_GameGrid.GetNode(m_CurrentNode.gridPositionX + 1, m_CurrentNode.gridPositionY);
            if (l_NextNode != null)
            {
                if (l_NextNode.isTransitable)
                {
                    m_NextPosition = l_NextNode.worldPosition;
                    m_Direction.y = 0;
                }
                else
                {
                    m_Direction.x = 0;
                }
            }
        }

        else if (m_Direction.y > 0)
        {
            Node l_NextNode = m_GameGrid.GetNode(m_CurrentNode.gridPositionX, m_CurrentNode.gridPositionY + 1);
            if (l_NextNode != null)
            {
                if (l_NextNode.isTransitable)
                {
                    m_NextPosition = l_NextNode.worldPosition;
                    m_Direction.x = 0;
                }
                else
                {
                    m_Direction.y = 0;
                }
            }
        }
        else if (m_Direction.y < 0)
        {
            Node l_NextNode = m_GameGrid.GetNode(m_CurrentNode.gridPositionX, m_CurrentNode.gridPositionY - 1);
            if (l_NextNode != null)
            {
                if (l_NextNode.isTransitable)
                {
                    m_NextPosition = l_NextNode.worldPosition;
                    m_Direction.x = 0;
                }
                else
                {
                    m_Direction.y = 0;
                }
            }

        }
    }
    private void SetPreviousNode()
    {
        if (m_NextPosition != m_CurrentNode.worldPosition)
        {
            m_LastVisitedNode = m_CurrentNode;
            m_GameGrid.GetNodeContainingPosition(m_LastVisitedNode.worldPosition).hasBeenVisited = true;
        }
    }
    private void CheckPreviousNode()
    {
        if (m_LastVisitedNode != null)
        {
            if (m_GameGrid.GetNodeContainingPosition(m_NextPosition).hasBeenVisited)
            {
                PuzzleManager.m_instance.ResetPlayerPosition();
                m_GameGrid.ResetVisitedNodes();
            }
        }
    }
    private void ChangeCanMoveState()
    {
        m_CanMove = !m_CanMove;
    }

}
