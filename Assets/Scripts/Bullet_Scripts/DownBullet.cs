using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownBullet : BulletBase
{
    // Update is called once per frame
    void Update()
    {
        BaseUpdate();

        this.transform.position -= moveSpeed * transform.up * Time.deltaTime;//
    }
}
