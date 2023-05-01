using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
OnEnable()
Ȱ��ȭ �� ������ ȣ��Ǵ� �Լ��Դϴ�.(Awake/Start�� �޸� Ȱ��ȭ �� ������)

OnDisable()
��Ȱ��ȭ �� ������ ȣ��Ǵ� �Լ��Դϴ�.(��ũ��Ʈ�� ������Ʈ��)
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