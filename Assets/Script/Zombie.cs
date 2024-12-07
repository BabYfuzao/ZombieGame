using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private GameObject playerGameObject1;
    private GameObject playerGameObject2;
    private Player player1;
    private Player player2;

    public bool isBoss = false;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float instantiateInterval = 2f;
    private float instantiateTimer;

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

    public GameObject virusPrefab;
    public int virusDrop = 1;
    public float virusDropChance = 0.5f;

    private Player attackingPlayer;
    private float damageTimer;

    private Transform playerTransform;

    private SpriteRenderer spriteRenderer;

    private AudioSource audioSource;
    public AudioClip hitSourceClip;
    public AudioClip popSourceClip;

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

        moveSpeed = moveSpeedInFast;
        changeIntervalTimer = fast;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ChangeIntervalTimer();
        FindClosestPlayer();
        if (playerTransform != null)
        {
            MoveToPlayer();
            if (isBoss)
            {
                FireBullets();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameController.instance.isGameOver && !GameController.instance.isGamePause && GameController.instance.isGameInProgress)
        {
            attackingPlayer = GetAttackingPlayer(collision);
            if (attackingPlayer != null)
            {
                if (collision.gameObject.CompareTag("Player1Sword") || collision.gameObject.CompareTag("Player1Bullet") || collision.gameObject.CompareTag("Player2Sword") || collision.gameObject.CompareTag("Player2Bullet"))
                {
                    hP -= attackingPlayer.attackPower;
                    audioSource.PlayOneShot(hitSourceClip);
                    StartCoroutine(HitEffect());
                    ZombieDead(attackingPlayer);
                }
                damageTimer = 1f;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!GameController.instance.isGameOver && !GameController.instance.isGamePause && GameController.instance.isGameInProgress)
        {
            attackingPlayer = GetAttackingPlayer(collision);
            damageTimer += Time.deltaTime;
            if (attackingPlayer != null && damageTimer >= 1f && (collision.gameObject.CompareTag("Player1") && player1.isMuscle || collision.gameObject.CompareTag("Player2") && player1.isMuscle) && attackingPlayer.remainingHP > 0)
            {
                hP -= attackingPlayer.attackPower;
                audioSource.PlayOneShot(hitSourceClip);
                damageTimer = 0f;
                StartCoroutine(HitEffect());
                ZombieDead(attackingPlayer);
            }
        }
    }

    private IEnumerator HitEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    void ZombieDead(Player attackingPlayer)
    {
        if (hP <= 0)
        {
            audioSource.PlayOneShot(popSourceClip);

            DropItems(coinDrop, coinDropChance, coinPrefab);
            DropItems(virusDrop, virusDropChance, virusPrefab);

            Destroy(gameObject, popSourceClip.length);
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

    void DropItems(int dropCount, float dropChance, GameObject prefab)
    {
        for (int i = 0; i < dropCount; i++)
        {
            if (Random.value <= dropChance)
            {
                Vector2 dropPosition = (Vector2)transform.position + GetRandomOffset();
                Instantiate(prefab, dropPosition, Quaternion.identity);
            }
        }
    }

    Vector2 GetRandomOffset()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
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
        float scale = transform.localScale.y;
        float playerDirection = playerTransform.position.x - transform.position.x;

        transform.localScale = new Vector3(playerDirection < 0 ? -scale : scale, scale, 1);
        
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

    void FireBullets()
    {
        instantiateTimer -= Time.deltaTime;

        if (instantiateTimer <= 0f)
        {
            for (int i = 0; i < 8; i++)
            {
                float angle = i * 45f;
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            }

            instantiateTimer = instantiateInterval;
        }
    }

    private Player GetAttackingPlayer(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1Sword") || collision.gameObject.CompareTag("Player1Bullet") || (collision.gameObject.CompareTag("Player1") && player1.isMuscle))
        {
            return player1;
        }
        else if (collision.gameObject.CompareTag("Player2Sword") || collision.gameObject.CompareTag("Player2Bullet") || (collision.gameObject.CompareTag("Player2") && player2.isMuscle))
        {
            return player2;
        }
        return null;
    }
}