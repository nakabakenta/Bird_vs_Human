using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletBase
{
    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
        direction = (playerTransform.position - this.transform.position).normalized;
        this.transform.rotation = Quaternion.LookRotation(direction);
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();

        this.transform.position += moveSpeed * transform.forward * Time.deltaTime;//‘O•ûŒü‚ÉˆÚ“®‚·‚é
    }

    //Õ“Ë”»’è(OnTriggerEnter)
    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}
