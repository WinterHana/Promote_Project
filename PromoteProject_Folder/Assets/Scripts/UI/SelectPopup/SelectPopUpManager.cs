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
    public bool sign;           // 예 아니오 선택 관련 변수
    bool isSelect;       
    public int num;             // 팝업 내용을 가져옴

    Dictionary<int, string> dic;    // 팝업창에 쓸 내용 파싱해서 저장

    private static SelectPopUpManager _instance = null;

    #region 싱글톤
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
        isSelect = true;
        text.text = dic[num];
    }

    public void closePopUp()
    {
        isSelect = false;
    }

    // 버튼에 적용할 함수들
    void yes() { 
         
    }

    void no() { 
    
    }
}
