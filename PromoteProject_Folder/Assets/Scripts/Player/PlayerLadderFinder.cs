using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadderFinder : MonoBehaviour
{
    public bool findLadder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ladder"))
        {
            findLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ladder"))
        {
            findLadder = false;
        }
    }

}
