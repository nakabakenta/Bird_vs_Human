using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageButton : ButtonBase, IPointerEnterHandler, IPointerClickHandler
{
    public static string selectButton;//�I�����Ă���{�^��
    public static bool buttonClick;   //�{�^���̃N���b�N��

    public Vector2 ButtonPosition
    {
        get { return buttonPosition; }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        moveButton = true;
        selectButton = null;
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
                Transform selectButtonAlpha = GameObject.Find(selectButton).transform.Find("Alpha_UI_Base_64_03");
                Transform selectButtonMark = GameObject.Find(selectButton).transform.Find("UI_Base_64_04");
                //�I�����Ă���{�^����T���ăR���|�[�l���g���擾
                RectTransform selectButtonRectTransform = GameObject.Find(selectButton).GetComponent<RectTransform>();
                //�I�����Ă���{�^����������
                selectButtonRectTransform.anchoredPosition = new Vector2(buttonPosition.x, selectButtonRectTransform.anchoredPosition.y);
                selectButtonAlpha.gameObject.SetActive(true);
                selectButtonMark.gameObject.SetActive(false);
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
