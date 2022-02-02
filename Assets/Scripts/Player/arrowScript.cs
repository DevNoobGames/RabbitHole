using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowScript : MonoBehaviour
{
    public GameObject doorTarget;
    public GameObject target;
    
    // Update is called once per frame
    void Update()
    {
        GameObject target = FindClosestEnemy();

        if (doorTarget)
        {
            transform.LookAt(doorTarget.transform.position);
        }
        else if (target)
        {
            transform.LookAt(target.transform.position);
        }
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("flower");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
