using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Inst { get; private set; }
    [FormerlySerializedAs("_player")] public GameObject player;
    [FormerlySerializedAs("_selectTargetPanel")] [SerializeField] SelectTargetPanel selectTargetPanel;
    public bool isUsingSkill;
    /*
    enum ESkillType { Targeting, NoneTarget }
    public enum ESkillState { NotUsing, UsingSkill }

    ESkillState _eSkillState;
    */
    #region 실행 함수
    void Awake()
    {
        Inst = this;
    }
    void Start()
    {
        
    }


    void Update()
    {
        
    }
    #endregion
    public void ShowPanel()
    {
        selectTargetPanel.Show();
    }
    public void HidePanel()
    {
        selectTargetPanel.Hide();
    }
        /*
        public void SetESkillStatee()
        {
            if (isUsingSkill)
            {
                _eSkillState = ESkillState.UsingSkill;
            }
            else if (!isUsingSkill)
            {
                _eSkillState = ESkillState.NotUsing;
            }

        }
        */
    }
