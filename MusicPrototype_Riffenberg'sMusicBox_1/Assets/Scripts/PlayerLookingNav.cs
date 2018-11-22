using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookingNav : MonoBehaviour
{
    private Camera m_SceneCamera;
    [SerializeField] private Transform m_rotatePosTransform;
    [SerializeField] private LayerMask m_mouseMask;

    [SerializeField] private float 
        m_rotSpeed,
        m_moveSpeed
        ;
    private Vector3 m_originalObjectPos, m_targetObjectPos;
    private Quaternion m_originalObjectRot;

    [HideInInspector] public bool m_isMoving, m_BoxOn;

    private Transform m_actualObject;
    [SerializeField] Transform m_UIObject;

   

	// Use this for initialization
	void Start ()
    {
        GameManager.m_instance.m_playerNav = this;
        m_SceneCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!m_isMoving)
        {
            //Vector3 l_mouseOnWorld = m_SceneCamera.ViewportToWorldPoint(Input.mousePosition);

            InteractableObject l_actualInteractable = null;

            Ray l_mouseRay = m_SceneCamera.ViewportPointToRay(Input.mousePosition);

            RaycastHit l_RaycastHit;
            if (Physics.Raycast(l_mouseRay, out l_RaycastHit, 200.0f, m_mouseMask.value))
            {
                if(l_RaycastHit.transform.tag == "Interactable")
                {
                    l_actualInteractable = GameManager.m_instance.GetInteractableObject(l_RaycastHit.transform);
                    l_actualInteractable.MouseOver();
                }
            }

            //Comprobar objeto que estamos encima, highliht (?)
            if (Input.GetMouseButtonDown(0) && l_actualInteractable != null)
            {
                l_actualInteractable.OnClick();

            }
        }

    }

    public void RotateSide(float _degreesToRotate)
    {
        m_isMoving = true;
        StartCoroutine(RotateObject(_degreesToRotate));

    }
    public void BringObjectClose(Transform _IntRotobject)
    {
        m_actualObject = _IntRotobject;
        m_isMoving = true;
        m_originalObjectPos = m_actualObject.position;
        m_originalObjectRot = m_actualObject.rotation;

        StartCoroutine(MoveObject( m_rotatePosTransform.position,m_rotatePosTransform.rotation,false));

    }
    public void LeaveObjectDown()
    {
        m_isMoving = true;


        StartCoroutine(MoveObject(m_originalObjectPos, m_originalObjectRot, true));
    }
    IEnumerator RotateObject(float _angles)
    {
        m_UIObject.gameObject.SetActive(false);

        float l_originalRot = m_actualObject.transform.rotation.y;
        float l_rotCounting = 0f;

        Quaternion l_finalRot = Quaternion.Euler(0, m_actualObject.rotation.y + _angles, 0);

        while (l_rotCounting < Mathf.Abs(_angles))
        {
            l_rotCounting += m_rotSpeed * Time.deltaTime;
            m_actualObject.RotateAround(m_actualObject.position, Vector3.up, Mathf.Sign(_angles) * m_rotSpeed * Time.deltaTime);
            yield return null;
        }
        m_isMoving = false;
        m_UIObject.gameObject.SetActive(true);
        
    }
    IEnumerator MoveObject(Vector3 _targetPos,Quaternion _targetRot, bool _detachEnd)
    {
        float l_originalRot = m_actualObject.transform.rotation.y;
        float l_rotCounting = 0f;

        while ((_targetPos - m_actualObject.position).magnitude <= 0)
        {
            l_rotCounting += m_rotSpeed * Time.deltaTime;
           // m_actualObject.Rotate(m_actualObject.position, Vector3.up, Mathf.Sign(_targetRot) * m_rotSpeed * Time.deltaTime);
            Vector3 l_direction = _targetPos - m_actualObject.position;
            m_actualObject.position += l_direction * m_moveSpeed * Time.deltaTime;
            //m_actualObject.rotation =;
            yield return null;
        }
        if(_detachEnd)
        {
            m_actualObject = null;
            m_BoxOn = false;
            m_UIObject.gameObject.SetActive(false);
        }
        else
        {
            m_UIObject.gameObject.SetActive(true);
            m_BoxOn = true;
        }
    }
    IEnumerator CloseTopuzzle(Transform _targetToClose)
    {
        //while ()
        //{
        //    yield return null;
        //}
    }
}
