using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [HideInInspector] public Vector2 bottomLeft;
    [HideInInspector] public Vector2 topRight;

    [SerializeField] private TextMeshProUGUI labelScore;
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private Paddle paddlePrefab;
    [SerializeField] private bool resetBallWithFeatures;

    private Ball ball;
    private int playerScore, opponentScore;

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

        // Can be stored in json
        playerScore = PlayerPrefs.GetInt(K.playerScore);
        opponentScore = PlayerPrefs.GetInt(K.opponentScore);

        UpdateUI(playerScore, opponentScore);

        InstantiateObjects();
    }

    public void GameOver(PaddleLocation side)
    {
        switch (side)
        {
            case PaddleLocation.Top:
                playerScore += 1;
                break;
            case PaddleLocation.Bottom: // Is You! Can be given an option in Game Settings.
                opponentScore += 1;
                break;
        }

        UpdateUI(playerScore, opponentScore);

        ball.Reset(resetBallWithFeatures);
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

    private void UpdateUI(int playerScore, int opponentScore)
    {
        labelScore.text = $"{playerScore} : {opponentScore}";
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(K.playerScore, playerScore);
        PlayerPrefs.SetInt(K.opponentScore, opponentScore);
    }

}
