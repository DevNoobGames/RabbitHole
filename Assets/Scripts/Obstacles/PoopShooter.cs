using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopShooter : MonoBehaviour
{
    public GameObject poop;
    public Transform shootPos;

    public bool forward;
    public float maxRotation;
    public float minRotation;
    public float moveSpeed;
    public bool WeirdEulerNumbers;

    private void Start()
    {
        StartCoroutine(shooting(1));
        forward = true;
    }

    private void Update()
    {
        if (WeirdEulerNumbers)
        {
            if (forward == true)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + moveSpeed, transform.eulerAngles.z);
                if (transform.eulerAngles.y >= maxRotation && transform.eulerAngles.y <= minRotation)
                {
                    forward = false;
                }
            }
            else if (forward == false)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - moveSpeed, transform.eulerAngles.z);
                if (transform.eulerAngles.y <= minRotation && transform.eulerAngles.y >= maxRotation)
                {
                    forward = true;
                }
            }
        }
        else if (!WeirdEulerNumbers)
        {
            if (forward == true)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + moveSpeed, transform.eulerAngles.z);
                if (transform.eulerAngles.y >= maxRotation)
                {
                    forward = false;
                }
            }
            else if (forward == false)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - moveSpeed, transform.eulerAngles.z);
                if (transform.eulerAngles.y <= minRotation)
                {
                    forward = true;
                }
            }
        }
    }

IEnumerator shooting(float sec)
    {
        yield return new WaitForSeconds(sec);
        GameObject pp = Instantiate(poop, shootPos.position, Quaternion.identity);
        pp.GetComponent<Rigidbody>().AddForce(transform.forward * 100);
        float secc = Random.Range(0.5f, 3);
        StartCoroutine(shooting(secc));
    }
}
