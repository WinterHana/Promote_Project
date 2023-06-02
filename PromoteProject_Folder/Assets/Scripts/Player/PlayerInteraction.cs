using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("��ȣ �ۿ� ��ü")]
    public List<InteractionObject> interactionObjects;
    [Header("��ȣ �ۿ� ȿ���� ����")]
    public AudioSource InteractionSound;


    void Update()
    {
        // ��ȣ�ۿ��� �Ͼ�� �߻���Ű��
        if (Input.GetButtonDown("Interaction") && PlayerMove.isMove == true) 
        {
            Interaction();
        }   
    }

    private void Interaction()
    {
        if (interactionObjects == null || interactionObjects.Count == 0) return;
        InteractionSound.Play();

        // List�� �޾Ƽ� ����
        foreach (InteractionObject _interactionObject in interactionObjects)
        {
            _interactionObject.Interaction();
        }
    }
}
