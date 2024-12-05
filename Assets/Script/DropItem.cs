using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public bool virus;
    private Player closestPlayer;

    void Start()
    {
        if (!virus)
        {
            Invoke("DestroyCoin", 15f);
        }
    }

    void DestroyCoin()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        FindClosestPlayer();

        if (closestPlayer != null && IsPlayerInRange(closestPlayer))
        {
            Vector3 direction = (closestPlayer.transform.position - transform.position).normalized;
            transform.position += direction * 15f * Time.deltaTime;

        }
    }

    void FindClosestPlayer()
    {
        Player[] players = FindObjectsOfType<Player>();
        float closestDistance = Mathf.Infinity;

        foreach (Player player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player;
            }
        }
    }

    bool IsPlayerInRange(Player player)
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        return distance <= 3f;
    }
}