using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public HpStat Hphealth;
    public HpStat Hpworking;

    // 최종 체력 및 피로도 채우기
    protected float initHealth;
    protected float initMaxHealth;
    protected float initWorking;
    protected float initMaxWorking;

    public static PlayerInfo player;
    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (player == null)
        {
            player = this;
        }
        else if (player != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        initHealth = PlayerStat.instance.health;
        initWorking = PlayerStat.instance.working;
        initMaxHealth = PlayerStat.instance.maxHealth;
        initMaxWorking = PlayerStat.instance.maxWorking;

        Hphealth.Initialize(initHealth, initMaxHealth);
        Hpworking.Initialize(initWorking, initMaxWorking);
    }

    private void Update()
    {

    }
}
