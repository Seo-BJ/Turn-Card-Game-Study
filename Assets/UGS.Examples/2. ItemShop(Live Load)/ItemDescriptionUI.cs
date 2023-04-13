using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemDescriptionUI : MonoBehaviour
{
    public Text itemName;
    public Text itemGradeAndType;
    [FormerlySerializedAs("STR")] public Text str;
    [FormerlySerializedAs("DEX")] public Text dex;
    [FormerlySerializedAs("INT")] public Text @int;
    [FormerlySerializedAs("LUK")] public Text luk;


    public string GetItemName(Example2.Item.Weapons data)
    {
        if (data.Grade == 0)
        {
            return $"<color=#ffffff>{data.LocaleID}</color>";
        }
        if (data.Grade == 1)
        {
            return $"<color=#73FF98>{data.LocaleID}</color>";
        }
        if (data.Grade == 2)
        {
            return $"<color=#3B8AFF>{data.LocaleID}</color>";
        }
        if (data.Grade == 3)
        {
            return $"<color=#FF23ED>{data.LocaleID}</color>" ;
        }
        if (data.Grade == 4)
        {
            return $"<color=#FFA800>{data.LocaleID}</color>" ;;
        }
        return null;
    }
    public string GetItemGradeType(Example2.Item.Weapons data)
    {
        if (data.Grade == 0)
        {
            return $"<color=#ffffff>Normal </color>" + data.Type;
        }
        if (data.Grade == 1)
        {
            return $"<color=#73FF98>Rare </color>" + data.Type;
        }
        if (data.Grade == 2)
        {
            return $"<color=#3B8AFF>Unique </color>" + data.Type;
        }
        if (data.Grade == 3)
        {
            return $"<color=#FF23ED>Epic </color>" + data.Type;
        }
        if (data.Grade == 4)
        {
            return $"<color=#FFA800>Legendary </color>" + data.Type;
        }
        return null;
    }
    public void ShowUI(Example2.Item.Weapons data)
    {
        
        itemName.text = GetItemName(data);
        itemGradeAndType.text = GetItemGradeType(data);
        str.text = $"STR\n{data.Str}";
        dex.text = $"DEX\n{data.Dex}";
        @int.text = $"INT\n{data.INT}";
        luk.text = $"LUK\n{data.Luk}";
        this.gameObject.SetActive(true);
    }
     
    public void HideUI()
    {
        this.gameObject.SetActive(false);
    }
}
