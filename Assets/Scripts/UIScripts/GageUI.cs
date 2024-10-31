using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GageUI : MonoBehaviour
{
    private float coolTime = 0.0f;//クールタイム(ゲージ)
    private float charge = 0.5f;  //ゲージがチャージされる間隔

    private Slider gage;//Slider(ゲージ)

    // Start is called before the first frame update
    void Start()
    {
        gage = GameObject.Find("GAGE").GetComponent<Slider>();//

        gage.value = 0;    //
        gage.minValue = 0; //
        gage.maxValue = 20;//
    }

    // Update is called once per frame
    void Update()
    {
        //クールタイムにTime.deltaTimeを足す
        coolTime += Time.deltaTime;

        if(gage.value < gage.maxValue && coolTime > charge && PlayerController.status == "Normal")
        {
            gage.value++;
            coolTime = 0.0f;
        }
        else if(gage.value == gage.maxValue && PlayerController.status == "Normal")
        {
            PlayerController.useGage = true;
        }
        else if(PlayerController.status == "Invincible")
        {
            gage.value = 0;
        }
    }
}
