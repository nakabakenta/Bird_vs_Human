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

    private float intervalF = 0.25f;//�Ԋu(�O���U��)
    private float intervalD = 0.50f;//�Ԋu(�����U��)
    private float attackL = 0.25f;  //�U�����o��܂ł̊Ԋu(�O���U��)
    private float attackR = 0.50f;  //�U�����o��܂ł̊Ԋu(�����U��)

    private float elapsedTime = 0.0f;
    private float invincibleTime = 10.0f;

    //�r���[�|�[�g���W�ϐ�
    private float viewPointX;//�r���[�|�C���g���W.X
    private float viewPointY;//�r���[�|�C���g���W.Y
    //�ړ��t���O�ϐ�
    private bool forward; //�O�ړ�
    private bool backward;//��ړ�
    private bool up;      //��ړ�
    private bool down;    //���ړ�
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
        intervalF += Time.deltaTime;
        intervalD += Time.deltaTime;

        if(hp > 0)
        {
            Behavior();//�֐�PlayControl���Ăяo��
        }

        //�ړ���̃r���[�|�[�g���W�l���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//���X���W
        viewPointY = Camera.main.WorldToViewportPoint(this.transform.position).y;//���Y���W

        //�ړ��\�ȉ�ʔ͈͎w��
        //-X���W
        if (viewPointX >= 0)
        {
            backward = true;
        }
        else
        {
            backward = false;
        }
        //+X���W
        if (viewPointX <= 1)
        {
            forward = true;
        }
        else
        {
            forward = false;
        }
        //-Y���W
        if (viewPointY >= 0)
        {
            down = true;
        }
        else
        {
            down = false;
        }
        //+Y���W
        if (viewPointY <= 1)
        {
            up = true;
        }
        else
        {
            up = false;
        }

        if(playerStatus == "Invincible")
        {
            Invincible();
        }
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
        //�̗͂�0���ゾ������
        if (hp > 0)
        {
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
            //�O���U��
            if (Input.GetMouseButton(0) && intervalF > attackL)
            {
                Instantiate(forwardBullet, this.transform.position, Quaternion.identity);
                intervalF = 0.0f;
            }
            //�����U��
            if (Input.GetMouseButton(1) && intervalD > attackR)
            {
                Instantiate(downBullet, this.transform.position, Quaternion.identity);
                intervalD = 0.0f;
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
        }
    }

    void SetObjRenderer(bool set)
    {
        for (int i = 0; i < objRenderer.Length; i++)
        {
            objRenderer[i].enabled = set;//Renderer��objRenderer�ɃZ�b�g����
        }
    }

    //�_���[�W����
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

    void Invincible()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime > invincibleTime)
        {
            elapsedTime = 0.0f;
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