using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingSphere : MonoBehaviour
{
    public float speed = 20f;
    public float moveDuration = 0.1f; // 이동 시간
    private Vector2 moveDirection;
    private float moveTimer = 0f;

    private void Start()
    {
        SetRandomDirection();
    }

    private void Update()
    {
        moveTimer += Time.deltaTime;

        transform.Translate(moveDirection * speed * Time.deltaTime);

        if (moveTimer >= moveDuration)
        {
            SetRandomDirection();
            moveTimer = 0f;
        }
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene("Start Scene");
    }

    private void SetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        moveDirection = Quaternion.Euler(0, 0, randomAngle) * Vector2.right;
    }
}