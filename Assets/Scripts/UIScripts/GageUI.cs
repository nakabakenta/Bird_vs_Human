using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GageUI : MonoBehaviour
{
    //����
    private float gageTimer = 0.0f;   //�Q�[�W�^�C�}�[
    private float gageInterval = 0.5f;//�Q�[�W��������Ԋu
    //�R���|�[�l���g
    private Slider gage;//Slider(�Q�[�W)

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
        //�Q�[�W�^�C�}�[��Time.deltaTime�𑫂�
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
