using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] int health = 10;

    public void setHealth(int num)
    {
        health = num;
    }
}
