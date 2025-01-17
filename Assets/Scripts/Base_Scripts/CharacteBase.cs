using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteBase : MonoBehaviour
{
    //�X�e�[�^�X
    public int hp;       //�̗�
    public int attack;   //�U����
    public float speed;  //�ړ����x
    public string status;//���
    //����
    public bool isAction = false;      //�s���̉�
    public float animationTimer = 0.0f;//�A�j���[�V�����^�C�}�[
    public bool isAnimation = false;   //�A�j���[�V�����̉�
    public bool isDamage;              //�_���[�W�̉�
    //���W
    public Vector3 mousePosition, worldPosition, viewPortPosition;
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public Transform thisTransform;                            //"Transform"
    public Rigidbody rigidBody;                                //"Rigidbody"
    public BoxCollider boxCollider;                            //"BoxCollider"
    public CapsuleCollider capsuleCollider;                    //"CapsuleCollider"
    public AudioClip damage;                                   //"AudioClip(�_���[�W)"
    public Animator animator = null;                           //"Animator"
    public RuntimeAnimatorController runtimeAnimatorController;//"RuntimeAnimatorController"
    public AudioSource audioSource;                            //"AudioSource"
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public Transform playerTransform;                         //"Transform(�v���C���[)"

    //�֐�"GetComponent"
    public virtual void GetComponent()
    {
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        thisTransform = this.GetComponent<Transform>();
        rigidBody = this.gameObject.GetComponent<Rigidbody>();
        boxCollider = this.gameObject.GetComponent<BoxCollider>();
        runtimeAnimatorController = animator.runtimeAnimatorController;
        audioSource = this.GetComponent<AudioSource>();
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        playerTransform = GameObject.Find("Player").transform;
    }

    //�֐�"Damage"
    public virtual void Damage()
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
