using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelUIHandle : MonoBehaviour
{
    public GameObject muscleIconForPlayer1;
    public GameObject muscleIconForPlayer2;

    public GameObject shooterIconForPlayer1;
    public GameObject shooterIconForPlayer2;

    public GameObject swordIconForPlayer1;
    public GameObject swordIconForPlayer2;

    public TextMeshProUGUI player1CoinCountText;
    public TextMeshProUGUI player2CoinCountText;

    public TextMeshProUGUI player1VirusCountText;
    public TextMeshProUGUI player2VirusCountText;

    public TextMeshProUGUI resultForPlayer1Text;
    public TextMeshProUGUI resultForPlayer2Text;

    public TextMeshProUGUI attackPowerText1;
    public TextMeshProUGUI maxHPText1;
    public TextMeshProUGUI regenHPText1;

    public TextMeshProUGUI attackPowerText2;
    public TextMeshProUGUI maxHPText2;
    public TextMeshProUGUI regenHPText2;

    public GameObject swordIcon1;
    public GameObject swordSkillIcon1;
    public TextMeshProUGUI slashRadiusText1;

    public GameObject swordIcon2;
    public GameObject swordSkillIcon2;
    public TextMeshProUGUI slashRadiusText2;

    public GameObject shooterIcon1;
    public GameObject shooterSkillIcon1;
    public TextMeshProUGUI shootCDText1;

    public GameObject shooterIcon2;
    public GameObject shooterSkillIcon2;
    public TextMeshProUGUI shootCDText2;

    public GameObject muscleIcon1;
    public GameObject muscleSkillIcon1;
    public TextMeshProUGUI moveSpeedText1;

    public GameObject muscleIcon2;
    public GameObject muscleSkillIcon2;
    public TextMeshProUGUI moveSpeedText2;

    private GameObject playerGameObject1;
    private GameObject playerGameObject2;
    private Player player1;
    private Player player2;

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

        HideAllActive();
    }

    void HideAllActive()
    {
        player1CoinCountText.gameObject.SetActive(false);
        player2CoinCountText.gameObject.SetActive(false);

        player1VirusCountText.gameObject.SetActive(false);
        player2VirusCountText.gameObject.SetActive(false);

        HidePlayerPanel(attackPowerText1, maxHPText1, regenHPText1,
                            swordIcon1, swordSkillIcon1, slashRadiusText1,
                            shooterIcon1, shooterSkillIcon1, shootCDText1,
                            muscleIcon1, muscleSkillIcon1, moveSpeedText1);

        HidePlayerPanel(attackPowerText2, maxHPText2, regenHPText2,
                            swordIcon2, swordSkillIcon2, slashRadiusText2,
                            shooterIcon2, shooterSkillIcon2, shootCDText2,
                            muscleIcon2, muscleSkillIcon2, moveSpeedText2);
    }

    void HidePlayerPanel(params UnityEngine.Object[] texts)
    {
        foreach (var obj in texts)
        {
            if (obj is TextMeshProUGUI text)
            {
                text.gameObject.SetActive(false);
            }
            else if (obj is GameObject gameObject)
            {
                gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        PreparePanelUpdate();
        StatePanelUpdate();
    }

    void PreparePanelUpdate()
    {
        player1CoinCountText.gameObject.SetActive(true);
        player1CoinCountText.text = player1.coinCount.ToString();

        player2CoinCountText.gameObject.SetActive(true);
        player2CoinCountText.text = player2.coinCount.ToString();

        player1VirusCountText.gameObject.SetActive(true);
        player1VirusCountText.text = player1.virusCount.ToString();

        player2VirusCountText.gameObject.SetActive(true);
        player2VirusCountText.text = player2.virusCount.ToString();
    }

    void StatePanelUpdate()
    {
        UpdatePlayerPanel(player1,
                          attackPowerText1, maxHPText1, regenHPText1,
                          swordIcon1, swordSkillIcon1, slashRadiusText1,
                          shooterIcon1, shooterSkillIcon1, shootCDText1,
                          muscleIcon1, muscleSkillIcon1, moveSpeedText1);

        UpdatePlayerPanel(player2,
                          attackPowerText2, maxHPText2, regenHPText2,
                          swordIcon2, swordSkillIcon2, slashRadiusText2,
                          shooterIcon2, shooterSkillIcon2, shootCDText2,
                          muscleIcon2, muscleSkillIcon2, moveSpeedText2);
    }

    void UpdatePlayerPanel(Player player,
                           TextMeshProUGUI attackPowerText, TextMeshProUGUI maxHPText, TextMeshProUGUI regenHPText,
                           GameObject swordIcon, GameObject swordSkillIcon, TextMeshProUGUI slashRadiusText,
                           GameObject shooterIcon, GameObject shooterSkillIcon, TextMeshProUGUI shootCDText,
                           GameObject muscleIcon, GameObject muscleSkillIcon, TextMeshProUGUI moveSpeedText)
    {
        attackPowerText.gameObject.SetActive(true);
        maxHPText.gameObject.SetActive(true);
        regenHPText.gameObject.SetActive(true);

        attackPowerText.text = "Attack Power: " + player.attackPower.ToString();
        maxHPText.text = "Max HP: " + player.maxHP.ToString();
        regenHPText.text = $"{player.regenHPAmount} HP / {player.regenHPCD} s";

        if (player.isSword)
        {
            swordIcon.SetActive(true);
            slashRadiusText.gameObject.SetActive(true);
            slashRadiusText.text = "Slash Radius: " + player.slashRadius.ToString();

            swordSkillIcon.SetActive(player.swordSkill);
        }
        else
        {
            swordIcon.SetActive(false);
            swordSkillIcon.SetActive(false);
            slashRadiusText.gameObject.SetActive(false);
        }

        if (player.isShooter)
        {
            shooterIcon.SetActive(true);
            shootCDText.gameObject.SetActive(true);
            shootCDText.text = "Shoot Cooldown: " + player.shootCD.ToString();

            shooterSkillIcon.SetActive(player.shooterSkill);
        }
        else
        {
            shooterIcon.SetActive(false);
            shooterSkillIcon.SetActive(false);
            shootCDText.gameObject.SetActive(false);
        }

        if (player.isMuscle)
        {
            muscleIcon.SetActive(true);
            moveSpeedText.gameObject.SetActive(true);
            moveSpeedText.text = "Move Speed: " + player.moveSpeed.ToString();

            muscleSkillIcon.SetActive(player.muscleSkill);
        }
        else
        {
            muscleIcon.SetActive(false);
            muscleSkillIcon.SetActive(false);
            moveSpeedText.gameObject.SetActive(false);
        }
    }
}