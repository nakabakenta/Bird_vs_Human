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

    public void Stage1()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void Stage2()
    {
        SceneManager.LoadScene("Stage2");
    }

    public void Stage3()
    {
        SceneManager.LoadScene("Stage3");
    }

    public void Stage4()
    {
        SceneManager.LoadScene("Stage4");
    }

    public void Stage5()
    {
        SceneManager.LoadScene("Stage5");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
