using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCardData 
{
    public int ID;
    public string Name;
    public string Effect;
    public int Cost;
    public int Range;
    public int Speed;
    public string CardType;
    public string CardGrade;
    public int CardLevel;
    public int CardCount;

    public BaseCardData(int id, string name, string effect, int cost, int range, int speed, string cardType, string cardGrade, int cardLevel, int cardCount)
    {
        this.ID = id;
        this.Name = name;
        this.Effect = effect;
        this.Cost = cost;
        this.Range = range;
        this.Speed = speed;
        this.CardType = cardType;
        this.CardGrade = cardGrade;
        this.CardLevel = cardLevel;
        this.CardCount = cardCount;
    }
}
