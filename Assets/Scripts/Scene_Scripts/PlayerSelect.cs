using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private SceneLoader sceneLoader;//"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = this.GetComponent<SceneLoader>();//����"Script(SceneLoader)"���擾����
        GameManager.gameStart = false;
    }
    //�v���C���[�Z���N�g�ꗗ
    //�X�Y��
    public void Sparrow()
    {
        GameManager.playerNumber = PlayerList.Player.number[0];//"GameManager"��"playerNumber"��"�X�Y��(0)"�ɂ���
        sceneLoader.StageSelect();                             //"Script(SceneLoader)"��"�֐�(StageSelect)"�����s����
    }
    //�J���X
    public void Crow()
    {
        GameManager.playerNumber = PlayerList.Player.number[1];//"GameManager"��"playerNumber"��"�J���X(1)"�ɂ���
        sceneLoader.StageSelect();                             //"Script(SceneLoader)"��"�֐�(StageSelect)"�����s����
    }
    //�R�K��
    public void Chickadee()
    {
        GameManager.playerNumber = PlayerList.Player.number[2];//"GameManager"��"playerNumber"��"�R�K��(2)"�ɂ���
        sceneLoader.StageSelect();                             //"Script(SceneLoader)"��"�֐�(StageSelect)"�����s����
    }
    //�y���M��
    public void Penguin()
    {
        GameManager.playerNumber = PlayerList.Player.number[3];//"GameManager"��"playerNumber"��"�y���M��(3)"�ɂ���
        sceneLoader.StageSelect();                             //"Script(SceneLoader)"��"�֐�(StageSelect)"�����s����
    }
}
