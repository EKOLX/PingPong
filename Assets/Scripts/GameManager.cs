using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [HideInInspector] public Vector2 bottomLeft;
    [HideInInspector] public Vector2 topRight;

    [SerializeField] private Ball ballPrefab;
    [SerializeField] private Paddle paddlePrefab;

    private Ball ball;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        InstantiateObjects();
    }

    public void GameOver(PaddleLocation side)
    {
        switch (side)
        {
            case PaddleLocation.Top:
                print("Bottom player won!");
                break;
            case PaddleLocation.Bottom:
                print("Top player won!");
                break;
        }

        ball.Reset();
    }

    private void InstantiateObjects()
    {
        ball = Instantiate(ballPrefab);
        ball.name = "Ball";

        Paddle paddle1 = Instantiate(paddlePrefab);
        Paddle paddle2 = Instantiate(paddlePrefab);

        paddle1.SetTo(PaddleLocation.Top);
        paddle2.SetTo(PaddleLocation.Bottom);
    }

}
