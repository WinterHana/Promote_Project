using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("상호 작용 물체")]
    public List<InteractionObject> interactionObjects;
    [Header("상호 작용 효과음 지정")]
    public AudioSource InteractionSound;


    void Update()
    {
        // 상호작용이 일어나면 발생시키기
        if (Input.GetButtonDown("Interaction") && PlayerMove.isMove == true) 
        {
            Interaction();
        }   
    }

    private void Interaction()
    {
        if (interactionObjects == null || interactionObjects.Count == 0) return;
        InteractionSound.Play();

        // List로 받아서 실행
        foreach (InteractionObject _interactionObject in interactionObjects)
        {
            _interactionObject.Interaction();
        }
    }
}
