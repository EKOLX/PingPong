using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private float movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement = Input.GetAxisRaw(K.horizontal);

        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
    }
}
