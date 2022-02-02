using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumSwing : MonoBehaviour
{
    float MaxAngleDeflection = 30.0f;
    float SpeedOfPendulum = 1.0f;
    float time;

    private void Start()
    {
        time = Random.Range(0, 60);
        SpeedOfPendulum = Random.Range(1.5f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float angle = MaxAngleDeflection * Mathf.Sin(time * SpeedOfPendulum);
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
