using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Skill : MonoBehaviour
{
    [FormerlySerializedAs("_isTargetSkill")] public bool isTargetSkill;
    void Start()
    {
   
    }
    
    void TargetSkill()
    {
        SkillManager.Inst.isUsingSkill = true;
    }
    public virtual void BasicAttack(RaycastHit2D[] targetEnemys, Vector3 targetPos, bool canPenetrate, int damage)
    {
        for (int i = 0; i < targetEnemys.Length; i++)
        {
            if (targetEnemys[i].transform.position == targetPos && targetEnemys[i].transform.CompareTag("Enemy"))
            {
                if (canPenetrate)
                {
                    EnemyTest targetStat = targetEnemys[i].transform.GetComponent<EnemyTest>();
                    GetDamageTest(targetStat, damage);
                    AfterUseTargetSkill();
                    Debug.Log(targetEnemys[i].transform.name);
                    
                }
                else
                {
                    EnemyTest targetStat = targetEnemys[0].transform.GetComponent<EnemyTest>();
                    GetDamageTest(targetStat, damage);
                    AfterUseTargetSkill();
                    Debug.Log(targetEnemys[0].transform.name);
                    Debug.Log("관통불가");
                }

            }
        }
    }
    public virtual void ExpressRange(int[] dir, int[] min, int[] max, string type)
    {
        BaseCardData cardData = gameObject.GetComponent<CardUI>().BaseCardData;
        int range = cardData.Range;
        List<Vector3> rangePos = GridManager.Inst.IntToCellWorldPos(SkillManager.Inst.player.transform.position, dir, min, max);
        GridManager.Inst.InstantiateGridUI(rangePos, type);
    }
    void GetDamageTest(EnemyTest targetStat, int damage)
    {
        targetStat.Hp -= damage;
        Debug.Log(targetStat.Hp);
        
    }
    void AfterUseTargetSkill()
    {
        DeckManager.Inst.AfterUseSkill();
        SkillManager.Inst.isUsingSkill = false;
        SkillManager.Inst.HidePanel();
        GridManager.Inst.DestroyAllColorTile();
        DeckManager.Inst.selectCard = null;
        isTargetSkill = false;

    }
    

}
