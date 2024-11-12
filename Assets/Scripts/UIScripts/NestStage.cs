using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestStage : MonoBehaviour
{
    private SceneLoader sceneLoader;//

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = GetComponent<SceneLoader>();//Script"SceneLoader"‚ðŽæ“¾‚·‚é
    }


    public void NextStage()
    {
        if (Stage.stage == 1)
        {
            Stage.stage = 2;
        }
        else if (Stage.stage == 2)
        {
            Stage.stage = 3;
        }
        else if (Stage.stage == 3)
        {
            Stage.stage = 4;
        }
        else if (Stage.stage == 4)
        {
            Stage.stage = 5;
        }
        else if (Stage.stage == 5)
        {
            
        }

        sceneLoader.StageScene();
    }
}
