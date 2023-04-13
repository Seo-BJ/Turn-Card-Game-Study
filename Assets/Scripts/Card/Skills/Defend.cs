using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : PlayerSkill
{
    public override void GetBlock(int armor)
    {
        base.GetBlock(armor);
    }
    public override void UseSkill(Dice dice)
    {
        GetBlock(dice.diceNumber);
        
    }
}
