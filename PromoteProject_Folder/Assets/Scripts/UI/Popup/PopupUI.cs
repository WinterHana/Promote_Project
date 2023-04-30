using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * 팝업 UI를 마우스로 클릭할 때 이벤트를 발생시킬 수 있도록 스크립트를 작성하고
 * PopupUI 게임오브젝트에 컴포넌트에 넣어준다.
 */

public class PopupUI : MonoBehaviour, IPointerDownHandler
{
    public Button _closeButton;
    public event Action OnFocus;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        OnFocus();
    }
}
