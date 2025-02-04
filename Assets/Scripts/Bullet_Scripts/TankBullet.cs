using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBullet : BulletBase
{
    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
    }

    // Update is called once per frame
    void Update()
    {
        BaseUpdate();

        this.transform.position += moveSpeed * transform.forward * Time.deltaTime;//‘O•ûŒü‚ÉˆÚ“®‚·‚é

        if(this.transform.position.x + 0.25f > playerTransform.position.x &&
           this.transform.position.x - 0.25f < playerTransform.position.x)
        {
            Destroy();
        }
    }

    public override void Destroy()
    {
        Instantiate(effect, new Vector3(this.transform.position.x, 0.0f, this.transform.position.z), this.transform.rotation);
        base.Destroy();
    }

    //Õ“Ë”»’è(OnTriggerEnter)
    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}
