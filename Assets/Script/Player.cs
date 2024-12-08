using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Threading;

public class Player : MonoBehaviour
{
    public int playerId;

    public int zombieKillCount;

    public int coinCount;
    public int virusCount;

    public bool autoAttack;

    public int attackPower;
    public TextMeshProUGUI autoAttackStateDisplayText;

    public int remainingHP;
    public int maxHP;
    public TextMeshProUGUI playerHPText;
    public float regenHPTimer = 0f;
    public float regenHPCD;
    public int regenHPAmount;

    public Slider hPSlider;

    //Muscle
    public bool isMuscle = false;
    public bool muscleSkill = false;

    public float scale;
    public float moveSpeed;

    public GameObject explosionPrefab;

    //Sword
    public bool isSword = false;
    public bool swordSkill = false;

    public GameObject swordPrefab;
    private GameObject swordObject;
    public GameObject swordSkillPrefab;
    private GameObject swordSkillObject;

    public float slashRadius;
    public float slashCDTimer = 0f;
    public float slashCD;

    //Shooter
    public bool isShooter = false;
    public bool shooterSkill = false;

    public GameObject[] bulletPrefab;
    private GameObject bulletObject;

    public float shootRadius;
    public float shootCDTimer = 0f;
    public float shootCD;

    public float damageTimer;

    private GameObject zombieGameObject;
    private Zombie zombie;

    //
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    //Sound effect
    private AudioSource audioSource;
    public AudioClip coinSourceClip;
    public AudioClip slashSourceClip;
    public AudioClip shootSourceClip;
    public AudioClip explosionSourceClip;
    public AudioClip hitSourceClip;

    void Start()
    {
        if (zombieGameObject != null)
        {
            zombie = zombieGameObject.GetComponent<Zombie>();
        }

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        remainingHP = maxHP;
        hPSlider.value = remainingHP;
        autoAttack = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        autoAttackStateDisplayText.text = autoAttack ? " = on" : " = off";

        if (remainingHP > 0 && !GameController.instance.isGameOver && !GameController.instance.isGamePause && GameController.instance.isGameInProgress)
        {
            TextHandle();

            PlayerMove();

            HPBarUpdate();

            RegenerateHP();

            if (isMuscle)
            {
                MuscleSkillScaleChange();
            }

            if (isSword)
            {
                Sword();
            }

            if (isShooter)
            {
                Shoot();
            }
        }
        PlayerDead();
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
                if (isMuscle && muscleSkill)
                {
                    scale += 0.01f;
                }
                damageTimer = 1f;
            }

            else if (collision.gameObject.CompareTag("ZombieBullet"))
            {
                Destroy(collision.gameObject);
                remainingHP--;
                hPSlider.value = remainingHP;
                StartCoroutine(HitEffect());
                audioSource.PlayOneShot(hitSourceClip);
            }

            else if (collision.gameObject.CompareTag("Coin"))
            {
                Destroy(collision.gameObject);
                coinCount++;
                audioSource.PlayOneShot(coinSourceClip);
            }

