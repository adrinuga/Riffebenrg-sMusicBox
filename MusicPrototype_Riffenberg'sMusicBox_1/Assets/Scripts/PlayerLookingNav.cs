using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


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

    [HideInInspector]
    public bool
        m_isMoving = false,
        m_BoxOn = false;
        

    [HideInInspector]  public Transform m_actualObject;
    [SerializeField]
    public Transform m_UIObject;

    [SerializeField] Animation m_cameraAnimation;

    [Header("Events")]

    [SerializeField]
    private UnityEvent
        m_CloseToFinal,
        m_OutFromFinal
        ;



    // Use this for initialization
    void Start ()
    {
        GameManager.m_instance.m_playerNav = this;
        m_SceneCamera = Camera.main;

        if (GameManager.m_instance.m_beforeSceneInfo.m_lastIndexScene != 0)
        {
            m_BoxOn = true;
            m_UIObject.gameObject.SetActive(true);
            m_actualObject = GameManager.m_instance.m_SafePrevious;

        }

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!m_isMoving)
        {
            //Debug.Log("inplayernav");
            //Vector3 l_mouseOnWorld = m_SceneCamera.ViewportToWorldPoint(Input.mousePosition);

            InteractableObject l_actualInteractable = null;

            Ray l_mouseRay = m_SceneCamera.ScreenPointToRay(Input.mousePosition);

            RaycastHit l_RaycastHit;
            if (Physics.Raycast(l_mouseRay, out l_RaycastHit, 200.0f, m_mouseMask.value))
            {
                //Debug.Log("Looking fr object");
                if(l_RaycastHit.transform.tag == "Interactable")
                {
                    Debug.Log("InteractableFound");
                    l_actualInteractable = GameManager.m_instance.GetInteractableObject(l_RaycastHit.transform);
                    l_actualInteractable.MouseOver();
                }
            }

            
            if (Input.GetMouseButtonDown(0) && l_actualInteractable != null)
            {
                //Debug.Log("InteractableClicked");
                l_actualInteractable.OnClick();

            }
        }

    }

    public void RotateSide(float _degreesToRotate)
    {
        m_isMoving = true;
        StartCoroutine(RotateObject(_degreesToRotate));

    }
    public void BringObjectClose(Transform _IntRotobject, float _time)
    {
        m_actualObject = _IntRotobject;
        m_originalObjectPos = m_actualObject.position;
        m_originalObjectRot = m_actualObject.rotation;

        StartCoroutine(WaitTimeToNotMoving(_time,true));

    }
    public void LeaveObjectDown(float _time)
    {
        m_isMoving = true;

        StartCoroutine(WaitTimeToNotMoving(_time, false));
        
    }
    public void PlayAnimation(AnimationClip _clip)
    {
        m_cameraAnimation.clip = _clip;

        m_cameraAnimation.Play();

        m_isMoving = true;
    }
    public void LeaveObject()
    {
        m_BoxOn = false;
    }

    IEnumerator RotateObject(float _angles)
    {
        HideMouse();
        m_UIObject.gameObject.SetActive(false);

        float l_originalRot = m_actualObject.transform.rotation.eulerAngles.y;
        float l_rotCounting = 0f;

        Quaternion l_finalRot = Quaternion.Euler(0, m_actualObject.rotation.y + _angles, 0);

        while (l_rotCounting < Mathf.Abs(_angles))
        {
            l_rotCounting += m_rotSpeed * Time.deltaTime;
            m_actualObject.RotateAround(m_actualObject.position, Vector3.up, Mathf.Sign(_angles) * m_rotSpeed * Time.deltaTime);
            yield return null;
        }
        m_actualObject.eulerAngles = new Vector3(m_actualObject.eulerAngles.x, l_originalRot + _angles, m_actualObject.eulerAngles.z);

        m_isMoving = false;
        m_UIObject.gameObject.SetActive(true);
        
        ShowMouse();
    }
    IEnumerator WaitTimeToNotMoving(float _timeToWait, bool m_objectBring)
    {
        HideMouse();
        m_isMoving = true;
        yield return new WaitForSeconds(_timeToWait);
        m_isMoving = false;

        if (m_objectBring)
        {
            m_BoxOn = true;
            m_UIObject.gameObject.SetActive(true);
        }
        else
        {
            m_actualObject = null;
            m_BoxOn = false;
        }
        ShowMouse();
    }

  
   

    public void HideMouse()
    {
        Cursor.visible = false;
    }
    public void ShowMouse()
    {
        Cursor.visible = true;
    }
    public void InvokeEvent(float _isClosingCamera)
    {
        if(_isClosingCamera <= 0)
        {
            m_CloseToFinal.Invoke();
        }
        else if (_isClosingCamera >= 0)
        {
            m_OutFromFinal.Invoke();
        }
        m_isMoving = false;

    }
   

}
