using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] PlayerStat stat;
    void Start()
    {

    }

    void Update()
    {


    }
    public void GetDamage(int damage)
    {
        if (stat.Armor > 0)
        {
            int overdamage = stat.Armor - damage;
            if (overdamage <= 0)
            {
                stat.Armor = 0;
                stat.PlayerCurrentHp += overdamage;
            }
            else
            {
                stat.Armor -= damage;
            }
        }
        else
        {
            stat.PlayerCurrentHp -= damage;

        }

        if (stat.PlayerCurrentHp <= 0)
        {
            stat.PlayerCurrentHp = 0;
        }
    }
    public void RestoreHp(int restore)
    {
        stat.PlayerCurrentHp += restore;
        if (stat.PlayerCurrentHp >= stat.PlayerMaxHp)
        {
            stat.PlayerCurrentHp = stat.PlayerMaxHp;
        }
    }
    public void GainArmor(int armor)
    {
        stat.Armor += (armor + stat.Reg);
    }

    public void IncreaseMaxHp(int increment)
    {
        stat.PlayerMaxHp += increment;
    }

    public void GetGold(int gold)
    {
        stat.Gold += gold;
    }
    public void UseGold(int gold)
    {
        stat.Gold -= gold;
    }

    public void GetExp(int exp) //������ �ý���
    {
        stat.Exp += exp;
    }
    public void UseMp(int cost)
    {
        stat.PlayerCurrentMp -= cost;
    }
    public void RestoreMp(int restore)
    {
        stat.PlayerCurrentMp += restore;

    }
    public void IncreaseMaxMp(int increment)
    {
        stat.PlayerMaxMp += increment;
    }
}
