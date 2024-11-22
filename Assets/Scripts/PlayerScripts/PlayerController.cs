using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�X�e�[�^�X
    public static int hp;             //�̗�
    public static float speed;        //�ړ����x
    public static string playerStatus;//�v���C���[�̏��
    public static bool useGage;       //�Q�[�W�̎g�p��
    public static bool allySacrifice; //�����̋]����
    //����
    private float attackTimerF = 0.25f;   //�O�U���Ԋu�^�C�}�[
    private float attackTimerD = 0.50f;   //���U���Ԋu�^�C�}�[
    private float attackIntervalF = 0.25f;//�U���Ԋu(�O���U��)
    private float attackIntervalD = 0.50f;//�U���Ԋu(�����U��)
    private float invincibleTimer = 0.0f; //���G�^�C�}�[
    private float invincible = 10.0f;     //���G�p������
    //�r���[�|�C���g���W.X, Y
    private float viewPointX, viewPointY;
    //�_���[�W�֌W�ϐ�
    private float blinkingTime = 1.0f;     //�_�ŁE���G�̎�������
    private float rendererSwitch = 0.05f;  //Renderer�̗L���E������؂�ւ��鎞��(�_�ł̐؂�ւ��鎞��)
    private float rendererTimer;           //Renderer�̗L���E�����̌o�ߎ���(�_�ł̌o�ߎ���)
    private float rendererTotalElapsedTime;//Renderer�̗L���E�����̍��v�o�ߎ���
    private bool isDamage;                 //�_���[�W�̉�
    private bool isObjRenderer;            //objRenderer�̗L���E�����t���O
    //�I�u�W�F�N�g
    private GameObject[] player = new GameObject[3];//�v���C���[�I�u�W�F�N�g
    public GameObject forwardBullet, downBullet;    //�e�I�u�W�F�N�g
    public GameObject[] group = new GameObject[3];  //�Q��I�u�W�F�N�g
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private Rigidbody rigidBody;     //"Rigidbody"
    private BoxCollider boxCollider; //"BoxCollider"
    private Animator animator = null;//"Animator"
    private Renderer[] objRenderer;  //"Renderer"
    //�R���[�`��
    private Coroutine blinking;//
    //�}�E�X���W
    private Vector3 mousePosition, worldPosition;

    // Start is called before the first frame update
    void Start()
    {
        allySacrifice = false;

        //�v���C���[�I�u�W�F�N�g��T���Ď擾����
        player[0] = GameObject.Find("Sparrow_Player");
        player[1] = GameObject.Find("Crow_Player");
        player[2] = GameObject.Find("Chickadee_Player");

        objRenderer = this.gameObject.GetComponentsInChildren<Renderer>();//���̃I�u�W�F�N�g��Renderer(�q�I�u�W�F�N�g���܂�)���擾
        rigidBody = this.gameObject.GetComponent<Rigidbody>();            //���̃I�u�W�F�N�g��Rigidbody���擾
        boxCollider = this.gameObject.GetComponent<BoxCollider>();        //���̃I�u�W�F�N�g��BoxCollider���擾
        useGage = false;
        playerStatus = "Normal";

        player[0].SetActive(false);
        player[1].SetActive(false);
        player[2].SetActive(false);

        SetPlayer();//�֐�"SetPlayer"���Ăяo��
    }

    // Update is called once per frame
    void Update()
    {
        //�N�[���^�C����Time.deltaTime�𑫂�
        attackTimerF += Time.deltaTime;
        attackTimerD += Time.deltaTime;

        if(hp > 0)
        {
            Behavior();//�֐�PlayControl���Ăяo��
        }

        //�r���[�|�C���g���W���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//���X���W
        viewPointY = Camera.main.WorldToViewportPoint(this.transform.position).y;//���Y���W
    }

    //�X�e�[�^�X��ݒ�
    void SetPlayer()
    {
        player[GameManager.playerNumber].SetActive(true);
        animator = player[GameManager.playerNumber].GetComponent<Animator>();//"Animator"���擾

        if(GameManager.gameStart == false)
        {
            GameManager.remain = 3;
            GameManager.gameStart = true;
        }

        hp = PlayerList.Player.hp[GameManager.playerNumber];      //�̗�
        speed = PlayerList.Player.speed[GameManager.playerNumber];//�ړ����x
    }

    //����֐�
    void Behavior()
    {
        //�}�E�X���W���擾���āA�X�N���[�����W�����[���h���W�ɕϊ�����
        worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9.0f));

        //
        if(Stage.gameStatus == "Play")
        {
            this.transform.position = worldPosition;

            //�O���U��
            if (Input.GetMouseButton(0) && attackTimerF > attackIntervalF)
            {
                Instantiate(forwardBullet, this.transform.position, Quaternion.identity);
                attackTimerF = 0.0f;
            }
            //�����U��
            if (Input.GetMouseButton(1) && attackTimerD > attackIntervalD)
            {
                Instantiate(downBullet, this.transform.position, Quaternion.identity);
                attackTimerD = 0.0f;
            }
            //�Q�[�W���
            if (Input.GetKeyDown(KeyCode.E) && useGage == true)
            {
                useGage = false;
                playerStatus = "Invincible";
                Instantiate(group[GameManager.playerNumber], this.transform.position, Quaternion.identity);
            }
        }

        //Vector3 position = this.transform.position;

        //if (viewPointX >= 0 && viewPointX <= 1)
        //{
        //    position.x = worldPosition.x;
        //    this.transform.position = position;
        //}
        //else
        //{

        //}

        //if (viewPointY >= 0 && viewPointY <= 1)
        //{
        //    position.y = worldPosition.y;
        //    this.transform.position = position;
        //}
        //else
        //{

        //}

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

    //�_���[�W�֐�
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
        hp = 0;                         //hp��"0"�ɂ���
        boxCollider.enabled = false;    //BoxCollider�𖳌��ɂ���
        animator.SetBool("Death", true);//Animator��"Death"(���S)��L���ɂ���
        rigidBody.useGravity = true;    //RigidBody�̏d�͂�L���ɂ���
        GameManager.remain--;
    }

    //�Փ˔���(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Enemy" && "playerStatus"��"Normal"�������� 
        if (collision.gameObject.tag == "Enemy" && playerStatus == "Normal")
        {
            if(BirdAlly.allyCount > 0)
            {
                BirdAlly.allyCount--;
                allySacrifice = true;
            }
            else if(BirdAlly.allyCount <= 0)
            {
                Damage();//�_���[�W�֐�"Damage"�����s����
            }
        }
    }
}