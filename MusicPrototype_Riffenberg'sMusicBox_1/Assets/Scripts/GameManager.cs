using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager m_instance = null;

    public List<InteractableObject> m_interactableObjects;
    public PlayerLookingNav m_playerNav;
    public FinalPuzzleMov m_finalPuzzle;
    public bool 
        m_melodyPuzzle,
        m_rythmPuzzle,
        m_harmonyPuzzle
        ;
    public bool m_finalAvaliable;

    public struct NavSceneInfo
    {
        public Vector3 m_boxPos;
        public Quaternion m_boxRot;
        public int m_lastIndexScene;
    }
    [HideInInspector]
    public bool
        m_puzzleCompletedM = false,
        m_puzzleCompletedH = false,
        m_puzzleCompletedR = false
        ;

    public NavSceneInfo m_beforeSceneInfo;
    [HideInInspector] public Transform m_SafePrevious;

    public enum PuzzleType
    {
        puzzleM,
        puzzleH,
        puzzleR
    }

	// Use this for initialization
	void Awake ()
    {
		if(m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        m_beforeSceneInfo.m_lastIndexScene = 0;
	}
    void Start()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {

    }
    public InteractableObject  GetInteractableObject(Transform _object)
    {
        InteractableObject l_iObject = null;
        foreach(InteractableObject i in m_interactableObjects)
        {
            if (i.ReturnObject() == _object)
            {
                l_iObject = i;
               
            }
            
        }
        return l_iObject;
    }
    public void SaveInfo(Vector3 _boxPos, Quaternion _boxRot, int _sceneIndex)
    {
        NavSceneInfo l_newSave = new NavSceneInfo();

        l_newSave.m_boxRot = _boxRot;
        l_newSave.m_boxPos = _boxPos;

        l_newSave.m_lastIndexScene = _sceneIndex;

        m_beforeSceneInfo = l_newSave;
    }
    public void AddCompletedPuzzle(PuzzleType _pType)
    {
        switch (_pType)
        {
            case (PuzzleType.puzzleM):

                m_puzzleCompletedM = true;

                break;
            case (PuzzleType.puzzleH):

                m_puzzleCompletedH = true;

                break;
            case (PuzzleType.puzzleR):

                m_puzzleCompletedR = true;

                break;
            default:
                break;
        }

        if (m_puzzleCompletedH && m_puzzleCompletedR && m_puzzleCompletedM)
            m_finalAvaliable = true;


    }

    public void ResetLists()
    {
        m_interactableObjects = new List<InteractableObject>();
    }

}
