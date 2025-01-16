using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //����
    public static bool buttonSelect;//�{�^������������
    private bool setActive;         //�I�u�W�F�N�g�\���̉�
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject buttonAlpha;      //"GameObject(��I��)"
    public AudioClip cursor;            //"AudioClip(�J�[�\��)"
    public AudioClip select;            //"AudioClip(�I��)"
    private Button button;              //"Button" 
    private RectTransform rectTransform;//"RectTransform"
    private AudioSource audioSource;    //"AudioSource"
    private SceneLoader sceneLoader;    //"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        button = this.GetComponent<Button>();
        rectTransform = this.GetComponent<RectTransform>();
        audioSource = this.GetComponent<AudioSource>();
        sceneLoader = this.GetComponent<SceneLoader>();
        //������������
        buttonSelect = false;

        rectTransform.anchoredPosition = new Vector2(-400, rectTransform.anchoredPosition.y);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x - titleUIClass.speed * Time.deltaTime, rectTransform.anchoredPosition.y);
        rectTransform.anchoredPosition = new Vector2(-400, rectTransform.anchoredPosition.y);

        //�{�^����"�����Ă��Ȃ�"�ꍇ
        if (buttonSelect == false)
        {
            buttonAlpha.SetActive(false);
            audioSource.PlayOneShot(cursor);//"select(�J�[�\��)"��炷

            //�X�e�[�W1
            if (button.gameObject.name == "Button_Stage1")
            {
                Stage.nowStage = 1;//"Stage"��"nowStage"��"1"�ɂ���
            }
            //�X�e�[�W2
            else if (button.gameObject.name == "Button_Stage2")
            {
                Stage.nowStage = 2;//"Stage"��"nowStage"��"2"�ɂ���
            }
            //�X�e�[�W3
            else if (button.gameObject.name == "Button_Stage3")
            {
                Stage.nowStage = 3;//"Stage"��"nowStage"��"3"�ɂ���
            }
            //�X�e�[�W4
            else if (button.gameObject.name == "Button_Stage4")
            {
                Stage.nowStage = 4;//"Stage"��"nowStage"��"4"�ɂ���
            }
            //�X�e�[�W5
            else if (button.gameObject.name == "Button_Stage5")
            {
                Stage.nowStage = 5;//"Stage"��"nowStage"��"5"�ɂ���
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonSelect == false)
        {
            buttonAlpha.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //�{�^����"�����Ă��Ȃ�"�ꍇ
        if (buttonSelect == false)
        {
            buttonSelect = true;            //�{�^����"������"�ɂ���
            audioSource.PlayOneShot(select);//"select(�I��)"��炷
            Invoke("SceneLoad", 2.0f);      //�֐�"SceneLoad"��"2.0f"��Ɏ��s����
        }

        InvokeRepeating("Flash", 0.0f, 0.25f);//�֐�"Flash"��"0.0f"��Ɏ��s����"0.25f"���ɌJ��Ԃ�
    }

    //�֐�"Flash"
    void Flash()
    {
        //"setActive"��"true"�̏ꍇ��"false"�A"false"�̏ꍇ��"true"�ɂ���
        setActive = !setActive;

        //�q�I�u�W�F�N�g���擾
        foreach (Transform child in transform)
        {
            //�q�I�u�W�F�N�g�̖��O��"Alpha"�̏ꍇ
            if (child.name == "Alpha_UI_Base_02")
            {
                //�q�I�u�W�F�N�g���\���ɂ���
                child.gameObject.SetActive(false);
            }
            //����ȊO�̏ꍇ
            else
            {
                //�q�I�u�W�F�N�g��"setActive"�ɂ���
                child.gameObject.SetActive(setActive);
            }
        }
    }

    //�֐�"SceneLoad"
    void SceneLoad()
    {
        sceneLoader.StageScene();//"SceneLoader"�̊֐�"StageScene"�����s����
    }
}
