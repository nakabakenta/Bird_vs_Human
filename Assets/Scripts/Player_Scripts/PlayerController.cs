using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�X�e�[�^�X
    public static int hp;             //�̗�
    public static int remain;         //�c�@
    public static string playerStatus;//�v���C���[�̏��
    //�ړ��̌��E�ʒu
    private Vector2[,] limitPosition = new Vector2[5, 2]
    {
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.8f),},
    };
    //����
    public static float[] attackTimer = new float[2];   //�U���^�C�}�[([�O��],[����])
    public static float[] attackInterval = new float[2];//�U���Ԋu([�O��],[����])
    public static float gageTimer;                      //�Q�[�W�^�C�}�[
    public static float gageInterval;                   //�Q�[�W�~�ώ���
    public static int level;                            //���x��
    public static int exp;                              //�o���l
    public static int ally;                             //������
    private float invincibleTimer = 0.0f;               //���G�^�C�}�[
    private float invincibleInterval = 10.0f;           //���G��������
    private float blinkingTime = 1.0f;                  //�_�Ŏ�������
    private float rendererSwitch = 0.05f;               //Renderer�؂�ւ�����
    private float rendererTimer;                        //Renderer�؂�ւ��̌o�ߎ���
    private float rendererTotalTime;                    //Renderer�؂�ւ��̍��v�o�ߎ���
    private bool isDamage;                              //�_���[�W�̉�
    private bool isObjRenderer;                         //objRenderer�̉�
    private float levelAttackInterval = 0.0f;           //���x���A�b�v���̍U���Ԋu�Z�k
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject[] player = new GameObject[3];  //"GameObject(�v���C���[)"
    public GameObject forwardBullet, downBullet;     //"GameObject(�e)"
    public GameObject[] group = new GameObject[3];   //"GameObject(�Q��)"
    private GameObject nowPlayer;                    //"GameObject(���݂̃v���C���[)"
    private GameObject[] nowAlly = new GameObject[2];//"GameObject(���݂̖���)"
    private Transform thisTransform ;                //"Transform"
    private Rigidbody rigidBody;                     //"Rigidbody"
    private BoxCollider boxCollider;                 //"BoxCollider"
    private Renderer[] objRenderer;                  //"Renderer"
    //�R���[�`��
    private Coroutine blinking;//
    //���W
    private Vector3 mousePosition, worldPosition, viewPortPosition;

    // Start is called before the first frame update
    void Start()
    {
        //����������������
        gageTimer = 0.0f;    
        gageInterval = 20.0f;
        ally = 0;
        level = 1;
        exp = 0;
        thisTransform = this.gameObject.transform;//���̃I�u�W�F�N�g��"Transform"���擾
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾����
        objRenderer = this.gameObject.GetComponentsInChildren<Renderer>();
        rigidBody = this.gameObject.GetComponent<Rigidbody>();            
        boxCollider = this.gameObject.GetComponent<BoxCollider>();
        
        SetPlayer();//�֐�"SetPlayer"�����s����
    }

    // Update is called once per frame
    void Update()
    {
        //�̗͂�"0����"�̏ꍇ
        if (hp > 0)
        {
            Action();//�֐�"Action"�����s����
        }
    }

    //�֐�"SetPlayer"
    void SetPlayer()
    {
        //�I�������v���C���[�����̃I�u�W�F�N�g�̎q�I�u�W�F�N�g�Ƃ��Đ�������
        nowPlayer = Instantiate(player[GameManager.playerNumber], this.transform.position, Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        //�I�������v���C���[�̃X�e�[�^�X��ݒ肷��
        hp = PlayerList.Player.hp[GameManager.playerNumber];                           //�̗�
        attackTimer[0] = PlayerList.Player.attackInterval[0, GameManager.playerNumber];//�U���^�C�}�[[�O��]
        attackTimer[1] = PlayerList.Player.attackInterval[1, GameManager.playerNumber];//�U���^�C�}�[[����]
        playerStatus = "Normal";                                                       //�v���C���[�̏�Ԃ�"Normal"�ɂ���
        //�Q�[����"�J�n���Ă��Ȃ�"�ꍇ
        if (GameManager.gameStart == false)
        {
            remain = 3;                  //�c�@
            GameManager.gameStart = true;//�Q�[����"�J�n����"�ɂ���
        }
    }

    //�֐�"Action"
    void Action()
    {
        //�Q�[���̏�Ԃ�"Play"�̏ꍇ
        if (Stage.gameStatus == "Play")
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
            if (playerStatus == "Normal")
            {
                //�I�������v���C���[�̍U���Ԋu��ݒ肷��
                attackInterval[0] = PlayerList.Player.attackInterval[0, GameManager.playerNumber];
                attackInterval[1] = PlayerList.Player.attackInterval[1, GameManager.playerNumber];

                gageTimer += Time.deltaTime;//�Q�[�W�^�C�}�[�Ɍo�ߎ��Ԃ𑫂�
            }
            //�v���C���[�̏�Ԃ�"Invincible"�̏ꍇ
            else if (playerStatus == "Invincible")
            {
                //���G���̍U���Ԋu��ݒ肷��
                attackInterval[0] = PlayerList.Invincible.attackInterval[0];
                attackInterval[1] = PlayerList.Invincible.attackInterval[1];
            }

            //�O���U��
            //�}�E�X��"���N���b�N���ꂽ"&&"�U���^�C�}�[[�O��]"��"�U���Ԋu[�O��]" - "���x���A�b�v���̍U���Ԋu�Z�k"�ȏ�̏ꍇ
            if (Input.GetMouseButton(0) && attackTimer[0] >= attackInterval[0] - levelAttackInterval)
            {
                //���̃I�u�W�F�N�g�̈ʒu�ɑO���e�𐶐�����
                Instantiate(forwardBullet, this.transform.position, Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z));
                //�U���^�C�}�[[�O��]������������
                attackTimer[0] = 0.0f;
            }
            //�����U��
            //�}�E�X��"�E�N���b�N���ꂽ"&&"�U���^�C�}�[[����]"��"�U���Ԋu[����]" - "���x���A�b�v���̍U���Ԋu�Z�k"�ȏ�̏ꍇ
            if (Input.GetMouseButton(1) && attackTimer[1] >= attackInterval[1] - levelAttackInterval)
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
                playerStatus = "Invincible";
                //���̃I�u�W�F�N�g�̈ʒu�ɌQ��𐶐�����
                Instantiate(group[GameManager.playerNumber], this.transform.position, Quaternion.identity);
                //�Q�[�W�^�C�}�[������������
                gageTimer = 0.0f;
            }
            //�o���l���ő�o���l�Ɠ������ꍇ
            if (exp == PlayerList.Player.maxExp[GameManager.playerNumber])
            {
                LevelUp();//�֐�"LevelUp"�����s����
            }
        }
        //Esc�L�[��"�����ꂽ"&&�Q�[���̏�Ԃ�"Play"�̏ꍇ
        if (Input.GetKeyDown(KeyCode.Escape) && Stage.gameStatus == "Play")
        {
            Stage.gameStatus = "Pause";//�Q�[���̏�Ԃ�"Pause"�ɂ���
        }
        //Esc�L�[��"�����ꂽ"&&�Q�[���̏�Ԃ�"Pause"�̏ꍇ
        else if (Input.GetKeyDown(KeyCode.Escape) && Stage.gameStatus == "Pause")
        {
            Stage.gameStatus = "Play";//�Q�[���̏�Ԃ�"Play"�ɂ���
        }
        //�v���C���[�̏�Ԃ�"Invincible"�̏ꍇ
        if (playerStatus == "Invincible")
        {
            Invincible();//�֐�"Invincible"�����s����
        }
    }

    //�֐�"SetObjRenderer"
    void SetObjRenderer(bool set)
    {
        for (int i = 0; i < objRenderer.Length; i++)
        {
            objRenderer[i].enabled = set;//Renderer��objRenderer�ɃZ�b�g����
        }
    }

    ///�֐�"LevelUp"
    void LevelUp()
    {
        //���x����"5�ȉ�"�̏ꍇ
        if(level >= 5)
        {
            levelAttackInterval += 0.1f;//���x���A�b�v���̍U���Ԋu�Z�k��0.1f����
        }

        exp = 0;//�o���l������������
    }

    //�֐�"Damage"
    void Damage()
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
            Death();//�֐�"Death"�����s����
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
            rendererTotalTime += Time.deltaTime;
            rendererTimer += Time.deltaTime;
            //Renderer�؂�ւ��̌o�ߎ��Ԃ�Renderer�؂�ւ����Ԉȏ�̏ꍇ
            if (rendererTimer >= rendererSwitch)
            {
                rendererTimer = 0.0f;          //Renderer�؂�ւ��̌o�ߎ��Ԃ�����������
                isObjRenderer = !isObjRenderer;//"objRenderer"��"true"�̏ꍇ��"false"�A"false"�̏ꍇ��"true"�ɂ���
                SetObjRenderer(isObjRenderer); //�֐�"SetObjRenderer"�����s����
            }
            //Renderer�؂�ւ��̍��v�o�ߎ��Ԃ��_�Ŏ������Ԉȏ�̏ꍇ
            if (rendererTotalTime >= blinkingTime)
            {
                isDamage = false;    //�_���[�W��"�󂯂Ă��Ȃ�"�ɂ���
                isObjRenderer = true;//Renderer��L��������
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
            playerStatus = "Normal";//�v���C���[�̏�Ԃ�"Normal"�ɂ���
        }  
    }

    //�֐�"Death"
    void Death()
    {
        //�A�j���[�^�[�R���|�[�l���g���擾����
        Animator animator = nowPlayer.GetComponent<Animator>();
        hp = 0;                         //�̗͂�"0"�ɂ���
        boxCollider.enabled = false;    //BoxCollider��"����"�ɂ���
        animator.SetBool("Death", true);//Animator��"Death"�ɂ���
        rigidBody.useGravity = true;    //RigidBody�̏d�͂�L���ɂ���
        remain -= 1;                    //�c�@��"-1"����
    }

    //�Փ˔���(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //(�Փ˂����I�u�W�F�N�g�̃^�O��"Enemy" || "BossEnemy" || "EnemyBullet" ) && �v���C���[�̏�Ԃ�"Normal"�̏ꍇ
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossEnemy" || collision.gameObject.tag == "EnemyBullet") && playerStatus == "Normal")
        {
            //��������"0����"�̏ꍇ
            if (ally > 0)
            {
                Destroy(nowAlly[ally - 1]);//����������
                ally -= 1;                 //��������"-1"����
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

    //�֐�"Ally"
    void Ally()
    {
        //��������"0�Ɠ�����"�ꍇ
        if (ally == 0)
        {
            //�����𐶐�����
            nowAlly[ally] = Instantiate(player[GameManager.playerNumber], new Vector3(this.transform.position.x - 1.0f, this.transform.position.y, this.transform.position.z), Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        }
        //��������"1�Ɠ�����"�ꍇ
        else if (ally == 1)
        {
            //�����𐶐�����
            nowAlly[ally] = Instantiate(player[GameManager.playerNumber], new Vector3(this.transform.position.x - 2.0f, this.transform.position.y, this.transform.position.z), Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        }

        ally += 1;//��������"+1"����
    }
}

public class PlayerBase : CharacterBase
{
    //public override void Damage()
    //{

    //}
}