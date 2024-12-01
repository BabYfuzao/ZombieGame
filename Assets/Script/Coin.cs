using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float coinDisappearTime = 15f;

    void Start()
    {
        Invoke("DestroyCoin", coinDisappearTime);
    }

    void Update()
    {

    }

    void DestroyCoin()
    {
        Destroy(gameObject);
    }
}
