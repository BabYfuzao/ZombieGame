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

    public TextMeshProUGUI priceText;
    public bool isBuyButton;
    public int baseCost;
    public int increment;

    private AudioSource audioSource;
    public AudioClip buySoundEffect;

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

        audioSource = GetComponent<AudioSource>();

        panelUIHandleObject = GameObject.Find("PanelUIHandle");
        panelUIHandle = panelUIHandleObject.GetComponent<PanelUIHandle>();
    }

    void Update()
    {
        if (isBuyButton)
        {
            priceText.text = "$" + baseCost.ToString();
        }
    }

    //Start Panel
    //GameStart
    public void MuscleChoiceButtonForPlayer1()
    {
        player1.isMuscle = true;
        UpdateChoiceButtons(1);
        ShowGameStartButton();
    }

    public void SwordChoiceButtonForPlayer1()
    {
        player1.isSword = true;
        UpdateChoiceButtons(1);
        ShowGameStartButton();
    }

    public void ShooterChoiceButtonForPlayer1()
    {
        player1.isShooter = true;
        UpdateChoiceButtons(1);
        ShowGameStartButton();
    }

    public void MuscleChoiceButtonForPlayer2()
    {
        player2.isMuscle = true;
        UpdateChoiceButtons(2);
        ShowGameStartButton();
    }

    public void SwordChoiceButtonForPlayer2()
    {
        player2.isSword = true;
        UpdateChoiceButtons(2);
        ShowGameStartButton();
    }

    public void ShooterChoiceButtonForPlayer2()
    {
        player2.isShooter = true;
        UpdateChoiceButtons(2);
        ShowGameStartButton();
    }

    private void UpdateChoiceButtons(int playerNumber)
    {
        if (playerNumber == 1)
        {
            player1MuscleChoiceButton.SetActive(false);
            player1SwordChoiceButton.SetActive(false);
            player1ShooterChoiceButton.SetActive(false);
            panelUIHandle.muscleIconForPlayer1.SetActive(player1.isMuscle);
            panelUIHandle.swordIconForPlayer1.SetActive(player1.isSword);
            panelUIHandle.shooterIconForPlayer1.SetActive(player1.isShooter);
            GameController.instance.isPlayer1Choose = true;
            GameController.instance.FirstChoice(player1);
        }
        else if (playerNumber == 2)
        {
            player2MuscleChoiceButton.SetActive(false);
            player2SwordChoiceButton.SetActive(false);
            player2ShooterChoiceButton.SetActive(false);
            panelUIHandle.muscleIconForPlayer2.SetActive(player2.isMuscle);
            panelUIHandle.swordIconForPlayer2.SetActive(player2.isSword);
            panelUIHandle.shooterIconForPlayer2.SetActive(player2.isShooter);
            GameController.instance.isPlayer2Choose = true;
            GameController.instance.FirstChoice(player2);
        }
    }

    void ShowGameStartButton()
    {
        if (GameController.instance.isPlayer1Choose && GameController.instance.isPlayer2Choose)
        {
            gameStartButton.SetActive(true);
        }
    }

    public void AutoAttackControlForPlayer1()
    {
        player1.autoAttack = !player1.autoAttack;
    }

    public void AutoAttackControlForPlayer2()
    {
        player2.autoAttack = !player2.autoAttack;
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
        if (player1.coinCount >= baseCost && player1.maxHP < 100)
        {
            player1.maxHP++;
            player1.remainingHP++;
            player1.coinCount -= baseCost;
            baseCost += increment;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer1($"Player1 HP Max +1. Current HP Max: {player1.maxHP}");
        }
        else if (player1.coinCount < baseCost)
        {
            UpdateResultTextForPlayer1($"You don't have enough coins. Current HP Max: {player1.maxHP}");
        }
        else if (player1.maxHP >= 100)
        {
            UpdateResultTextForPlayer1($"You have reached maximum HP Max. Current HP Max: {player1.maxHP}");
        }
    }

    public void ReganHPCDBuyPlayer1()
    {
        if (player1.coinCount >= baseCost && player1.regenHPCD > 0.5)
        {
            player1.regenHPCD -= 0.1f;
            player1.coinCount -= baseCost;
            baseCost += increment;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer1($"HP Regen CD -0.1s. Current HP Regen CD: {player1.regenHPCD} s");
        }
        else if (player1.coinCount < baseCost)
        {
            UpdateResultTextForPlayer1($"You don't have enough coins. Current Regen HP CD: {player1.regenHPCD} s");
        }
        else if (player1.regenHPCD <= 0.5)
        {
            UpdateResultTextForPlayer1($"Cannot reduce CD HP Regen further. Current Regen HP CD: {player1.regenHPCD} s");
        }
    }

    public void ReganHPAmountBuyPlayer1()
    {
        if (player1.coinCount >= baseCost && player1.regenHPAmount < 10)
        {
            player1.regenHPAmount++;
            player1.coinCount -= baseCost;
            baseCost += increment;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer1($"HP Regen Amount +1. Current HP Regen Amount: {player1.regenHPAmount}");
        }
        else if (player1.coinCount < baseCost)
        {
            UpdateResultTextForPlayer1($"You don't have enough coins. Current Regen HP Amount: {player1.regenHPAmount}");
        }
        else if (player1.regenHPAmount >= 10)
        {
            UpdateResultTextForPlayer1($"You have reached maximum Regen HP Amount. Current Regen HP Amount: {player1.regenHPAmount}");
        }
    }

    public void AttackPowerBuyPlayer1()
    {
        if (player1.coinCount >= baseCost && player1.attackPower < 10)
        {
            player1.attackPower++;
            player1.coinCount -= baseCost;
            baseCost += increment;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer1($"Player1 Attack Power +1. Current Attack Power: {player1.attackPower}");
        }
        else if (player1.coinCount < baseCost)
        {
            UpdateResultTextForPlayer1($"You don't have enough coins. Current Attack Power: {player1.attackPower}");
        }
        else if (player1.attackPower >= 10)
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

            audioSource.PlayOneShot(buySoundEffect);

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
        if (player1.coinCount >= baseCost && player1.isSword && player1.slashRadius < 0.5f)
        {
            player1.slashRadius += 0.01f;
            player1.coinCount -= baseCost;
            baseCost += increment;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer1($"Slash Radius +0.01. Current Slash Radius: {player1.slashRadius}");
        }
        else if (player1.coinCount < baseCost)
        {
            UpdateResultTextForPlayer1($"You don't have enough coins. Current Slash Radius: {player1.slashRadius}");
        }
        else if (player1.slashRadius >= 0.5f)
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

            audioSource.PlayOneShot(buySoundEffect);

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

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer1("Shooter unlocked!");
        }
        else if (player1.virusCount < 1)
        {
            UpdateResultTextForPlayer1($"You don't have enough virus.");
        }
        else if (player1.isShooter)
        {
            UpdateResultTextForPlayer1("You already unlocked Shooter.");
        }
    }

    public void ShootCDBuyPlayer1()
    {
        if (player1.coinCount >= baseCost && player1.isShooter && player1.shootCD > 0.2f)
        {
            player1.shootCD -= 0.05f;
            player1.coinCount -= baseCost;
            baseCost += increment;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer1($"Shoot CD -0.05s. Current Shoot CD: {player1.shootCD}");
        }
        else if (player1.coinCount < baseCost)
        {
            UpdateResultTextForPlayer1($"You don't have enough coins. Current Shoot CD: {player1.shootCD}");
        }
        else if (player1.shootCD <= 0.2f)
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

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer1("Shooter skill unlocked!");
        }
        else if (player1.virusCount < 1)
        {
            UpdateResultTextForPlayer1($"You don't have enough virus.");
        }
        else if (player1.shooterSkill)
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

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer1("Muscle unlocked!");
        }
        else if (player1.virusCount < 1)
        {
            UpdateResultTextForPlayer1($"You don't have enough virus.");
        }
        else if (player1.isMuscle)
        {
            UpdateResultTextForPlayer1("You already unlocked Muscle.");
        }
    }

    public void MoveSpeedBuyPlayer1()
    {
        if (player1.coinCount >= baseCost && player1.isMuscle && player1.moveSpeed < 15f)
        {
            player1.moveSpeed += 0.5f;
            player1.coinCount -= baseCost;
            baseCost += increment;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer1($"Move speed +0.5. Current Move Speed: {player1.moveSpeed}");
        }
        else if (player1.coinCount < baseCost)
        {
            UpdateResultTextForPlayer1($"You don't have enough coins. Current Move Speed: {player1.moveSpeed}");
        }
        else if (player1.moveSpeed >= 15f)
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

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer1("Muscle skill unlocked!");
        }
        else if (player1.virusCount < 1)
        {
            UpdateResultTextForPlayer1($"You don't have enough virus.");
        }
        else if (player1.muscleSkill)
        {
            UpdateResultTextForPlayer1("You already unlocked Muscle skill.");
        }
    }

    // Player2
    // Normal
    public void MaxHPBuyPlayer2()
    {
        if (player2.coinCount >= baseCost && player2.maxHP < 100)
        {
            player2.maxHP++;
            player2.remainingHP++;
            player2.coinCount -= baseCost;
            baseCost += increment;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer2($"Player2 HP Max +1. Current HP Max: {player2.maxHP}");
        }
        else if (player2.coinCount < baseCost)
        {
            UpdateResultTextForPlayer2($"You don't have enough coins. Current HP Max: {player2.maxHP}");
        }
        else if (player2.maxHP >= 100)
        {
            UpdateResultTextForPlayer2($"You have reached maximum HP Max. Current HP Max: {player2.maxHP}");
        }
    }

    public void ReganHPCDBuyPlayer2()
    {
        if (player2.coinCount >= baseCost && player2.regenHPCD > 0.5)
        {
            player2.regenHPCD -= 0.1f;
            player2.coinCount -= baseCost;
            baseCost += increment;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer2($"HP Regen CD -0.1s. Current HP Regen CD: {player2.regenHPCD} s");
        }
        else if (player2.coinCount < baseCost)
        {
            UpdateResultTextForPlayer2($"You don't have enough coins. Current Regen HP CD: {player2.regenHPCD} s");
        }
        else if (player2.regenHPCD <= 0.5)
        {
            UpdateResultTextForPlayer2($"Cannot reduce CD HP Regen further. Current Regen HP CD: {player2.regenHPCD} s");
        }
    }

    public void ReganHPAmountBuyPlayer2()
    {
        if (player2.coinCount >= baseCost && player2.regenHPAmount < 10)
        {
            player2.regenHPAmount++;
            player2.coinCount -= baseCost;
            baseCost += increment;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer2($"HP Regen Amount +1. Current HP Regen Amount: {player2.regenHPAmount}");
        }
        else if (player2.coinCount < baseCost)
        {
            UpdateResultTextForPlayer2($"You don't have enough coins. Current Regen HP Amount: {player2.regenHPAmount}");
        }
        else if (player2.regenHPAmount >= 10)
        {
            UpdateResultTextForPlayer2($"You have reached maximum Regen HP Amount. Current HP Regen Amount: {player2.regenHPAmount}");
        }
    }

    public void AttackPowerBuyPlayer2()
    {
        if (player2.coinCount >= baseCost && player2.attackPower < 10)
        {
            player2.attackPower++;
            player2.coinCount -= baseCost;
            baseCost += increment;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer2($"Player2 Attack Power +1. Current Attack Power: {player2.attackPower}");
        }
        else if (player2.coinCount < baseCost)
        {
            UpdateResultTextForPlayer2($"You don't have enough coins. Current Attack Power: {player2.attackPower}");
        }
        else if (player2.attackPower >= 10)
        {
            UpdateResultTextForPlayer2($"You have reached maximum Attack Power. Current Attack Power: {player2.attackPower}");
        }
    }

    // Sword
    public void SwordBuyPlayer2()
    {
        if (player2.virusCount >= 1 && !player2.isSword)
        {
            player2.isSword = true;
            player2.virusCount--;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer2("Sword unlocked!");
        }
        else if (player2.virusCount < 1)
        {
            UpdateResultTextForPlayer2($"You don't have enough virus.");
        }
        else if (player2.isSword)
        {
            UpdateResultTextForPlayer2("You already unlocked Sword.");
        }
    }

    public void SlashRadiusBuyPlayer2()
    {
        if (player2.coinCount >= baseCost && player2.isSword && player2.slashRadius < 0.5f)
        {
            player2.slashRadius += 0.01f;
            player2.coinCount -= baseCost;
            baseCost += increment;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer2($"Slash Radius +0.01. Current Slash Radius: {player2.slashRadius}");
        }
        else if (player2.coinCount < baseCost)
        {
            UpdateResultTextForPlayer2($"You don't have enough coins. Current Slash Radius: {player2.slashRadius}");
        }
        else if (player2.slashRadius >= 0.5f)
        {
            UpdateResultTextForPlayer2($"You have reached maximum Slash Radius. Current Slash Radius: {player2.slashRadius}");
        }
    }

    public void SwordSkillBuyPlayer2()
    {
        if (player2.virusCount >= 1 && player2.isSword && !player2.swordSkill)
        {
            player2.swordSkill = true;
            player2.virusCount--;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer2("Sword skill unlocked!");
        }
        else if (player2.virusCount < 1)
        {
            UpdateResultTextForPlayer2($"You don't have enough virus.");
        }
        else if (player2.swordSkill)
        {
            UpdateResultTextForPlayer2("You already unlocked Sword skill.");
        }
    }

    // Shooter
    public void ShooterBuyPlayer2()
    {
        if (player2.virusCount >= 1 && !player2.isShooter)
        {
            player2.isShooter = true;
            player2.virusCount--;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer2("Shooter unlocked!");
        }
        else if (player2.virusCount < 1)
        {
            UpdateResultTextForPlayer2($"You don't have enough virus.");
        }
        else if (player2.isShooter)
        {
            UpdateResultTextForPlayer2("You already unlocked Shooter.");
        }
    }

    public void ShootCDBuyPlayer2()
    {
        if (player2.coinCount >= baseCost && player2.isShooter && player2.shootCD > 0.2f)
        {
            player2.shootCD -= 0.05f;
            player2.coinCount -= baseCost;
            baseCost += increment;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer2($"Shoot CD -0.05s. Current Shoot CD: {player2.shootCD}");
        }
        else if (player2.coinCount < baseCost)
        {
            UpdateResultTextForPlayer2($"You don't have enough coins. Current Shoot CD: {player2.shootCD}");
        }
        else if (player2.shootCD <= 0.2f)
        {
            UpdateResultTextForPlayer2($"Cannot reduce Shoot CD further. Current Shoot CD: {player2.shootCD}");
        }
    }

    public void ShooterSkillBuyPlayer2()
    {
        if (player2.virusCount >= 1 && player2.isShooter && !player2.shooterSkill)
        {
            player2.shooterSkill = true;
            player2.virusCount--;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer2("Shooter skill unlocked!");
        }
        else if (player2.virusCount < 1)
        {
            UpdateResultTextForPlayer2($"You don't have enough virus.");
        }
        else if (player2.shooterSkill)
        {
            UpdateResultTextForPlayer2("You already unlocked Shooter skill.");
        }
    }

    // Muscle
    public void MuscleBuyPlayer2()
    {
        if (player2.virusCount >= 1 && !player2.isMuscle)
        {
            player2.isMuscle = true;
            player2.virusCount--;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer2("Muscle unlocked!");
        }
        else if (player2.virusCount < 1)
        {
            UpdateResultTextForPlayer2($"You don't have enough virus.");
        }
        else if (player2.isMuscle)
        {
            UpdateResultTextForPlayer2("You already unlocked Muscle.");
        }
    }

    public void MoveSpeedBuyPlayer2()
    {
        if (player2.coinCount >= baseCost && player2.isMuscle && player2.moveSpeed < 15f)
        {
            player2.moveSpeed += 0.5f;
            player2.coinCount -= baseCost;
            baseCost += increment;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer2($"Move speed +0.5. Current Move Speed: {player2.moveSpeed}");
        }
        else if (player2.coinCount < baseCost)
        {
            UpdateResultTextForPlayer2($"You don't have enough coins. Current Move Speed: {player2.moveSpeed}");
        }
        else if (player2.moveSpeed >= 15f)
        {
            UpdateResultTextForPlayer2($"You have reached maximum Move Speed. Current Move Speed: {player2.moveSpeed}");
        }
    }

    public void MuscleSkillBuyPlayer2()
    {
        if (player2.virusCount >= 1 && player2.isMuscle && !player2.muscleSkill)
        {
            player2.muscleSkill = true;
            player2.virusCount--;

            audioSource.PlayOneShot(buySoundEffect);

            UpdateResultTextForPlayer2("Muscle skill unlocked!");
        }
        else if (player2.virusCount < 1)
        {
            UpdateResultTextForPlayer2($"You don't have enough virus.");
        }
        else if (player2.muscleSkill)
        {
            UpdateResultTextForPlayer2("You already unlocked Muscle skill.");
        }
    }
}
