using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : PlayerSkill
{
    int[] _dir = { 1, 2, 3, 4, 5, 6 };
    int[] _min = { 1, 1, 1, 1, 1, 1 };
    int[] _max = { 1, 1, 1, 1, 1, 1 };
    void OnMouseDown()
    {
        if (!SkillManager.Inst.isUsingSkill)
        {
            SkillManager.Inst.isUsingSkill = true;
        }
    }
    public override void MoveToClickCell(Vector3 mousePos, Vector3 playerPos, int[] dir, int[] min, int[] max)
    {
        base.MoveToClickCell(mousePos, playerPos, dir, min, max);   
    }
    void Update()
    {
        if (SkillManager.Inst.isUsingSkill)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MoveToClickCell(Input.mousePosition, player.transform.position, _dir, _min, _max);
            }
        }
    }
}
