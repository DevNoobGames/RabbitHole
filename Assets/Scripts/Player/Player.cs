using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class Player : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject aimCam;

    public Camera cam;

    public CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin noise;


    public Animator _walkingAnim;
    public Chaser chase;
    public Animator rabbitRunAnim;
    public Transform resetPoint;
    public CharacterController charCon;
    public GameObject pauseMenu;
    public TextMeshProUGUI timerText;
    public float timer;
    public GameObject winPanel;
    public TextMeshProUGUI winTextScore;

    [Header("Shooting mechanics")]
    public GameObject leafBullet;
    public Transform shootPos;
    bool canShoot = true;
    public float reloadTime = 1;
    public LayerMask ignoreMe;

    [Header("Audio")]
    public AudioSource shootAUD;
    public AudioSource powerupAUD;

    [Header("PowerBar")]
    public RawImage powerbarImg;
    public Texture[] powerbarSequence;
    public int activeImg = 0;
    public TextMeshProUGUI friendsSavedText;
    public GameObject allCollectedAnim;
    public arrowScript arrowScr;
    public GameObject escapeDoor;

    [Header("Health and pain")]
    public Animation hurtAnim;
    public List<GameObject> leaves = new List<GameObject>();
    int healthRemaining = 5;
    bool canGetHurt = true;
    public GameObject gameOverPanel;
    public AudioSource hurtAUD;

    private void Start()
    {
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        camAim();
        walkingAnim();
        shooting();
        pauseMenuFunction();
        timerFunction();
    }

    public void timerFunction()
    {
        timer += Time.deltaTime;
        float minutes = Mathf.FloorToInt(timer / 60);
        float seconds = Mathf.FloorToInt(timer % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }

    public void pauseMenuFunction()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeInHierarchy)
            {
                closePauseMenu();
            }
            else
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
    public void closePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void shooting()
    {
        if (Input.GetMouseButton(0) && canShoot)
        {
            canShoot = false;
            StartCoroutine(reloader());

            shootAUD.Play();
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            ray.origin = cam.transform.position;
            if (Physics.Raycast(ray, out RaycastHit hit, 300f,~ignoreMe))
            {
                Noise(0.81f, 0.13f);
                GameObject bullet = Instantiate(leafBullet, shootPos.position, Quaternion.identity);
                bullet.GetComponent<LeafBullet>().targetPos = hit.point;
            }


        }
    }
    IEnumerator reloader()
    {
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
        Noise(0, 0);
    }
    public void Noise(float amplitudeGain, float frequencyGain)
    {
        noise.m_AmplitudeGain = amplitudeGain;
        noise.m_FrequencyGain = frequencyGain;
    }

    public void camAim()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mainCam.SetActive(false);
            aimCam.SetActive(true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            aimCam.SetActive(false);
            mainCam.SetActive(true);
        }
    }

    public void walkingAnim()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
        {
            _walkingAnim.SetBool("walking", true);
        }
        else if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
        {
            _walkingAnim.SetBool("walking", true);
        }
        else
        {
            _walkingAnim.SetBool("walking", false);
        }
    }

    public void gotHurt()
    {
        canGetHurt = false;
        hurtAnim.Play();
        hurtAUD.Play();
        Debug.Log(leaves.Count);
        if (leaves.Count > 0)
        {
            Destroy(leaves[leaves.Count - 1]);
            leaves.RemoveAt(leaves.Count - 1);
        }
        healthRemaining -= 1;
        if (healthRemaining <= 0)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        StartCoroutine(resetHurt());
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void newGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0); //CHANGE THIS TO ACTIVE
    }

    IEnumerator resetHurt()
    {
        yield return new WaitForSeconds(1);
        canGetHurt = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            if (canGetHurt)
            {
                canGetHurt = false;
                gotHurt();
            }
        }

        if (collision.gameObject.CompareTag("Poop"))
        {
            if (canGetHurt)
            {
                canGetHurt = false;
                gotHurt();
            }
            Destroy(collision.gameObject);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("flower"))
        {
            Destroy(other.gameObject.transform.parent.gameObject);
            activeImg += 1;
            powerbarImg.texture = powerbarSequence[activeImg];
            powerupAUD.Play();
            friendsSavedText.text = "Flowers saved: " + activeImg + "/12";
            if (activeImg == 12)
            {
                allCollectedAnim.SetActive(true);
                arrowScr.doorTarget = escapeDoor;
            }
        }

        if (other.CompareTag("attackBunny"))
        {
            if (canGetHurt)
            {
                canGetHurt = false;
                gotHurt();
            }
        }

        if (other.CompareTag("mainBunny"))
        {
            if (canGetHurt)
            {
                canGetHurt = false;
                gotHurt();
            }
        }

        if (other.CompareTag("activateBunny"))
        {
            chase.enabled = true;
            rabbitRunAnim.enabled = true;
        }

        if (other.CompareTag("doomsFire"))
        {
            if (canGetHurt)
            {
                canGetHurt = false;
                gotHurt();
                charCon.enabled = false;
                transform.position = new Vector3(resetPoint.position.x, resetPoint.position.y, resetPoint.position.z);
                charCon.enabled = true;

            }
        }

        if (other.CompareTag("winCube"))
        {
            winPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);
            winTextScore.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
            Time.timeScale = 0;

        }
    }
}
