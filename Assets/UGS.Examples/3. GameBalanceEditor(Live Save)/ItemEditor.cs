using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UGS;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ItemEditor : MonoBehaviour
{
    public GameObject waitObj;
    public GameObject prefab;
    public RectTransform contentTransform;
    public InputField itemIndex;
    public InputField localeID;
    [FormerlySerializedAs("Type")] public InputField type;
    [FormerlySerializedAs("Grade")] public InputField grade;
    [FormerlySerializedAs("STR")] public InputField str;
    [FormerlySerializedAs("DEX")] public InputField dex;
    [FormerlySerializedAs("INT")] public InputField @int;
    [FormerlySerializedAs("LUK")] public InputField luk;
    [FormerlySerializedAs("IconName")] public InputField iconName;
    [FormerlySerializedAs("Price")] public InputField price;

    public void Awake()
    {
        waitObj.gameObject.SetActive(true); 
        UnityGoogleSheet.LoadFromGoogle<int, Example3.Item.Data>((list,map)=> 
        {
            Debug.Log("Loaded From Google | Item Count: : " + list.Count);
            CreateEditor();
            waitObj.gameObject.SetActive(false);

            SelecItem(FindObjectOfType<EditableItemModel>());
        }, true); 


        //Example3.Item.Data.LoadFromGoogle((list,map)=> {
        //    Debug.Log("Loaded From Google | Item Count: : " + list.Count); 
        //    CreateEditor();
        //    waitObj.gameObject.SetActive(false);
        //}, true);  
    }
    public void CreateEditor()
    {
        // Destroy already created
        for (int i = 0; i < contentTransform.childCount; i++)
            Destroy(contentTransform.transform.GetChild(i).gameObject);

        // Sort
        var sortData = Example3.Item.Data.DataList.OrderBy(x=>x.Price);
        foreach (var data in sortData)
        {
            var editable = Instantiate(prefab);
            editable.transform.SetParent(contentTransform, false);
            var editableItem = editable.GetComponent<EditableItemModel>();
            editableItem.EditTargetData = data;


            Color color = new Color(1,1,1,1);
            switch (data.Grade)
            {
                case 0:
                    break;
                case 1:
                    color = new Color(0.77f, 1, 0.72f);
                    break;
                case 2:
                    color = new Color(0, 0.70f, 1);
                    break;
                case 3:
                    color = new Color(1, 0, 0.75f);
                    break;
                case 4:
                    color = new Color(1, 0.74f, 0);
                    break;

            } 
            editableItem.itemName.text = data.LocaleID;
            editableItem.itemName.color = color;
        }
    }
    public void SelecItem(EditableItemModel mdl)
    {
        itemIndex.text = mdl.EditTargetData.ItemIndex.ToString();
        localeID.text  = mdl.EditTargetData.LocaleID;
        type.text      = mdl.EditTargetData.Type;
        grade.text     = mdl.EditTargetData.Grade.ToString();
        str.text       = mdl.EditTargetData.Str.ToString();
        dex.text       = mdl.EditTargetData.Dex.ToString();
        @int.text       = mdl.EditTargetData.INT.ToString();
        luk.text       = mdl.EditTargetData.Luk.ToString();
        iconName.text  = mdl.EditTargetData.IconName;
        price.text     = mdl.EditTargetData.Price.ToString();
    }
    public void UpdateGoogleSheet() 
    {
        waitObj.gameObject.SetActive(true);
        UnityGoogleSheet.Write<Example3.Item.Data>(new Example3.Item.Data()
        {
            ItemIndex = int.Parse(this.itemIndex.text),
            LocaleID = this.localeID.text,
            Type = this.type.text,
            Grade = int.Parse(this.grade.text),
            Str = int.Parse(this.str.text),
            Dex = int.Parse(this.dex.text),
            INT = int.Parse(this.@int.text),
            Luk = int.Parse(this.luk.text),
            IconName = this.iconName.text,
            Price = int.Parse(this.price.text)
        } 
        , (data)=> {
            Example3.Item.Data.LoadFromGoogle((list, map) => {
                waitObj.gameObject.SetActive(false);
                Debug.Log("Update Google Sheet!");
                CreateEditor();
            }, true); 
        }); 
    }
}
