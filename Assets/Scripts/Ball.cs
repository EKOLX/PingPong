using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private float movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Run();
    }

    private void Run()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        rb.velocity = new Vector2(x * speed, y * speed);
    }

}
