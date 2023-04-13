using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityEditor.PlayerSettings;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] PlayerStat stat;
    public GameObject player;
    [FormerlySerializedAs("_playerComponent")] public Player playerComponent;
    [FormerlySerializedAs("_grid")] public Grid grid;



    public virtual void UseSkill(Dice dice)
    {

    }
    public virtual List<Vector3> MoveCell(Vector3 pos, int[] dir, int[] min, int[] max)
    {
        List<Vector3> result = GridManager.Inst.IntToCellWorldPos(pos, dir, min, max);
        return result;
    }
    public virtual void MoveToClickCell(Vector3 mousePos, Vector3 playerPos, int[] dir, int[] min, int[] max)
    {
        Vector3 clickedPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 targetPos = GridManager.Inst.MousePosToCellWorldPos(clickedPos);
        List<Vector3> result = GridManager.Inst.IntToCellWorldPos(playerPos, dir, min, max);

        if (result.Contains(targetPos))
        {
            playerComponent.MoveTransform(new PRS(targetPos, playerComponent.originPrs.rot, playerComponent.originPrs.scale), true, 0.2f);
            SkillManager.Inst.isUsingSkill = false;
        }
    }
    public virtual int SelectTarget(Vector3 pos)
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit.collider != null)
        {
            EnemyStat targetEnemyStat = hit.collider.GetComponent<EnemyStat>();
            int targetEnemyHp = targetEnemyStat.EnemyCurrentHp;
            return targetEnemyHp;
        }
        else
        {
            return 0;
        }
    }
    public virtual void AttackEnemy(int damage, int enemyHp)
    {
        enemyHp -= damage;
        if (enemyHp < 0 )
        {
            enemyHp = 0;
        }
    }
    public virtual void GetDamage(int damage)
    {
        if (stat.Armor > 0)
        {
            int overdamage = stat.Armor - damage;
            if (overdamage <= 0)
            {
                stat.Armor = 0;
                stat.PlayerCurrentHp += overdamage;
            }
            else
            {
                stat.Armor -= damage;
            }
        }
        else
        {
            stat.PlayerCurrentHp -= damage;

        }

        if (stat.PlayerCurrentHp <= 0)
        {
            stat.PlayerCurrentHp = 0;
        }
    }
    public virtual void RestoreHp(int restore)
    {
        stat.PlayerCurrentHp += restore;
        if (stat.PlayerCurrentHp >= stat.PlayerMaxHp)
        {
            stat.PlayerCurrentHp = stat.PlayerMaxHp;
        }
    }
    public virtual void GetBlock(int armor)
    {
        stat.Armor += (armor + stat.Reg);
    }

    public virtual void IncreaseMaxHp(int increment)
    {
        stat.PlayerMaxHp += increment;
    }

    public virtual void GetGold(int gold)
    {
        stat.Gold += gold;
    }
    public virtual void UseGold(int gold)
    {
        stat.Gold -= gold;
    }
    public virtual void GetExp(int exp) //레벨업 시스템
    {
        stat.Exp += exp;
    }
    public virtual void UseMp(int cost)
    {
        stat.PlayerCurrentMp -= cost;
    }
    public virtual void RestoreMp(int restore)
    {
        stat.PlayerCurrentMp += restore;

    }
    public virtual void IncreaseMaxMp(int increment)
    {
        stat.PlayerMaxMp += increment;
    }
}
