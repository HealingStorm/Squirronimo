using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    private Player playerScript;
    public int boosterBoost;
    public AudioSource jumpSound;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerScript = other.GetComponent<Player>();
            if (playerScript.rb2D.velocity.y < 0)
            {
                jumpSound.Play();
                playerScript.rb2D.velocity += new Vector2(0, boosterBoost);
                Destroy(gameObject, 1f);
            }
        }
    }
}
