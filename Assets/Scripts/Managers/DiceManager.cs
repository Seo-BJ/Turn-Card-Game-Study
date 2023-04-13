using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
public class DiceManager : MonoBehaviour
{
    public static DiceManager Inst { get; private set; }
    #region  변수
    [FormerlySerializedAs("_onDiceSlot")] public bool onDiceSlot;
    [FormerlySerializedAs("_isDiceDrag")] public bool isDiceDrag;
    [FormerlySerializedAs("_selectDice")] public Dice selectDice;
    [FormerlySerializedAs("_playerStat")] public PlayerStat playerStat;
    [FormerlySerializedAs("_diceSpawnPoint")] [SerializeField] Transform diceSpawnPoint;
    [FormerlySerializedAs("_diceList")] public List<Dice> diceList;

    private Vector3 _diceSclae = new Vector3(80, 80, 1);

    enum EDiceState { CanNothing, CanMouseOver, CanMouseDrag }
    [FormerlySerializedAs("_eDiceState")] [SerializeField] EDiceState eDiceState;
    #endregion
    #region 실행 함수
    void Awake()
    {
        Inst = this;
    }
    void Start()
    {
        //TurnManager.OnAddDice += DiceSpawn;
    }
    private void OnDestroy()
    {
        //TurnManager.OnAddDice -= DiceSpawn;
    }
    void Update()
    {
        if(isDiceDrag)
        {
            DiceDrag();
        }
        SetEDiceState();
    }
    #endregion

    #region Dice 기본 함수
    void DetectCard()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(Utils.MousePos, Vector2.zero);
        if (hit.collider != null)
        {
            CardUI detectCard = hit.collider.GetComponent<CardUI>();
            Vector3 diceSlotPos = detectCard.card.transform.position;
            PlayerSkill detectCardSkill = hit.collider.GetComponent<PlayerSkill>();
            detectCardSkill.UseSkill(selectDice);

            selectDice.transform.position = diceSlotPos;
            selectDice.originPrs = new PRS(diceSlotPos, Utils.Qi, _diceSclae);
            DiceMoveAfterUse(selectDice);
            diceList.Remove(selectDice);

            if (detectCard.name != "Reroll")
            {
                DiceReAlignment();
            }
        }
        else
        {
            selectDice.GetComponent<BoxCollider2D>().enabled = true;
        }
        //_selectDice.DestroyDice(); 0.5초 후에 실행되야함

