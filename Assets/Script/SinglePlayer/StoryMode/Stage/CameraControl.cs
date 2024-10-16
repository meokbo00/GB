using UnityEngine;

public class CameraControl : MonoBehaviour 
{
    GameObject player;
    GameObject mainPlayer;
    public RectTransform stage1RectTransform;
    public Camera mainCamera;

    void Start()
    {
        this.player = GameObject.Find("Player");
        this.mainPlayer = GameObject.Find("Main Player");
    }

    void Update()
    {
        Vector3 targetPos;
        
        if (mainPlayer != null)
        {
            targetPos = mainPlayer.transform.position;
            transform.position = new Vector3(targetPos.x, targetPos.y + 1.1f, transform.position.z);
        }
        else if (player != null)
        {
            targetPos = player.transform.position;
            transform.position = new Vector3(targetPos.x, targetPos.y + 5, transform.position.z);
        }
        else
        {
            return;
        }

    }
}