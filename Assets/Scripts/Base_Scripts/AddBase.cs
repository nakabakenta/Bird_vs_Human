using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AddBase : MonoBehaviour
{
    public float flashInterval;
    public float[] fadeSpeed;
    protected bool loadScene = false;
    protected bool setActive;
    protected bool[] fade = new bool[2];

    public GameObject []gameObjectFade;
    public Image[] imageFlash;
    public Text[] textFlash;
    public TMP_Text[] tMPTextFlash;
    public AudioClip[] bgm;
    protected Image[] imageFade = new Image[2];
    protected AudioSource audioSource;//"AudioSource"
    private Color[] colorFade = new Color[2];

    public TMP_Text score;        //TMP_Text(スコア)
    public TMP_Text remain;       //TMP_Text(残り)
    public TMP_Text hp;           //TMP_Text(体力)

    public TMP_Text resultScore; //TMP_Text(スコア)
    public TMP_Text resultRemain;//TMP_Text(残り)

    public void BaseStart()
    {
        for (int i = 0; i < gameObjectFade.Length; i++)
        {
            imageFade[i] = gameObjectFade[i].GetComponent<Image>();
        }

        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void Fade(int number)
    {
        colorFade[number] = imageFade[number].color;

        if (fade[number] == true)
        {
            colorFade[number].a += fadeSpeed[number] * Time.deltaTime;  // Alphaを増加
        }
        else if (fade[number] == false)
        {
            colorFade[number].a -= fadeSpeed[number] * Time.deltaTime;  // Alphaを増加
        }

        imageFade[number].color = colorFade[number];
    }

    public void CoarseFlash()
    {
        setActive = !setActive;
        this.gameObject.SetActive(setActive);
    }

    //コルーチン"SmoothFlash"
    public IEnumerator SmoothFlash()
    {
        while (true)
        {
            float flashTimer = 0.0f;
            while (flashTimer < flashInterval)
            {
                flashTimer += Time.deltaTime;
                float alpha = Mathf.Lerp(0, 1, flashTimer / flashInterval);

                if(imageFlash != null)
                {
                    for (int i = 0; i < imageFlash.Length; i++)
                    {
                        Color color = imageFlash[i].color;
                        color.a = alpha;
                        imageFlash[i].color = color;
                    }
                }

                if (textFlash != null)
                {
                    for (int i = 0; i < textFlash.Length; i++)
                    {
                        Color color = textFlash[i].color;
                        color.a = alpha;
                        textFlash[i].color = color;
                    }
                }

                if (tMPTextFlash != null)
                {
                    for (int i = 0; i < tMPTextFlash.Length; i++)
                    {
                        Color color = tMPTextFlash[i].color;
                        color.a = alpha;
                        tMPTextFlash[i].color = color;
                    }
                }

                yield return null;
            }

            flashTimer = 0f;

            while (flashTimer < flashInterval)
            {
                flashTimer += Time.deltaTime;
                float alpha = Mathf.Lerp(1, 0, flashTimer / flashInterval);

                if (imageFlash != null)
                {
                    for (int i = 0; i < imageFlash.Length; i++)
                    {
                        Color color = imageFlash[i].color;
                        color.a = alpha;
                        imageFlash[i].color = color;
                    }
                }

                if (textFlash != null)
                {
                    for (int i = 0; i < textFlash.Length; i++)
                    {
                        Color color = textFlash[i].color;
                        color.a = alpha;
                        textFlash[i].color = color;
                    }
                }

                if (imageFlash != null)
                {
                    for (int i = 0; i < tMPTextFlash.Length; i++)
                    {
                        Color color = tMPTextFlash[i].color;
                        color.a = alpha;
                        tMPTextFlash[i].color = color;
                    }
                }

                yield return null;
            }
        }
    }

    public void ResultScore()
    {
        //
        if (GameManager.score >= 10000)
        {
            resultScore.text = "" + GameManager.score;
        }
        //
        else if (GameManager.score >= 1000)
        {
            resultScore.text = "0" + GameManager.score;
        }
        //
        else if (GameManager.score >= 100)
        {
            resultScore.text = "00" + GameManager.score;
        }
        //
        else if (GameManager.score >= 10)
        {
            resultScore.text = "000" + GameManager.score;
        }
        //
        else if (GameManager.score >= 0)
        {
            resultScore.text = "0000" + GameManager.score;
        }
    }

    public void PlayBGM(AudioClip bgm)
    {
        audioSource.clip = bgm;
        audioSource.Play();
    }

    public void LoadScene()
    {
        Time.timeScale = 1;

        if (GameManager.nextScene == "Stage1")
        {
            Stage.nowStage = 1;
        }
        else if (GameManager.nextScene == "Stage2")
        {
            Stage.nowStage = 2;
        }
        else if (GameManager.nextScene == "Stage3")
        {
            Stage.nowStage = 3;
        }
        else if (GameManager.nextScene == "Stage4")
        {
            Stage.nowStage = 4;
        }
        else if (GameManager.nextScene == "Stage5")
        {
            Stage.nowStage = 5;
        }

        SceneManager.LoadScene(GameManager.nextScene);
        GameManager.nowScene = GameManager.nextScene;
    }

    public static class Scene
    {
        public enum Name
        {
            Title,
            Stage1,
            Stage2,
            Stage3,
            Stage4,
            Stage5,
        }
    }
}