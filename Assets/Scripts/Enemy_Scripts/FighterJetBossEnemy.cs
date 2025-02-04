using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterJetBossEnemy : EnemyBase
{
    //����
    private float bulletRotation;       //�e�̉�]
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject bullet;       //"GameObject(�e)"

    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ�
        enemyType = Enemy.EnemyType.Vehicle.ToString();//�G�̌^
        bossEnemy = true;
        //�֐������s����
        GetComponent();//�R���|�[�l���g����������
        BaseStart();   //�֐�"BaseStart"�����s����
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();
    }

    public override void BaseUpdate()
    {
        base.BaseUpdate();

        action = true;
    }

    //�֐�"Action"
    public override void Action()
    {
        attackTimer += Time.deltaTime;//�U���Ԋu��"Time.deltaTime(�o�ߎ���)"�𑫂�

        if (viewPortPosition.x < -0.5)
        {
            this.transform.position = new Vector3(this.transform.position.x, playerTransform.position.y, this.transform.position.z);
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, (int)Characte.Direction.Horizontal, this.transform.rotation.z);
            bulletRotation = (int)Characte.Direction.Horizontal;
        }
        else if (viewPortPosition.x > 1.5)
        {
            this.transform.position = new Vector3(this.transform.position.x, playerTransform.position.y, this.transform.position.z);
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, -(int)Characte.Direction.Horizontal, this.transform.rotation.z);
            bulletRotation = -(int)Characte.Direction.Horizontal;
        }

        this.transform.position += moveSpeed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����

        if (attackTimer > attackInterval)
        {
            Instantiate(bullet, this.transform.position, Quaternion.Euler(this.transform.rotation.x, bulletRotation, this.transform.rotation.z));
            attackTimer = 0.0f;
        }
    }

    public override void DeathEnemy()
    {
        bossEnemy = false;
        base.DeathEnemy();
        //
        Instantiate(effect, this.transform.position, this.transform.rotation);
        Invoke("Destroy", 1.0f);//�֐�"Destroy"��"5.0f"��Ɏ��s
    }

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}