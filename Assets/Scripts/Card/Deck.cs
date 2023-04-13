using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UGS;
using UnityEngine;
using UnityEngine.Serialization;

public class Deck : MonoBehaviour
{
    [SerializeField] List<CardData> _playerDeck = new List<CardData>();
    [FormerlySerializedAs("_cardSpawnPoint")] [SerializeField] Transform cardSpawnPoint;
    [FormerlySerializedAs("_cardFolder")] [SerializeField] GameObject cardFolder;

    
    void Start()
    {
        ES3.Save($"{ CardTable.Inst.GetCardName(10)}", CardTable.Inst.ReturnCardData(10));
        
    }
    private void OnDestroy()
    {
       
    }
    void CardSpawn()
    {
        List<PRS> prsList = CardAlignment();
        string[] deck = ES3.GetKeys();
        for (int i = 0; i < prsList.Count(); i++)
        {
            InstantiateCardOnScene(deck[i], prsList[i]);
        }
    }

    void InstantiateCardOnScene(string cardName, PRS prs)
    {
        CardData cardData = ES3.Load<CardData>($"{cardName}");
        GameObject targetCardObject = Instantiate(Resources.Load<GameObject>($"Card/{cardName}"), cardSpawnPoint.position, Utils.Qi);
        targetCardObject.transform.SetParent(cardFolder.transform, true);
        CardUI targetUI = targetCardObject.GetComponent<CardUI>();
        //targetUI.SetDataOnPrefab(cardData.name, cardData.effect);
        targetUI.MoveTransform(prs, true, 1.0f);
    }
    List<PRS> CardAlignment()
    {
        int cardsCount = ES3.GetKeys().Length;

        float interval = 20.0f;
        float cardWidth = 200.0f;
        Vector3 cardleft = new Vector3(-700, -400, -10);
        Vector3 cardright = new Vector3(700, -400, -10);
        Vector3 cardSclae = new Vector3(200, 250, 1);
        cardleft.x = -(cardsCount - 1) * (interval + cardWidth) / 2;
        cardright.x = +(cardsCount - 1) * (interval + cardWidth) / 2;

        List<PRS> originCardPrSs = new List<PRS>();
        originCardPrSs = AlignmentList(cardleft, cardright, cardsCount, cardSclae);

        return originCardPrSs;
    }
    List<PRS> AlignmentList (Vector3 leftTr, Vector3 rightTr, int objCount, Vector3 scale)
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

    #region 임시작성
    /*
    void Awake()
    {
        UserData.Data.Load();
    }
    void AddCardToDeck(int id)
    {
        var newData = new UserData.Data();
        var Datamap = UserData.Data.GetDictionary();
        newData.index = Datamap.Count() + 3;
        newData.id = id;
        newData.name = CardTable.Inst.GetCardName(id);
        newData.requestDice = CardTable.Inst.GetRequestDice(id);
        newData.effect = CardTable.Inst.GetCardEffect(id);

        UnityGoogleSheet.Write<UserData.Data>(newData);
    }
    */
    #endregion
}
