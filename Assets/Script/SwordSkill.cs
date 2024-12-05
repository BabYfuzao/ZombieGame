using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill : MonoBehaviour
{
    private Player targetPlayer;

    public float radius = 1f;
    public float rotationSpeed = 100f;

    private float angle;

    public void Initialize(Player player)
    {
        targetPlayer = player;
    }

    void Update()
    {
        if (targetPlayer != null)
        {
            FollowPlayer(targetPlayer.transform);
        }
    }

    void FollowPlayer(Transform playerTransform)
    {
        angle += rotationSpeed * Time.deltaTime;

        Vector2 offset = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;
        transform.position = (Vector2)playerTransform.position + offset;

        Vector2 direction = (playerTransform.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);
    }
}