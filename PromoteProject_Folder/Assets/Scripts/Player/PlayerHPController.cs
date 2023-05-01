using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPController : MonoBehaviour
{
    public HpStat Hphealth;
    public HpStat Hpworking;

    // ���� ü�� �� �Ƿε�
    protected float initHealth;
    protected float initMaxHealth;
    protected float initWorking;
    protected float initMaxWorking;

    private void Start()
    {
        initHealth = PlayerStat.instance.health;
        initWorking = PlayerStat.instance.working;
        initMaxHealth = PlayerStat.instance.maxHealth;
        initMaxWorking = PlayerStat.instance.maxWorking;

        Hphealth.Initialize(initHealth, initMaxHealth);
        Hpworking.Initialize(initWorking, initMaxWorking);
    }
}
