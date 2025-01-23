using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteBase : MonoBehaviour
{
    //�X�e�[�^�X
    public string type;
    public int hp;             //�̗�
    public float speed;        //�ړ����x
    //����
    public bool isDamage;//�_���[�W�̉�
    //���W
    public Vector3 worldPosition, viewPortPosition;
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public Transform thisTransform;                            //"Transform"
    public Rigidbody rigidBody;                                //"Rigidbody"
    public BoxCollider boxCollider;                            //"BoxCollider"
    public CapsuleCollider capsuleCollider;                    //"CapsuleCollider"
    public Renderer[] thisRenderer;                            //"Renderer"
    public AudioClip damage;                                   //"AudioClip(�_���[�W)"
    public Animator animator = null;                           //"Animator"
    public RuntimeAnimatorController runtimeAnimatorController;//"RuntimeAnimatorController"
    public AudioSource audioSource;                            //"AudioSource"
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public Transform playerTransform;                          //"Transform(�v���C���[)"

    //�֐�"GetComponent"
    public void GetComponent()
    {
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        thisTransform = this.GetComponent<Transform>();
        rigidBody = this.gameObject.GetComponent<Rigidbody>();
        //"BoxCollider"�����݂��Ă���ꍇ
        if (TryGetComponent<BoxCollider>(out boxCollider))
        {
            boxCollider = this.gameObject.GetComponent<BoxCollider>();
        }
        //"CapsuleCollider"�����݂��Ă���ꍇ
        if (TryGetComponent<CapsuleCollider>(out capsuleCollider))
        {
            capsuleCollider = this.gameObject.GetComponent<CapsuleCollider>();
        }
        audioSource = this.GetComponent<AudioSource>();
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        playerTransform = GameObject.Find("Player").transform;
    }

    //�֐�"Destroy"
    public void Destroy()
    {
        Destroy(this.gameObject);//���̃I�u�W�F�N�g������
    }
}