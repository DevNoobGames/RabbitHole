using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSeq : MonoBehaviour
{
    public bool introFinished = false;

    [Header("Start game")]
    public GameObject[] activateOnStartAfterFilm;
    public GameObject[] deactivateOnStartAfterFilm;

    [Header("Intro seq")]
    public GameObject mainMenuCam;
    public GameObject cam1;
    public GameObject openingEyes;
    public GameObject cam2;
    public GameObject oxeyeDaisy1;
    public GameObject text1;
    public GameObject cam3;
    public GameObject oxeyeDaise2;
    public GameObject text2;
    public GameObject cam4;
    public GameObject text3;
    public GameObject cam5;
    public GameObject text4;

    [Header("Start Menu")]
    public GameObject menu;

    [Header("Song")]
    public AudioSource introSong;
    public AudioSource gameBG;
    public AudioSource popUp;

    public void Start()
    {
        StartCoroutine(camSeq());
    }

    IEnumerator camSeq()
    {
        mainMenuCam.SetActive(false);
        introSong.Play();
        cam1.SetActive(true);
        openingEyes.SetActive(true);
        yield return new WaitForSeconds(6);
        cam1.SetActive(false);
        openingEyes.SetActive(false);

        cam2.SetActive(true);
        oxeyeDaisy1.SetActive(true);
        yield return new WaitForSeconds(5);
        text1.SetActive(true);
        popUp.Play();
        yield return new WaitForSeconds(2f);
        cam2.SetActive(false);
        oxeyeDaisy1.SetActive(false);
        text1.SetActive(false);

        cam3.SetActive(true);
        oxeyeDaise2.SetActive(true);
        yield return new WaitForSeconds(6.5f);
        popUp.Play();
        text2.SetActive(true);
        text3.SetActive(true);
        text4.SetActive(true);

        yield return new WaitForSeconds(8);
        startGame();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !introFinished)
        {
            StopAllCoroutines();
            startGame();
        }
    }

    public void startGame()
    {
        introSong.Stop();
        introFinished = true;
        foreach (GameObject item in deactivateOnStartAfterFilm)
        {
            item.SetActive(false);
        }
        foreach (GameObject item1 in activateOnStartAfterFilm)
        {
            item1.SetActive(true);
        }
        Time.timeScale = 0;
        menu.SetActive(true);
        StartCoroutine(timeScaleShi()); //because timescale starts with items in setactive
    }
    IEnumerator timeScaleShi()
    {
        yield return new WaitForSeconds(0.3f);
        gameBG.Play();
        Time.timeScale = 0;
        menu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void closeMenu()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
