using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject spawnerParent;
    private GameManager gameManager;
    private bool playerdead;

    void Awake()
    {
        gameManager = GameManager._instance;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerdead = true;
            UIMenuManager.s_Singleton.gameOverScreen.SetActive(true);
            gameManager.PlayerRB2D.gravityScale = 0;
            gameManager.PlayerRB2D.velocity = new Vector2(0, 0);
            gameManager.player.transform.position = new Vector2(0, 0);
            gameManager.spawnerToggle = false;
            for (int i = 0; i < spawnerParent.transform.childCount; i++)
            {
                Destroy(spawnerParent.transform.GetChild(i).gameObject);
            }
        }
    }
}
