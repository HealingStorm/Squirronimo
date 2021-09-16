using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Vector3 spawningZone;
    public Transform player;
    public GameObject booster;
    public GameObject stationnaryEnemy;
    public Transform spawnerParent;
    private int boostOrBad;

    private float timer;
    public float timerDefault;

    void Start()
    {
        timer = timerDefault;
        boostOrBad = 0;
    }

    void Update()
    {
        if (GameManager._instance.spawnerToggle == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = timerDefault;

                spawningZone = new Vector3(Random.Range(-6f, 6f), Random.Range(player.position.y + 7, player.position.y + 30), player.position.z);
                if (boostOrBad == 0)
                {
                    GameObject boosterClone = Instantiate(booster, spawningZone, Quaternion.identity, spawnerParent);

                    boostOrBad = 1;
                }
                else
                {
                    GameObject stationnaryEnemyClone = Instantiate(stationnaryEnemy, spawningZone, Quaternion.identity, spawnerParent);
                    boostOrBad = 0;
                }
            }
        }
    }
}
