using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    //�X�e�[�^�X
    public float moveSpeed;//�e�̈ړ����x
    //���W
    protected Vector3 viewPortPosition;
    //����
    protected Vector3 direction;//�I�u�W�F�N�g�̕���
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject effect;               //"GameObject(�G�t�F�N�g)"
    public AudioClip audioClip;             //"audioClip"
    protected AudioSource audioSource;      //"AudioSource"
    private BoxCollider boxCollider;        //"BoxCollider"
    private CapsuleCollider capsuleCollider;//"CapsuleCollider"
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    protected Transform playerTransform;//"Transform(�v���C���[)"

    public void BaseStart()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();

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

        if (this.tag == "EnemyBullet")
        {
            //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
            playerTransform = GameObject.Find("Player").transform;//"Transform(�v���C���[)"
        }
    }

    public void BaseUpdate()
    {
        //���̃I�u�W�F�N�g�̃��[���h���W���r���[�|�[�g���W�ɕϊ����Ď擾����
        viewPortPosition.x = Camera.main.WorldToViewportPoint(this.transform.position).x;
        viewPortPosition.y = Camera.main.WorldToViewportPoint(this.transform.position).y;

        if (viewPortPosition.x < 0 || viewPortPosition.x > 1 || 
            viewPortPosition.y < 0)
        {
            Destroy();
        }
    }

    //�֐�"Destroy"
    virtual public void Destroy()
    {
        Destroy(this.gameObject);//���̃I�u�W�F�N�g������
    }

    //�Փ˔���(OnTriggerEnter)
    virtual public void OnTriggerEnter(Collider collision)
    {
        if (this.tag == "PlayerBullet" && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossEnemy"))
        {
            Destroy();
        }
        else if (this.tag == "EnemyBullet" && collision.gameObject.tag == "Player")
        {
            Destroy();
        }
    }
}