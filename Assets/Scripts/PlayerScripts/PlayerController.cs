using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�X�e�[�^�X
    public GameObject forwardBullet;//
    public GameObject downBullet;   //

    public static int hp;     //�̗�
    public static float speed;//�ړ����x
    
    private float coolTimeL = 0.25f;//�N�[���^�C��(�O���U��)
    private float coolTimeR = 0.50f;//�N�[���^�C��(�����U��)
    private float attackL = 0.25f;  //�U�����o��܂ł̊Ԋu(�O���U��)
    private float attackR = 0.50f;  //�U�����o��܂ł̊Ԋu(�����U��)
    //�r���[�|�[�g���W�ϐ�
    private float viewX;//�r���[�|�[�gX���W
    private float viewY;//�r���[�|�[�gY���W
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
    }

    // Update is called once per frame
    void Update()
    {
        //�N�[���^�C����Time.deltaTime�𑫂�
        coolTimeL += Time.deltaTime;
        coolTimeR += Time.deltaTime;

        //�֐�PlayControl���Ăяo��
        PlayControl();             

        //�ړ���̃r���[�|�[�g���W�l���擾
        viewX = Camera.main.WorldToViewportPoint(this.transform.position).x;//���X���W
        viewY = Camera.main.WorldToViewportPoint(this.transform.position).y;//���Y���W

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

    //�������
    void PlayControl()
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
            if (Input.GetMouseButton(0) && coolTimeL > attackL)
            {
                Instantiate(forwardBullet, this.transform.position, Quaternion.identity);
                coolTimeL = 0.0f;
            }
            //�����U��
            if (Input.GetMouseButton(1) && coolTimeR > attackR)
            {
                Instantiate(downBullet, this.transform.position, Quaternion.identity);
                coolTimeR = 0.0f;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                
            }
        }
    }

    //�Փ˔���(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�^�OEnemy�̕t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Enemy")
        {
            Damage();//�֐�Damage���Ăяo��
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
}