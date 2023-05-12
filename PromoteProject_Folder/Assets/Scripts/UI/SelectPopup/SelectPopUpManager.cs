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
    public bool sign;           // �� �ƴϿ� ���� ���� ����
    bool isSelect;       
    public int num;             // �˾� ������ ������

    Dictionary<int, string> dic;    // �˾�â�� �� ���� �Ľ��ؼ� ����

    private static SelectPopUpManager _instance = null;

    #region �̱���
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

    // ��ư�� ������ �Լ���
    void yes() { 
         
    }

    void no() { 
    
    }
}
