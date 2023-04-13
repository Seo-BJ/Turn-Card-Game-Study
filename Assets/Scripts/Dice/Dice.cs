using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Dice : MonoBehaviour
{
    [FormerlySerializedAs("_originPRS")] public PRS originPrs;
    [FormerlySerializedAs("_diceNumber")] public int diceNumber;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #region 마우스 입력 함수
    void OnMouseDown()
    {
        DiceManager.Inst.DiceMouseDown(this);
    }
    void OnMouseUp()
    {
        DiceManager.Inst.DiceMouseUp();
    }
    public void DestroyDice()
    {
        Destroy(gameObject);
    }




    #endregion 
    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween) //DOTween 사용해서 이동
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else //그냥 이동
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
}
