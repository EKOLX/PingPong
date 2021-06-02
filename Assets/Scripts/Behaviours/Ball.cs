using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 6.0f;

    private Vector2 direction;
    private float radius;

    private void Start()
    {
        radius = transform.localScale.x / 2;

        Run();
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // Bounce off left and right walls
        if ((direction.x < 0 && transform.position.x < GameManager.Instance.bottomLeft.x + radius)
        || (direction.x > 0 && transform.position.x > GameManager.Instance.topRight.x - radius))
        {
            direction.x = -direction.x;
        }

        // Game over
        if (direction.y > 0 && transform.position.y > GameManager.Instance.topRight.y)
        {
            GameManager.Instance.GameOver(PaddleLocation.Top);
        }
        else if (direction.y < 0 && transform.position.y < GameManager.Instance.bottomLeft.y)
        {
            GameManager.Instance.GameOver(PaddleLocation.Bottom);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(K.paddle))
        {
            direction.y = -direction.y;
        }
    }

    public void Reset(bool withFeatures = false)
    {
        transform.position = Vector2.zero;

        if (withFeatures)
        {
            speed += 0.5f;
            transform.localScale += new Vector3(0.1f, 0.1f, 0);
        }

        Run();
    }

    private void Run()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        direction = new Vector2(x, y).normalized;
    }

}
