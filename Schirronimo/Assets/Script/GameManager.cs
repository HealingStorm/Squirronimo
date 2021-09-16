using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        PlayerRB2D = player.GetComponent<Rigidbody2D>();
        gameCam = gamecamGO.GetComponent<CinemachineVirtualCamera>();
        playerScript = player.GetComponent<Player>();
    }
    
    public GameObject player;
    private Rigidbody2D PlayerRB2D;
    private Player playerScript;

    public GameObject gamecamGO;
    private CinemachineVirtualCamera gameCam;

    void Start()
    {
        GameStart();
        StartCoroutine(TakeOff());
    }
    
    public void GameStart()
    {
        Debug.Log("gamestart");
        player.transform.position = new Vector2(0,0);
        PlayerRB2D.gravityScale = 0;
        playerScript.DisableMovementInputs();

        gameCam.Follow = null;
        gamecamGO.transform.position = new Vector3(0,4,-10);
        
    }

    IEnumerator TakeOff()
    {
        yield return new WaitForSeconds(5f);

        playerScript.EnableMovementInputs();
        playerScript.DisableTakeoffInputs();

        if (playerScript.tapNumber <= 5)
        {
            Debug.Log("jump 100 meters");
        }
        else
        {
            Debug.Log("jump 200 meters");
        }
    }
}
