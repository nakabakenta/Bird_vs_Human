using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Title : AddBase
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

        fade[0] = true;
        PlayBGM(bgm[0]);
        GameManager.nowScene = "Title";
        //コルーチン"SmoothFlash"を実行する
        StartCoroutine("SmoothFlash");

        gameObjectFade[0].SetActive(false);
        uIStory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (start == false)
        {
            TitleTitle();
        }
        else if (start == true)
        {
            TitleStory();
        }
    }

    void TitleTitle()
    {
        //マウスを(左 || 右)クリックをしたら
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            start = true;
            uITitle.SetActive(false);
        }
    }

    void TitleStory()
    {
        uIStory.SetActive(true);

        if (audioSource.clip == bgm[0])
        {
            PlayBGM(bgm[1]);
        }

        if (rectTransform.anchoredPosition.y < 1620)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + moveSpeed * Time.deltaTime);
        }
        else if (rectTransform.anchoredPosition.y >= 1620)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 1620);
            gameObjectFade[0].SetActive(true);
            loadScene = true;
        }

        if (loadScene == true)
        {
            if (imageFade[0].color.a < 1)
            {
                Fade(0);
            }
            else if (imageFade[0].color.a >= 1)
            {
                GameManager.nextScene = "PlayerSelect";
                SceneChange();
            }
        }
        else if (loadScene == false)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                gameObjectFade[0].SetActive(true);
                loadScene = true;
            }
        }
    }
}
