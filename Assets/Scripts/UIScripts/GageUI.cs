using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GageUI : MonoBehaviour
{
    //処理
    private float gageTimer = 0.0f;   //ゲージタイマー
    private float gageInterval = 0.5f;//ゲージが増える間隔
    //コンポーネント
    private Slider gage;//Slider(ゲージ)

    // Start is called before the first frame update
    void Start()
    {
        gage = GameObject.Find("Gage").GetComponent<Slider>();//

        gage.value = 0;    //
        gage.minValue = 0; //
        gage.maxValue = 20;//
    }

    // Update is called once per frame
    void Update()
    {
        //ゲージタイマーにTime.deltaTimeを足す
        gageTimer += Time.deltaTime;

        if(gage.value < gage.maxValue && gageTimer > gageInterval && PlayerController.playerStatus == "Normal")
        {
            gage.value++;
            gageTimer = 0.0f;
        }
        else if(gage.value == gage.maxValue && PlayerController.playerStatus == "Normal")
        {
            PlayerController.useGage = true;
        }
        else if(PlayerController.playerStatus == "Invincible")
        {
            gage.value = 0;
        }
    }
}
