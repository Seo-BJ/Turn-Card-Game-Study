using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Serialization;

public class SelectTargetPanel : MonoBehaviour
{
    [FormerlySerializedAs("_selectTargetTMP")] [SerializeField] TMP_Text selectTargetTMP;

    public void Show()
    {
        Sequence sequence = DOTween.Sequence() //순서대로 반응
            .Append(transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.InOutQuad)); //0.3초동안 Vecotr3 1으로 커짐
            

    }
    public void Hide()
    {
        Sequence sequence = DOTween.Sequence()
            .Append(transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InOutQuad)); //0으로 작아짐
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
