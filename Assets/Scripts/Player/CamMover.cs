using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMover : MonoBehaviour
{
    float rotationY = 0;

    public float lookSpeed = 2;
    public float lookLimit = 45.0f;
    public bool CanMove;


    // Update is called once per frame
    void Update()
    {
        if (CanMove && Time.timeScale != 0)
        {
            rotationY += Input.GetAxis("Mouse Y") * lookSpeed;
            rotationY = Mathf.Clamp(rotationY, -lookLimit, lookLimit);
            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }
}
