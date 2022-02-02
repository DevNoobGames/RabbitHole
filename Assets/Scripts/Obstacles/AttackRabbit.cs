using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRabbit : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        float speed = Random.Range(0.8f, 1.2f);
        anim.speed = speed;
    }
}
