using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Timeline.Actions;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private GameObject playerGameObject1;
    private GameObject playerGameObject2;
    private Player player1;
    private Player player2;

    public TextMeshProUGUI player1ZombieKillCountText;
    public TextMeshProUGUI player2ZombieKillCountText;

    public TextMeshProUGUI player1CoinCountText;
    public TextMeshProUGUI player2CoinCountText;

    public float levelCountDownTimer;
    public TextMeshProUGUI levelCountDownTimerText;

    public int level;
    public TextMeshProUGUI levelText;
    public bool isBossLevel = false;
    public GameObject bossLevelIcon;

    public GameObject preparePanel;
    public GameObject statePanel;

    public bool isGamePause = false;
    public bool isGameInProgress = false;

    public GameObject resultPanel;
    public bool isGameOver = false;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerGameObject1 = GameObject.Find("Player1");
        playerGameObject2 = GameObject.Find("Player2");

        if (playerGameObject1 != null)
        {
            player1 = playerGameObject1.GetComponent<Player>();
        }

        if (playerGameObject2 != null)
        {
            player2 = playerGameObject2.GetComponent<Player>();
        }

        level = 1;

        HideUI();

        PreparePanel();

        isGameInProgress = false;
    }

    void Update()
    {
        TextHandle();

        LevelHandle();

        StatePanel();

        GameOver();
    }

    public void HideUI()
    {
        player1.playerHPText.gameObject.SetActive(false);
        player2.playerHPText.gameObject.SetActive(false);
        player1ZombieKillCountText.gameObject.SetActive(false);
        player2ZombieKillCountText.gameObject.SetActive(false);
        player1CoinCountText.gameObject.SetActive(false);
        player2CoinCountText.gameObject.SetActive(false);
        levelCountDownTimerText.gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        player1.playerHPText.gameObject.SetActive(true);
        player2.playerHPText.gameObject.SetActive(true);
        player1ZombieKillCountText.gameObject.SetActive(true);
        player2ZombieKillCountText.gameObject.SetActive(true);
        player1CoinCountText.gameObject.SetActive(true);
        player2CoinCountText.gameObject.SetActive(true);
        levelCountDownTimerText.gameObject.SetActive(true);
    }

    void TextHandle()
    {
        player1ZombieKillCountText.text = "Killed " + player1.zombieKillCount.ToString();
        player2ZombieKillCountText.text = "Killed " + player2.zombieKillCount.ToString();

        player1CoinCountText.text = "Coin " + player1.coinCount.ToString();
        player2CoinCountText.text = "Coin " + player2.coinCount.ToString();

        int minutes = Mathf.FloorToInt(levelCountDownTimer / 60);
        int seconds = Mathf.FloorToInt(levelCountDownTimer % 60);
        levelCountDownTimerText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);

        levelText.text = "level " + level.ToString();
    }

    //Set the level number and determine if the level is a boss level. And the timer to handle level if end.
    void LevelHandle()
    {
        if (!isGameOver && isGameInProgress)
        {
            if (levelCountDownTimer > 0)
            {
                levelCountDownTimer -= Time.deltaTime;
            }
            else if (levelCountDownTimer <= 0)
            {
                DestroyAllZombie();
                level++;
                if (level % 4 == 0)
                {
                    isBossLevel = true;
                }
                else
                {
                    isBossLevel = false;
                }
                HideUI();
                PreparePanel();
            }
        }
        bossLevelIcon.SetActive(isBossLevel);
    }

    void DestroyAllZombie()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (GameObject zombie in zombies)
        {
            Destroy(zombie);
        }
    }

    //Press esc to pause or continue and show the state panel for p1 and p2
    void StatePanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver && isGameInProgress)
        {
            isGamePause = !isGamePause;
            Time.timeScale = isGamePause ? 0 : 1;
            Cursor.visible = isGamePause;

            if (isGamePause)
            {
                HideUI();
            }
            else
            {
                ShowUI();
            }

            statePanel.SetActive(isGamePause);
        }
    }

    //Game start ,level end after and levle start before will show this panel
    void PreparePanel()
    {
        levelCountDownTimer = 0;
        isGameInProgress = false;
        Time.timeScale = 0;

        preparePanel.SetActive(true);
    }

    //Player dead handle
    void GameOver()
    {
        if (player1 != null && player2 != null)
        {
            if (player1.remainingHP <= 0 && player2.remainingHP <= 0)
            {
                resultPanel.SetActive(true);
                isGameOver = true;
            }
        }
    }
}
