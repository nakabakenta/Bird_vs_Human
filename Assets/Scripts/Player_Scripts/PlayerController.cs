using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerBase
{
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject[] player = new GameObject[3];  //"GameObject(�v���C���[)"
    public GameObject forwardBullet, downBullet;     //"GameObject(�e)"
    public GameObject[] group = new GameObject[3];   //"GameObject(�Q��)"

    public int PlayerHp
    {
        get { return hp; }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        StartPlayer();
        //�I�������v���C���[�����̃I�u�W�F�N�g�̎q�I�u�W�F�N�g�Ƃ��Đ�������
        nowPlayer = Instantiate(player[GameManager.selectPlayer], this.transform.position, Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        animator = nowPlayer.GetComponent<Animator>();
        thisRenderer = this.gameObject.GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //�̗͂�"0����"�̏ꍇ
        if (hp > 0)
        {
            UpdatePlayer();//�֐�"UpdatePlayer"�����s����
        }
    }

    public override void InputButton()
    {
        //�O���U��
        //�}�E�X��"���N���b�N���ꂽ"&&"�U���^�C�}�[[�O��]"��"�U���Ԋu[�O��]" - "���x���A�b�v���̍U���Ԋu�Z�k"�ȏ�̏ꍇ
        if (Input.GetMouseButton(0) && attackTimer[0] >= attackInterval)
        {
            //���̃I�u�W�F�N�g�̈ʒu�ɑO���e�𐶐�����
            Instantiate(forwardBullet, this.transform.position, Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z));
            //�U���^�C�}�[[�O��]������������
            attackTimer[0] = 0.0f;
        }
        //�����U��
        //�}�E�X��"�E�N���b�N���ꂽ"&&"�U���^�C�}�[[����]"��"�U���Ԋu[����]" - "���x���A�b�v���̍U���Ԋu�Z�k"�ȏ�̏ꍇ
        if (Input.GetMouseButton(1) && attackTimer[1] >= attackInterval)
        {
            //���̃I�u�W�F�N�g�̈ʒu�ɉ����e�𐶐�����
            Instantiate(downBullet, this.transform.position, Quaternion.identity);
            //�U���^�C�}�[[����]������������
            attackTimer[1] = 0.0f;
        }
        //�Q�[�W���
        //�}�E�X�z�C�[����"�N���b�N���ꂽ"&&"�Q�[�W�^�C�}�["��"�Q�[�W�~�ώ���"�ȏ�̏ꍇ
        if (Input.GetMouseButtonDown(2) && gageTimer >= gageInterval)
        {
            //�v���C���[�̏�Ԃ�"Invincible"�ɂ���
            status = "Invincible";
            //���̃I�u�W�F�N�g�̈ʒu�ɌQ��𐶐�����
            Instantiate(group[GameManager.selectPlayer], this.transform.position, Quaternion.identity);
            //�Q�[�W�^�C�}�[������������
            gageTimer = 0.0f;
        }
    }

    //�Փ˔���(OnTriggerEnter)
    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }

    //�֐�"Ally"
    void Ally()
    {
        //��������"0�Ɠ�����"�ꍇ
        if (ally == 0)
        {
            //�����𐶐�����
            playerAlly[ally] = Instantiate(player[GameManager.selectPlayer], new Vector3(this.transform.position.x - 1.0f, this.transform.position.y, this.transform.position.z), Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        }
        //��������"1�Ɠ�����"�ꍇ
        else if (ally == 1)
        {
            //�����𐶐�����
            playerAlly[ally] = Instantiate(player[GameManager.selectPlayer], new Vector3(this.transform.position.x - 2.0f, this.transform.position.y, this.transform.position.z), Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        }

        ally += 1;//��������"+1"����
    }
}

