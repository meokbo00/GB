using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SPFailSceneManager : MonoBehaviour
{
    void Update()
    {
        StageGameManager stageGameManager = FindObjectOfType<StageGameManager>();
        int randomnumber = Random.Range(1, 6);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject.name == "GoStage")
            {
                if (stageGameManager.StageClearID <= 5)
                {
                    SceneManager.LoadScene("Stage");
                }
                else if (stageGameManager.StageClearID >= 6)
                {
                    SceneManager.LoadScene("Main Stage");
                }
            }
            if (hit.collider != null && hit.collider.gameObject.name == "GoMenu")
            {
                SceneManager.LoadScene("Start Scene");
            }
            if (hit.collider != null && hit.collider.gameObject.name == "Retry")
            {
                SceneManager.LoadScene("Story-InGame");
            }
            if (hit.collider != null && hit.collider.gameObject.name == "Random")
            {
                switch(randomnumber)
                {
                    case 1:
                        SceneManager.LoadScene("Example Scene");
                        break;
                    case 2:
                        SceneManager.LoadScene("Design Scene");
                        break;
                    case 3:
                        SceneManager.LoadScene("1-1 Intro Scene");
                        break;
                    case 4:
                        SceneManager.LoadScene("Credit Scene");
                        break;
                    case 5:
                        SceneManager.LoadScene("ChallengeScene");
                        break;
                }
            }
        }
    }
}