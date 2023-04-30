using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public List<InteractionObject> interactionObjects;

    void Update()
    {
        // 상호작용이 일어나면 발생시키기
        if (Input.GetButtonDown("Interaction")) 
        {
            Interaction();
        }   
    }

    private void Interaction()
    {
        if (interactionObjects == null || interactionObjects.Count == 0) return;

        // List로 받아서 실행
        foreach (InteractionObject _interactionObject in interactionObjects)
        {
            _interactionObject.Interaction();
        }
    }
}
