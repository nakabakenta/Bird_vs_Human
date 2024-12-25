using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;//"Time.timeScale"‚ð"1"‚É‚·‚é
    }

    public void PlayerSelect()
    {
        GameManager.nowScene = "PlayerSelect";
        LoadScene();
    }

    public void StageSelect()
    {
        GameManager.nowScene = "StageSelect";
        LoadScene();
    }

    public void StageScene()
    {
        if(Stage.nowStage == 1)
        {
            GameManager.nowScene = "Stage1";
            LoadScene();
        }
        else if (Stage.nowStage == 2)
        {
            GameManager.nowScene = "Stage2";
            LoadScene();
        }
        else if (Stage.nowStage == 3)
        {
            GameManager.nowScene = "Stage3";
            LoadScene();
        }
        else if (Stage.nowStage == 4)
        {
            GameManager.nowScene = "Stage4";
            LoadScene();
        }
        else if (Stage.nowStage == 5)
        {
            GameManager.nowScene = "Stage5";
            LoadScene();
        }
    }

    public void NextStage()
    {
        if (Stage.nowStage == 1)
        {
            GameManager.nowScene = "Stage2";
            LoadScene();
        }
        else if (Stage.nowStage == 2)
        {
            GameManager.nowScene = "Stage3";
            LoadScene();
        }
        else if (Stage.nowStage == 3)
        {
            GameManager.nowScene = "Stage4";
            LoadScene();
        }
        else if (Stage.nowStage == 4)
        {
            GameManager.nowScene = "Stage5";
            LoadScene();
        }
    }

    public void GameClear()
    {
        GameManager.nowScene = "GameClear";
        LoadScene();
    }

    public void GameOver()
    {
        GameManager.nowScene = "GameOver";
        LoadScene();
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(GameManager.nowScene);
    }
}
