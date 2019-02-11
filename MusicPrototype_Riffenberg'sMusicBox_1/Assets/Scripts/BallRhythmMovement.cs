using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRhythmMovement : BeatActor {

    public Node m_CurrentNode;
	public Vector3 m_NextPosition, m_SpawnPosition;
    public Vector2 m_InitialDirection;
    public float m_ErrorRange;

	[SerializeField] private GridScript m_GameGrid;
	[SerializeField] private float m_Speed;
    [SerializeField] private GameObject m_trailObject;


    private List<GameObject> m_trailObjects = new List<GameObject>();

    private Vector2 m_Direction;
    private Vector2 m_InputDirection;
    private Node m_LastVisitedNode;
    private bool m_CanMove = true;
    private float l_StartMoveSample;
    private bool l_HasMoved = false;

    private float m_PressedTime = -1, m_BeatTime;


	void Start () 
	{
        m_SpawnPosition = transform.position;
        m_CurrentNode = m_GameGrid.GetNodeContainingPosition(transform.position);
        transform.position = m_CurrentNode.worldPosition;
        m_NextPosition = m_CurrentNode.worldPosition;

        m_Direction = m_InitialDirection;

        SetBehavior();

        m_ErrorRange *= BeatManager.audioFrequency;

    }

    void Update()
    {
        m_CurrentNode = m_GameGrid.GetNodeContainingPosition(transform.position);

        if (m_CanMove)
        {
            GetInput();

            if (BeatManager.currentSample >= l_StartMoveSample && !l_HasMoved)
            {
                Debug.Log("Direction is: " + m_Direction);
                Move();
                if (m_Direction != Vector2.zero) SpawnPath();
                l_HasMoved = true;
            }

            CheckPreviousNode();
            SetPreviousNode();
        }
        transform.position = Vector3.MoveTowards(transform.position, m_NextPosition, m_Speed * Time.deltaTime);

        if(!m_CurrentNode.isTransitable)
        {
            ResetPosition();
        }

        if (BeatListener())
        {
            m_BeatTime = BeatManager.currentSample;
            l_StartMoveSample = m_BeatTime + m_ErrorRange;
            l_HasMoved = false;
        }
    }
    private void SpawnPath()
    {
        m_trailObjects.Add(Instantiate(m_trailObject, transform.position, m_trailObject.transform.rotation));
    }
    private void ResetPosition()
    {
        m_CurrentNode = m_GameGrid.GetNodeContainingPosition(m_SpawnPosition);
        transform.position = m_CurrentNode.worldPosition;
        m_NextPosition = m_CurrentNode.worldPosition;
        m_LastVisitedNode = null;

        m_Direction = m_InitialDirection;

        foreach (GameObject trailObj in m_trailObjects)
        {
            Destroy(trailObj);
        }
        m_trailObjects = new List<GameObject>();

        m_CanMove = false;
        CancelInvoke();
        Invoke("ChangeCanMoveState", .5f);
    }
    private void GetInput()
    {
        m_PressedTime = -1;
        m_InputDirection = Vector2.zero;
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                m_InputDirection.x = -1;
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                m_InputDirection.x = 1;
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                m_InputDirection.y = 1;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                m_InputDirection.y = -1;
            }
            m_PressedTime = BeatManager.currentSample;

            if (m_PressedTime != -1 && Mathf.Abs(m_PressedTime - m_BeatTime) < m_ErrorRange)
            {
                if (m_InputDirection != Vector2.zero)
                {
                    Debug.Log("Input direction is: " + m_InputDirection);

                    m_Direction = m_InputDirection;

                    transform.position = m_CurrentNode.worldPosition;

                }
            }
        }

        
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
                    ResetPosition();
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
                    ResetPosition();
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
                    ResetPosition();
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
                    ResetPosition();
                }
            }
        }
    }
    private void SetPreviousNode()
    {
        if (m_NextPosition != m_CurrentNode.worldPosition)
        {
            m_LastVisitedNode = m_CurrentNode;
        }
    }
    private void CheckPreviousNode()
    {
        if (m_LastVisitedNode != null)
        {
            if (m_NextPosition == m_LastVisitedNode.worldPosition)
            {
                ResetPosition();
            }
        }
    }
    private void ChangeCanMoveState()
    {
        m_CanMove = !m_CanMove;
    }

}
