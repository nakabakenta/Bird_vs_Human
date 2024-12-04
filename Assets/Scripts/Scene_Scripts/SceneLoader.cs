using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void PlayerSelect()
    {
        SceneManager.LoadScene("PlayerSelect");
    }

    public void StageSelect()
    {
        SceneManager.LoadScene("StageSelect");
    }

    public void StageScene()
    {
        if(Stage.nowStage == 1)
        {
            SceneManager.LoadScene("Stage1");
        }
        else if (Stage.nowStage == 2)
        {
            SceneManager.LoadScene("Stage2");
        }
        else if (Stage.nowStage == 3)
        {
            SceneManager.LoadScene("Stage3");
        }
        else if (Stage.nowStage == 4)
        {
            SceneManager.LoadScene("Stage4");
        }
        else if (Stage.nowStage == 5)
        {
            SceneManager.LoadScene("Stage5");
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
