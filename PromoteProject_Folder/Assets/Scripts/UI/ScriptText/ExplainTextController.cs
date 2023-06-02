using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExplainTextController : MonoBehaviour
{
    public float moveSpeed;
    public float destroyTime;
    public string guide;
    TextMeshProUGUI text;
    Color alpha;

    void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        Invoke("DestroyObject", destroyTime);
    }

    void Update()
    {
        text.text = guide;
        text.transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
