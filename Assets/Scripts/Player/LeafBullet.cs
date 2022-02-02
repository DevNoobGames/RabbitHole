using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBullet : MonoBehaviour
{
    public Vector3 targetPos;
    float speed = 10;

    private void Update()
    {
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject, 5f);
        }
    }
}
