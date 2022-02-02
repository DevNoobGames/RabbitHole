using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitch : MonoBehaviour
{
    public GameObject oldCam;
    public GameObject newCam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oldCam.SetActive(false);
            newCam.SetActive(true);
        }
    }
}
