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

        GameController.instance.levelCountDownTimer = GameController.instance.isBossLevel ? 61f : 121f;

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
            return; // 如果沒有可抽取的物品，則返回
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

        // 根據當前數值更新可抽取的物品列表
        if (player.attackPower < 100) // 假設攻擊力上限為100
            availableItems.Add("Attack Power +5");

        if (player.slashCD > 1) // 假設攻擊冷卻時間下限為1
            availableItems.Add("Slash Cooldown -1");

        if (player.shootCD > 1) // 假設攻擊冷卻時間下限為1
            availableItems.Add("Shoot Cooldown -1");

        if (player.maxHP < 200) // 假設最大生命值上限為200
            availableItems.Add("Max HP +20");

        if (player.remainingHP < player.maxHP) // 檢查當前生命值是否低於最大生命值
            availableItems.Add("Remaining HP +15");

        if (player.regenHPAmount < 20) // 假設生命值再生量上限為20
            availableItems.Add("HP Regeneration +5");

        if (player.scale < 5.0f) // 假設比例上限為5.0
            availableItems.Add("Scale +0.1");

        if (player.moveSpeed < 10) // 假設移動速度上限為10
            availableItems.Add("Move Speed +2");

        if (player.slashRadius < 5) // 假設劍的範圍上限為5
            availableItems.Add("Slash Radius +1");

        if (player.shootRadius < 5) // 假設射擊範圍上限為5
            availableItems.Add("Shoot Radius +1");

        return availableItems;
    }

    private void ApplyItemEffect(Player player, string item)
    {
        switch (item)
        {
            case "Attack Power +5":
                player.attackPower += 5;
                break;
            case "Slash Cooldown -1":
                player.slashCD = Mathf.Max(0.1f, player.slashCD - 1f);
                break;
            case "Shoot Cooldown -1":
                player.shootCD = Mathf.Max(0.1f, player.shootCD - 1f);
                break;
            case "Max HP +20":
                player.maxHP += 20;
                player.remainingHP += 20;
                break;
            case "HP Regeneration +5":
                player.regenHPAmount += 5;
                break;
            case "Scale +0.1":
                player.scale += 0.1f;
                break;
            case "Move Speed +2":
                player.moveSpeed += 2;
                break;
            case "Slash Radius +1":
                player.slashRadius += 1;
                break;
            case "Shoot Radius +1":
                player.shootRadius += 1;
                break;
            default:
                break;
        }
    }
}
