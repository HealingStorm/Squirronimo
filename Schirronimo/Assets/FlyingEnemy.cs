using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public int speed;
    private Vector3 direction;
    public SpriteRenderer sprR;

    void Start()
    {
        direction = new Vector3(1,0,0);
    }
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
        if(transform.position.x >= 6)
        {
            direction = -direction;
            sprR.flipX = true;
        }
        if(transform.position.x <= -6)
        {
            direction = -direction;
            sprR.flipX = false;
        }
    }
}
