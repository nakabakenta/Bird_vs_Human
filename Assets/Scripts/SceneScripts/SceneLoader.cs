using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        if(Stage.stage == 1)
        {
            SceneManager.LoadScene("Stage1");
        }
        else if (Stage.stage == 2)
        {
            SceneManager.LoadScene("Stage2");
        }
        else if (Stage.stage == 3)
        {
            SceneManager.LoadScene("Stage3");
        }
        else if (Stage.stage == 4)
        {
            SceneManager.LoadScene("Stage4");
        }
        else if (Stage.stage == 5)
        {
            SceneManager.LoadScene("Stage5");
        }
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
