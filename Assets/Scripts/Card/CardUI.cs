using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Serialization;

public class CardUI : MonoBehaviour
{
    [FormerlySerializedAs("_card")] public GameObject card;
    [FormerlySerializedAs("_cardName")] [SerializeField] TMP_Text cardName;
    [FormerlySerializedAs("_cardEffect")] [SerializeField] TMP_Text cardEffect;
    [FormerlySerializedAs("_cardCost")] [SerializeField] TMP_Text cardCost;
    //[SerializeField] TMP_Text _cardLevel;
    [FormerlySerializedAs("_cardRange")] [SerializeField] TMP_Text cardRange;
    [FormerlySerializedAs("_cardSpeed")] [SerializeField] TMP_Text cardSpeed;
    [FormerlySerializedAs("_originPRS")] public PRS originPrs;
    [FormerlySerializedAs("_targetingCard")] public bool targetingCard;
    public BaseCardData BaseCardData;
    

    void Start()
    {
        //SetDataOnPrefab(1100);
    }
    void OnMouseDown()
    {

        DeckManager.Inst.CardMouseDown(gameObject);
       
    }
    void OnMouseUp()
    {
        DeckManager.Inst.CardMouseUp();
    }
    void UseSkill()
    {
        //_card.GetComponent<PlayerSkill>().UseSkill();



    }
    public void SetDataOnPrefab(BaseCardData cardData)
    {
        cardName.text = cardData.Name;
        cardEffect.text = cardData.Effect;
        cardCost.text = cardData.Cost.ToString();
        //_cardLevel.text = cardData.cardLevel.ToString();
        cardRange.text = cardData.Range.ToString();
        cardSpeed.text = cardData.Speed.ToString();
        BaseCardData = cardData;

        if (cardData.Range == 0)
        {
            targetingCard = false;
        }
        else
        {
            targetingCard = true;
        }
    }
    
    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween) //DOTween ����ؼ� �̵�
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else //�׳� �̵�
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
    /*
    void test(int diceNumber)
    {
        string targetString = _cardEffect.text;
        targetString.Replace("\"dice\"",   diceNumber.ToString());
        SetEffect(targetString);
    }
    void otherTest(int diceNumber)
    {
        string targetString = _cardEffect.text;
        targetString.Replace(diceNumber.ToString(), "\"dice\"");
        SetEffect(targetString);
    }*/

}
