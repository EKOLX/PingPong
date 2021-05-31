using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private bool isGamePlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(K.ball))
        {
            if (isGamePlayer)
            {
                print("Player 2 won!");
            }
            else
            {
                print("Player 1 won!");
            }
        }
    }

}
