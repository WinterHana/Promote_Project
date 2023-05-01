using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/*
 * �˾� UI�� ���콺�� Ŭ���� �� �̺�Ʈ�� �߻���ų �� �ֵ��� ��ũ��Ʈ�� �ۼ��ϰ�
 * PopupUI ���ӿ�����Ʈ�� ������Ʈ�� �־��ش�.
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
