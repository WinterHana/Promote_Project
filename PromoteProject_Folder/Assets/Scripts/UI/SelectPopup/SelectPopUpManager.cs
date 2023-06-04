using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectPopUpManager : MonoBehaviour
{
    [Header("�ڽ� ������Ʈ ����")]
    public GameObject panel;
    public TextMeshProUGUI text;
    public Button accept;
    public Button cancel;
    [Header("���� ���� ����")]
    public bool sign;           // �� �ƴϿ� ���� ���� ����
    public bool isSelect;
    [Header("����â ȿ���� ���� ����")]
    public AudioSource openAudio;
    public AudioSource yesAudio;
    public AudioSource noAudio;

    Dictionary<int, string> dic;    // �˾�â�� �� ���� �Ľ��ؼ� ����

    public delegate void YesNoCallBack();
    public event YesNoCallBack yesCallBack;
    public event YesNoCallBack noCallBack;


    #region �̱���


    private static SelectPopUpManager _instance = null;

    public static SelectPopUpManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (SelectPopUpManager)FindObjectOfType(typeof(SelectPopUpManager));
                
                if (_instance == null)
                {
                    Debug.Log("No instnace of SelectPopUpManager");
                }
            }
            return _instance;
        }
    }
    void Awake()
    {
        
        if (_instance != null && _instance != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            _instance = this;
            dic = SelectPopParser.parser();
            // DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        isSelect = false;
    }

    private void Update()
    {
        if (isSelect) {
            panel.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClosePopUp();     // escŰ ������ ����
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnYes();
            }
        } 
        else panel.SetActive(false);
    }

    public void OpenPopUp(int num)
    {
        sign = false;
        isSelect = true;
        text.text = dic[num];
        openAudio.Play();

        SetYesCallBack(() =>
        {
            sign = true;
            yesAudio.Play();
            ClosePopUp();
        });

        SetNoCallBack(() =>
        {
            noAudio.Play();
            ClosePopUp();
        });
    }

    public void SetYesCallBack(YesNoCallBack listener)
    {
        yesCallBack += listener;
    }

    public void SetNoCallBack(YesNoCallBack listener)
    {
        noCallBack += listener;
    }

    public void OnYes()
    {
        yesCallBack?.Invoke();
    }

    public void OnNo()
    {
        noCallBack?.Invoke();
    }

    public void ClosePopUp()
    {
        isSelect = false;
    }
}

