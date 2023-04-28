using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HpStat : MonoBehaviour
{
    // HP 조절에 대한 것
    [SerializeField] private Image content;
    [SerializeField] private TextMeshProUGUI statText;

    private float currentFill;
    public float MyMaxValue { get; set; }

    private float currentValue;
    public float MyCurrentValue
    {
        get 
        {
            return currentValue;
        }
        set
        {
            if (value > MyMaxValue) currentValue = MyMaxValue;
            else if (value < 0) currentValue = 0;
            else currentValue = value;

            currentFill = currentValue / MyMaxValue;
            statText.text = currentValue + " / " + MyMaxValue;
        }
    }

    private void Start()
    {
        content = GetComponent<Image>();
    }

    private void Update()
    {
        if(content != null) UpdateFill();
    }

    // 체력과 마나 값을 세팅(현재, 최대)
    public void Initialize(float currentValue, float maxValue) 
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }

    public void UpdateFill()
    {
        content.fillAmount = currentFill;
    }
}
