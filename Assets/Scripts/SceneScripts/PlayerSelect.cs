using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    private SceneLoader sceneLoader;//SceneLoader

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = this.GetComponent<SceneLoader>();//���̃I�u�W�F�N�g��Script"SceneLoader"���擾����
    }

    //�X�Y��
    public void Sparrow()
    {
        GameManager.playerNumber = PlayerStatus.PlayerStatusList.number[0];//�v���C���[�ԍ���"�X�Y��"�̔ԍ��ɂ���
        sceneLoader.StageSelect();                                         //"sceneLoader"�̊֐�"StageSelect"���Ăяo��
    }
    //�J���X
    public void Crow()
    {
        GameManager.playerNumber = PlayerStatus.PlayerStatusList.number[1];
        sceneLoader.StageSelect();
    }
    //�R�K��
    public void Chickadee()
    {
        GameManager.playerNumber = PlayerStatus.PlayerStatusList.number[2];
        sceneLoader.StageSelect();
    }
    //�y���M��
    public void Penguin()
    {
        GameManager.playerNumber = PlayerStatus.PlayerStatusList.number[3];
        sceneLoader.StageSelect();
    }
}
