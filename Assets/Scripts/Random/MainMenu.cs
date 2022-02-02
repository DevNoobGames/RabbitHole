using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject introSeqe;

    public void startTheeeeeeeeeeeeeeeeeee()
    {
        introSeqe.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void hejda()
    {
        Application.Quit();
    }
}
