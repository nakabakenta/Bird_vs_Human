using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject playerUI;                    //�v���C���[
    public GameObject[] player = new GameObject[3];//�v���C���[
    public GameObject[] nameUI = new GameObject[3];//�v���C���[
    public GameObject playerSelectUI;              //UI�v���C���[�Z���N�g
    public GameObject playerInformationUI;
    public Slider[] statusUI = new Slider[3];
    private float sliderSpeed = 5.0f;             //�X���C�_�[�̑��x

    // Start is called before the first frame update
    void Start()
    {
        GameManager.selectPlayer = -1;
        GameManager.playBegin = false;
        playerUI.SetActive(false);
        playerInformationUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerSelectButton.buttonClick == true)
        {
            playerSelectUI.SetActive(false);
        }

        if (GameManager.selectPlayer != -1)
        {
            playerUI.SetActive(true);
            playerInformationUI.SetActive(true);

            for (int i = 0; i < 3; i++)
            {
                if (i == GameManager.selectPlayer)
                {
                    player[i].SetActive(true);
                    nameUI[i].SetActive(true);
                }
                else
                {
                    player[i].SetActive(false);
                    nameUI[i].SetActive(false);
                }
            }

            if (statusUI[0].value != PlayerBase.Player.hp[GameManager.selectPlayer])
            {
                if (statusUI[0].value < PlayerBase.Player.hp[GameManager.selectPlayer])
                {
                    statusUI[0].value += sliderSpeed * Time.deltaTime;
                }
                if (statusUI[0].value > PlayerBase.Player.hp[GameManager.selectPlayer])
                {
                    statusUI[0].value -= sliderSpeed * Time.deltaTime;
                }
            }

            if (statusUI[1].value != PlayerBase.Player.attackPower[GameManager.selectPlayer])
            {
                if (statusUI[1].value < PlayerBase.Player.attackPower[GameManager.selectPlayer])
                {
                    statusUI[1].value += sliderSpeed * Time.deltaTime;
                }
                if (statusUI[1].value > PlayerBase.Player.attackPower[GameManager.selectPlayer])
                {
                    statusUI[1].value -= sliderSpeed * Time.deltaTime;
                }
            }

            if (statusUI[2].value != PlayerBase.Player.attackSpeed[GameManager.selectPlayer])
            {
                if (statusUI[2].value < PlayerBase.Player.attackSpeed[GameManager.selectPlayer])
                {
                    statusUI[2].value += sliderSpeed * Time.deltaTime;
                }
                if (statusUI[2].value > PlayerBase.Player.attackSpeed[GameManager.selectPlayer])
                {
                    statusUI[2].value -= sliderSpeed * Time.deltaTime;
                }
            }
        }
    }
}
