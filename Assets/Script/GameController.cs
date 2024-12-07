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

    private GameObject[] zombieSpawnerObject;
    private ZombieSpawner[] zombieSpawner;

    public TextMeshProUGUI player1ZombieKillCountText;
    public TextMeshProUGUI player2ZombieKillCountText;

    public TextMeshProUGUI player1CoinCountText;
    public TextMeshProUGUI player2CoinCountText;

    public TextMeshProUGUI player1VirusCountText;
    public TextMeshProUGUI player2VirusCountText;

    public float levelCountDownTimer;
    public TextMeshProUGUI levelCountDownTimerText;

    public GameObject pauseButton;

    public GameObject player1HPBar;
    public GameObject player2HPBar;

    public bool isPlayer1Dead = false;
    public bool isPlayer2Dead = false;

    public int level = 1;
    public TextMeshProUGUI levelText;
    public bool isBossLevel = false;
    public GameObject bossLevelIcon;

    public GameObject startPanel;
    public bool isPlayer1Choose = false;
    public bool isPlayer2Choose = false;

    public GameObject preparePanel;
    public GameObject statePanel;

    public bool isGamePause = false;
    public bool isGameInProgress = false;

    public GameObject losePanel;
    public GameObject winPanel;
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

        zombieSpawnerObject = GameObject.FindGameObjectsWithTag("ZombieSpawnArea");
        zombieSpawner = new ZombieSpawner[zombieSpawnerObject.Length];

        for (int i = 0; i < zombieSpawnerObject.Length; i++)
        {
            if (zombieSpawnerObject[i] != null)
            {
                zombieSpawner[i] = zombieSpawnerObject[i].GetComponent<ZombieSpawner>();
            }
        }


        level = 1;

        HideUI();

        StartPanel();

        isGameInProgress = false;
    }

    void Update()
    {
        TextHandle();

        LevelHandle();
    }

    public void HideUI()
    {
        player1HPBar.gameObject.SetActive(false);
        player2HPBar.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        player1.playerHPText.gameObject.SetActive(false);
        player2.playerHPText.gameObject.SetActive(false);
        player1ZombieKillCountText.gameObject.SetActive(false);
        player2ZombieKillCountText.gameObject.SetActive(false);
        player1CoinCountText.gameObject.SetActive(false);
        player2CoinCountText.gameObject.SetActive(false);
        player1VirusCountText.gameObject.SetActive(false);
        player2VirusCountText.gameObject.SetActive(false);
    }

    public void ShowUI()
    {
        player1HPBar.gameObject.SetActive(true);
        player2HPBar.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);
        player1.playerHPText.gameObject.SetActive(true);
        player2.playerHPText.gameObject.SetActive(true);
        player1ZombieKillCountText.gameObject.SetActive(true);
        player2ZombieKillCountText.gameObject.SetActive(true);
        player1CoinCountText.gameObject.SetActive(true);
        player2CoinCountText.gameObject.SetActive(true);
        player1VirusCountText.gameObject.SetActive(true);
        player2VirusCountText.gameObject.SetActive(true);
    }

    void TextHandle()
    {
        player1ZombieKillCountText.text = player1.zombieKillCount.ToString();
        player2ZombieKillCountText.text = player2.zombieKillCount.ToString();

        player1CoinCountText.text = player1.coinCount.ToString();
        player2CoinCountText.text = player2.coinCount.ToString();

        player1VirusCountText.text = player1.virusCount.ToString();
        player2VirusCountText.text = player2.virusCount.ToString();

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
                DestroyObject();
                if (!isGameOver && level < 12)
                {
                    level++;
                    if (level % 4 == 0)
                    {
                        isBossLevel = true;
                        for (int i = 0; i < zombieSpawner.Length; i++)
                        {
                            if (zombieSpawner[i] != null)
                            {
                                zombieSpawner[i].isBossSpawn = false;
                            }
                        }
                    }
                    else
                    {
                        isBossLevel = false;
                    }
                    HideUI();
                    PreparePanel();
                }

                if (level >= 12)
                {
                    GameOver();
                }
            }
        }
        bossLevelIcon.SetActive(isBossLevel);
    }

    void DestroyObject()
    {
        DestroyTaggedObjects("Zombie");
        DestroyTaggedObjects("Player1Bullet");
        DestroyTaggedObjects("Player2Bullet");
    }

    private void DestroyTaggedObjects(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }

    public void FirstChoice(Player player)
    {
        player.moveSpeed = 3f;
        player.maxHP = 5;
        player.regenHPCD = 15f;
        player.regenHPAmount = 1;
        player.attackPower = 2;

        if (player.isMuscle)
        {
            player.maxHP += 10;
            player.regenHPCD -= 12f;
            player.regenHPAmount += 1;
            player.attackPower += 1;
        }

        else if (player.isSword)
        {
            player.maxHP += 2;
            player.regenHPCD -= 3f;
            player.attackPower += 2;
        }

        else if (player.isShooter)
        {
            player.moveSpeed += 2f;
            player.attackPower -= 1;
        }

        player.remainingHP = player.maxHP;
    }

    public void StartPanel()
    {
        levelCountDownTimer = 0;
        isGameInProgress = false;
        Time.timeScale = 0;

        startPanel.SetActive(true);
    }

    public void GameStart()
    {
        isGameInProgress = true;
        Time.timeScale = 1;
        startPanel.SetActive(false);

        levelCountDownTimer = isBossLevel ? 91f : 61f;

        ShowUI();
    }

    //Press esc to pause or continue and show the state panel for p1 and p2
    public void StatePanel()
    {
        if (!isGameOver && isGameInProgress)
        {
            isGamePause = !isGamePause;
            Time.timeScale = isGamePause ? 0 : 1;

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

    public void NextLevel()
    {
        isGameInProgress = true;
        Time.timeScale = 1;
        preparePanel.SetActive(false);

        player1.remainingHP = player1.maxHP;
        player1.hPSlider.value = player1.remainingHP;

        player2.remainingHP = player2.maxHP;
        player2.hPSlider.value = player2.remainingHP;

        levelCountDownTimer = isBossLevel ? 91f : 61f;

        ShowUI();
    }

    //Player dead handle
    public void GameOver()
    {
        if (isPlayer1Dead && isPlayer2Dead)
        {
            HideUI();
            losePanel.SetActive(true);
            isGameOver = true;
        }

        else if (level >= 12)
        {
            HideUI();
            winPanel.SetActive(true);
            isGameOver = true;
        }
        
    }
}
