using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditableItemModel : MonoBehaviour
{
    public Example3.Item.Data EditTargetData;
    public Text itemName;

    public void SelectThis()
    {
        FindObjectOfType<ItemEditor>().SelecItem(this);
    }
}
