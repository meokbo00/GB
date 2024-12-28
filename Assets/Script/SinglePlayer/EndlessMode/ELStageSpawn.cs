using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ELStageSpawn : MonoBehaviour
{
    public static StageGameManager stageGameManager;

    void Start()
    {
        stageGameManager = FindAnyObjectByType<StageGameManager>();
        
    }

    void Update()
    {
        
    }
}
