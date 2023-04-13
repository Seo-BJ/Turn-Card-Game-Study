using CardsData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UGS;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EventUI : MonoBehaviour
{
    [FormerlySerializedAs("_canvas")] [SerializeField] Canvas canvas;
    [FormerlySerializedAs("_eventTitle")] [SerializeField] TMP_Text eventTitle;
    [FormerlySerializedAs("_eventDescription")] [SerializeField] TMP_Text eventDescription;
    [FormerlySerializedAs("_eventUIPrefab")] [SerializeField] GameObject eventUIPrefab;
    void Awake()
    {
        EventData.Data.Load();
    }
    void Start()
    {
        InstantiateEventUI(1);

    }
    void InstantiateEventUI(int id)
    {
        GameObject eventUI = Instantiate(Resources.Load<GameObject>("UI/EventImage"));
        eventUI.transform.SetParent(canvas.transform);

        TMP_Text eventTitle = eventUI.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        eventTitle.text = GetEventTitle(id);

        TMP_Text eventText = eventUI.transform.GetChild(2).gameObject.GetComponent<TMP_Text>();
        eventText.text = GetEventText(id);

        int buttonCount = GetButtonCount(id);

    }

    void Update()
    {
       
    }
    public string GetEventTitle(int id)
    {
        var localMap = EventData.Data.GetDictionary();
        return localMap[id].EventTitle;
    }
    public string GetEventText(int id)
    {
        var localMap = EventData.Data.GetDictionary();
        return localMap[id].EventText;

    }
    public int GetButtonCount(int id)
    {
        var localMap = EventData.Data.GetDictionary();
        return localMap[id].ButtonCount;
    }
    public List<string> GetButtonStringList(int id)
    {
        List<string> result = new List<string>();
        int buttonCount = GetButtonCount(id);
        switch (buttonCount)
        {
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
        }
        return result;
    }
    public List<string> ReturnButtonTextList(int id)
    {
        List<string> result = new List<string>();

        var localMap = EventData.Data.GetDictionary();
        result.Append(localMap[id].Button1);



        /*
        int buttonCount = localMap[id].buttonCount;
        
        for (int i = 1; i < buttonCount + 1; i++)
        {
            result.Append(localMap[id].buttonNum);
        }
        */
        return result;
    }



}
