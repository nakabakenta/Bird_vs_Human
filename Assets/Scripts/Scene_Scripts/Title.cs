using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Title : CustomBase
{
    public float moveSpeed;
    public GameObject uITitle;
    public GameObject uIStory;
    public RectTransform rectTransform;//"RectTransform"
    private bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        BaseStart();

        PlayBGM(bgm[0]);

        blackout = true;
        GameManager.nowScene = "Title";
        //コルーチン"SmoothFlash"を実行する
        StartCoroutine("SmoothFlash");

        gameObjectBlackout.SetActive(false);
        uIStory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(start == false)
        {
            //マウスを(左 || 右)クリックをしたら
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                start = true;
                uITitle.SetActive(false);
            }
        }
        else if(start == true)
        {
            Story();
        }
    }

    void Story()
    {
        if(audioSource.clip == bgm[0])
        {
            PlayBGM(bgm[1]);
        }


        uIStory.SetActive(true);

        if (rectTransform.anchoredPosition.y < 1620)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + moveSpeed * Time.deltaTime);
        }
        else if(rectTransform.anchoredPosition.y >= 1620)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 540);
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            gameObjectBlackout.SetActive(true);
            loadScene = true;
        }

        if(loadScene == true)
        {
            if (imageBlackout.color.a < 1)
            {
                Blackout();
            }
            else if (imageBlackout.color.a >= 1)
            {
                GameManager.nextScene = "PlayerSelect";
                LoadScene();
            }
        }
    }
}
