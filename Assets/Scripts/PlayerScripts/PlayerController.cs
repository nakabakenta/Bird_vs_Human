using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�X�e�[�^�X
    public GameObject forwardBullet;//
    public GameObject downBullet;   //

    public GameObject aaa;

    public static int hp;             //�̗�
    public static float speed;        //�ړ����x
    public static bool gage;          //�Q�[�W�t���O
    public static string playerStatus;//�v���C���[�̏��

    private float intervalTimerF = 0.25f; //�Ԋu�^�C�}�[(�O���U��)
    private float intervalTimerD = 0.50f; //�Ԋu�^�C�}�[(�����U��)
    private float attackIntervalF = 0.25f;//�U���Ԋu(�O���U��)
    private float attackIntervalD = 0.50f;//�U���Ԋu(�����U��)

    private float invincibleTimer = 0.0f;//���G�^�C�}�[
    private float invincible = 10.0f;    //���G�p������

    //�r���[�|�[�g���W�ϐ�
    private float viewPointX, viewPointY;//�r���[�|�C���g���W.X, Y
    //�_���[�W�֌W�ϐ�
    private float blinkingTime = 1.0f;     //�_�ŁE���G�̎�������
    private float rendererSwitch = 0.05f;  //Renderer�̗L���E������؂�ւ��鎞��(�_�ł̐؂�ւ��鎞��)
    private float rendererElapsedTime;     //Renderer�̗L���E�����̌o�ߎ���(�_�ł̌o�ߎ���)
    private float rendererTotalElapsedTime;//Renderer�̗L���E�����̍��v�o�ߎ���
    private bool isDamage;                 //�_���[�W���󂯂Ă��邩�̃t���O
    private bool isObjRenderer;            //objRenderer�̗L���E�����t���O
    //�R���|�[�l���g�擾�ϐ�
    private DamageManager damageManager;
    private Rigidbody rigidBody;     //Rigidbody�ϐ�
    private BoxCollider boxCollider; //CapsuleCollider�ϐ�
    private Animator animator = null;//Animator�ϐ�
    private Renderer[] objRenderer;  //Renderer�z��ϐ�
    //�R���[�`���ϐ�
    private Coroutine blinking;//

    //�}�E�X���W
    private Vector3 mousePosition, worldPosition;

    void Awake()
    {
        if (GameManager.gameStart == false)
        {
            SetPlayerStatus();//�֐�"SetPlayerStatus"���Ăяo��
        }

        objRenderer = this.gameObject.GetComponentsInChildren<Renderer>();//���̃I�u�W�F�N�g��Renderer(�q�I�u�W�F�N�g���܂�)���擾
    }

    // Start is called before the first frame update
    void Start()
    {
        damageManager = GetComponent<DamageManager>();//
        rigidBody = this.gameObject.GetComponent<Rigidbody>();      //���̃I�u�W�F�N�g��Rigidbody���擾
        boxCollider = this.gameObject.GetComponent<BoxCollider>();  //���̃I�u�W�F�N�g��BoxCollider���擾
        animator = this.GetComponent<Animator>();                   //���̃I�u�W�F�N�g��Animator���擾
        gage = false;
        playerStatus = "Normal";
    }

    // Update is called once per frame
    void Update()
    {
        //�N�[���^�C����Time.deltaTime�𑫂�
        intervalTimerF += Time.deltaTime;
        intervalTimerD += Time.deltaTime;

        if(hp > 0)
        {
            Behavior();//�֐�PlayControl���Ăяo��
        }

        //�ړ���̃r���[�|�[�g���W�l���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//���X���W
        viewPointY = Camera.main.WorldToViewportPoint(this.transform.position).y;//���Y���W
    }

    //�X�e�[�^�X��ݒ�
    void SetPlayerStatus()
    {
        //�X�Y��
        if (GameManager.playerSelect == "Sparrow")
        {
            hp = PlayerStatus.Sparrow.hp;      //�̗�
            speed = PlayerStatus.Sparrow.speed;//�ړ����x
        }
        //�J���X
        else if(GameManager.playerSelect == "Crow")
        {

        }
        //
        else if (GameManager.playerSelect == "Chickadee")
        {

        }
        //
        else if (GameManager.playerSelect == "Penguin")
        {

        }
        //����ȊO"�f�o�b�N�p"(�����G�ɃX�Y���̃X�e�[�^�X���Q�Ƃ���)
        else
        {
            GameManager.playerSelect = PlayerStatus.Sparrow.name;//
            hp = PlayerStatus.Sparrow.hp;                        //�̗�
            speed = PlayerStatus.Sparrow.speed;                  //�ړ����x
        }
        GameManager.remain = 3;
    }

    //����֐�
    void Behavior()
    {
        //�}�E�X���W���擾���āA�X�N���[�����W�����[���h���W�ɕϊ�����
        worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 7.0f));

        //
        if(Stage.gameStatus == "Play")
        {
            this.transform.position = worldPosition;
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

        //�O���U��
        if (Input.GetMouseButton(0) && intervalTimerF > attackIntervalF)
        {
            Instantiate(forwardBullet, this.transform.position, Quaternion.identity);
            intervalTimerF = 0.0f;
        }
        //�����U��
        if (Input.GetMouseButton(1) && intervalTimerD > attackIntervalD)
        {
            Instantiate(downBullet, this.transform.position, Quaternion.identity);
            intervalTimerD = 0.0f;
        }
        //
        if (Input.GetKeyDown(KeyCode.E) && gage == true)
        {
            gage = false;
            playerStatus = "Invincible";
            Instantiate(aaa, this.transform.position, Quaternion.identity);
        }
        //
        if (Input.GetKeyDown(KeyCode.Escape) && Stage.gameStatus == "Play")
        {
            Stage.gameStatus = "Menu";
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Stage.gameStatus == "Menu")
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
        if (isDamage)
        {
            return;
        }

        //damageManager.PlayerDamage();

        hp -= 1;//�̗͂�-1����

        //�̗͂�0�ȉ���������
        if (hp <= 0)
        {
            hp = 0;                         //
            boxCollider.enabled = false;    //BoxCollider�𖳌�������
            animator.SetBool("Death", true);//Animator��Death(���S���[�V����)��L��������
            rigidBody.useGravity = true;    //RigidBody�̏d�͂�L��������
        }
        StartCoroutine("Blinking");//�R���[�`��(Blinking)���Ăяo��
    }

    IEnumerator Blinking()
    {
        isDamage = true;

        rendererTotalElapsedTime = 0;
        rendererElapsedTime = 0;

        while (true)
        {
            rendererTotalElapsedTime += Time.deltaTime;
            rendererElapsedTime += Time.deltaTime;
            if (rendererSwitch <= rendererElapsedTime)
            {
                rendererElapsedTime = 0;       //��_���[�W�_�ŏ���
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

    //�Փ˔���(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�^�OEnemy�̕t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Enemy" && playerStatus == "Normal")
        {
            Damage();//�֐�Damage���Ăяo��
        }
    }
}