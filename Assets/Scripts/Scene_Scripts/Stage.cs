using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    //����
    public static int nowStage;                  //���݂̃X�e�[�W
    public static bool[] bossEnemy = new bool[5];//�{�X�̐�����
    public static string gameStatus;             //�Q�[���̏��

//���̃I�u�W�F�N�g�̃R���|�[�l���g
private SceneLoader sceneLoader;//"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = this.GetComponent<SceneLoader>();//����"Script(SceneLoader)"���擾����
        bossEnemy = new bool[5]                        //�{�X�̑��݉ۂ�"false(���Z�b�g)"����
        { 
            false, false, false, false, false 
        };
        bossEnemy[nowStage - 1] = true;                //���݂̃X�e�[�W�̃{�X��"true(����)"�ɂ���
        gameStatus = "Play";                           //�Q�[���̏�Ԃ�"Play"�ɂ���
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStatus == "Play")
        {
            Time.timeScale = 1;
        }
        else if(gameStatus == "Pause")
        {
            Time.timeScale = 0;
        }

        if(PlayerController.remain <= 0 && PlayerController.hp <= 0)
        {
            sceneLoader.GameOver();
        }

        if(bossEnemy[nowStage - 1] == false)
        {
            gameStatus = "Clear";
        }
    }
}
