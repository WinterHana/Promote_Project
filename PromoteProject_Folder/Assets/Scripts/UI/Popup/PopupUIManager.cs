using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Popup UI Container �Ǵ� �� ���ӿ�����Ʈ�� ������Ʈ�� �־��ָ�, �̱������� �ۼ��ϴ� ���� ����.

��ũ�帮��Ʈ�� ���� ���� Ȱ��ȭ�� �˾����� �����Ѵ�

�� �˾��� ���� ���� ��ũ�帮��Ʈ�� ���� �տ� �����ϸ�, ���� ���� ��ũ�帮��Ʈ���� �����Ѵ�.

ESC Ű�� ���� ��� ��ũ�帮��Ʈ�� ù ��° �˾��� �ݴ´�.

�� �˾��� ������ ����Ű�� ���� ��� �ش� �˾� ���ų� �ݴ´�.

�� �˾��� ���콺�� Ŭ���� ��� ��ũ�帮��Ʈ�� ���� �տ� ������ �Ѵ�.

�˾��� ���°� ����� ������ ��ü �˾����� ���� ������ ��ũ�帮��Ʈ�� ������� �����Ų��.
 */

public class PopupUIManager : MonoBehaviour
{
    /***********************************************************************
    *                               Public Fields
    ***********************************************************************/
    public PopupUI _characterInfoPopup;

    [Space]
    public KeyCode _escapeKey = KeyCode.Escape;
    public KeyCode _charInfoKey = KeyCode.C;

    /***********************************************************************
    *                               Private Fields
    ***********************************************************************/
    /// <summary> �ǽð� �˾� ���� ��ũ�� ����Ʈ </summary>
    private LinkedList<PopupUI> _activePopupLList;

    /// <summary> ��ü �˾� ��� </summary>
    private List<PopupUI> _allPopupList;

    /***********************************************************************
    *                               Unity Callbacks
    ***********************************************************************/
    private void Awake()
    {
        _activePopupLList = new LinkedList<PopupUI>();
        Init();
        InitCloseAll();
    }

    private void Update()
    {
        // ESC ���� ��� ��ũ�帮��Ʈ�� First �ݱ�
        if (Input.GetKeyDown(_escapeKey))
        {
            if (_activePopupLList.Count > 0)
            {
                ClosePopup(_activePopupLList.First.Value);
            }
        }

        // ����Ű ����
        ToggleKeyDownAction(_charInfoKey, _characterInfoPopup);
    }

    /***********************************************************************
    *                               Private Methods
    ***********************************************************************/
    private void Init()
    {
        // 1. ����Ʈ �ʱ�ȭ
        _allPopupList = new List<PopupUI>()
        {
           _characterInfoPopup
        };

        // 2. ��� �˾��� �̺�Ʈ ���
        foreach (var popup in _allPopupList)
        {
            // ��� ��Ŀ�� �̺�Ʈ
            popup.OnFocus += () =>
            {
                _activePopupLList.Remove(popup);
                _activePopupLList.AddFirst(popup);
                RefreshAllPopupDepth();
            };

            // �ݱ� ��ư �̺�Ʈ
            popup._closeButton.onClick.AddListener(() => ClosePopup(popup));
        }
    }

    /// <summary> ���� �� ��� �˾� �ݱ� </summary>
    private void InitCloseAll()
    {
        foreach (var popup in _allPopupList)
        {
            ClosePopup(popup);
        }
    }

    /// <summary> ����Ű �Է¿� ���� �˾� ���ų� �ݱ� </summary>
    private void ToggleKeyDownAction(in KeyCode key, PopupUI popup)
    {
        if (Input.GetKeyDown(key))
            ToggleOpenClosePopup(popup);
    }

    /// <summary> �˾��� ����(opened/closed)�� ���� ���ų� �ݱ� </summary>
    private void ToggleOpenClosePopup(PopupUI popup)
    {
        if (!popup.gameObject.activeSelf) OpenPopup(popup);
        else ClosePopup(popup);
    }

    /// <summary> �˾��� ���� ��ũ�帮��Ʈ�� ��ܿ� �߰� </summary>
    private void OpenPopup(PopupUI popup)
    {
        _activePopupLList.AddFirst(popup);
        popup.gameObject.SetActive(true);
        RefreshAllPopupDepth();
    }

    /// <summary> �˾��� �ݰ� ��ũ�帮��Ʈ���� ���� </summary>
    private void ClosePopup(PopupUI popup)
    {
        _activePopupLList.Remove(popup);
        popup.gameObject.SetActive(false);
        RefreshAllPopupDepth();
    }

    /// <summary> ��ũ�帮��Ʈ �� ��� �˾��� �ڽ� ���� ���ġ </summary>
    private void RefreshAllPopupDepth()
    {
        foreach (var popup in _activePopupLList)
        {
            popup.transform.SetAsFirstSibling();
        }
    }
}
