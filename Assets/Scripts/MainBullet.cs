using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBullet : MonoBehaviour
{
    public int speed;//�e�̑��x

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += speed * transform.forward * Time.deltaTime;
    }

    //��ʊO�ɏo�������
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
