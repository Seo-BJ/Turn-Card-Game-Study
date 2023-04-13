using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }

    [FormerlySerializedAs("_notificationPanel")] [SerializeField] NotificationPanel notificationPanel;

    void Awake()
    {
        Inst = this;
        HexCardData.Data.Load();
    }

   
    void Start()
    {
        MakeStartDeck();
        StartGame();
    }


    void Update()
    {









#if UNITY_EDITOR
        InputCheatKey();
#endif
    }

    void MakeStartDeck()
    {
        ES3.Save($"{HexCardTable.Inst.GetCardName(1100)}", HexCardTable.Inst.ReturnBaseCardData(1100, 1, 4));
        ES3.Save($"{HexCardTable.Inst.GetCardName(1200)}", HexCardTable.Inst.ReturnBaseCardData(1200, 1, 3));
        ES3.Save($"{HexCardTable.Inst.GetCardName(1101)}", HexCardTable.Inst.ReturnBaseCardData(1101, 1, 1));
        ES3.Save($"{HexCardTable.Inst.GetCardName(1201)}", HexCardTable.Inst.ReturnBaseCardData(1201, 1, 1));
        ES3.Save($"{HexCardTable.Inst.GetCardName(1202)}", HexCardTable.Inst.ReturnBaseCardData(1202, 1, 1));
    }

    public void SelectCharacter()
    {

    }
    
    

    void InputCheatKey()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            TurnManager.OnAddCard.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            TurnManager.Inst.EndTurn();
        }

    }
    public void StartGame()
    {
        StartCoroutine(TurnManager.Inst.StartGameCo());
    }
    
    public void Notification(string message)
    {
        notificationPanel.Show(message);
    }
}
