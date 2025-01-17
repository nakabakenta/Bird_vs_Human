using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public int hp;          //�̗�
    public int attack;      //�U����
    public float speed;     //�ړ����x
    public float jump;      //�W�����v��
    public string nowStatus;//���

    //�֐�"Damage"
    void Damage()
    {
        hp -= 1;//�̗͂�"-1"����
    }

    //�֐�"Death"
    public virtual void Death()
    {
        hp = 0;//�̗͂�"0"�ɂ���
    }

    //�֐�"Destroy"
    public virtual void Destroy()
    {
        Destroy(this.gameObject);//���̃I�u�W�F�N�g������
    }
}