            if (!isMuscle || !isSword || !isShooter || !muscleSkill || !swordSkill || !shooterSkill)
            {
                if (collision.gameObject.CompareTag("Virus"))
                {
                    Destroy(collision.gameObject);
                    virusCount++;
                    audioSource.PlayOneShot(coinSourceClip);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= 1f && remainingHP > 0)
            {
                remainingHP--;
                hPSlider.value = remainingHP;
                damageTimer = 0f;
                StartCoroutine(HitEffect());
                audioSource.PlayOneShot(hitSourceClip);
            }
        }
    }

    private IEnumerator HitEffect()
    {
        spriteRenderer.color = Color.gray;
        yield return new WaitForSeconds(0.2f);

        spriteRenderer.color = Color.white;
    }

    void TextHandle()
    {
        playerHPText.text = remainingHP.ToString() + "/" + maxHP.ToString();
    }

    void PlayerMove()
    {
        float horizontal = Input.GetAxis($"Horizontal{playerId}");
        float vertical = Input.GetAxis($"Vertical{playerId}");

        Vector2 move = new Vector2(horizontal, vertical).normalized;

        if (horizontal < -0.1f)
        {
            transform.localScale = new Vector3(-scale, scale, 1);
            anim.Play($"player{playerId}_walk");
        }
        else if (horizontal > 0.1f)
        {
            transform.localScale = new Vector3(scale, scale, 1);
            anim.Play($"player{playerId}_walk");
        }
        else if (vertical > 0.1f)
        {
            transform.localScale = new Vector3(scale, scale, 1);
            anim.Play($"player{playerId}_walk");
        }
        else if (vertical < -0.1f)
        {
            transform.localScale = new Vector3(-scale, scale, 1);
            anim.Play($"player{playerId}_walk");
        }
        else
        {
            anim.Play($"player{playerId}_idle");
        }
        transform.Translate(move * moveSpeed * Time.deltaTime);
    }

    void HPBarUpdate()
    {
        if (hPSlider != null) // ȷ�� hPSlider ��Ϊ null
        {
            if (hPSlider.maxValue != maxHP)
            {
                hPSlider.maxValue = maxHP;
                hPSlider.value = remainingHP;
            }
        }
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
            hPSlider.value = remainingHP;
            regenHPTimer = 0f;
        }
    }

    void MuscleSkillScaleChange()
    {
        if (scale > 0.5f)
        {
            GameObject explosionObject = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosionObject.transform.localScale = new Vector3(16f, 16f, 1f);
            Destroy(explosionObject, 0.5f);
            scale = 0.15f;
            audioSource.PlayOneShot(explosionSourceClip);
        }
    }

    //Instantiate SwordPrefab with player around to attack zombie
    void Sword()
    {   
        
        if (slashCDTimer < slashCD)
        {
            slashCDTimer += Time.deltaTime;
        }

        KeyCode attackKey = playerId == 1 ? KeyCode.Space : KeyCode.RightShift;

        if (swordObject == null && slashCDTimer >= slashCD && Input.GetKeyDown(attackKey) || (slashCDTimer >= slashCD && autoAttack))
        {
            swordObject = Instantiate(swordPrefab, transform.position, Quaternion.identity);
            Destroy(swordObject, 0.2f);
            audioSource.PlayOneShot(slashSourceClip);

            slashCDTimer = 0f;
        }

        if (swordObject != null)
        {
            swordObject.transform.position = transform.position;
            swordObject.transform.localScale = new Vector3(slashRadius, slashRadius, 1f);
        }

        if (swordSkillObject == null && swordSkill)
        {
            swordSkillObject = Instantiate(swordSkillPrefab, transform.position, Quaternion.identity);
            SwordSkill skill = swordSkillObject.GetComponent<SwordSkill>();
            skill.Initialize(this);
        }
    }

    //Instantiate BulletPrefab to shoot to zombie
    void Shoot()
    {
        if (shootCDTimer < shootCD)
        {
            shootCDTimer += Time.deltaTime;
        }

        KeyCode attackKey = playerId == 1 ? KeyCode.Space : KeyCode.RightShift;

        if (IsZombieInRange() && shootCDTimer >= shootCD && Input.GetKeyDown(attackKey) || (IsZombieInRange() && shootCDTimer >= shootCD && autoAttack))
        {
            if (shooterSkill)
            {
                bulletObject = bulletPrefab[1];
            }
            else
            {
                bulletObject = bulletPrefab[0];
            }
            Instantiate(bulletObject, transform.position, Quaternion.identity);
            audioSource.PlayOneShot(shootSourceClip);
            shootCDTimer = 0f;
        }
    }

    void PlayerDead()
    {
        if (remainingHP <= 0)
        {
            remainingHP = 0;
            anim.Play($"player{playerId}_dead");
            Destroy(swordSkillObject);
            TextHandle();
            StartCoroutine(DestroyPlayerAfterDelay(5f));
            if (playerId == 1)
            {
                GameController.instance.isPlayer1Dead = true;
            }
            else if (playerId == 2)
            {
                GameController.instance.isPlayer2Dead = true;
            }
            GameController.instance.GameOver();
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
