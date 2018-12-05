using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLightMoving : MonoBehaviour {

    [SerializeField] private Transform m_Light;
    [SerializeField] private float m_Speed = 2;
    [SerializeField] private float m_Distance;

    private Vector3 m_Destination;
	// Use this for initialization
	void Start () {
        m_Destination = transform.position + Vector3.right * m_Distance;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mov = new Vector3(m_Destination.x + Mathf.Sin(m_Speed * Time.time) * m_Distance, transform.position.y, transform.position.z);
        transform.position = mov;
    }
}
