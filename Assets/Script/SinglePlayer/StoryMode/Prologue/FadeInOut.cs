using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    public Image[] images;
    public float fadeTime = 1f;

    void Start()
    {
        StartCoroutine(SequentialFadeEffect());
    }

    IEnumerator SequentialFadeEffect()
    {
        for (int i = 0; i < images.Length; i++)
        {
            yield return StartCoroutine(FadeEffect(images[i]));
            images[i].gameObject.SetActive(false);
        }

        SceneManager.LoadScene("Start Scene");
        
    }

    IEnumerator FadeEffect(Image image)
    {
        image.gameObject.SetActive(true);
        for (float t = 0f; t <= 1f; t += Time.deltaTime / fadeTime)
        {
            Color newColor = image.color;
            newColor.a = Mathf.Lerp(0f, 1f, t);
            image.color = newColor;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        for (float t = 0f; t <= 1f; t += Time.deltaTime / fadeTime)
        {
            Color newColor = image.color;
            newColor.a = Mathf.Lerp(1f, 0f, t);
            image.color = newColor;
            yield return null;
        }
    }
}