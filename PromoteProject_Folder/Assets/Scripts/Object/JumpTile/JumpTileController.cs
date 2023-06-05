using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTileController : MonoBehaviour
{
    [SerializeField] float power;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            // Debug.Log("작동하다");

            Rigidbody2D rid = collision.GetComponent<Rigidbody2D>();

            rid.AddForce(new Vector2(0, 5) * power, ForceMode2D.Impulse);
        }
    }
}
