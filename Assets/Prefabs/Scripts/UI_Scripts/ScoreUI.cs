using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    private TMP_Text score; //TMP_Text(ÉXÉRÉA)

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("Text_ScoreNumber").GetComponent<TMP_Text>();//
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (GameManager.score >= 10000)
        {
            score.text = "" + GameManager.score;
        }
        //
        else if (GameManager.score >= 1000)
        {
            score.text = "0" + GameManager.score;
        }
        //
        else if (GameManager.score >= 100)
        {
            score.text = "00" + GameManager.score;
        }
        //
        else if (GameManager.score >= 10)
        {
            score.text = "000" + GameManager.score;
        }
        //
        else if (GameManager.score >= 0)
        {
            score.text = "0000" + GameManager.score;
        }
    }
}
