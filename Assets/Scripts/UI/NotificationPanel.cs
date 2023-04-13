using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class NotificationPanel : MonoBehaviour
{
    [FormerlySerializedAs("_whosTurnTMP")] [SerializeField] TMP_Text whosTurnTMP;
    [FormerlySerializedAs("_turnCountTMP")] [SerializeField] TMP_Text turnCountTMP;

    public void Show(string message)
    {
        whosTurnTMP.text = message;
        string turnCount = TurnManager.Inst.turnCount.ToString();
        turnCountTMP.text = $"{turnCount}턴";
        Sequence sequence = DOTween.Sequence() //순서대로 반응
            .Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad)) //0.3초동안 Vecotr3 1으로 커짐
            .AppendInterval(0.9f) //0.9초 대기
            .Append(transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad)); //0으로 작아짐 + 페이드 기능도 추가



    }
    void Start()
    {
        ScaleZero();
    }

    [ContextMenu("ScaleOne")]
    void ScaleOne() => transform.localScale = Vector3.one;
    [ContextMenu("ScaleZero")]
    void ScaleZero() => transform.localScale = Vector3.zero;


}
