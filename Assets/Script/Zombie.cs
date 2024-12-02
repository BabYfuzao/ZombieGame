using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Zombie : MonoBehaviour
{
    private GameObject playerGameObject1;
    private GameObject playerGameObject2;
    private Player player1;
    private Player player2;

    public bool isBoss = false;

    public int hP;

    public float moveSpeed;
    public float moveSpeedInFast = 3f;
    public float moveSpeedInSlow = 0.5f;
    public float fast = 0.2f;
    public float slow = 1f;
    private float changeIntervalTimer;

    public GameObject coinPrefab;
    public int coinDrop = 1;
    public float coinDropChance = 0.5f;

    private Transform playerTransform;

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

        moveSpeed = moveSpeedInFast;
        changeIntervalTimer = fast;
    }

    void Update()
    {
        ChangeIntervalTimer();
        FindClosestPlayer();
        if (playerTransform != null)
        {
            MoveToPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player attackingPlayer = null;

        if (collision.gameObject.CompareTag("Player1Sword") || collision.gameObject.CompareTag("Player1Bullet") || (collision.gameObject.CompareTag("Player1") && player1.isMuscle))
        {
            attackingPlayer = player1;
        }
        else if (collision.gameObject.CompareTag("Player2Sword") || collision.gameObject.CompareTag("Player2Bullet") || (collision.gameObject.CompareTag("Player2") && player2.isMuscle))
        {
            attackingPlayer = player2;
        }

        if (attackingPlayer != null)
        {
            hP -= attackingPlayer.attackPower;
            ZombieDead(attackingPlayer);
        }
    }

    void ZombieDead(Player attackingPlayer)
    {
        if (hP <= 0)
        {
            for (int i = 0; i < coinDrop; i++)
            {
                float dropChance = Random.value;
                if (dropChance <= coinDropChance)
                {
                    Vector2 randomOffset = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                    Vector2 dropPosition = (Vector2)transform.position + randomOffset;
                    Instantiate(coinPrefab, transform.position, Quaternion.identity);
                }
            }
            Destroy(gameObject);
            if (attackingPlayer == player1)
            {
                player1.zombieKillCount++;
            }
            else if (attackingPlayer == player2)
            {
                player2.zombieKillCount++;
            }
        }
    }

    void ChangeIntervalTimer()
    {
        changeIntervalTimer -= Time.deltaTime;
    }

    private void FindClosestPlayer()
    {
        if (player1 != null && player1.gameObject.activeSelf && player2 != null && player2.gameObject.activeSelf)
        {
            float distanceToPlayer1 = Vector2.Distance(transform.position, player1.transform.position);
            float distanceToPlayer2 = Vector2.Distance(transform.position, player2.transform.position);
            playerTransform = distanceToPlayer1 < distanceToPlayer2 ? player1.transform : player2.transform;
        }
        else if (player1 != null && player1.gameObject.activeSelf)
        {
            playerTransform = player1.transform;
        }
        else if (player2 != null && player2.gameObject.activeSelf)
        {
            playerTransform = player2.transform;
        }
        else
        {
            playerTransform = null;
        }
    }

    void MoveToPlayer()
    {
        if (changeIntervalTimer <= 0f)
        {
            if (moveSpeed == moveSpeedInFast)
            {
                moveSpeed = moveSpeedInSlow;
                changeIntervalTimer = slow;
            }
            else
            {
                moveSpeed = moveSpeedInFast;
                changeIntervalTimer = fast;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
    }
}
