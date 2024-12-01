using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sword : MonoBehaviour
{
    public float delay;

    void Start()
    {
        Invoke("SwordDestroy", delay);
    }

    void Update()
    {

    }

    void SwordDestroy()
    {
        Destroy(gameObject);
    }
}
