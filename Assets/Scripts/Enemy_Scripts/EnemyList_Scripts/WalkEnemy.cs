using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : EnemyBase
{

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        base.StartEnemy();
        //�X�e�[�^�X��ݒ�
        hp = EnemyList.WalkEnemy.hp;                   //�̗�
        speed = EnemyList.WalkEnemy.speed;             //�ړ����x
        //
        Direction();
        //
        nowAnimationNumber = (int)HumanoidAnimation.Walk;
        nowAnimationName = HumanoidAnimation.Walk.ToString();
        AnimationPlay();//�֐�"AnimationPlay"�����s����
    }

    // Update is called once per frame
    void Update()
    {
        base.UpdateEnemy();

        Debug.Log(isAnimation);
        Debug.Log(nowAnimationNumber);

        if (isAction == true)
        {
            Direction();
        }
        else if (isAction == false)
        {
            if (viewPortPosition.x < 1)
            {
                isAction = true;
            }
        }

        //"hp > 0 && isAction == true"
        if (hp > 0 && isAction == true)
        {
            nowDirection();
        }
    }

    //�֐�"Direction"
    void Direction()
    {
        //
        if (this.transform.position.z > playerTransform.position.z + 0.5f)
        {
            nowDirection = Vertical;//
        }
        //
        if (this.transform.position.z >= playerTransform.position.z - 0.5f &&
            this.transform.position.z <= playerTransform.position.z + 0.5f)
        {
            nowDirection = Horizontal;//
        }
    }

    //�֐�"Vertical"
    void Vertical()
    {
        //
        if (isAnimation == true)
        {
            Wait();//�֐�"Wait"�����s
        }
        else if(isAnimation == false)
        {
            //
            if (PlayerController.hp > 0)
            {
                this.transform.position += speed * transform.forward * Time.deltaTime;                                                 //�O�����Ɉړ�����
                this.transform.eulerAngles = new Vector3(this.transform.rotation.x, -EnemyList.rotation * 2, this.transform.rotation.z);
            }
            else if (PlayerController.hp <= 0)
            {
                nowAnimationNumber = (int)HumanoidAnimation.Dance;
                nowAnimationName = HumanoidAnimation.Dance.ToString();
                AnimationPlay();//�֐�"AnimationPlay"�����s����
            }
        }

        this.transform.position = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);
    }

    //�֐�"Horizontal"
    void Horizontal()
    {
        //
        if (isAnimation == true)
        {
            Wait();//�֐�"Wait"�����s
        }
        else if (isAnimation == false)
        {
            //
            if (PlayerController.hp > 0)
            {
                this.transform.position += speed * transform.forward * Time.deltaTime;                                              //�O�����Ɉړ�����
                this.transform.eulerAngles = new Vector3(this.transform.rotation.x, -EnemyList.rotation, this.transform.rotation.z);
            }
            else if (PlayerController.hp <= 0)
            {
                nowAnimationNumber = (int)HumanoidAnimation.Dance;
                nowAnimationName = HumanoidAnimation.Dance.ToString();
                AnimationPlay();//�֐�"AnimationPlay"�����s����
            }
        }

        this.transform.position = new Vector3(this.transform.position.x, 0.0f, playerTransform.position.z);
    }

    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}