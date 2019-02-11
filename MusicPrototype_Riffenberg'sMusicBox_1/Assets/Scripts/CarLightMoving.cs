using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLightMoving : MonoBehaviour {

    [SerializeField] private float m_Speed = 2;
    [SerializeField] private float m_Distance;
    [SerializeField] private Transform m_BackLight;

    private float BackLightOffset;

    private bool m_Swap = false;

    private Vector3 m_Origin;
	// Use this for initialization
	void Start () {
        m_Origin = transform.position + Vector3.right * m_Distance;
        BackLightOffset = m_BackLight.localPosition.x;

    }

    // Update is called once per frame
    void Update () {

        float l_SinValue = Mathf.Sin(m_Speed * Time.time);
        transform.position = new Vector3(m_Origin.x + l_SinValue * m_Distance, transform.position.y, transform.position.z);
        if(Mathf.Abs(l_SinValue) > Mathf.Abs(0.8f) && !m_Swap)
        {
            m_BackLight.localPosition = new Vector3(BackLightOffset * -1, m_BackLight.localPosition.y, m_BackLight.localPosition.z);
            BackLightOffset = -BackLightOffset;
            m_Swap = true;
        }
        else if (Mathf.Abs(l_SinValue) < Mathf.Abs(0.5f) && m_Swap)
        {
            m_Swap = false;
        }


    }
}
