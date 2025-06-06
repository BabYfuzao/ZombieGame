using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform targetZombie;

    public bool bomb;
    public GameObject explosionPrefab;

    void Update()
    {
        FindClosestZombie();
        if (targetZombie != null)
        {
            MoveToZombie();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            if (bomb)
            {
                GameObject explosionObject = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(explosionObject, 0.5f);
            }
            Destroy(gameObject);
        }
    }

    void FindClosestZombie()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        float closestDistance = Mathf.Infinity;
        targetZombie = null;

        foreach (GameObject zombie in zombies)
        {
            float distance = Vector2.Distance(transform.position, zombie.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetZombie = zombie.transform;
            }
        }
    }

    void MoveToZombie()
    {
        if (targetZombie != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetZombie.position, 15f * Time.deltaTime);
        }
    }
}
