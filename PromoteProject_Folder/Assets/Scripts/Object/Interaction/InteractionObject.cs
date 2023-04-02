using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ȣ�ۿ��� �� ������Ʈ�� �� ��ũ��Ʈ�� ��ӹ޴´�.
// �÷��̾�� ��ȣ�ۿ��� �����Ǿ� ����
public abstract class InteractionObject : MonoBehaviour
{
    private PlayerInteraction player;

    public abstract void Interaction();     // ���⿡ ��ȣ�ۿ��� ������ �����Ѵ�.

    // ��ȣ�ۿ��� ��ü�� Ontrigger�� ������ �����Ѵ�.
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
