using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Inst { get; private set; }

    [FormerlySerializedAs("_battleDeck")] [SerializeField] List<GameObject> battleDeck = new List<GameObject>();
    [FormerlySerializedAs("_hand")] [SerializeField] List<GameObject> hand = new List<GameObject>();
    [FormerlySerializedAs("_grave")] [SerializeField] List<GameObject> grave = new List<GameObject>();

    [FormerlySerializedAs("_cardSpawnPoint")] [SerializeField] Transform cardSpawnPoint;
    [FormerlySerializedAs("_cardGrave")] [SerializeField] Transform cardGrave;
    [FormerlySerializedAs("_cardFolder")] [SerializeField] GameObject cardFolder;
    [FormerlySerializedAs("_handArea")] [SerializeField] BoxCollider2D handArea;

    [FormerlySerializedAs("_graveButtonTMP")] [SerializeField] TMP_Text graveButtonTMP;
    [FormerlySerializedAs("_deckButtonTMP")] [SerializeField] TMP_Text deckButtonTMP;
    enum ECardState { CanNothing, CanMouseOver, CanMouseDrag }
    [FormerlySerializedAs("_eCardState")] [SerializeField] ECardState eCardState;
    [FormerlySerializedAs("_isCardDrag")] public bool isCardDrag;
    [FormerlySerializedAs("_selectCard")] public GameObject selectCard;
    
    void Awake()
    {
        Inst = this;
    }
    void Start()
    {
        MakeBattleDeck();
        TurnManager.OnAddCard += AddCard;
    }
    void Update()
    {
        
        if (isCardDrag || SkillManager.Inst.isUsingSkill == true)
        {
            DragCard();
            if (Input.GetMouseButtonDown(1))
            {
                isCardDrag = false;
                selectCard.GetComponent<CardUI>().MoveTransform(selectCard.GetComponent<CardUI>().originPrs, true, 0.7f);
                selectCard = null;
                SkillManager.Inst.isUsingSkill = false;
                SkillManager.Inst.HidePanel();
                GridManager.Inst.DestroyAllColorTile();
            }
        }
        SetECardState();
    }
    private void OnDestroy()
    {
        TurnManager.OnAddCard -= AddCard;
    }
    /*
    void CardSpawn()
    {
        List<PRS> prsList = CardAlignment();
        string[] deck = ES3.GetKeys();
        for (int i = 0; i < prsList.Count(); i++)
        {
            InstantiateCardOnScene(, prsList[i]);
        }
            
    }
    */
    /*
    1. OnMouseDown - 오브젝트 위에서 마우스 왼쪽 버튼이 눌렸을 때 호출
    2. OnMouseDrag - OnMouseDown이 일어나고 마우스 버튼을 때기 전까지 계속 호출됨
    3. OnMouseEnter - 마우스가 오브젝트 위로 올라갔을 때 호출
    4. OnMouseExit - 마우스가 오브젝트에서 벗어났을 때 호출
    5. OnMouseOver - 마우스가 오브젝트 위에 있으면 계속 호출됨
    6. OnMouseUp - OnMouseDown이 일어난 후에 마우스를 때면 호출 (OnMouseDown이 일어나지 않으면 호출되지 않음)
    7. OnMouseUpAsButton - OnMouseDown이 호출된 오브젝트 위에 마우스가 위치한 상태에서 OnMouseUp이 호출됬을 때
    */

    void HandleDeckTMP()
    {
        graveButtonTMP.text = grave.Count.ToString();
        deckButtonTMP.text = battleDeck.Count.ToString();
    }



    void SetECardState()
    {
        if (TurnManager.Inst.isLoading || SkillManager.Inst.isUsingSkill)
        {
            eCardState = ECardState.CanNothing;
        }
        else if (!TurnManager.Inst.myTurn)
        { 
            eCardState = ECardState.CanMouseOver;
        }
        else if (TurnManager.Inst.myTurn && !SkillManager.Inst.isUsingSkill)
        {
            eCardState = ECardState.CanMouseDrag;
        }
    }
    public void CardMouseDown(GameObject card) // CanMouseDrag가 아니면 작동X
    {
        if (eCardState != ECardState.CanMouseDrag)
            return;
        Debug.Log("CardMouseDown");
        selectCard = card;
        if (IsTargetingCard(selectCard))
        {
            SkillManager.Inst.isUsingSkill = true;
            selectCard.GetComponent<Skill>().isTargetSkill = true;
            SkillManager.Inst.ShowPanel();
        }
        else
        {
            isCardDrag = true;
            SkillManager.Inst.isUsingSkill = true;
        }
        
    }
    public void CardMouseUp() // 마찬가지로 CanMouseDrag가 아니면 작동X
    {
        if (IsTargetingCard(selectCard))
            return;

        if (IsCardOnMyHand())
        {
            selectCard.GetComponent<CardUI>().MoveTransform(selectCard.GetComponent<CardUI>().originPrs, true, 0.7f);
            selectCard = null;
            isCardDrag = false;
            SkillManager.Inst.isUsingSkill = false;
        }
        else
        {
            Debug.Log("스킬사용");
            AfterUseSkill();
            selectCard = null;
            isCardDrag = false;
            SkillManager.Inst.isUsingSkill = false;
        }
    }
    public void AfterUseSkill()
    {
        CardUI selectCardUI = selectCard.GetComponent<CardUI>();
        selectCardUI.MoveTransform(new PRS(cardGrave.position, Utils.Qi, selectCardUI.originPrs.scale), true, 1.0f);
        grave.Add(selectCard);
        hand.Remove(selectCard);
        AlignCard();
        HandleDeckTMP();
    }

    bool IsCardOnMyHand()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward);
        int layer = LayerMask.NameToLayer("HandArea");
        bool onMyCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
        return onMyCardArea;
    }
    void DragCard()
    {
        if (IsTargetingCard(selectCard))
            return;

        CardUI selectCardUI = selectCard.GetComponent<CardUI>();
        selectCardUI.MoveTransform(new PRS(Utils.MousePos, Utils.Qi, selectCardUI.originPrs.scale), false);
    }
    void EnlargeDice()
    {
        //다이스 크기 커짐
    }
    bool IsTargetingCard(GameObject card)
    {
        
        if (card.GetComponent<CardUI>().targetingCard)
        { 
            return true;
        }
        else
        {
            return false;
        }
    }













    public void AddCard()
    {
        if (battleDeck.Count == 0) //뽑을 카드가 없으면 덱 생성
        {
            ReRollDeck();
        }
        GameObject card = DrawCard();
        DeckToHand(card);
        AlignCard();
    }
    public GameObject DrawCard() // player deck에서 index 0의 카드를 return
    {
        GameObject card = battleDeck[0];
        battleDeck.RemoveAt(0);
        return card;
    }
    void ReRollDeck()
    {
        int count = grave.Count;
        for (int i = 0; i < count; i++)
        {
            OneCardGraveToDeck();
        }
        for (int i = 0; i < battleDeck.Count; i++) //player deck 셔플
        {
            int rand = Random.Range(i, battleDeck.Count);
            GameObject card = battleDeck[i];
            battleDeck[i] = battleDeck[rand];
            battleDeck[rand] = card;
        }
    }
    void OneCardGraveToDeck()
    {
        GameObject card = grave[0];
        card.GetComponent<CardUI>().MoveTransform(new PRS(cardSpawnPoint.position, Utils.Qi, card.GetComponent<CardUI>().originPrs.scale), false);
        grave.Remove(card);
        battleDeck.Add(card);
        HandleDeckTMP();
    }
    void DeckToHand(GameObject card)
    {
        hand.Add(card);
        HandleDeckTMP();
    }
    void MakeBattleDeck()
    {
        string[] deck = ES3.GetKeys();
        for (int i = 0; i < deck.Length; i++)
        {
            BaseCardData cardData = ES3.Load<BaseCardData>($"{deck[i]}");
            Debug.Log(cardData.CardCount);
            string cardName = cardData.Name;
            for (int k = 0; k < cardData.CardCount; k++)
            {
                GameObject card = MakeCardObject(cardName);
                card.name = cardName + $"{k}";
                battleDeck.Add(card);
            }
        }
        for (int i = 0; i < battleDeck.Count; i++) //player deck 셔플
        {
            int rand = Random.Range(i, battleDeck.Count);
            GameObject card = battleDeck[i];
            battleDeck[i] = battleDeck[rand];
            battleDeck[rand] = card;
        }
    }
    GameObject MakeCardObject(string cardName)
    {
        BaseCardData cardData = ES3.Load<BaseCardData>($"{cardName}");
        GameObject targetCardObject = Instantiate(Resources.Load<GameObject>($"HexCard/{cardName}"), cardSpawnPoint.position, Utils.Qi);
        targetCardObject.transform.SetParent(cardFolder.transform, true);
        //_hand.Add(targetCardObject);
        targetCardObject.GetComponent<CardUI>().SetDataOnPrefab(cardData);

        return targetCardObject;
        //targetUI.MoveTransform(prs, true, 1.0f);
    }
    
    void AlignCard()
    {
        List<PRS> prs = CardAlignment();
        for (int i = 0; i < hand.Count; i++)
        {
            GameObject targetCard = hand[i];
            CardUI targetUI = targetCard.GetComponent<CardUI>();
            targetUI.originPrs = prs[i];
            targetUI.MoveTransform(targetUI.originPrs, true, 0.7f);
        }
    }
    List<PRS> CardAlignment()
    {
        int cardsCount = hand.Count;

        float interval = 2.0f;
        float cardWidth = 10.0f;
        Vector3 cardleft = new Vector3(0, -30, -10);
        Vector3 cardright = new Vector3(0, -30, -10);
        Vector3 cardSclae = new Vector3(1, 1, 1);
        
        float length = (cardsCount - 1) * (interval + cardWidth) / 2;
        if (length < 35)
        {
            cardleft.x = -length;
            cardright.x = length;
        }
        else if (length >= 35)
        {
            cardleft.x = -35;
            cardright.x = 35;
        }

        List<PRS> originCardPrSs = new List<PRS>();
        originCardPrSs = AlignmentList(cardleft, cardright, cardsCount, cardSclae);

        return originCardPrSs;
    }
    List<PRS> AlignmentList(Vector3 leftTr, Vector3 rightTr, int objCount, Vector3 scale)
    {
        // + 카드 크기 증가 및 카드 개수에 따른 좌우 확장
        float[] objLerps = new float[objCount]; //0~1까지 중 자기의 상대적인 포지션을 나타냄
        List<PRS> results = new List<PRS>(objCount);

        switch (objCount) // 간격 계산
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0, 1.0f }; break;
            case 3: objLerps = new float[] { 0, 0.5f, 1.0f }; break;
            default: //카드들의 간격 계산
                float interval = 1f / (objCount - 1);
                for (int i = 0; i < objCount; i++)
                    objLerps[i] = interval * i;
                break;
        }
        for (int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr, rightTr, objLerps[i]);
            var targetRot = Utils.Qi;
            results.Add(new PRS(targetPos, targetRot, scale));
        }
        return results;
    }



}
