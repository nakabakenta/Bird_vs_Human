using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : UIBase
{
    //����
    public static int nowStage;                  //���݂̃X�e�[�W
    public static bool[] bossEnemy = new bool[5];//�{�X�̐�����
    private GameObject pauseUI;
    private bool setPauseUI;

    // Start is called before the first frame update
    void Start()
    {
        bossEnemy[nowStage - 1] = true;//���݂̃X�e�[�W�̃{�X��"true(����)"�ɂ���
        GameManager.status = "Play";   //�Q�[���̏�Ԃ�"Play"�ɂ���

        pauseUI = GameObject.Find("Pause_UI");
        setPauseUI = false;
        pauseUI.SetActive(setPauseUI);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerBase.status == "GameOver")
        {
            GameManager.nextScene = "GameOver";
            LoadScene();
        }

        if(bossEnemy[nowStage - 1] == false)
        {
            GameManager.status = "Clear";
        }

        if (GameManager.status == "Pause")
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        else if (GameManager.status == "Play")
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void SetUI()
    {
        setPauseUI = !setPauseUI;
        pauseUI.SetActive(setPauseUI);
    }
}
