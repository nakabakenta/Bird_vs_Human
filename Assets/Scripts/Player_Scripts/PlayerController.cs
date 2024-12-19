using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�X�e�[�^�X(public)
    public static int hp;             //�v���C���[�̗̑�
    public static string playerStatus;//�v���C���[�̏��
    //�v���C���[�̈ړ����E�l
    private Vector2[,] limitPosition = new Vector2[5, 2]
    {
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.8f),},
    };
    //����
    public static float[] attackTimer = new float[2];   //�U���Ԋu�^�C�}�[
    public static float[] attackInterval = new float[2];//�U���Ԋu
    public static float gageTimer = 0.0f;               //�Q�[�W�^�C�}�[
    public static float gageInterval = 20.0f;           //�Q�[�W�~�ώ���
    public static int level;               //���x��
    public static int exp;                 //�o���l
    public static int ally;                //������
    private float invincibleTimer = 0.0f;  //���G�^�C�}�[
    private float invincible = 10.0f;      //���G�p������
    private float blinkingTime = 1.0f;     //�_�ŁE���G�̎�������
    private float rendererSwitch = 0.05f;  //Renderer�̗L���E������؂�ւ��鎞��(�_�ł̐؂�ւ��鎞��)
    private float rendererTimer;           //Renderer�̗L���E�����̌o�ߎ���(�_�ł̌o�ߎ���)
    private float rendererTotalElapsedTime;//Renderer�̗L���E�����̍��v�o�ߎ���
    private bool isDamage;                 //�_���[�W�̉�
    private bool isObjRenderer;            //objRenderer�̉�
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject[] player = new GameObject[3];  //"GameObject(�v���C���[)"
    public GameObject forwardBullet, downBullet;     //"GameObject(�e)"
    public GameObject[] group = new GameObject[3];   //"GameObject(�Q��)"
    private GameObject nowPlayer;                    //"GameObject(���݂̃v���C���[)"
    private GameObject[] nowally = new GameObject[2];//
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
        ally = 0;
        level = 1;
        exp = 0;

        thisTransform = this.gameObject.transform;//���̃I�u�W�F�N�g��"Transform"���擾
        SetPlayer();                              //�֐�"SetPlayer"�����s
        //�R���|�[�l���g���擾
        objRenderer = this.gameObject.GetComponentsInChildren<Renderer>();//���̃I�u�W�F�N�g��"Renderer(�q�I�u�W�F�N�g���܂�)���擾
        rigidBody = this.gameObject.GetComponent<Rigidbody>();            //���̃I�u�W�F�N�g��"Rigidbody"���擾
        boxCollider = this.gameObject.GetComponent<BoxCollider>();        //���̃I�u�W�F�N�g��"BoxCollider"���擾
        playerStatus = "Normal";
    }

    // Update is called once per frame
    void Update()
    {
        if (hp > 0)
        {
            Behavior();//�֐�PlayControl���Ăяo��
        }
    }

    //�֐�"SetPlayer"
    void SetPlayer()
    {
        nowPlayer = Instantiate(player[GameManager.playerNumber], this.transform.position, Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        player[GameManager.playerNumber].SetActive(true);

        if(GameManager.gameStart == false)
        {
            GameManager.remain = 3;
            GameManager.gameStart = true;
        }

        hp = PlayerList.Player.hp[GameManager.playerNumber];                           //�̗�
        attackTimer[0] = PlayerList.Player.attackInterval[0, GameManager.playerNumber];
        attackTimer[1] = PlayerList.Player.attackInterval[1, GameManager.playerNumber];
    }

    //�֐�"Behavior"
    void Behavior()
    {
        //
        if (Stage.gameStatus == "Play")
        {
            //�N�[���^�C����Time.deltaTime�𑫂�
            attackTimer[0] += Time.deltaTime;
            attackTimer[1] += Time.deltaTime;

            mousePosition = Input.mousePosition;
            viewPortPosition = Camera.main.ScreenToViewportPoint(new Vector3(mousePosition.x, mousePosition.y, 9.0f));

            viewPortPosition.x = Mathf.Clamp(viewPortPosition.x, limitPosition[Stage.nowStage - 1, 0].x, limitPosition[Stage.nowStage - 1, 1].x);
            viewPortPosition.y = Mathf.Clamp(viewPortPosition.y, limitPosition[Stage.nowStage - 1, 0].y, limitPosition[Stage.nowStage - 1, 1].y);

            this.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(viewPortPosition.x, viewPortPosition.y, 9.0f));

            if (playerStatus == "Normal")
            {
                attackInterval[0] = PlayerList.Player.attackInterval[0, GameManager.playerNumber];
                attackInterval[1] = PlayerList.Player.attackInterval[1, GameManager.playerNumber];
                gageTimer += Time.deltaTime;//�Q�[�W�^�C�}�[��Time.deltaTime�𑫂�
            }
            else if(playerStatus == "Invincible")
            {
                attackInterval[0] = PlayerList.Invincible.attackInterval[0];
                attackInterval[1] = PlayerList.Invincible.attackInterval[1];
            }

            //�O���U��
            if (Input.GetMouseButton(0) && attackTimer[0] >= attackInterval[0])
            {
                Instantiate(forwardBullet, this.transform.position, Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z));
                attackTimer[0] = 0.0f;
            }
            //�����U��
            if (Input.GetMouseButton(1) && attackTimer[1] >= attackInterval[1])
            {
                Instantiate(downBullet, this.transform.position, Quaternion.identity);
                attackTimer[1] = 0.0f;
            }
            //�Q�[�W���
            if (Input.GetMouseButtonDown(2) && gageTimer >= gageInterval)
            {
                gageTimer = 0.0f;
                playerStatus = "Invincible";
                Instantiate(group[GameManager.playerNumber], this.transform.position, Quaternion.identity);
            }

            if(exp == PlayerList.Player.maxExp[GameManager.playerNumber])
            {
                LevelUp();
            }
        }
        //
        if (Input.GetKeyDown(KeyCode.Escape) && Stage.gameStatus == "Play")
        {
            Stage.gameStatus = "Pause";
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Stage.gameStatus == "Pause")
        {
            Stage.gameStatus = "Play";
        }

        //�v���C���[�̏�Ԃ�"Invincible(���G)"�ł����
        if (playerStatus == "Invincible")
        {
            Invincible();//�֐�"Invincible(���G)"���Ăяo��
        }
    }

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
        if(level == 1)
        {

        }
        else if(level == 2)
        {

        }
        else if (level == 3)
        {

        }
        else if (level == 4)
        {

        }
        else if (level == 5)
        {

        }

        exp = 0;
    }

    //�֐�"Damage"
    void Damage()
    {
        //�_�Œ��͓�d�Ɏ��s���Ȃ�
        if (isDamage == true)
        {
            return;
        }

        hp -= 1;//�̗�"hp"��"-1"����

        //�̗͂�0���ゾ������
        if (hp > 0)
        {
            StartCoroutine("Blinking");//�R���[�`��"Blinking"�����s����
        }
        //�̗͂�0�ȉ���������
        else if (hp <= 0)
        {
            Death();//���S�֐�"Death"�����s����
        }
    }

    IEnumerator Blinking()
    {
        isDamage = true;

        rendererTotalElapsedTime = 0;
        rendererTimer = 0;

        while (true)
        {
            rendererTotalElapsedTime += Time.deltaTime;
            rendererTimer += Time.deltaTime;
            if (rendererSwitch <= rendererTimer)
            {
                rendererTimer = 0;             //��_���[�W�_�ŏ���
                isObjRenderer = !isObjRenderer;//Renderer�̗L���E������؂�ւ���(�_�ŏ���)
                SetObjRenderer(isObjRenderer); //
            }

            if (blinkingTime <= rendererTotalElapsedTime)
            {
                //��_���[�W�_�ł̏I�����̏���
                isDamage = false;
                isObjRenderer = true;//Renderer��L��������
                SetObjRenderer(true);//
                yield break;
            }
            yield return null;
        }
    }

    //���G�֐�
    void Invincible()
    {
        invincibleTimer += Time.deltaTime;

        if(invincibleTimer > invincible)
        {
            invincibleTimer = 0.0f;
            playerStatus = "Normal";
        }  
    }

    //���S�֐�
    void Death()
    {
        Animator animator = nowPlayer.GetComponent<Animator>();//
        hp = 0;                         //hp��"0"�ɂ���
        boxCollider.enabled = false;    //BoxCollider�𖳌��ɂ���
        animator.SetBool("Death", true);//Animator��"Death"(���S)��L���ɂ���
        rigidBody.useGravity = true;    //RigidBody�̏d�͂�L���ɂ���
        GameManager.remain--;
    }

    //�Փ˔���(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Enemy && playerStatus"��"Normal"�̏ꍇ
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossEnemy" || collision.gameObject.tag == "EnemyBullet") && playerStatus == "Normal")
        {
            if (ally > 0)
            {
                Destroy(nowally[ally - 1]);
                ally -= 1;
            }
            else if (ally <= 0)
            {
                Damage();//�֐�"Damage"�����s
            }
        }

        //�Փ˂����I�u�W�F�N�g�̃^�O��"PlayerAlly"�̏ꍇ
        if (collision.gameObject.tag == "PlayerAlly" && ally < 2)
        {
            Invoke("Ally", 0.01f);//�֐�"Ally"��"0.01f"��Ɏ��s
        }
    }

    void Ally()
    {
        if (ally == 0)
        {
            nowally[ally] = Instantiate(player[GameManager.playerNumber], new Vector3(this.transform.position.x - 1.0f, this.transform.position.y, this.transform.position.z), Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        }
        else if (ally == 1)
        {
            nowally[ally] = Instantiate(player[GameManager.playerNumber], new Vector3(this.transform.position.x - 2.0f, this.transform.position.y, this.transform.position.z), Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        }

        ally += 1;
    }
}