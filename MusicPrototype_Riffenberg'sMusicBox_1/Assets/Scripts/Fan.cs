using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan: MonoBehaviour
{
    [SerializeField] private float m_Speed;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, m_Speed * Time.deltaTime, 0);
    }
}

