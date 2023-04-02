using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 상호작용을 할 오브젝트는 이 스크립트를 상속받는다.
// 플레이어간의 상호작용이 구현되어 있음
public abstract class InteractionObject : MonoBehaviour
{
    private PlayerInteraction player;

    public abstract void Interaction();     // 여기에 상호작용할 내용을 구현한다.

    // 상호작용할 물체는 Ontrigger로 설정을 통일한다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            player = collision.gameObject.GetComponent<PlayerInteraction>();

            if (player != null)
            {
                player.interactionObjects.Add(this);

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.interactionObjects.Clear();
                player = null;
            }
        }
    }
}
