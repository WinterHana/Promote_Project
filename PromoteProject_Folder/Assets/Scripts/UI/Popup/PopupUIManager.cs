using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Popup UI Container 또는 빈 게임오브젝트에 컴포넌트로 넣어주며, 싱글톤으로 작성하는 것이 좋다.

링크드리스트를 통해 현재 활성화된 팝업들을 관리한다

각 팝업이 열릴 때는 링크드리스트의 가장 앞에 삽입하며, 닫힐 때는 링크드리스트에서 제거한다.

ESC 키를 누를 경우 링크드리스트의 첫 번째 팝업을 닫는다.

각 팝업에 지정된 단축키를 누를 경우 해당 팝업 열거나 닫는다.

각 팝업을 마우스로 클릭할 경우 링크드리스트의 가장 앞에 오도록 한다.

팝업의 상태가 변경될 때마다 전체 팝업들의 정렬 순서를 링크드리스트의 순서대로 변경시킨다.
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
    /// <summary> 실시간 팝업 관리 링크드 리스트 </summary>
    private LinkedList<PopupUI> _activePopupLList;

    /// <summary> 전체 팝업 목록 </summary>
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
        // ESC 누를 경우 링크드리스트의 First 닫기
        if (Input.GetKeyDown(_escapeKey))
        {
            if (_activePopupLList.Count > 0)
            {
                ClosePopup(_activePopupLList.First.Value);
            }
        }

        // 단축키 조작
        ToggleKeyDownAction(_charInfoKey, _characterInfoPopup);
    }

    /***********************************************************************
    *                               Private Methods
    ***********************************************************************/
    private void Init()
    {
        // 1. 리스트 초기화
        _allPopupList = new List<PopupUI>()
        {
           _characterInfoPopup
        };

        // 2. 모든 팝업에 이벤트 등록
        foreach (var popup in _allPopupList)
        {
            // 헤더 포커스 이벤트
            popup.OnFocus += () =>
            {
                _activePopupLList.Remove(popup);
                _activePopupLList.AddFirst(popup);
                RefreshAllPopupDepth();
            };

            // 닫기 버튼 이벤트
            popup._closeButton.onClick.AddListener(() => ClosePopup(popup));
        }
    }

    /// <summary> 시작 시 모든 팝업 닫기 </summary>
    private void InitCloseAll()
    {
        foreach (var popup in _allPopupList)
        {
            ClosePopup(popup);
        }
    }

    /// <summary> 단축키 입력에 따라 팝업 열거나 닫기 </summary>
    private void ToggleKeyDownAction(in KeyCode key, PopupUI popup)
    {
        if (Input.GetKeyDown(key))
            ToggleOpenClosePopup(popup);
    }

    /// <summary> 팝업의 상태(opened/closed)에 따라 열거나 닫기 </summary>
    private void ToggleOpenClosePopup(PopupUI popup)
    {
        if (!popup.gameObject.activeSelf) OpenPopup(popup);
        else ClosePopup(popup);
    }

    /// <summary> 팝업을 열고 링크드리스트의 상단에 추가 </summary>
    private void OpenPopup(PopupUI popup)
    {
        _activePopupLList.AddFirst(popup);
        popup.gameObject.SetActive(true);
        RefreshAllPopupDepth();
    }

    /// <summary> 팝업을 닫고 링크드리스트에서 제거 </summary>
    private void ClosePopup(PopupUI popup)
    {
        _activePopupLList.Remove(popup);
        popup.gameObject.SetActive(false);
        RefreshAllPopupDepth();
    }

    /// <summary> 링크드리스트 내 모든 팝업의 자식 순서 재배치 </summary>
    private void RefreshAllPopupDepth()
    {
        foreach (var popup in _activePopupLList)
        {
            popup.transform.SetAsFirstSibling();
        }
    }
}
