using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageUI : MonoBehaviour
{
    private TMP_Text score; //TMP_Text(�X�R�A)
    private TMP_Text remain;//TMP_Text(�c��)
    private TMP_Text hp;    //TMP_Text(�̗�)

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("SCORE").GetComponent<TMP_Text>();  //
        remain = GameObject.Find("REMAIN").GetComponent<TMP_Text>();//
        hp = GameObject.Find("HP").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (PlayerController.hp >= 10)
        {
            hp.text = "HP : " + PlayerController.hp;//
        }
        //
        else if(PlayerController.hp >= 0)
        {
            hp.text = "HP : 0" + PlayerController.hp;//
        }

        //
        if(GameManager.score >= 10000)
        {
            score.text = "SCORE : " + GameManager.score;
        }
        //
        else if(GameManager.score >= 1000)
        {
            score.text = "SCORE : 0" + GameManager.score;
        }
        //
        else if (GameManager.score >= 100)
        {
            score.text = "SCORE : 00" + GameManager.score;
        }
        //
        else if (GameManager.score >= 10)
        {
            score.text = "SCORE : 000" + GameManager.score;
        }
        //
        else if (GameManager.score >= 0)
        {
            score.text = "SCORE : 0000" + GameManager.score;
        }
    }
}