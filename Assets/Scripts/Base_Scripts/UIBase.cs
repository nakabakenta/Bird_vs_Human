using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBase : MonoBehaviour
{
    public void LoadScene()
    {
        if(GameManager.nowScene == "Title")
        {
            GameManager.nowScene = "PlayerSelect";
        }
        else if(GameManager.nowScene == "PlayerSelect")
        {
            GameManager.nowScene = "StageSelect";
        }
        else if (GameManager.nowScene == "StageSelect")
        {
            


        }



        if (Stage.nowStage == 1)
        {
            GameManager.nowScene = "Stage2";
            Stage.nowStage = 2;
        }
        else if (Stage.nowStage == 2)
        {
            GameManager.nowScene = "Stage3";
            Stage.nowStage = 3;
        }
        else if (Stage.nowStage == 3)
        {
            GameManager.nowScene = "Stage4";
            Stage.nowStage = 4;
        }
        else if (Stage.nowStage == 4)
        {
            GameManager.nowScene = "Stage5";
            Stage.nowStage = 5;
        }

        SceneManager.LoadScene(GameManager.nowScene);
    }
}
