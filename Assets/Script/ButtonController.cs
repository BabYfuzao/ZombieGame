using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonController : MonoBehaviour
{
    private GameObject playerGameObject1;
    private GameObject playerGameObject2;
    private Player player1;
    private Player player2;

    private GameObject panelUIHandleObject;
    private PanelUIHandle panelUIHandle;

    public GameObject player1MuscleChoiceButton;
    public GameObject player1SwordChoiceButton;
    public GameObject player1ShooterChoiceButton;
    public GameObject player2MuscleChoiceButton;
    public GameObject player2SwordChoiceButton;
    public GameObject player2ShooterChoiceButton;

    public GameObject gameStartButton;

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

        panelUIHandleObject = GameObject.Find("PanelUIHandle");
        panelUIHandle = panelUIHandleObject.GetComponent<PanelUIHandle>();
    }

    void Update()
    {
        
    }

    //Start Panel
    //GameStart
    public void MuscleChoiceButtonForPlayer1()
    {
        player1.isMuscle = true;

        player1MuscleChoiceButton.SetActive(false);
        player1SwordChoiceButton.SetActive(false);
        player1ShooterChoiceButton.SetActive(false);
        panelUIHandle.muscleIconForPlayer1.SetActive(true);

        GameController.instance.isPlayer1Choose = true;

        GameController.instance.FirstChoice(player1);

        ShowGameStartButton();
    }

    public void SwordChoiceButtonForPlayer1()
    {
        player1.isSword = true;

        player1MuscleChoiceButton.SetActive(false);
        player1SwordChoiceButton.SetActive(false);
        player1ShooterChoiceButton.SetActive(false);
        panelUIHandle.swordIconForPlayer1.SetActive(true);

        GameController.instance.isPlayer1Choose = true;

        GameController.instance.FirstChoice(player1);

        ShowGameStartButton();
    }

    public void ShooterChoiceButtonForPlayer1()
    {
        player1.isShooter = true;

        player1MuscleChoiceButton.SetActive(false);
        player1SwordChoiceButton.SetActive(false);
        player1ShooterChoiceButton.SetActive(false);
        panelUIHandle.shooterIconForPlayer1.SetActive(true);

        GameController.instance.isPlayer1Choose = true;

        GameController.instance.FirstChoice(player1);

        ShowGameStartButton();
    }

    public void MuscleChoiceButtonForPlayer2()
    {
        player2.isMuscle = true;

        player2MuscleChoiceButton.SetActive(false);
        player2SwordChoiceButton.SetActive(false);
        player2ShooterChoiceButton.SetActive(false);
        panelUIHandle.muscleIconForPlayer2.SetActive(true);

        GameController.instance.isPlayer2Choose = true;

        GameController.instance.FirstChoice(player2);

        ShowGameStartButton();
    }

    public void SwordChoiceButtonForPlayer2()
    {
        player2.isSword = true;

        player2MuscleChoiceButton.SetActive(false);
        player2SwordChoiceButton.SetActive(false);
        player2ShooterChoiceButton.SetActive(false);
        panelUIHandle.swordIconForPlayer2.SetActive(true);

        GameController.instance.isPlayer2Choose = true;

        GameController.instance.FirstChoice(player2);

        ShowGameStartButton();
    }

    public void ShooterChoiceButtonForPlayer2()
    {
        player2.isShooter = true;

        player2MuscleChoiceButton.SetActive(false);
        player2SwordChoiceButton.SetActive(false);
        player2ShooterChoiceButton.SetActive(false);
        panelUIHandle.shooterIconForPlayer2.SetActive(true);

        GameController.instance.isPlayer2Choose = true;

        GameController.instance.FirstChoice(player2);

        ShowGameStartButton();
    }

    void ShowGameStartButton()
    {
        if (GameController.instance.isPlayer1Choose && GameController.instance.isPlayer2Choose)
        {
            gameStartButton.SetActive(true);
        }
    }

    public void GameStartButton()
    {
        GameController.instance.GameStart();
    }

    //Game in progress
    public void StatePanel()
    {
        GameController.instance.StatePanel();
    }

    public void NextLevel()
    {
        GameController.instance.NextLevel();
    }

    private void UpdateResultTextForPlayer1(string message)
    {
        panelUIHandle.resultForPlayer1Text.text = message;
    }

    private void UpdateResultTextForPlayer2(string message)
    {
        panelUIHandle.resultForPlayer2Text.text = message;
    }

    // Player1
    // Normal
    public void MaxHPBuyPlayer1()
    {
        if (player1.coinCount >= 10 && player1.maxHP < 50)
        {
            player1.maxHP++;
            player1.remainingHP++;
            player1.coinCount -= 10;

            UpdateResultTextForPlayer1($"Player1 HP Max +1. Current HP Max: {player1.maxHP}");
        }
        else if (player1.coinCount < 10)
        {
            UpdateResultTextForPlayer1($"You don't have enough coins. Current HP Max: {player1.maxHP}");
        }
        else if (player1.maxHP >= 50)
        {
            UpdateResultTextForPlayer1($"You have reached maximum HP Max. Current HP Max: {player1.maxHP}");
        }
    }

    public void ReganHPSpeedBuyPlayer1()
    {
        if (player1.coinCount >= 5*GameController.instance.level && player1.regenHPCD > 0.5)
        {
            player1.regenHPCD -= 0.1f;
            player1.coinCount -= 10;

            UpdateResultTextForPlayer1($"HP Regen CD -0.1s. Current HP Regen CD: {player1.regenHPCD} s");
        }
        else if (player1.coinCount < 10)
        {
            UpdateResultTextForPlayer1($"You don't have enough coins. Current Regen HP CD: {player1.regenHPCD} s");
        }
        else
        {
            UpdateResultTextForPlayer1($"Cannot reduce CD HP Regen further. Current Regen HP CD: {player1.regenHPCD} s");
        }
    }

    public void AttackPowerBuyPlayer1()
    {
        if (player1.coinCount >= 50 && player1.attackPower < 6)
        {
            player1.attackPower++;
            player1.coinCount -= 50;

            UpdateResultTextForPlayer1($"Player1 Attack Power +1. Current Attack Power: {player1.attackPower}");
        }
        else if (player1.coinCount < 50)
        {
            UpdateResultTextForPlayer1($"You don't have enough coins. Current Attack Power: {player1.attackPower}");
        }
        else if (player1.attackPower >= 6)
        {
            UpdateResultTextForPlayer1($"You have reached maximum Attack Power. Current Attack Power: {player1.attackPower}");
        }
    }

    // Sword
    public void SwordBuyPlayer1()
    {
        if (player1.virusCount >= 1 && !player1.isSword)
        {
            player1.isSword = true;
            player1.virusCount--;

            UpdateResultTextForPlayer1("Sword unlocked!");
        }
        else if (player1.virusCount < 1)
        {
            UpdateResultTextForPlayer1($"You don't have enough virus.");
        }
        else if (player1.isSword)
        {
            UpdateResultTextForPlayer1("You already unlocked Sword.");
        }
    }

    public void SlashRadiusBuyPlayer1()
    {
        if (player1.coinCount >= 10 && player1.isSword && player1.slashRadius < 8f)
        {
            player1.slashRadius += 0.1f;
            player1.coinCount -= 10;

            UpdateResultTextForPlayer1($"Slash Radius +0.1. Current Slash Radius: {player1.slashRadius}");
        }
        else if (player1.coinCount < 10)
        {
            UpdateResultTextForPlayer1($"You don't have enough coins. Current Slash Radius: {player1.slashRadius}");
        }
        else if (player1.slashRadius > 8f)
        {
            UpdateResultTextForPlayer1($"You have reached maximum Slash Radius. Current Slash Radius: {player1.slashRadius}");
        }
    }

    public void SwordSkillBuyPlayer1()
    {
        if (player1.virusCount >= 1 && player1.isSword && !player1.swordSkill)
        {
            player1.swordSkill = true;
            player1.virusCount--;

            UpdateResultTextForPlayer1("Sword skill unlocked!");
        }
        else if (player1.virusCount < 1)
        {
            UpdateResultTextForPlayer1($"You don't have enough virus.");
        }
        else if (player1.swordSkill)
        {
            UpdateResultTextForPlayer1("You already unlocked Sword skill.");
        }
    }

    // Shooter
    public void ShooterBuyPlayer1()
    {
        if (player1.virusCount >= 1 && !player1.isShooter)
        {
            player1.isShooter = true;
            player1.virusCount--;

            UpdateResultTextForPlayer1("Shooter unlocked!");
        }
        else if (player1.virusCount < 1)
        {
            UpdateResultTextForPlayer1($"You don't have enough virus.");
        }
        else
        {
            UpdateResultTextForPlayer1("You already unlocked Shooter.");
        }
    }

    public void ShootCDBuyPlayer1()
    {
        if (player1.coinCount >= 10 && player1.isShooter && player1.shootCD > 0.1f)
        {
            player1.shootCD -= 0.05f;
            player1.coinCount -= 10;

            UpdateResultTextForPlayer1($"Shoot CD -0.05s. Current Shoot CD: {player1.shootCD}");
        }
        else if (player1.coinCount < 10)
        {
            UpdateResultTextForPlayer1($"You don't have enough coins. Current Shoot CD: {player1.shootCD}");
        }
        else
        {
            UpdateResultTextForPlayer1($"Cannot reduce Shoot CD further. Current Shoot CD: {player1.shootCD}");
        }
    }

    public void ShooterSkillBuyPlayer1()
    {
        if (player1.virusCount >= 1 && player1.isShooter && !player1.shooterSkill)
        {
            player1.shooterSkill = true;
            player1.virusCount--;

            UpdateResultTextForPlayer1("Shooter skill unlocked!");
        }
        else if (player1.virusCount < 1)
        {
            UpdateResultTextForPlayer1($"You don't have enough virus.");
        }
        else
        {
            UpdateResultTextForPlayer1("You already unlocked Shooter skill.");
        }
    }

    // Muscle
    public void MuscleBuyPlayer1()
    {
        if (player1.virusCount >= 1 && !player1.isMuscle)
        {
            player1.isMuscle = true;
            player1.virusCount--;

            UpdateResultTextForPlayer1("Muscle unlocked!");
        }
        else if (player1.virusCount < 1)
        {
            UpdateResultTextForPlayer1($"You don't have enough virus.");
        }
        else
        {
            UpdateResultTextForPlayer1("You already unlocked Muscle.");
        }
    }

    public void MoveSpeedBuyPlayer1()
    {
        if (player1.coinCount >= 10 && player1.isMuscle && player1.moveSpeed < 0.5f)
        {
            player1.moveSpeed += 0.1f;
            player1.coinCount -= 10;

            UpdateResultTextForPlayer1($"Move speed +0.1. Current Move Speed: {player1.moveSpeed}");
        }
        else if (player1.coinCount < 10)
        {
            UpdateResultTextForPlayer1($"You don't have enough coins. Current Move Speed: {player1.moveSpeed}");
        }
        else
        {
            UpdateResultTextForPlayer1($"You have reached maximum Move Speed. Current Move Speed: {player1.moveSpeed}");
        }
    }

    public void MuscleSkillBuyPlayer1()
    {
        if (player1.virusCount >= 1 && player1.isMuscle && !player1.muscleSkill)
        {
            player1.muscleSkill = true;
            player1.virusCount--;

            UpdateResultTextForPlayer1("Muscle skill unlocked!");
        }
        else if (player1.virusCount < 1)
        {
            UpdateResultTextForPlayer1($"You don't have enough virus.");
        }
        else
        {
            UpdateResultTextForPlayer1("You already unlocked Muscle skill.");
        }
    }

    // Player2
    // Normal
    public void MaxHPBuyPlayer2()
    {
        if (player2.coinCount >= 1)
        {
            player2.maxHP++;
            player2.remainingHP++;
            player2.coinCount--;
        }
    }

    public void ReganHPSpeedBuyPlayer2()
    {
        // Implement logic for Player 2's HP regeneration speed purchase
    }

    public void AttackPowerBuyPlayer2()
    {
        // Implement logic for Player 2's attack power purchase
    }

    // Sword
    public void SwordBuyPlayer2()
    {
        // Implement logic for Player 2's sword purchase
    }

    public void SlashRadiusBuyPlayer2()
    {
        // Implement logic for Player 2's sword radius purchase
    }

    public void SwordSkillBuyPlayer2()
    {
        // Implement logic for Player 2's sword skill purchase
    }

    // Shooter
    public void ShooterBuyPlayer2()
    {
        // Implement logic for Player 2's shooter purchase
    }

    public void ShootCDBuyPlayer2()
    {
        // Implement logic for Player 2's shoot cooldown purchase
    }

    public void ShooterSkillBuyPlayer2()
    {
        // Implement logic for Player 2's shooter skill purchase
    }

    // Muscle
    public void MuscleBuyPlayer2()
    {
        // Implement logic for Player 2's muscle purchase
    }

    public void MoveSpeedBuyPlayer2()
    {
        // Implement logic for Player 2's move speed purchase
    }

    public void MuscleSkillBuyPlayer2()
    {
        // Implement logic for Player 2's muscle skill purchase
    }
}
