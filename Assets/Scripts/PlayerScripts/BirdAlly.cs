using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAlly : MonoBehaviour
{
    //����
    public static int allyCount;//�����J�E���g
    private int allyNumber;     //�����ԍ�
    private bool playerFollow;  //�v���C���[�ւ̒ǔ��̉�
    //�r���[�|�C���g���W.X
    private float viewPointX;
    //�I�u�W�F�N�g
    public GameObject[] ally = new GameObject[3];//���ԃI�u�W�F�N�g
    //�R���|�[�l���g
    private Transform thisTransform;  //"Transform"(���̃I�u�W�F�N�g)
    private Transform playerTransform;//"Transform"(�v���C���[)
    private BoxCollider boxCollider;  //"BoxCollider"
    //private Animator animator = null ;//"Animator"

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.gameObject.transform;                //���̃I�u�W�F�N�g��"Transform"���擾
        playerTransform = GameObject.Find("Player").transform;    //�Q�[���I�u�W�F�N�g"Player"��T����"Transform"���擾
        boxCollider = this.gameObject.GetComponent<BoxCollider>();//���̃I�u�W�F�N�g��"BoxCollider"���擾
        //animator = this.gameObject.GetComponent<Animator>();      //���̃I�u�W�F�N�g��"Animator"���擾
        playerFollow = false;                                     //�v���C���[�ւ̒ǔ���"false"�ɂ���

        Instantiate(ally[GameManager.playerNumber], this.transform.position, this.transform.rotation, thisTransform);
    }

    // Update is called once per frame
    void Update()
    {
        //�r���[�|�C���g���W���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//���X���W

        if (playerFollow == true && allyNumber == 1)
        {
            this.transform.position = new Vector3(playerTransform.position.x - 1.0f, playerTransform.position.y, playerTransform.position.z);
        }
        else if(playerFollow == true && allyNumber == 2)
        {
            this.transform.position = new Vector3(playerTransform.position.x - 2.0f, playerTransform.position.y, playerTransform.position.z);
        }

        if(PlayerController.allySacrifice == true && playerFollow == true)
        {
            Destroy(this.gameObject);
            PlayerController.allySacrifice = false;
            //animator.SetBool("Death", true);//"Animator"��"Death"(���S)��L���ɂ���
        }

        if(playerFollow == false && viewPointX < 0)
        {
            Destroy(this.gameObject);
        }
    }

    //�Փ˔���(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Player"��������
        if (collision.gameObject.tag == "Player" && allyCount < 3)
        {
            playerFollow = true;
            allyCount++;

            if(allyCount == 1)
            {
                allyNumber = 1;
            }
            else if (allyCount == 2)
            {
                allyNumber = 2;
            }
        }
    }
}