using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : UIBase
{
    //����
    public static int nowStage;                  //���݂̃X�e�[�W
    public static bool[] bossEnemy = new bool[5];//�{�X�̐�����
    public static string status;                 //���

    // Start is called before the first frame update
    void Start()
    {
        bossEnemy = new bool[5]                        //�{�X�̑��݉ۂ�"false(���Z�b�g)"����
        { 
            false, false, false, false, false 
        };
        bossEnemy[nowStage - 1] = true;                //���݂̃X�e�[�W�̃{�X��"true(����)"�ɂ���
        status = "Play";                               //�Q�[���̏�Ԃ�"Play"�ɂ���
    }

    // Update is called once per frame
    void Update()
    {
        if(status == "Play")
        {
            Time.timeScale = 1;
        }
        else if(status == "Pause")
        {
            Time.timeScale = 0;
        }

        if (PlayerController.remain <= 0)
        {
            GameManager.nowScene = "GameOver";
            LoadScene();
        }

        if(bossEnemy[nowStage - 1] == false)
        {
            status = "Clear";
        }
    }
}
