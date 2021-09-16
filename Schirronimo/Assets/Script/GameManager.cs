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
        startCam = startcamGO.GetComponent<CinemachineVirtualCamera>();
        playerScript = player.GetComponent<Player>();

        startCam.Priority = 5;
        gameCam.Priority = 1;
        GameStart();
        StartCoroutine(TakeOff());
    }
    
    public GameObject player;
    private Rigidbody2D PlayerRB2D;
    private Player playerScript;

    public GameObject gamecamGO;
    private CinemachineVirtualCamera gameCam;

    public GameObject startcamGO;
    private CinemachineVirtualCamera startCam;

    public float gravityScale;

    public int takeoffHeight;

    
    public void GameStart()
    {
        Debug.Log("gamestart");
        player.transform.position = new Vector2(0,0);
        PlayerRB2D.gravityScale = 0;
        playerScript.DisableMovementInputs();

        gameCam.Follow = null;
        gamecamGO.transform.position = new Vector3(0,2,-10);
        
    }

    IEnumerator TakeOff()
    {
        yield return new WaitForSeconds(3f);

        PlayerRB2D.gravityScale = gravityScale;
        playerScript.EnableMovementInputs();
        playerScript.DisableTakeoffInputs();

        gameCam.Priority = 10;
        gameCam.Follow = player.transform;

        if (playerScript.tapNumber <= 5)
        {
            Debug.Log("jump 10 meters");
            playerScript.jumpHeight = takeoffHeight;
            playerScript.rb2D.AddForce(new Vector2(0, playerScript.jumpHeight));
            playerScript.tapNumber = 0;
        }
        if (playerScript.tapNumber > 5 && playerScript.tapNumber <= 10)
        {
            Debug.Log("jump 20 meters");
            playerScript.jumpHeight = takeoffHeight + 1000;
            playerScript.rb2D.AddForce(new Vector2(0, playerScript.jumpHeight));
            playerScript.tapNumber = 0;
        }
        if (playerScript.tapNumber > 10)
        {
            Debug.Log("jump 20 meters");
            playerScript.jumpHeight = takeoffHeight + 2000;
            playerScript.rb2D.AddForce(new Vector2(0, playerScript.jumpHeight));
            playerScript.tapNumber = 0;
        }
    }
}
