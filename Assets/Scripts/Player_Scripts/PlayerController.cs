using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerBase
{
    //�ړ��̌��E�ʒu
    private Vector2[,] limitPosition = new Vector2[5, 2]
    {
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.8f),},
    };
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject[] player = new GameObject[3];  //"GameObject(�v���C���[)"
    public GameObject forwardBullet, downBullet;     //"GameObject(�e)"
    public GameObject[] group = new GameObject[3];   //"GameObject(�Q��)"
    //�R���[�`��
    private Coroutine blinking;//

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

    //�֐�"UpdatePlayer"
    public override void UpdatePlayer()
    {
        //�Q�[���̏�Ԃ�"Play"�̏ꍇ
        if (Stage.status == "Play")
        {
            //�U���^�C�}�[�Ɍo�ߎ��Ԃ𑫂�
            attackTimer[0] += Time.deltaTime;//�U���^�C�}�[[�O��]
            attackTimer[1] += Time.deltaTime;//�U���^�C�}�[[����]
            //�}�E�X�̈ʒu���擾����
            mousePosition = Input.mousePosition;
            //�}�E�X�̈ʒu(�X�N���[�����W)���r���[�|�C���g���W�ɕϊ�����
            viewPortPosition = Camera.main.ScreenToViewportPoint(new Vector3(mousePosition.x, mousePosition.y, 9.0f));
            //�ړ��̌��E�ʒu��ݒ肷��
            viewPortPosition.x = Mathf.Clamp(viewPortPosition.x, limitPosition[Stage.nowStage - 1, 0].x, limitPosition[Stage.nowStage - 1, 1].x);
            viewPortPosition.y = Mathf.Clamp(viewPortPosition.y, limitPosition[Stage.nowStage - 1, 0].y, limitPosition[Stage.nowStage - 1, 1].y);
            //�r���[�|�C���g���W�����[���h���W�ɕϊ�����
            this.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(viewPortPosition.x, viewPortPosition.y, 9.0f));

            //�v���C���[�̏�Ԃ�"Normal"�̏ꍇ
            if (status == "Normal")
            {
                if (GameManager.selectPlayer == 0)
                {
                    attackTimeInterval[0] = 2.0f;
                    attackTimeInterval[1] = 2.0f;
                }
                else if (GameManager.selectPlayer == 1)
                {
                    attackTimeInterval[0] = 3.0f;
                    attackTimeInterval[1] = 3.0f;
                }
                else if (GameManager.selectPlayer == 2)
                {
                    attackTimeInterval[0] = 1.0f;
                    attackTimeInterval[1] = 1.0f;
                }

                gageTimer += Time.deltaTime;//�Q�[�W�^�C�}�[�Ɍo�ߎ��Ԃ𑫂�
            }
            //�v���C���[�̏�Ԃ�"Invincible"�̏ꍇ
            else if (status == "Invincible")
            {
                //���G���̍U���Ԋu��ݒ肷��
                attackTimeInterval[0] = PlayerBase.InvincibleStatus.attackSpeed;
                attackTimeInterval[1] = PlayerBase.InvincibleStatus.attackSpeed;
            }

            //�O���U��
            //�}�E�X��"���N���b�N���ꂽ"&&"�U���^�C�}�[[�O��]"��"�U���Ԋu[�O��]" - "���x���A�b�v���̍U���Ԋu�Z�k"�ȏ�̏ꍇ
            if (Input.GetMouseButton(0) && attackTimer[0] >= attackTimeInterval[0] - levelAttackInterval)
            {
                //���̃I�u�W�F�N�g�̈ʒu�ɑO���e�𐶐�����
                Instantiate(forwardBullet, this.transform.position, Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z));
                //�U���^�C�}�[[�O��]������������
                attackTimer[0] = 0.0f;
            }
            //�����U��
            //�}�E�X��"�E�N���b�N���ꂽ"&&"�U���^�C�}�[[����]"��"�U���Ԋu[����]" - "���x���A�b�v���̍U���Ԋu�Z�k"�ȏ�̏ꍇ
            if (Input.GetMouseButton(1) && attackTimer[1] >= attackTimeInterval[1] - levelAttackInterval)
            {
                //���̃I�u�W�F�N�g�̈ʒu�ɉ����e�𐶐�����
                Instantiate(downBullet, this.transform.position, Quaternion.identity);
                //�U���^�C�}�[[����]������������
                attackTimer[1] = 0.0f;
            }
            //�Q�[�W���
            //�}�E�X�z�C�[����"�N���b�N���ꂽ"&&"�Q�[�W�^�C�}�["��"�Q�[�W�~�ώ���"�ȏ�̏ꍇ
            if (Input.GetMouseButtonDown(2) && gageTimer >= gageTimeInterval)
            {
                //�v���C���[�̏�Ԃ�"Invincible"�ɂ���
                status = "Invincible";
                //���̃I�u�W�F�N�g�̈ʒu�ɌQ��𐶐�����
                Instantiate(group[GameManager.selectPlayer], this.transform.position, Quaternion.identity);
                //�Q�[�W�^�C�}�[������������
                gageTimer = 0.0f;
            }
            //�o���l���ő�o���l�Ɠ������ꍇ
            if (exp == maxExp)
            {
                LevelUp();//�֐�"LevelUp"�����s����
            }
        }
        //Esc�L�[��"�����ꂽ"&&�Q�[���̏�Ԃ�"Play"�̏ꍇ
        if (Input.GetKeyDown(KeyCode.Escape) && Stage.status == "Play")
        {
            Stage.status = "Pause";//�Q�[���̏�Ԃ�"Pause"�ɂ���
        }
        //Esc�L�[��"�����ꂽ"&&�Q�[���̏�Ԃ�"Pause"�̏ꍇ
        else if (Input.GetKeyDown(KeyCode.Escape) && Stage.status == "Pause")
        {
            Stage.status = "Play";//�Q�[���̏�Ԃ�"Play"�ɂ���
        }
        //�v���C���[�̏�Ԃ�"Invincible"�̏ꍇ
        if (status == "Invincible")
        {
            Invincible();//�֐�"Invincible"�����s����
        }
    }

    //�֐�"SetObjRenderer"
    void SetObjRenderer(bool set)
    {
        for (int i = 0; i < thisRenderer.Length; i++)
        {
            thisRenderer[i].enabled = set;//Renderer��thisRenderer�ɃZ�b�g����
        }
    }

    //�֐�"LevelUp"
    void LevelUp()
    {
        hp += 1;
        exp = 0;//�o���l������������
    }

    //�֐�"Damage"
    public override void DamagePlayer()
    {
        base.DamagePlayer();

        //�̗͂�"0����"�̏ꍇ
        if (hp > 0)
        {
            StartCoroutine("Blinking");//�R���[�`��"Blinking"�����s����
        }
        //�̗͂�"0�ȉ�"��������
        else if (hp <= 0)
        {
           Death();
        }
    }

    //�R���[�`��"Blinking"
    IEnumerator Blinking()
    {
        isDamage = true;         //�_���[�W��"�󂯂Ă���"�ɂ���
        //�^�C�}�[��������
        rendererTimer = 0.0f;
        rendererTotalTime = 0.0f;

        while (true)
        {
            //�^�C�}�[�Ɍo�ߎ��Ԃ𑫂�
            rendererTimer += Time.deltaTime;
            rendererTotalTime += Time.deltaTime;
            //Renderer�؂�ւ��̌o�ߎ��Ԃ�Renderer�؂�ւ����Ԉȏ�̏ꍇ
            if (rendererSwitch <= rendererTimer)
            {
                rendererTimer = 0.0f;          //Renderer�؂�ւ��̌o�ߎ��Ԃ�����������
                isRenderer = !isRenderer;//"objRenderer"��"true"�̏ꍇ��"false"�A"false"�̏ꍇ��"true"�ɂ���
                SetObjRenderer(isRenderer); //�֐�"SetObjRenderer"�����s����
            }
            //Renderer�؂�ւ��̍��v�o�ߎ��Ԃ��_�Ŏ������Ԉȏ�̏ꍇ
            if (blinkingTime <= rendererTotalTime)
            {
                isDamage = false;    //�_���[�W��"�󂯂Ă��Ȃ�"�ɂ���
                isRenderer = true;//Renderer��L��������
                SetObjRenderer(true);//�֐�"SetObjRenderer"�����s����
                yield break;         //�R���[�`�����~����
            }
            yield return null;
        }
    }

    //�֐�"Invincible"
    void Invincible()
    {
        invincibleTimer += Time.deltaTime;       //���G�^�C�}�[�Ɍo�ߎ��Ԃ𑫂�
        //���G�^�C�}�[�����G�������Ԉȏ�̏ꍇ
        if (invincibleTimer >= invincibleInterval)
        {
            invincibleTimer = 0.0f; //���G�^�C�}�[������������
            status = "Normal";//�v���C���[�̏�Ԃ�"Normal"�ɂ���
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

