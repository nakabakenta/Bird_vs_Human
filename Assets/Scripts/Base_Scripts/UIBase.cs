using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBase : MonoBehaviour
{
    public void LoadScene()
    {
        Time.timeScale = 1;                          //"Time.timeScale"��"1"�ɂ���
        SceneManager.LoadScene(GameManager.nowScene);
    }
}
