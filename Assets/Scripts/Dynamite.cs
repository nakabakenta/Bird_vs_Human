using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    //このオブジェクトのコンポーネント
    public GameObject effect;       //"GameObject(エフェクト)"
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 5.0f)
        {
            Instantiate(effect, new Vector3(this.transform.position.x, 0.0f, this.transform.position.z), this.transform.rotation);
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }
}
