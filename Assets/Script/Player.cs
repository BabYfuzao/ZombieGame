using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int playerId;

    public int zombieKillCount;

    public int coinCount;

    public int attackPower = 1;

    public int remainingHP;
    public int maxHP = 5;
    public TextMeshProUGUI playerHPText;
    public float regenHPTimer = 0f;
    public float regenHPCD = 3f;
    public int regenHPAmount = 1;

    public float scale = 1f;

    public float moveSpeed = 5f;

    public bool isMuscle = false;

    public bool isSword = false;
    public GameObject swordPrefab;
    private GameObject swordGameObject;
    public float slashRadius = 1f;
    public float slashCDTimer = 0f;
    public float slashCD = 2f;

    public bool isShooter = false;
    public GameObject bulletPrefab;
    public float shootRadius = 5f;
    public float shootCDTimer = 0f;
    public float shootCD = 2f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        remainingHP = maxHP;
    }

    void Update()
    {
        if (remainingHP > 0 && !GameController.instance.isGameOver && !GameController.instance.isGamePause && GameController.instance.isGameInProgress)
        {
            TextHandle();

            PlayerMove();

            RegenerateHP();

            if (isSword)
            {
                Sword();
            }

            if (isShooter)
            {
                Shoot();
            }
        }

        else if (remainingHP <= 0)
        {
            remainingHP = 0;
            TextHandle();
            StartCoroutine(DestroyPlayerAfterDelay(5f));
            this.enabled = false;
        }
    }

    private IEnumerator DestroyPlayerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameController.instance.isGameOver)
        {
            if (collision.gameObject.CompareTag("Zombie"))
            {
                remainingHP--;
            }

            else if (collision.gameObject.CompareTag("Coin"))
            {
                Destroy(collision.gameObject);
                coinCount++;
            }
        }
    }
    void TextHandle()
    {
        playerHPText.text = "HP " + remainingHP.ToString() + "/" + maxHP.ToString();
    }

    void PlayerMove()
    {
        float horizontal = Input.GetAxis($"Horizontal{playerId}");
        float vertical = Input.GetAxis($"Vertical{playerId}");
        Vector2 move = new Vector2(horizontal, vertical).normalized;
        transform.Translate(move * moveSpeed * Time.deltaTime);
    }

    void RegenerateHP()
    {
        if (remainingHP >= maxHP)
        {
            return;
        }

        regenHPTimer += Time.deltaTime;

        if (regenHPTimer >= regenHPCD)
        {
            remainingHP = Mathf.Min(remainingHP + regenHPAmount, maxHP);
            regenHPTimer = 0f;
        }
    }

    //Instantiate SwordPrefab with player around to attack zombie
    void Sword()
    {
        if (slashCDTimer < slashCD)
        {
            slashCDTimer += Time.deltaTime;
        }

        KeyCode attackKey = playerId == 1 ? KeyCode.Q : KeyCode.Keypad0;

        if (slashCDTimer >= slashCD && Input.GetKeyDown(attackKey))
        {
            if (swordGameObject == null)
            {
                swordGameObject = Instantiate(swordPrefab, transform.position, Quaternion.identity);
                swordGameObject.transform.localScale = new Vector3(slashRadius, slashRadius, 1f);
            }

            slashCDTimer = 0f;
        }

        if (swordGameObject != null)
        {
            swordGameObject.transform.position = (Vector2)transform.position;
        }
    }

    //Instantiate BulletPrefab to shoot to zombie
    void Shoot()
    {
        if (shootCDTimer < shootCD)
        {
            shootCDTimer += Time.deltaTime;
        }

        KeyCode attackKey = playerId == 1 ? KeyCode.Q : KeyCode.Keypad0;

        if (IsZombieInRange() && shootCDTimer >= shootCD && Input.GetKeyDown(attackKey))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            shootCDTimer = 0f;
        }
    }

    //Determine if zombie in shoot radius
    bool IsZombieInRange()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (GameObject zombie in zombies)
        {
            float distance = Vector2.Distance(transform.position, zombie.transform.position);
            if (distance <= shootRadius)
            {
                return true;
            }
        }
        return false;
    }
}
