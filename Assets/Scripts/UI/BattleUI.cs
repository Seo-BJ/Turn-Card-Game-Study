using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [FormerlySerializedAs("_endTurnButton")] [SerializeField] Button endTurnButton;
    void Start()
    {
        endTurnButton.onClick.AddListener(TurnEnd);
    }
    public void TurnEnd()
    {
        TurnManager.Inst.EndTurn();
    }
    

}
