using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterJetEnemy : EnemyBase
{
    //����
    private float attackTimer = 0.5f;   //�U���Ԋu�^�C�}�[
    private float attackInterval = 0.5f;//�U���Ԋu
    private float bulletRotation;       //�e�̉�]
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject bullet;       //"GameObject(�e)"
    public GameObject effect;       //"GameObject(�G�t�F�N�g)"
    public AudioClip explosion;     //"AudioClip(����)"

    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ�
        enemyType = EnemyType.Vehicle.ToString();//�G�̌^
        hp = EnemyList.FighterJetEnemy.hp;      //�̗�
        speed = EnemyList.FighterJetEnemy.speed;//�ړ����x

        //�֐������s����
        GetComponent();//�R���|�[�l���g����������
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemy();
    }

    //�֐�"Action"
    public override void Action()
    {
        attackTimer += Time.deltaTime;//�U���Ԋu��"Time.deltaTime(�o�ߎ���)"�𑫂�

        if (viewPortPosition.x < -0.5)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, EnemyList.rotation, this.transform.rotation.z);
            bulletRotation = EnemyList.rotation;
        }
        else if (viewPortPosition.x > 1.5)
        {
            this.transform.position = new Vector3(this.transform.position.x, playerTransform.position.y, this.transform.position.z);
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, -EnemyList.rotation, this.transform.rotation.z);
            bulletRotation = -EnemyList.rotation;
        }

        this.transform.position += speed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����

        if (attackTimer > attackInterval)
        {
            Instantiate(bullet, this.transform.position, Quaternion.Euler(this.transform.rotation.x, bulletRotation, this.transform.rotation.z));
            attackTimer = 0.0f;
        }
    }

    public override void DeathEnemy()
    {
        base.DeathEnemy();
        //
        Instantiate(effect, this.transform.position, this.transform.rotation, thisTransform);
        audioSource.PlayOneShot(explosion);                                                  //"explosion"��炷
        Invoke("Destroy", 1.0f);                                                             //�֐�"Destroy"��"5.0f"��Ɏ��s
    }

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}
