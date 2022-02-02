using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chaser : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;

    [Header ("Jumpspeed")]
    public bool linking;
    public float origSpeed;
    public float linkSpeed;     // just change linkspeed to alter off mesh link traverse speed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        origSpeed = agent.speed;
        linking = false;
    }

    void Update()
    {
        agent.SetDestination(target.transform.position);
    }

    void FixedUpdate()
    {
        if (agent.isOnOffMeshLink && linking == false)
        {
            linking = true;
            agent.speed = agent.speed * linkSpeed;
        }
        else if (agent.isOnNavMesh && linking == true)
        {
            linking = false;
            agent.velocity = Vector3.zero;
            agent.speed = origSpeed;
        }
    }

}
