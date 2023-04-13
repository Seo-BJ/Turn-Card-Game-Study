using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemLocalization : MonoBehaviour
{
    public string itemID;
    [FormerlySerializedAs("ItemName")] public Text itemName;
    [FormerlySerializedAs("ItemDescription")] public Text itemDescription;


    public void Start()
    {
        SetitemText();
    }

    public void SetitemText()
    {
        itemName.text = LocalizationManager.Instance.GetItemName(itemID);
        itemDescription.text = LocalizationManager.Instance.GetItemDescription(itemID); 
    }
}
