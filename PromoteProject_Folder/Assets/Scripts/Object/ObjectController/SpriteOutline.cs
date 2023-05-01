using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
OnEnable()
활성화 될 때마다 호출되는 함수입니다.(Awake/Start와 달리 활성화 될 때마다)

OnDisable()
비활성화 될 때마다 호출되는 함수입니다.(스크립트든 오브젝트든)
 */
[ExecuteInEditMode]
public class SpriteOutline : MonoBehaviour
{
    public Color color = Color.white;
    [Range(0, 16)]
    public int outlineSize = 1;
    private SpriteRenderer spriteRenderer;
    GameObject key;
    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        key = transform.GetChild(0).gameObject;

        key.SetActive(false);
    }

    void OnDisable()
    {
        UpdateOutline(false);
        key.SetActive(false);
    }

    /*
    void Update()
    {
        UpdateOutline(true);
    }
    */

    void UpdateOutline(bool outline)
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        mpb.SetFloat("_Outline", outline ? 1f : 0);
        mpb.SetColor("_OutlineColor", color);
        mpb.SetFloat("_OutlineSize", outlineSize);
        spriteRenderer.SetPropertyBlock(mpb);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UpdateOutline(true);
            key.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UpdateOutline(false);
            key.SetActive(false);
        }
    }
}