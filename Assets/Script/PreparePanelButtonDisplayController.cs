using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreparePanelButtonDisplayController : MonoBehaviour
{
    private GameObject playerGameObject1;
    private GameObject playerGameObject2;
    private Player player1;
    private Player player2;

    //PreparePanel Button Display
    public GameObject preparePanelPlayer1MuscleSkillBuyButtton;
    public GameObject preparePanelPlayer1SwordSkillBuyButtton;
    public GameObject preparePanelPlayer1ShooterSkillBuyButtton;

    public GameObject preparePanelPlayer1MoveSpeeedBuyButtton;
    public GameObject preparePanelPlayer1SlashRadiusBuyButtton;
    public GameObject preparePanelPlayer1ShootCDBuyButtton;

    public GameObject preparePanelPlayer2MuscleSkillBuyButtton;
    public GameObject preparePanelPlayer2SwordSkillBuyButtton;
    public GameObject preparePanelPlayer2ShooterSkillBuyButtton;

    public GameObject preparePanelPlayer2MoveSpeeedBuyButtton;
    public GameObject preparePanelPlayer2SlashRadiusBuyButtton;
    public GameObject preparePanelPlayer2ShootCDBuyButtton;

    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.isMuscle)
        {
            preparePanelPlayer1MoveSpeeedBuyButtton.SetActive(true);
            preparePanelPlayer1MuscleSkillBuyButtton.SetActive(true);
        }

        if (player1.isSword)
        {
            preparePanelPlayer1SlashRadiusBuyButtton.SetActive(true);
            preparePanelPlayer1SwordSkillBuyButtton.SetActive(true);
        }

        if (player1.isShooter)
        {
            preparePanelPlayer1ShootCDBuyButtton.SetActive(true);
            preparePanelPlayer1ShooterSkillBuyButtton.SetActive(true);
        }

        if (player2.isMuscle)
        {
            preparePanelPlayer2MoveSpeeedBuyButtton.SetActive(true);
            preparePanelPlayer2MuscleSkillBuyButtton.SetActive(true);
        }

        if (player2.isSword)
        {
            preparePanelPlayer2SlashRadiusBuyButtton.SetActive(true);
            preparePanelPlayer2SwordSkillBuyButtton.SetActive(true);
        }

        if (player2.isShooter)
        {
            preparePanelPlayer2ShootCDBuyButtton.SetActive(true);
            preparePanelPlayer2ShooterSkillBuyButtton.SetActive(true);
        }
    }
}
