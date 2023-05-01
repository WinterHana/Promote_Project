using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public List<InteractionObject> interactionObjects;

    void Update()
    {
        // ��ȣ�ۿ��� �Ͼ�� �߻���Ű��
        if (Input.GetButtonDown("Interaction")) 
        {
            Interaction();
        }   
    }

    private void Interaction()
    {
        if (interactionObjects == null || interactionObjects.Count == 0) return;

        // List�� �޾Ƽ� ����
        foreach (InteractionObject _interactionObject in interactionObjects)
        {
            _interactionObject.Interaction();
        }
    }
}
