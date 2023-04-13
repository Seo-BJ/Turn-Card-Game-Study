using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reroll : PlayerSkill
{
    public override void UseSkill(Dice dice)
    {
        DiceManager.Inst.RerollAllDice();
    }
}
