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

    public bool sign;           // �� �ƴϿ� ���� ���� ����
    public bool isSelect;       

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
            //DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        isSelect = false;
        dic = SelectPopParser.parser();
    }

    private void Update()
    {
        if (isSelect) panel.SetActive(true);
        else panel.SetActive(false);
    }

    public void OpenPopUp(int num)
    {
        sign = false;
        isSelect = true;
        text.text = dic[num];

        SetYesCallBack(() =>
        {
            sign = true;
            ClosePopUp();
        });

        SetNoCallBack(() =>
        {
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

