using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Vector3 spawningZone;
    public Transform player;
    public GameObject booster;
    public GameObject stationnaryEnemy;
    public GameObject horizontalEnemy;
    public Transform spawnerParent;
    private bool boostOrBad;
    private int staticOrMoveE;

    private float timer;
    public float timerDefault;

    void Start()
    {
        timer = timerDefault;
        boostOrBad = false;
        staticOrMoveE = 0;
    }

    void Update()
    {
        if (GameManager._instance.spawnerToggle == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = timerDefault;

                spawningZone = new Vector3(Random.Range(-6f, 6f), Random.Range(player.position.y + 7, player.position.y + 35), player.position.z);
                if (boostOrBad == false)
                {
                    GameObject boosterClone = Instantiate(booster, spawningZone, Quaternion.identity, spawnerParent);

                    boostOrBad = true;
                }
                else
                {
                    staticOrMoveE = Random.Range(0, 4);
                    if (staticOrMoveE <= 2)
                    {
                        GameObject stationnaryEnemyClone = Instantiate(stationnaryEnemy, spawningZone, Quaternion.identity, spawnerParent);
                        boostOrBad = false;
                    }
                    if (staticOrMoveE == 3)
                    {
                        GameObject horizontalEnemyClone = Instantiate(horizontalEnemy, spawningZone, Quaternion.identity, spawnerParent);
                        boostOrBad = false;
                    }
                }
            }
        }
    }
}
