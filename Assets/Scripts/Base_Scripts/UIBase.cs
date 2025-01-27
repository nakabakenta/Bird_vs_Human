using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBase : MonoBehaviour
{
    public void LoadScene()
    {
        Time.timeScale = 1;                          //"Time.timeScale"‚ð"1"‚É‚·‚é
        SceneManager.LoadScene(GameManager.nowScene);
    }
}
