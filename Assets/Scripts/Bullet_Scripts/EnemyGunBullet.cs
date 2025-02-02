using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunBullet : BulletBase
{
    //����
    private Vector3 direction;//�I�u�W�F�N�g�̕���
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private Transform playerTransform;//"Transform(�v���C���[)"

    // Start is called before the first frame update
    void Start()
    {
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        playerTransform = GameObject.Find("Player").transform;//"Transform(�v���C���[)"

        direction = (playerTransform.position - this.transform.position).normalized;
        this.transform.rotation = Quaternion.LookRotation(direction);
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();

        this.transform.position += moveSpeed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����
    }

    //�Փ˔���(OnTriggerEnter)
    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}
