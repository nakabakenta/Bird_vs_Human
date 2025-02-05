using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighterJetBullet : BulletBase
{
    // Update is called once per frame
    void Update()
    {
        BaseUpdate();

        this.transform.position += moveSpeed * transform.forward * Time.deltaTime;//‘O•ûŒü‚ÉˆÚ“®‚·‚é
    }
}
