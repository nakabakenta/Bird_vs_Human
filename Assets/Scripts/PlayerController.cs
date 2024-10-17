using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //���@�X�e�[�^�X
    public int hp;     //�̗�
    public int power;  //�U����
    public float speed;//�ړ����x

    private float coolTime = 0.2f;//�N�[���^�C��
    private float spanTime = 0.2f;//�U�����o��܂ł̊Ԋu

    private Vector3 nowPosition; //���݂̈ʒu
    private Vector3 nextPosition;//�ړ���̈ʒu
    private float viewX;//�r���[�|�[�gX���W
    private float viewY;//�r���[�|�[�gY���W

    public GameObject bullet;//

    // Start is called before the first frame update
    void Start()
    {
        nowPosition = new Vector3(0, 0, 0);
        nextPosition = nowPosition;
    }

    // Update is called once per frame
    void Update()
    {
        coolTime += Time.deltaTime;//�N�[���^�C����Time.deltaTime�𑫂�

        //��ړ�
        if (Input.GetKey(KeyCode.W))
        {
            nextPosition.y = nowPosition.y + speed * Time.deltaTime;
        }
        //���ړ�
        if (Input.GetKey(KeyCode.S))
        {
            nextPosition.y = nowPosition.y - speed * Time.deltaTime;
        }
        //���ړ�
        if (Input.GetKey(KeyCode.D))
        {
            nextPosition.z = nowPosition.z + speed * Time.deltaTime;
        }
        //�E�ړ�
        if (Input.GetKey(KeyCode.A))
        {
            nextPosition.z = nowPosition.z - speed * Time.deltaTime;
        }
        //�ړ���̃r���[�|�[�g���W�l���擾
        viewX = Camera.main.WorldToViewportPoint(nextPosition).x;
        viewY = Camera.main.WorldToViewportPoint(nextPosition).y;
        //�����ړ���̃r���[�|�[�gX���W��0����1�͈̔͂Ȃ��
        if (0 <= viewX && viewX <= 1)
        {
            //�ړ�����
            transform.position = nextPosition;
            //nowPosition��nextPosition��������(����Update�Ŏg��)
            nowPosition = nextPosition;
        }
        //�����ړ���̃r���[�|�[�gX���W��0����1�͈̔͂Ȃ��
        //if (0 <= viewY && viewY <= 1)
        //{
        //    //�ړ�����
        //    transform.position = nextPosition;
        //    //nowPosition��nextPosition��������(����Update�Ŏg��)
        //    nowPosition = nextPosition;
        //}

        //��X�g���̂ł����Ă���
        //if (Input.GetKeyDown(KeyCode.E))
        //{

        //}

        //�U������
        if (Input.GetMouseButton(0) && coolTime > spanTime)
        {
            Instantiate(bullet, this.transform.position, Quaternion.identity);
            coolTime = 0.0f;
        }
    }
}
