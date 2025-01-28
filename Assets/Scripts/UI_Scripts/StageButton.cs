using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageButton : ButtonBase, IPointerEnterHandler, IPointerClickHandler
{
    public static string selectButton;//�I�����Ă���{�^��
    public static bool buttonClick;   //�{�^���̃N���b�N��

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        buttonClick = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //�{�^����"�N���b�N���Ă��Ȃ�"�ꍇ
        if (buttonClick == false)
        {
            if (selectButton != null)
            {
                //�I�����Ă���{�^����T��
                Transform findAlpha = GameObject.Find(selectButton).transform.Find("Alpha_UI_Base_64_03");
                Transform findSelectMark = GameObject.Find(selectButton).transform.Find("UI_Base_64_04");
                //�I�����Ă���{�^����T���ăR���|�[�l���g���擾
                RectTransform findRectTransform = GameObject.Find(selectButton).GetComponent<RectTransform>();
                //�I�����Ă���{�^����������
                findRectTransform.anchoredPosition = new Vector2(buttonPosition.x, findRectTransform.anchoredPosition.y);
                findAlpha.gameObject.SetActive(true);
                findSelectMark.gameObject.SetActive(false);
            }

            selectButton = button.gameObject.name;//�I�����Ă���{�^���̖��O������

            if(selectButton == "Button_Restart")
            {
                GameManager.nextScene = GameManager.nowScene;
            }
            else if (selectButton == "Button_BackToMenu")
            {
                GameManager.nextScene = "PlayerSelect";
            }
            else if (selectButton == "Button_NextStage")
            {
                if(GameManager.nowScene == "Stage1")
                {
                    GameManager.nextScene = "Stage2";
                }
                else if (GameManager.nowScene == "Stage2")
                {
                    GameManager.nextScene = "Stage3";
                }
                else if (GameManager.nowScene == "Stage3")
                {
                    GameManager.nextScene = "Stage4";
                }
                else if (GameManager.nowScene == "Stage4")
                {
                    GameManager.nextScene = "Stage5";
                }
            }
            else if(selectButton == "Button_GameClear")
            {
                GameManager.nextScene = "GameClear";
            }

            EnterButton();//�֐�"EnterButton"�����s����
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //�{�^����"�N���b�N���Ă��Ȃ�"�ꍇ
        if (buttonClick == false)
        {
            buttonClick = true;//�{�^����"�N���b�N����"�ɂ���
            ClickButton();     //�֐�"ClickButton"�����s����
        }
    }
}
