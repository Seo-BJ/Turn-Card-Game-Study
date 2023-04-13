using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public int CardLevel;
    public int CardCount;
    public BaseCardData BaseCardData;

    public Card(BaseCardData baseCardData, int cardLevel, int cardCount)
    {
        this.BaseCardData = baseCardData;
        this.CardLevel = cardLevel;
        this.CardCount = cardCount;
    }

}
