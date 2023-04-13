using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UGS;
using UnityEngine;

public class CardTable : MonoBehaviour
{
    public static CardTable Inst { get; private set; }
    void Awake()
    {
        Inst = this;
        CardsData.Data.Load();

        
    }
    void Start()
    {

    }
    
    public CardData ReturnCardData(int id)
    {
        CardData cardData = new CardData(id, GetCardName(id), GetRequestDiceCount(id), GetCardEffect(id), GetDiceCondition(id), GetCanRepeatCount(id));
        return cardData;
    }
    public int GetCanRepeatCount(int id)
    {
        var localMap = CardsData.Data.GetDictionary();
        return localMap[id].CanRepeatCount;
    }
    public string GetCardName(int id)
    {
        var localMap = CardsData.Data.GetDictionary();
        if (localMap[id].Name == null )
        {
            return null;
        }
        else
        {
            return localMap[id].Name;
        } 
    }
    public int GetRequestDiceCount(int id)
    {
        var localMap = CardsData.Data.GetDictionary();
        return localMap[id].NeedDiceCount;
        
    }
    public string GetCardEffect(int id)
    {
        var localMap = CardsData.Data.GetDictionary();
        if (localMap[id].Effect == null)
        {
            return null;
        }
        else
        {
            return localMap[id].Effect;
        }
    }
    public string GetDiceCondition(int id)
    {
        var localMap = CardsData.Data.GetDictionary();
        return localMap[id].DiceCondition;
    }

}


