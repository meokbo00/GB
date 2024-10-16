using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearBall : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 lastVelocity;

    void Start()
    {
        float x = Random.Range(1, 5);
        float y = Random.Range(1, 5);
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.velocity = new Vector2(x, y) * 5;
    }

    void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        Vector2 reflectDirection = Vector2.Reflect(lastVelocity.normalized, normal);

        rb.velocity = reflectDirection * 5;
    }

    void OnMouseDown()
    {
        StageGameManager gameManager = FindObjectOfType<StageGameManager>();
        if (gameManager.StageClearID <= 6)
        {
            SceneManager.LoadScene("Stage");
        }
        else if (gameManager.StageClearID > 6 && gameManager.StageClearID <= 65)
        {
            SceneManager.LoadScene("Main Stage");
        }
        else if (gameManager.StageClearID == 66 && !gameManager.isending)
        {
            SceneManager.LoadScene("Final");
        }
        else if (gameManager.StageClearID == 66 && gameManager.isending)
        {
            SceneManager.LoadScene("Main Stage");
        }
    }
}