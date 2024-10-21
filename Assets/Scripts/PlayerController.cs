using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //���@�X�e�[�^�X
    public int hp;     //�̗�
    public int power;  //�U����
    public float speed;//�ړ����x

    public GameObject bullet;//

    private float coolTime = 0.2f;//�N�[���^�C��
    private float spanTime = 0.2f;//�U�����o��܂ł̊Ԋu
    //�r���[�|�[�g���W�ϐ�
    private float viewX;//�r���[�|�[�gX���W
    private float viewY;//�r���[�|�[�gY���W
    //�ړ��t���O
    private bool forward; //�O�ړ�
    private bool backward;//��ړ�
    private bool up;      //��ړ�
    private bool down;    //���ړ�
    //�_���[�W�֌W�ϐ�
    private float blinkingTime = 1.0f;//�_�ŁE���G�̎�������
    private bool isDamage;            //�_���[�W���󂯂Ă��邩�̃t���O
    private bool isObjRenderer;       //objRenderer���L�����������̃t���O

    private Rigidbody rigidBody;     //Rigidbody�ϐ�
    private BoxCollider boxCollider; //CapsuleCollider�ϐ�
    private Animator animator = null;//Animator�ϐ�
    private Renderer[] objRenderer;  //Renderer�z��

    //���Z�b�g���鎞�ׂ̈ɃR���[�`����ێ����Ă���
    Coroutine blinking;

    //�_���[�W�_�ł̍��v�o�ߎ���
    private float blinkingTotalElapsedTime;

    //�_���[�W�_�ł�Renderer�̗L���E�����؂�ւ��p�̌o�ߎ���
    float blinkingElapsedTime;

    //�_���[�W�_�ł�Renderer�̗L���E�����؂�ւ��p�̃C���^�[�o��
    float blinkingInterval = 0.05f;

    void Awake()
    {
        objRenderer = GetComponentsInChildren<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.gameObject.GetComponent<Rigidbody>();
        boxCollider = this.gameObject.GetComponent<BoxCollider>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //�N�[���^�C����Time.deltaTime�𑫂�
        coolTime += Time.deltaTime;

        PlayControl();

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
            //�U������
            if (Input.GetMouseButton(0) && coolTime > spanTime)
            {
                Instantiate(bullet, this.transform.position, Quaternion.identity);
                coolTime = 0.0f;
            }
        }
    }


    //�Փ˔���(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //
        if (collision.gameObject.tag == "Enemy")
        {
            Damage();//
        } 
    }

    void SetObjRenderer(bool b)
    {
        for (int i = 0; i < objRenderer.Length; i++)
        {
            objRenderer[i].enabled = b;
        }
    }

    void Damage()
    {
        //�_���[�W�_�Œ��͓�d�Ɏ��s���Ȃ��B
        if (isDamage)
            return;

        hp -= 1;//

        //�̗͂�0�ȉ���������
        if (hp <= 0)
        {
            boxCollider.enabled = false;    //BoxCollider�𖳌�������
            animator.SetBool("Death", true);//
            rigidBody.useGravity = true;    //RigidBody�̏d�͂�L��������
        }
        StartFlicker();
    }

    void StartFlicker()
    {
        //isDamaged�ő��d���s��h���ł���̂ŁA�����ő��d���s��e���Ȃ��Ă�OK    
        blinking = StartCoroutine(Blinking());
    }

    IEnumerator Blinking()
    {
        isDamage = true;

        blinkingTotalElapsedTime = 0;
        blinkingElapsedTime = 0;

        while (true)
        {

            blinkingTotalElapsedTime += Time.deltaTime;
            blinkingElapsedTime += Time.deltaTime;

            if (blinkingInterval <= blinkingElapsedTime)
            {
                //��_���[�W�_�ŏ���
                blinkingElapsedTime = 0;
                //Renderer�̗L���A�����̔��]
                isObjRenderer = !isObjRenderer;
                SetObjRenderer(isObjRenderer);
            }

            if (blinkingTime <= blinkingTotalElapsedTime)
            {
                //��_���[�W�_�ł̏I�����̏���
                isDamage = false;
                //�Ō�ɕK��Renderer��L��������
                isObjRenderer = true;
                SetObjRenderer(true);

                blinking = null;
                yield break;
            }

            yield return null;
        }
    }
}