using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectPopUpManager : MonoBehaviour
{
    [Header("자식 오브젝트 삽입")]
    public GameObject panel;
    public TextMeshProUGUI text;
    public Button accept;
    public Button cancel;

    public bool sign;           // 예 아니오 선택 관련 변수
    public bool isSelect;       

    Dictionary<int, string> dic;    // 팝업창에 쓸 내용 파싱해서 저장

    public delegate void YesNoCallBack();
    public event YesNoCallBack yesCallBack;
    public event YesNoCallBack noCallBack;


    #region 싱글톤


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

