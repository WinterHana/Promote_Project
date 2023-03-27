using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterController : MonoBehaviour
{
    [SerializeField] float power;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Test");
            if(transform.localScale.x > 0) collision.rigidbody.AddForce(new Vector2(power, 0));
            else collision.rigidbody.AddForce(new Vector2(-power, 0));

        }
    }
}
