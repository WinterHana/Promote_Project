using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField] bool init;
    Tilemap map;
    TilemapCollider2D col;
    bool spwn;

    private void Awake()
    {
        map = GetComponent<Tilemap>();
        col = GetComponent<TilemapCollider2D>();
        if (init)
        {
            col.enabled = true;
            Color c = map.color;
            c.a = 1;
            map.color = c;
            spwn = true;
        }
        else 
        {
            col.enabled = false;
            Color c = map.color;
            c.a = 0;
            map.color = c;
            spwn = false;
        }
    }

    IEnumerator FadeIn()
    {
        col.enabled = true;
        for (int i = 0; i < 10; i++)
        {
            float f = i / 10.0f;
            Color c = map.color;
            c.a = f;
            map.color = c;
            yield return new WaitForSeconds(0.1f);

        }
    }

    IEnumerator FadeOut()
    {
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            Color c = map.color;
            c.a = f;
            map.color = c;
            yield return new WaitForSeconds(0.1f);

        }
        col.enabled = false;
    }

    public void TileControl()
    {
        if (spwn)
        {
            spwn = false;
            StartCoroutine(FadeOut());
        }
        else 
        {
            spwn = true;
            StartCoroutine(FadeIn());
        }
    }
}
