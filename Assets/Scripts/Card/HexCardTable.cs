using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCardTable : MonoBehaviour
{
    
    public static HexCardTable Inst { get; private set; }
    void Awake()
    {
        Inst = this;
        HexCardData.Data.Load();
    }
    void Start()
    {

    }
    /*public Card SetCard(int id, int level, int count)
    {
        BaseCardData baseCardData = new BaseCardData(id, GetCardName(id), GetCardEffect(id), GetCardCost(id), GetCardRange(id), GetCardSpeed(id), GetCardType(id), GetCardGrade(id));
        Card card = new Card(baseCardData, level, count);
        return card;
    }*/
    public BaseCardData ReturnBaseCardData(int id, int level, int count)
    {
        BaseCardData baseCardData = new BaseCardData(id, GetCardName(id), GetCardEffect(id), GetCardCost(id), GetCardRange(id), GetCardSpeed(id), GetCardType(id), GetCardGrade(id), level, count);
        return baseCardData;
    }
    public string GetCardName(int id)
    {
        var localMap = HexCardData.Data.GetDictionary();
        if (localMap[id].CardName == null)
        {
            return null;
        }
        else
        {
            return localMap[id].CardName;
        }
    }
    public string GetCardEffect(int id)
    {
        var localMap = HexCardData.Data.GetDictionary();
        if (localMap[id].Effect == null)
        {
            return null;
        }
        else
        {
            return localMap[id].Effect;
        }
    }
    public int GetCardCost(int id)
    {
        var localMap = HexCardData.Data.GetDictionary();
        return localMap[id].Cost;
    }
    
    public int GetCardRange(int id)
    {
        var localMap = HexCardData.Data.GetDictionary();
        return localMap[id].Range;
    }
    public int GetCardSpeed(int id)
    {
        var localMap = HexCardData.Data.GetDictionary();
        return localMap[id].Speed;
    }
    
    public string GetCardType(int id)
    {
        var localMap = HexCardData.Data.GetDictionary();
        return localMap[id].CardType;
    }
    public string GetCardGrade(int id)
    {
        var localMap = HexCardData.Data.GetDictionary();
        return localMap[id].CardGrade;
    }
    public int GetStartCount(int id)
    {
        var localMap = HexCardData.Data.GetDictionary();
        return localMap[id].StartCount;
    }

    

}
