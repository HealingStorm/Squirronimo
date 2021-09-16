using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    private Player playerScript;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("gameover");
            playerScript = other.GetComponent<Player>();
            playerScript.rb2D.velocity -= new Vector2(0, 10);
        }
    }
}
