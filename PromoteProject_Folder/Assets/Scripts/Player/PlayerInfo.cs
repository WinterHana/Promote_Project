using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : Character
{
    public HpStat health;
    public HpStat working;

    private float initHealth = 100;
    private float initWorking = 50;


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

    protected override void Start()
    {
        health.Initialize(initHealth, initHealth);
        working.Initialize(initWorking, initWorking);

        base.Start();
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            working.MyCurrentValue -= 10;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            working.MyCurrentValue += 10;
        }
        */
    }
}
