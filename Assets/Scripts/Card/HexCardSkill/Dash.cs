using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Dash : Skill
{
    void Update()
    {
        if (SkillManager.Inst.isUsingSkill && isTargetSkill)
        {
            Debug.Log("다른거 발동");
        }
    }
}
