using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : CharacteBase
{
    //�X�e�[�^�X
    public static int attackPower;//�U����
    public static int remain;     //�c�@
    public static string status;  //���
    public float attackSpeed;     //�U�����x
    //���W
    public Vector3 mousePosition;
    //�ړ�����
    private Vector2[,] limitPosition = new Vector2[5, 2]
    {
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.8f),},
    };
    //����
    public static float[] attackTimer = new float[2];//�U���^�C�}�[([�O��],[����])
    public static float attackInterval;              //�U���Ԋu
    public static float gageTimer;                   //�Q�[�W�^�C�}�[
    public static float gageInterval = 20.0f;        //�Q�[�W�Ԋu
    public static int exp;                           //�o���l
    public static int ally;                          //������
    public float invincibleTimer = 0.0f;             //���G�^�C�}�[
    public float invincibleInterval = 10.0f;         //���G�Ԋu
    public float blinkingInterval = 1.0f;            //�_�ŊԊu
    public float rendererTimer;                      //Renderer�؂�ւ��̌o�ߎ���
    public float rendererInterval = 0.05f;           //Renderer�؂�ւ�����
    public float rendererTotalTime;                  //Renderer�؂�ւ��̍��v�o�ߎ���
    public bool isRenderer;                          //Renderer�̉�
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject nowPlayer;                       //"GameObject(���݂̃v���C���[)"
    public GameObject[] playerAlly = new GameObject[2];//"GameObject(�v���C���[�̖���)"

    //�֐�"StartPlayer"
    public void StartPlayer()
    {
        //�I�������v���C���[�̃X�e�[�^�X��ݒ肷��
        hp = Player.hp[GameManager.selectPlayer];                     //�̗�
        attackPower = Player.attackPower[GameManager.selectPlayer];   //�U����
        attackSpeed = (Player.maxStatus - Player.attackSpeed[GameManager.selectPlayer]) / 2.0f;
        attackTimer[0] = attackSpeed;
        attackTimer[1] = attackSpeed;
        status = "Normal";//�v���C���[�̏�Ԃ�"Normal"�ɂ���
        //����������������
        gageTimer = 0.0f;
        ally = 0;
        exp = 0;

        //�Q�[���̏�Ԃ�"Menu"�̏ꍇ
        if (GameManager.playBegin == false)
        {
            remain = 3;                  //
            GameManager.playBegin = true;//
        }
    }

    public void UpdatePlayer()
    {
        //�Q�[���̏�Ԃ�"Play"�̏ꍇ
        if (hp > 0 && GameManager.status == "Play")
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
                attackInterval = attackSpeed;

                gageTimer += Time.deltaTime;//�Q�[�W�^�C�}�[�Ɍo�ߎ��Ԃ𑫂�
            }
            //�v���C���[�̏�Ԃ�"Invincible"�̏ꍇ
            else if (status == "Invincible")
            {
                //���G���̍U���Ԋu��ݒ肷��
                attackInterval = PlayerBase.InvincibleStatus.attackSpeed;
            }

            InputButton();
        }

        //�v���C���[�̏�Ԃ�"Invincible"�̏ꍇ
        if (status == "Invincible")
        {
            Invincible();//�֐�"Invincible"�����s����
        }

        //�o���l���ő�o���l�Ɠ������ꍇ
        if (exp == Player.maxExp)
        {
            Heal();//�֐�"Heal"�����s����
        }

        if (status == "Death")
        {
            if (this.transform.position.y <= 0.0f)
            {
                this.transform.position = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);
            }
        }
    }

    public virtual void InputButton()
    {
        
    }

    //�֐�"Heal"
    public void Heal()
    {
        hp += 1;
        exp = 0;//�o���l������������
    }

    //�֐�"Invincible"
    void Invincible()
    {
        invincibleTimer += Time.deltaTime;       //���G�^�C�}�[�Ɍo�ߎ��Ԃ𑫂�
        //���G�^�C�}�[�����G�������Ԉȏ�̏ꍇ
        if (invincibleTimer >= invincibleInterval)
        {
            invincibleTimer = 0.0f;//���G�^�C�}�[������������
            status = "Normal";     //�v���C���[�̏�Ԃ�"Normal"�ɂ���
        }
    }

    //�֐�"Damage"
    public void Damage()
    {
        //�_���[�W��"�󂯂Ă���"�ꍇ
        if (isDamage == true)
        {
            return;//�Ԃ�
        }

        hp -= 1;//�̗͂�"-1"����

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

    //�֐�"SetObjRenderer"
    void SetObjRenderer(bool set)
    {
        for (int i = 0; i < thisRenderer.Length; i++)
        {
            thisRenderer[i].enabled = set;//Renderer��thisRenderer�ɃZ�b�g����
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
            if (rendererInterval <= rendererTimer)
            {
                rendererTimer = 0.0f;          //Renderer�؂�ւ��̌o�ߎ��Ԃ�����������
                isRenderer = !isRenderer;//"objRenderer"��"true"�̏ꍇ��"false"�A"false"�̏ꍇ��"true"�ɂ���
                SetObjRenderer(isRenderer); //�֐�"SetObjRenderer"�����s����
            }
            //Renderer�؂�ւ��̍��v�o�ߎ��Ԃ��_�Ŏ������Ԉȏ�̏ꍇ
            if (blinkingInterval <= rendererTotalTime)
            {
                isDamage = false;    //�_���[�W��"�󂯂Ă��Ȃ�"�ɂ���
                isRenderer = true;//Renderer��L��������
                SetObjRenderer(true);//�֐�"SetObjRenderer"�����s����
                yield break;         //�R���[�`�����~����
            }
            yield return null;
        }
    }

    //�֐�"Death"
    public void Death()
    {
        boxCollider.enabled = false;        //BoxCollider��"����"�ɂ���
        rigidBody.useGravity = true;        //RigidBody�̏d�͂�"�L��"�ɂ���
        animator.SetInteger("Animation", 1);
        hp = 0;                             //�̗͂�"0"�ɂ���
        remain -= 1;                        //�c�@��"-1"����
        status = "Death";
    }

    //�Փ˔���(OnTriggerEnter)
    public virtual void OnTriggerEnter(Collider collision)
    {
        //(�Փ˂����I�u�W�F�N�g�̃^�O��"Enemy" || "BossEnemy" || "EnemyBullet" ) && �v���C���[�̏�Ԃ�"Normal"�̏ꍇ
        if ((collision.gameObject.tag == "Enemy" || 
             collision.gameObject.tag == "BossEnemy" || 
             collision.gameObject.tag == "EnemyBullet") && 
             status == "Normal")
        {
            //��������"0����"�̏ꍇ
            if (ally > 0)
            {
                playerAlly[ally - 1].AddComponent<Rigidbody>();
                playerAlly[ally - 1].AddComponent<Test>();
                Animator allyAnimator = playerAlly[ally - 1].GetComponent<Animator>();
                allyAnimator.SetInteger("Animation", 1);
                playerAlly[ally - 1].transform.SetParent(null);//�e����O��
                playerAlly[ally - 1] = null;
                ally -= 1;                                     //��������"-1"����


                //Destroy(playerAlly[ally - 1]);//����������

            }
            //��������"0�ȉ�"�̏ꍇ
            else if (ally <= 0)
            {
                Damage();//�֐�"Damage"�����s����
            }
        }

        //�Փ˂����I�u�W�F�N�g�̃^�O��"PlayerAlly"�̏ꍇ
        if (collision.gameObject.tag == "PlayerAlly" && ally < 2)
        {
            Invoke("Ally", 0.01f);//�֐�"Ally"��"0.01f"��Ɏ��s����
        }
    }

    public static class Player
    {
        public enum PlayerName
        {
            Sparrow = 0,
            Crow = 1,
            Chickadee = 2,
            Penguin = 3,
        }
        
        public static int maxStatus = 8;//�ő�X�e�[�^�X�l
        public static int maxExp = 10;  //�ő�o���l

        //�̗�
        public static int[] hp = new int[] 
        { 3, 3, 3, 0 };
        //�U����
        public static int[] attackPower = new int[]
        { 4, 6, 2, 0 };
        //�U�����x
        public static float[] attackSpeed = new float[]
        { 5.0f, 3.0f, 7.0f, 0.0f };
    }

    public static class InvincibleStatus
    {
        public static float attackSpeed = 0.5f;
    }
}