using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public bool Player1Camera;
    public bool Player2Camera;

    private GameObject playerGameObject1;
    private GameObject playerGameObject2;
    private Player player1;
    private Player player2;

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
    }

    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (Player1Camera && player1 != null)
        {
            Vector3 playerPosition = player1.transform.position;
            playerPosition.z = transform.position.z;
            transform.position = playerPosition;
        }
        else if (Player2Camera && player2 != null)
        {
            Vector3 playerPosition = player2.transform.position;
            playerPosition.z = transform.position.z;
            transform.position = playerPosition;
        }
    }
}
