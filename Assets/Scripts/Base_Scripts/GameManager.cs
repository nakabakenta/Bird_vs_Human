using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�v���C���[�ԍ�
    public static int selectPlayer;
    //�Q�[���V�[��
    public static string status;  //���
    public static int score;      //�X�R�A
    public static string nowScene;

    //����
    public static bool[] stageClear = new bool[5] { false, false, false, false, false };//�X�e�[�W�N���A�t���O
}

//�o�O
//�X�e�[�W�{�^��
//�{�X�̓���
//�X�e�[�W5��UI
