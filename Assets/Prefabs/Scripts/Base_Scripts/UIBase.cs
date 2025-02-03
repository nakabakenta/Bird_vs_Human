using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBase : MonoBehaviour
{
    public void LoadScene()
    {
        Time.timeScale = 1;

        if (GameManager.nextScene == "Stage1")
        {
            Stage.nowStage = 1;
        }
        else if (GameManager.nextScene == "Stage2")
        {
            Stage.nowStage = 2;
        }
        else if (GameManager.nextScene == "Stage3")
        {
            Stage.nowStage = 3;
        }
        else if (GameManager.nextScene == "Stage4")
        {
            Stage.nowStage = 4;
        }
        else if (GameManager.nextScene == "Stage5")
        {
            Stage.nowStage = 5;
        }

        SceneManager.LoadScene(GameManager.nextScene);
        GameManager.nowScene = GameManager.nextScene;
    }
}
