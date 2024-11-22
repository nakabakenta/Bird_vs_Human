using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestStage : MonoBehaviour
{
    //このオブジェクトのコンポーネント
    private SceneLoader sceneLoader;//"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = this.GetComponent<SceneLoader>();//この"Script(SceneLoader)"を取得する
    }

    public void NextStage()
    {
        if (Stage.nowStage == 1)
        {
            Stage.nowStage = 2;
        }
        else if (Stage.nowStage == 2)
        {
            Stage.nowStage = 3;
        }
        else if (Stage.nowStage == 3)
        {
            Stage.nowStage = 4;
        }
        else if (Stage.nowStage == 4)
        {
            Stage.nowStage = 5;
        }
        else if (Stage.nowStage == 5)
        {
            
        }

        sceneLoader.StageScene();
    }
}
