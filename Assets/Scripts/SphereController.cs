using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    [SerializeField] private Rigidbody m_rigidBody;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_rigidBody.isKinematic = true;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            m_rigidBody.isKinematic = false;
        }
    }
}
