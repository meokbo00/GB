using System.Collections;
using UnityEngine;

public class GojungMoving : MonoBehaviour
{
    public float speed = 1.5f; 
    public float distance = 1.7f; 
    private bool movingLeft = true;

    void Start()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        Vector3 startPosition = transform.position;
        Vector3 leftPosition = startPosition + Vector3.left * distance;
        Vector3 rightPosition = startPosition + Vector3.right * distance;

        while (true)
        {
            if (movingLeft)
            {
                yield return StartCoroutine(MoveToPosition(leftPosition));
                movingLeft = false;
            }
            else
            {
                yield return StartCoroutine(MoveToPosition(rightPosition));
                movingLeft = true;
            }
        }
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }
}