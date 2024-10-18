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
    //�r���[�|�[�g���W�ϐ�
    private float viewX;//�r���[�|�[�gX���W
    private float viewY;//�r���[�|�[�gY���W
    //�ړ��ϐ�
    private bool forward; //�O�ړ�
    private bool backward;//��ړ�
    private bool up;      //��ړ�
    private bool down;    //���ړ�

    public GameObject bullet;//

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coolTime += Time.deltaTime;//�N�[���^�C����Time.deltaTime�𑫂�

        //�ړ�����
        //�O�ړ�
        if (Input.GetKey(KeyCode.D) && forward == true)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;
        }
        //��ړ�
        if (Input.GetKey(KeyCode.A) && backward == true)
        {
            this.transform.position -= speed * transform.forward * Time.deltaTime;
        }
        //��ړ�
        if (Input.GetKey(KeyCode.W) && up == true)
        {
            this.transform.position += speed * transform.up * Time.deltaTime;
        }
        //���ړ�
        if (Input.GetKey(KeyCode.S) && down == true)
        {
            this.transform.position -= speed * transform.up * Time.deltaTime;
        }
        
        //�ړ���̃r���[�|�[�g���W�l���擾
        viewX = Camera.main.WorldToViewportPoint(this.transform.position).x;
        viewY = Camera.main.WorldToViewportPoint(this.transform.position).y;

        //�ړ��\�ȉ�ʔ͈͎w��
        //-X���W
        if (viewX >= 0)
        {
            backward = true;
        }
        else
        {
            backward = false;
        }
        //+X���W
        if (viewX <= 1)
        {
            forward = true;
        }
        else
        {
            forward = false;
        }
        //-Y���W
        if (viewY >= 0)
        {
            down = true;
        }
        else
        {
            down = false;
        }
        //+Y���W
        if (viewY <= 1)
        {
            up = true;
        }
        else
        {
            up = false;
        }

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
