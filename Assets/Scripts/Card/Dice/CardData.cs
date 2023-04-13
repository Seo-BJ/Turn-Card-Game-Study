using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardData
{
    public int ID;
    public string Name;
    public int RequestDiceCount;
    public string Effect;
    public string DiceCondition;
    public int CanRepaetCount;

    public CardData(int id, string name, int requestDiceCount, string effect, string diceCondition, int canRepaetCount)
    {
        this.ID = id;
        this.Name = name;
        this.RequestDiceCount = requestDiceCount;
        this.Effect = effect;
        this.DiceCondition = diceCondition;
        this.CanRepaetCount = canRepaetCount;
    }
    
}
