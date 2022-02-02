using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryDoor : MonoBehaviour
{
    public float health = 100;
    public Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("leafBullet") && player.activeImg >= 12)
        {
            Destroy(other.gameObject);
            health -= 20;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
