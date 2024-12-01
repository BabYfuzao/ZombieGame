using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatePanelTextHandle : MonoBehaviour
{
    //Attack
    public TextMeshProUGUI attackPowerText;
    public TextMeshProUGUI attackCDText;

    //HP
    public TextMeshProUGUI maxHPText;
    public TextMeshProUGUI remainingHPText;
    public TextMeshProUGUI regenHPCDText;
    public TextMeshProUGUI regenHPAmountText;

    //For Muscle
    public TextMeshProUGUI scaleText;
    public TextMeshProUGUI moveSpeedText;

    //For Sword
    public TextMeshProUGUI slashRadiusText;

    //For Shooter
    public TextMeshProUGUI shootRadiusText;

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
    }

    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        if (player1 != null || player2 != null)
        {
            // Attack
            attackPowerText.text = "Attack Power: " + player1.attackPower.ToString();
            /*attackCDText.text = "Attack Cooldown: " + player.attackCD.ToString("F1");

            // HP
            maxHPText.text = "Max HP: " + player.maxHP.ToString();
            remainingHPText.text = "Remaining HP: " + player.remainingHP.ToString();
            regenHPAmountText.text = "HP Regeneration: " + player.regenHPAmount.ToString();
            regenHPCDText.text = "HP Regen Cooldown: " + player.regenHPCD.ToString("F1");

            // For Muscle
            scaleText.text = "Player Scale: " + player.scale.ToString("F1");
            moveSpeedText.text = "Move Speed: " + player.moveSpeed.ToString("F1");

            // For Sword
            slashRadiusText.text = "Slash Radius: " + player.slashRadius.ToString("F1");

            // For Shooter
            shootRadiusText.text = "Shoot Radius: " + player.shootRadius.ToString("F1");*/
        }
    }
}
