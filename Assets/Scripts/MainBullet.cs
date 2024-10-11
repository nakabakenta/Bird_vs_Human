using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBullet : MonoBehaviour
{
    public int speed;//íeÇÃë¨ìx

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += speed * transform.forward * Time.deltaTime;
    }

    //âÊñ äOÇ…èoÇΩÇÁè¡Ç∑
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
