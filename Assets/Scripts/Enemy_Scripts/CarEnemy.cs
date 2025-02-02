using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnemy : EnemyBase
{
    //����
    private bool carExit = false;//
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject enemy;     //"GameObject(�G)"
    public GameObject effect;    //"GameObject(�G�t�F�N�g)"
    public AudioClip brake;      //"AudioClip(�u���[�L)"
    public AudioClip horn;       //"AudioClip(�N���N�V����)"

    // Start is called before the first frame update
    void Start()
    {
        enemyType = Enemy.EnemyType.Vehicle.ToString();   //�G�̌^
        enemyOption = Enemy.EnemyOption.Normal.ToString();//
        carExit = false;
        //�֐������s����
        GetComponent();//�R���|�[�l���g����������
        Direction();
    }                                   

    // Update is called once per frame
    void Update()
    {
        UpdateEnemy();
    }

    public override void Action()
    {
        if(rotation == (int)Characte.Direction.Vertical)
        {
            if (this.transform.position.z <= playerTransform.position.z && carExit == false)
            {
                Instantiate(enemy, this.transform.position, this.transform.rotation);
                audioSource.PlayOneShot(horn);
                audioSource.PlayOneShot(brake);
                carExit = true;
            }
            else if (this.transform.position.z > playerTransform.position.z)
            {
                Move();
            }
        }
        else if(rotation == -(int)Characte.Direction.Horizontal)
        {
            Move();
        }
    }

    //�֐�"Death"
    public override void DeathEnemy()
    {
        base.DeathEnemy();

        Instantiate(effect, this.transform.position, this.transform.rotation, thisTransform);
    }
}
