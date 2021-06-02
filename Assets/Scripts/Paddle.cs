using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float speed;

    private float direction;
    private float sizeX;

    private void Start()
    {
        sizeX = transform.localScale.x;
    }

    private void Update()
    {
        direction = Input.GetAxisRaw(K.horizontal);

        // Walls
        if ((direction < 0 && transform.position.x - sizeX / 2 < GameManager.Instance.bottomLeft.x)
        || (direction > 0 && transform.position.x + sizeX / 2 > GameManager.Instance.topRight.x))
        {
            direction = 0;
        }

        if (direction != 0)
        {
            transform.Translate(direction * speed * Time.deltaTime * Vector2.right);
        }
    }

    public void SetTo(PaddleLocation location)
    {
        Vector2 position = Vector2.zero;

        switch (location)
        {
            case PaddleLocation.Top:
                position = new Vector2(0, GameManager.Instance.topRight.y);
                position -= Vector2.up * transform.localScale.y / 2;
                transform.name = "PaddleTop";
                break;
            case PaddleLocation.Bottom:
                position = new Vector2(0, GameManager.Instance.bottomLeft.y);
                position -= Vector2.down * transform.localScale.y / 2;
                transform.name = "PaddleBottom";
                break;
        }

        transform.position = position;
    }

}
