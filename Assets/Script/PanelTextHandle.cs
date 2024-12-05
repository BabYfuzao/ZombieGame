using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelTextHandle : MonoBehaviour
{
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

    public TextMeshProUGUI swordIconText1;
    public TextMeshProUGUI swordSkillIconText1;
    public TextMeshProUGUI slashRadiusText1;

    public TextMeshProUGUI swordIconText2;
    public TextMeshProUGUI swordSkillIconText2;
    public TextMeshProUGUI slashRadiusText2;

    public TextMeshProUGUI shooterIconText1;
    public TextMeshProUGUI shooterSkillIconText1;
    public TextMeshProUGUI shootCDText1;

    public TextMeshProUGUI shooterIconText2;
    public TextMeshProUGUI shooterSkillIconText2;
    public TextMeshProUGUI shootCDText2;

    public TextMeshProUGUI muscleIconText1;
    public TextMeshProUGUI muscleSkillIconText1;
    public TextMeshProUGUI moveSpeedText1;

    public TextMeshProUGUI muscleIconText2;
    public TextMeshProUGUI muscleSkillIconText2;
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

        HideAllTextActive();
    }

    void HideAllTextActive()
    {
        player1CoinCountText.gameObject.SetActive(false);
        player2CoinCountText.gameObject.SetActive(false);

        player1VirusCountText.gameObject.SetActive(false);
        player2VirusCountText.gameObject.SetActive(false);

        HidePlayerPanelText(attackPowerText1, maxHPText1, regenHPText1,
                            swordIconText1, swordSkillIconText1, slashRadiusText1,
                            shooterIconText1, shooterSkillIconText1, shootCDText1,
                            muscleIconText1, muscleSkillIconText1, moveSpeedText1);

        HidePlayerPanelText(attackPowerText2, maxHPText2, regenHPText2,
                            swordIconText2, swordSkillIconText2, slashRadiusText2,
                            shooterIconText2, shooterSkillIconText2, shootCDText2,
                            muscleIconText2, muscleSkillIconText2, moveSpeedText2);
    }

    void HidePlayerPanelText(params TextMeshProUGUI[] texts)
    {
        foreach (var text in texts)
        {
            text.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        PreparePanelUpdateText();
        StatePanelUpdateText();
    }

    void PreparePanelUpdateText()
    {
        player1CoinCountText.gameObject.SetActive(true);
        player1CoinCountText.text = "Coin: " + player1.coinCount.ToString();

        player2CoinCountText.gameObject.SetActive(true);
        player2CoinCountText.text = "Coin: " + player2.coinCount.ToString();

        player1VirusCountText.gameObject.SetActive(true);
        player1VirusCountText.text = "Virus " + player1.virusCount.ToString();

        player2VirusCountText.gameObject.SetActive(true);
        player2VirusCountText.text = "Virus " + player2.virusCount.ToString();
    }

    void StatePanelUpdateText()
    {
        UpdatePlayerPanel(player1,
                          attackPowerText1, maxHPText1, regenHPText1,
                          swordIconText1, swordSkillIconText1, slashRadiusText1,
                          shooterIconText1, shooterSkillIconText1, shootCDText1,
                          muscleIconText1, muscleSkillIconText1, moveSpeedText1);

        UpdatePlayerPanel(player2,
                          attackPowerText2, maxHPText2, regenHPText2,
                          swordIconText2, swordSkillIconText2, slashRadiusText2,
                          shooterIconText2, shooterSkillIconText2, shootCDText2,
                          muscleIconText2, muscleSkillIconText2, moveSpeedText2);
    }

    void UpdatePlayerPanel(Player player,
                           TextMeshProUGUI attackPowerText, TextMeshProUGUI maxHPText, TextMeshProUGUI regenHPText,
                           TextMeshProUGUI swordIconText, TextMeshProUGUI swordSkillIconText, TextMeshProUGUI slashRadiusText,
                           TextMeshProUGUI shooterIconText, TextMeshProUGUI shooterSkillIconText, TextMeshProUGUI shootCDText,
                           TextMeshProUGUI muscleIconText, TextMeshProUGUI muscleSkillIconText, TextMeshProUGUI moveSpeedText)
    {
        attackPowerText.gameObject.SetActive(true);
        maxHPText.gameObject.SetActive(true);
        regenHPText.gameObject.SetActive(true);

        attackPowerText.text = "Attack Power: " + player.attackPower.ToString();
        maxHPText.text = "Max HP: " + player.maxHP.ToString();
        regenHPText.text = ($"{player.regenHPAmount} HP / {player.regenHPCD} s");

        if (player.isSword)
        {
            swordIconText.gameObject.SetActive(true);
            slashRadiusText.gameObject.SetActive(true);
            swordIconText.text = "Sword Icon";
            slashRadiusText.text = "Slash Radius: " + player.slashRadius.ToString("F1");

            swordSkillIconText.gameObject.SetActive(player.swordSkill);
            swordSkillIconText.text = player.swordSkill ? "Sword Skill" : "";
        }
        else
        {
            swordIconText.gameObject.SetActive(false);
            swordSkillIconText.gameObject.SetActive(false);
            slashRadiusText.gameObject.SetActive(false);
        }

        if (player.isShooter)
        {
            shooterIconText.gameObject.SetActive(true);
            shootCDText.gameObject.SetActive(true);
            shooterIconText.text = "Shooter Icon";
            shootCDText.text = "Shoot Cooldown: " + player.shootCD.ToString("F1");

            shooterSkillIconText.gameObject.SetActive(player.shooterSkill);
            shooterSkillIconText.text = player.shooterSkill ? "Shooter Skill" : "";
        }
        else
        {
            shooterIconText.gameObject.SetActive(false);
            shooterSkillIconText.gameObject.SetActive(false);
            shootCDText.gameObject.SetActive(false);
        }

        if (player.isMuscle)
        {
            muscleIconText.gameObject.SetActive(true);
            moveSpeedText.gameObject.SetActive(true);
            muscleIconText.text = "Muscle Icon";
            moveSpeedText.text = "Move Speed: " + player.moveSpeed.ToString("F1");

            muscleSkillIconText.gameObject.SetActive(player.muscleSkill);
            muscleSkillIconText.text = player.muscleSkill ? "Muscle Skill" : "";
        }
        else
        {
            muscleIconText.gameObject.SetActive(false);
            muscleSkillIconText.gameObject.SetActive(false);
            moveSpeedText.gameObject.SetActive(false);
        }
    }
}