using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSelect : MonoBehaviour
{
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject[] player = new GameObject[3];//"GameObject(�v���C���[)"

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // �J�[�\�����{�^���ɏd�Ȃ����Ƃ��Ɏ��s�����C�x���g
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        Debug.Log("�{�^���ɃJ�[�\�����d�Ȃ�܂���");
    }

    //�v���C���[�Z���N�g�ꗗ
    //�X�Y��
    public void Sparrow()
    {
        GameManager.playerNumber = PlayerList.Player.number[0];//"GameManager"��"playerNumber"��"�X�Y��(0)"�ɂ���
        
    }
    //�J���X
    public void Crow()
    {
        GameManager.playerNumber = PlayerList.Player.number[1];//"GameManager"��"playerNumber"��"�J���X(1)"�ɂ���
        
    }
    //�R�K��
    public void Chickadee()
    {
        GameManager.playerNumber = PlayerList.Player.number[2];//"GameManager"��"playerNumber"��"�R�K��(2)"�ɂ���
        
    }
    //�y���M��
    public void Penguin()
    {
        GameManager.playerNumber = PlayerList.Player.number[3];//"GameManager"��"playerNumber"��"�y���M��(3)"�ɂ���
        
    }
}
