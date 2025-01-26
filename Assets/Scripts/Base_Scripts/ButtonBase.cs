using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBase : MonoBehaviour
{
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject alpha;            //"GameObject(������)"
    public GameObject selectMark;       //"GameObject(�I���}�[�N)"
    public AudioClip enter;             //"AudioClip(����)"
    public AudioClip click;             //"AudioClip(�N���b�N)"
    public Button button;              //"Button" 
    public RectTransform rectTransform;//"RectTransform"
    public AudioSource audioSource;    //"AudioSource"
    public SceneLoader sceneLoader;    //"Script(SceneLoader)"
    //����
    public Vector2 buttonPosition;     //�{�^���̈ʒu
    public bool setActive;             //�I�u�W�F�N�g�\���̉�

    public void GetComponent()
    {
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾����
        button = this.GetComponent<Button>();
        rectTransform = this.GetComponent<RectTransform>();
        audioSource = this.GetComponent<AudioSource>();
        sceneLoader = this.GetComponent<SceneLoader>();
        //���̃I�u�W�F�N�g�̃R���|�[�l���g��������
        selectMark.SetActive(false);
        //������������
        buttonPosition = rectTransform.anchoredPosition;
    }
}
