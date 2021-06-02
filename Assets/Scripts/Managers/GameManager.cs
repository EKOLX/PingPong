using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [HideInInspector] public Vector2 bottomLeft;
    [HideInInspector] public Vector2 topRight;
    [HideInInspector] public int playerScore, opponentScore;

    [SerializeField] private UIManager uiManager;
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private Paddle paddlePrefab;
    [SerializeField] private bool resetBallWithFeatures;

    private Ball ball;
    private int timer = 3;

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

        uiManager.onColorSet += ColorSet;

        // Can be stored in json
        playerScore = PlayerPrefs.GetInt(K.PrefKey.playerScore);
        opponentScore = PlayerPrefs.GetInt(K.PrefKey.opponentScore);

        uiManager.UpdateScore(playerScore, opponentScore);

        StartCoroutine(RunBall());

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

        uiManager.UpdateScore(playerScore, opponentScore);

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

    private IEnumerator RunBall()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1.0f);
            uiManager.UpdateTimer(--timer);
        }

        ball.Run();
    }

    private void ColorSet(Color newColor)
    {
        ball.SetColor(newColor);
    }

    private void OnDestroy()
    {
        uiManager.onColorSet -= ColorSet;

        PlayerPrefs.SetInt(K.PrefKey.playerScore, playerScore);
        PlayerPrefs.SetInt(K.PrefKey.opponentScore, opponentScore);
    }

}