        selectDice = null;
        isDiceDrag = false;

    }
    void DiceMoveAfterUse(Dice dice)
    {
        PRS originPrs = dice.originPrs;
        Vector3 originPos = originPrs.pos;
        originPos.y -= 500;
        PRS usedDicePrs = new PRS(originPos, originPrs.rot, originPrs.scale);
        dice.MoveTransform(usedDicePrs, true, 0.5f);
    }
    void DiceSpawn()
    {
        List<PRS> prsList = DiceAlignment();
        int presentDiceCount = diceList.Count;
        int maxSpawnDiceCount;
        if (presentDiceCount + playerStat.PlayerDiceCount <= playerStat.PlayerMaxDiceCount)
        {
            maxSpawnDiceCount = presentDiceCount + playerStat.PlayerDiceCount;
        }
        else
        {
            maxSpawnDiceCount = playerStat.PlayerMaxDiceCount;
        }
        for (int i = presentDiceCount; i < maxSpawnDiceCount; i++)
        {
            InstantiateRandomDice(prsList[i]);
        }
    }
    public void RerollOneDice(Dice dice)
    {
        int originIndex = diceList.IndexOf(dice);
        PRS prs = dice.originPrs;
        //dice.DestroyDice();
        InstantiateRandomDice(prs, "Insert", originIndex);
    }
    public void RerollAllDice()
    {
        int originCount = diceList.Count;  
        foreach(Dice dice in diceList)
        {
            DiceMoveAfterUse(dice);
        }
        diceList.Clear();

        List<PRS> prsList = DiceAlignment();
        for (int i = 0; i < originCount; i++)
        {
            InstantiateRandomDice(prsList[i]);
        }
    }
    void InstantiateRandomDice(PRS prs, string addType = "Add", int insertIndex = 0)
    {
        int randomInt = Random.Range(1, 7);
        string targetDiceName = $"Dice{randomInt}";
        GameObject dice = Instantiate(Resources.Load<GameObject>($"Dice/{targetDiceName}"), diceSpawnPoint.position, Utils.Qi);
        Dice targetDice = dice.GetComponent<Dice>();
        targetDice.originPrs = prs;
        targetDice.MoveTransform(prs, true, 1.0f);

        if (addType == "Add")
        {
            diceList.Add(targetDice);
        }
        else if (addType == "Insert")
        {
            diceList.Insert(insertIndex, targetDice);
        }
    }
    void DiceReAlignment()
    {
        List<PRS> prsList = DiceAlignment();
        for (int i = 0; i < diceList.Count; i++)
        {
            diceList[i].MoveTransform(prsList[i], true, 0.5f);
            diceList[i].originPrs = prsList[i];
        }
    }
    List<PRS> DiceAlignment()
    {
        float interval = 10.0f;
        float diceWidth = 80.0f;
        int playerMaxDiceCount = playerStat.PlayerMaxDiceCount;
        Vector3 diceleft = new Vector3(-800, -150, 0);
        Vector3 diceright = new Vector3(-800, -150, 0);

        diceright.x = (playerMaxDiceCount - 1) * (interval + diceWidth) - 800; 

        List<PRS> originCardPrSs = new List<PRS>();
        originCardPrSs = AlignmentList(diceleft, diceright, playerMaxDiceCount, _diceSclae);
        return originCardPrSs;
    }
    List<PRS> AlignmentList(Vector3 leftPosition, Vector3 rightPosition, int objectCount, Vector3 scale)
    {
        float[] objLerps = new float[objectCount];
        List<PRS> results = new List<PRS>(objectCount);
        switch (objectCount) // 간격 계산
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0, 1.0f }; break;
            case 3: objLerps = new float[] { 0, 0.5f, 1.0f }; break;
            default: //카드들의 간격 계산
                float interval = 1f / (objectCount - 1);
                for (int i = 0; i < objectCount; i++)
                    objLerps[i] = interval * i;
                break;
        }
        for (int i = 0; i < objectCount; i++) //원형으로 카드를 배치
        {
            var targetPos = Vector3.Lerp(leftPosition, rightPosition, objLerps[i]);
            var targetRot = Utils.Qi;
            results.Add(new PRS(targetPos, targetRot, scale));
        }
        return results;
    }
    #endregion
    #region 주사위 상태 설정 함수
    /*
    1. OnMouseDown - 오브젝트 위에서 마우스 왼쪽 버튼이 눌렸을 때 호출
    2. OnMouseDrag - OnMouseDown이 일어나고 마우스 버튼을 때기 전까지 계속 호출됨
    3. OnMouseEnter - 마우스가 오브젝트 위로 올라갔을 때 호출
    4. OnMouseExit - 마우스가 오브젝트에서 벗어났을 때 호출
    5. OnMouseOver - 마우스가 오브젝트 위에 있으면 계속 호출됨
    6. OnMouseUp - OnMouseDown이 일어난 후에 마우스를 때면 호출 (OnMouseDown이 일어나지 않으면 호출되지 않음)
    7. OnMouseUpAsButton - OnMouseDown이 호출된 오브젝트 위에 마우스가 위치한 상태에서 OnMouseUp이 호출됬을 때
    */
    void SetEDiceState()
    {
        if (TurnManager.Inst.isLoading || SkillManager.Inst.isUsingSkill)
        {
            eDiceState = EDiceState.CanNothing;
        }
        else if (!TurnManager.Inst.myTurn)
        {
            eDiceState = EDiceState.CanMouseOver;
        }
        else if (TurnManager.Inst.myTurn && !SkillManager.Inst.isUsingSkill )
        {
            eDiceState = EDiceState.CanMouseDrag;
        }
    }
    public void DiceMouseDown(Dice dice) // CanMouseDrag가 아니면 작동X
    {
        if (eDiceState != EDiceState.CanMouseDrag) 
            return;

        selectDice = dice;
        isDiceDrag = true;
    }
    public void DiceMouseUp() // 마찬가지로 CanMouseDrag가 아니면 작동X
    {
        if (eDiceState != EDiceState.CanMouseDrag)
            return;

        PutDiceOnSlot();
    }
    void PutDiceOnSlot()
    {
        selectDice.GetComponent<BoxCollider2D>().enabled = false;
        DetectCard();
        
    }

    /*
    void DetectDiceSlot()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward);
        int layer = LayerMask.NameToLayer("DiceSlot");
        _onDiceSlot = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
    }
    bool DetectDiceSlotVerTwo()
    {
        bool onDiceSlot;
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward);
        int layer = LayerMask.NameToLayer("DiceSlot");
        onDiceSlot = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
        return onDiceSlot;
    }
    */
    
    
    void DiceDrag()
    {
        selectDice.MoveTransform(new PRS(Utils.MousePos, Utils.Qi, selectDice.originPrs.scale), false);
    }
    void EnlargeDice()
    {
        //다이스 크기 커짐
    }
    #endregion
}
