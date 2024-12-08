using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressH : MonoBehaviour
{
    public GameObject showPanel;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            showPanel.SetActive(true);
        }
        else
        {
            showPanel.SetActive(false);
        }
    }
}
