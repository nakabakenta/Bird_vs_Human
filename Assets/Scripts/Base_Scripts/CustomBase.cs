using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CustomBase : MonoBehaviour
{
    public float flashInterval;
    public float blackoutSpeed;
    protected bool loadScene = false;
    protected bool setActive;
    protected bool blackout;

    public GameObject gameObjectBlackout;
    public Image[] imageFlash;
    public TMP_Text[] tMPTextFlash;
    public AudioClip[] bgm;
    protected Image imageBlackout;
    protected AudioSource audioSource;//"AudioSource"
    private Color colorBlackout;

    public void BaseStart()
    {
        imageBlackout = gameObjectBlackout.GetComponent<Image>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void Blackout()
    {
        colorBlackout = imageBlackout.color;

        if (blackout == true)
        {
            colorBlackout.a += blackoutSpeed * Time.deltaTime;  // AlphaÇëùâ¡
        }
        else if(blackout == false)
        {
            colorBlackout.a -= blackoutSpeed * Time.deltaTime;  // AlphaÇëùâ¡
        }

        imageBlackout.color = colorBlackout;
    }

    public void CoarseFlash()
    {
        setActive = !setActive;
        this.gameObject.SetActive(setActive);
    }

    //ÉRÉãÅ[É`Éì"SmoothFlash"
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
                        Color color = imageFlash[0].color;
                        color.a = alpha;
                        imageFlash[0].color = color;
                    }
                }

                if (tMPTextFlash != null)
                {
                    for (int i = 0; i < tMPTextFlash.Length; i++)
                    {
                        Color color = tMPTextFlash[0].color;
                        color.a = alpha;
                        tMPTextFlash[0].color = color;
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
                        Color color = imageFlash[0].color;
                        color.a = alpha;
                        imageFlash[0].color = color;
                    }
                }

                if (imageFlash != null)
                {
                    for (int i = 0; i < tMPTextFlash.Length; i++)
                    {
                        Color color = tMPTextFlash[0].color;
                        color.a = alpha;
                        tMPTextFlash[0].color = color;
                    }
                }

                yield return null;
            }
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
}
