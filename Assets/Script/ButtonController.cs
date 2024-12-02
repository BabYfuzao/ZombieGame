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

    public TextMeshProUGUI resultText;

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
        
    }

    public void StartLevel()
    {
        GameController.instance.isGameInProgress = true;
        Time.timeScale = 1;
        GameController.instance.preparePanel.SetActive(false);

        GameController.instance.levelCountDownTimer = GameController.instance.isBossLevel ? 25f : 21f;

        GameController.instance.ShowUI();
    }

    public void PullForPlayer1()
    {
        Pull(player1);
    }

    public void PullForPlayer2()
    {
        Pull(player2);
    }

    public void Pull(Player player)
    {
        List<string> availableItems = UpdateAvailableItems(player);

        if (availableItems.Count == 0)
        {
            resultText.text = "No items available to pull!";
            return;
        }

        int randomIndex = Random.Range(0, availableItems.Count);
        string selectedItem = availableItems[randomIndex];

        //Result display
        resultText.text = $"You pulled: {selectedItem}";

        //Data get
        ApplyItemEffect(player, selectedItem);

    }

    private List<string> UpdateAvailableItems(Player player)
    {
        List<string> availableItems = new List<string>();

        if (player.attackPower < 10)
            availableItems.Add("Attack Power +1");

        if (player.slashCD > 0.1f)
            availableItems.Add("Slash Cooldown -0.1");

        if (player.shootCD > 0.1f)
            availableItems.Add("Shoot Cooldown -0.1");

        if (player.maxHP < 20)
            availableItems.Add("Max HP +1");

        if (player.regenHPAmount < 5)
            availableItems.Add("HP Regeneration +1");

        if (player.regenHPCD < 10f)
            availableItems.Add("HP RegenCD - 0.5");

        if (player.scale < 8f)
            availableItems.Add("Scale +0.1");

        if (player.moveSpeed < 20)
            availableItems.Add("Move Speed +1");

        if (player.slashRadius < 6)
            availableItems.Add("Slash Radius +0.5");

        if (player.shootRadius < 12)
            availableItems.Add("Shoot Radius +1");

        return availableItems;
    }

    private void ApplyItemEffect(Player player, string item)
    {
        switch (item)
        {
            case "Attack Power +1":
                player.attackPower += 1;
                break;
            case "Slash Cooldown -0.05":
                player.slashCD -= 0.05f;
                break;
            case "Shoot Cooldown -0.05":
                player.shootCD -= 0.05f;
                break;
            case "Max HP +1":
                player.maxHP += 1;
                player.remainingHP += 1;
                break;
            case "HP Regeneration +1":
                player.regenHPAmount += 1;
                break;
            case "HP RegenCD - 0.5":
                player.regenHPCD -= 0.5f;
                break;
            case "Scale +0.1":
                player.scale += 0.1f;
                break;
            case "Move Speed +0.1":
                player.moveSpeed += 0.1f;
                break;
            case "Slash Radius +0.1":
                player.slashRadius += 0.1f;
                break;
            case "Shoot Radius +0.5":
                player.shootRadius += 0.5f;
                break;
            default:
                break;
        }
    }
}
