using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�v���C���[�Z���N�g�V�[��
    public static int playerNumber;//�v���C���[�ԍ�
    //�Q�[���V�[��
    public static bool gameStart = false;//�Q�[���X�^�[�g�t���O
    public static int score;             //�X�R�A
    public static int remain;            //�c�@
    //����
    public static bool[] stageClear = new bool[5] { false, false, false, false, false };//�X�e�[�W�N���A�t���O
    public static bool secretCharacter;//
}
