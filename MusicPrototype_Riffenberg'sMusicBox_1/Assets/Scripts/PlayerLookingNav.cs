using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookingNav : MonoBehaviour
{

    [SerializeField] private Transform m_menuPos, m_rotatePos;

    [SerializeField] private float m_rotSpeed, m_moveSpeed;

    [HideInInspector] public bool m_isMoving, m_BoxOn;

    

    private InteractableObject m_actualObject;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!m_isMoving)
        {
            //Comprobar objeto que estamos encima, highliht (?)
            if (Input.GetMouseButtonDown(0))
            {

            }

        }
        else
        {
            //if ()
            //{
            //    m_isMoving = false;
            //}
        }

    }
    public void MoveToStart()
    {
        m_isMoving = true;
    }
    public void RotateSide(float m_degreesToRotate)
    {
        m_isMoving = true;
    }
    public void BringObjectClose(Transform _IntRotobject)
    {

    }
    public void LeaveObjectDown()
    {

    }
}
