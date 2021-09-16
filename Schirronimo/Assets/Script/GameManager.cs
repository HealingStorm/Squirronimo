using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    //player
    public GameObject player;
    private Rigidbody2D PlayerRB2D;
    private Player playerScript;
    public float gravityScale;
    public int takeoffHeight;

    //Score
    public int currentScore;
    public int maxScore;
    public int highScore;

    //cams
    public GameObject gamecamGO;
    private CinemachineVirtualCamera gameCam;

    public GameObject startcamGO;
    private CinemachineVirtualCamera startCam;

    //collider game over
    public GameObject laser;

    //Spawner toggle
    public bool spawnerToggle;

    //UIManager reference
    public UIMenuManager menuManager;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        menuManager = UIMenuManager.s_Singleton;        
    }

    private void Start() 
    {
        spawnerToggle = false;
        PlayerRB2D = player.GetComponent<Rigidbody2D>();
        gameCam = gamecamGO.GetComponent<CinemachineVirtualCamera>();
        startCam = startcamGO.GetComponent<CinemachineVirtualCamera>();
        playerScript = player.GetComponent<Player>();
        startCam.Priority = 5;
        gameCam.Priority = 1;

        GameStart();
    }

    void Update()
    {
        //quand le joueur monte, le death ray monte, quand le joueur descend, celui-ci reste sur place pour le cueillir
        if(PlayerRB2D.velocity.y <= 0)
        {
            laser.transform.position = new Vector2(0, laser.transform.position.y);
        }

        if(PlayerRB2D.velocity.y > 0)
        {
            laser.transform.position = new Vector2(0, player.transform.position.y - 20);
        }

        menuManager.currentScoreTxt.GetComponent<Text>().text = currentScore.ToString();
        menuManager.highScoreTxt.GetComponent<Text>().text = maxScore.ToString();

        currentScore = Mathf.FloorToInt(player.transform.position.y);
        if(currentScore >= maxScore)
        {
            maxScore = currentScore;
            if(maxScore >= highScore)
            {
                highScore = maxScore;
            }
        }

        menuManager.finalScoreTxt.GetComponent<Text>().text = maxScore.ToString();
        menuManager.finalHighScore.GetComponent<Text>().text = highScore.ToString();
    }

    
    public void GameStart()
    {
        Debug.Log("gamestart");
        player.transform.position = new Vector2(0,0);
        PlayerRB2D.gravityScale = 0;
        playerScript.DisableMovementInputs();

        gameCam.Follow = null;
        gamecamGO.transform.position = new Vector3(0,2,-10);
        
        spawnerToggle = true;

        StartCoroutine(TakeOff());
        
    }

    IEnumerator TakeOff()
    {
        yield return new WaitForSeconds(2f);

        gameCam.Priority = 10;
        gameCam.Follow = player.transform;

        yield return new WaitForSeconds(1f);

        PlayerRB2D.gravityScale = gravityScale;
        playerScript.EnableMovementInputs();
        playerScript.DisableTakeoffInputs();

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
