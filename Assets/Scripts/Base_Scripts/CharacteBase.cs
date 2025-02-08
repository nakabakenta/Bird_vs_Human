using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteBase : MonoBehaviour
{
    //�X�e�[�^�X
    public int hp;         //�̗�
    public float moveSpeed;//�ړ����x
    //����
    public ClassMoveRange[] moveRange;
    protected Vector3 direction;      //�I�u�W�F�N�g�̕���
    protected bool isDamage;          //�_���[�W�̉�
    //���W
    protected Vector3 worldPosition, viewPortPosition;
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public AudioClip sEAction, damage, sEShot, sEDeath;                     //"AudioClip(�_���[�W)"
    protected Transform thisTransform;                            //"Transform"
    protected Rigidbody rigidBody;                                //"Rigidbody"
    protected BoxCollider boxCollider;                            //"BoxCollider"
    protected CapsuleCollider capsuleCollider;                    //"CapsuleCollider"
    protected Renderer[] thisRenderer;                            //"Renderer"
    protected Animator animator = null;                           //"Animator"
    protected RuntimeAnimatorController runtimeAnimatorController;//"RuntimeAnimatorController"
    protected AudioSource audioSource;                            //"AudioSource"
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    protected Transform playerTransform;                          //"Transform(�v���C���[)"

    [System.Serializable]
    public class ClassMoveRange
    {
        public Vector3[] range;
    }

    //�֐�"GetComponent"
    public void GetComponent()
    {
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        thisTransform = this.gameObject.GetComponent<Transform>();
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
        audioSource = this.gameObject.GetComponent<AudioSource>();

        if(GameManager.nowScene != "GameOver")
        {
            //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
            playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        }
    }

    //�֐�"Destroy"
    public void Destroy()
    {
        Destroy(this.gameObject);//���̃I�u�W�F�N�g������
    }

    public static class Characte
    {
        public enum Direction
        {
            Horizontal = 90,
            Vertical = 180,
        }
    }
}