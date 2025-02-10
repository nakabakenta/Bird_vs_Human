using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    protected string[] stageName = new string[5]
       {"Stage1","Stage2","Stage3","Stage4","Stage5"};

    public void SceneChange()
    {
        Time.timeScale = 1;

        for (int i = 0; i < 5; i++)
        {
            if (GameManager.nextScene == stageName[i])
            {
                Stage.nowStage = i += 1;
            }
        }

        SceneManager.LoadScene(GameManager.nextScene);
        GameManager.nowScene = GameManager.nextScene;
    }

    public static class Scene
    {
        public enum Name
        {
            Title,
            PlayerSelect,
            StageSelect,
            GameClear,
            GameOver,
        }
    }
}
