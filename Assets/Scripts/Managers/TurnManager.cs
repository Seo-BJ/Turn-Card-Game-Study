using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Inst { get; private set; }
    void Awake() => Inst = this;

    [FormerlySerializedAs("_turnEndButton")] public Button turnEndButton;
    [FormerlySerializedAs("_turnCountFloat")] public float turnCountFloat;
    [FormerlySerializedAs("_turnCount")] public int turnCount;  

    [Header("Develop")]
    [SerializeField][Tooltip("시작 턴 모드를 정합니다")] ETurnMode eturnMode;
    [SerializeField][Tooltip("카드 배분 속도를 빠르게 합니다.")] bool fastMode;
    [SerializeField][Tooltip("시작 카드 개수를 정합니다")] int startCardCount;

    

    [Header("Properties")]
    public bool isLoading; // True -> 카드, 여러 요소 클릭방지
    public bool myTurn;

    enum ETurnMode { My, Other }
    WaitForSeconds _delay05 = new WaitForSeconds(0.5f);
    WaitForSeconds _delay10 = new WaitForSeconds(1.0f);

    public static Action OnAddCard;

    void Start()
    {
        turnEndButton.onClick.AddListener(EndTurn);
        turnCount = 1;
    }
    
    void GameSetup()
    {
        if (fastMode)
            _delay05 = new WaitForSeconds(0.1f);
        switch (eturnMode)
        {
            case ETurnMode.My:
                myTurn = true;
                break;
            case ETurnMode.Other:
                myTurn = false;
                break;
        }
    }
    
    public IEnumerator StartGameCo()
    {

        GameSetup();
        isLoading = true;
        for (int i = 0; i < startCardCount; i++)
        {
            yield return _delay05;
            OnAddCard.Invoke();
        }
        StartCoroutine(StartTurnCo());
    }
    
    IEnumerator StartTurnCo()
    {
        isLoading = true;

        if (myTurn)
        {
            OnAddCard.Invoke();
            GameManager.Inst.Notification("내 턴");
        }
        else
        {
            GameManager.Inst.Notification("적 턴");
            turnEndButton.interactable = false;
        }
        yield return _delay10;
        if (myTurn)
        {
            turnEndButton.interactable = true;
        }
        isLoading = false;

    }
    public void EndTurn()
    {
        turnCountFloat += 0.5f;
        if (turnCountFloat % 1 == 0)
        {
            turnCount += 1;
        }
        myTurn = !myTurn;
        StartCoroutine(StartTurnCo());
        
    }
}
