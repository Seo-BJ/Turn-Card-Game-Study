using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.PlayerSettings;

public class HexStrike : Skill
{
    int[] _dir = { 1, 2, 3, 4, 5, 6 };
    int[] _min = { 1, 1, 1, 1, 1, 1 };
    int[] _max = { 2, 2, 2, 2, 2, 2 };

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void ExpressRange(int[] dir, int[] min, int[] max, string type)
    {
        base.ExpressRange(dir, min, max, type);
    }
    public override void BasicAttack(RaycastHit2D[] targetEnemys, Vector3 targetPos, bool canPenetrate, int damage)
    {
        base.BasicAttack(targetEnemys, targetPos, canPenetrate, damage);
    }
    void MousePosUI(int[] dir, int[] min, int[] max)
    {
        List<Vector3> targetPos = GridManager.Inst.IntToCellWorldPos(SkillManager.Inst.player.transform.position, dir, min, max);


    }
    // Update is called once per frame
    void Update()
    {
        if (SkillManager.Inst.isUsingSkill && isTargetSkill)
        {
            List<Vector3> targetPos = GridManager.Inst.IntToCellWorldPos(SkillManager.Inst.player.transform.position, _dir, _min, _max);
           

            if (Input.GetMouseButtonDown(0))
            {
                ExpressRange(_dir, _min, _max, "Attack");
                GameObject player = SkillManager.Inst.player;
                Vector3 targetWorldPos = GridManager.Inst.MousePosToCellWorldPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                Vector3 direction = targetWorldPos - player.transform.position;
                RaycastHit2D[] targetEnemys = Physics2D.RaycastAll(player.transform.position, direction); //range�� �߰�

                BasicAttack(targetEnemys, targetWorldPos, false, 8);
            }
        }
    }
    /*
    void test(Vector3 pos)
    {
        Vector3 targetWorldPos = GridManager.Inst.MousePosToCellWorldPos(Camera.main.ScreenToWorldPoint(pos));
        Vector3 direction = targetWorldPos - _player.transform.position;
        RaycastHit2D[] targetEnemys = Physics2D.RaycastAll(_player.transform.position, direction);
    }*/
    
    /*
    if (SkillManager.Inst.isUsingSkill)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 targetWorldPos = GridManager.Inst.MousePosToCellWorldPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                Vector3 direction = targetWorldPos- _player.transform.position;
                RaycastHit2D[] targetEnemys = Physics2D.RaycastAll(_player.transform.position, direction);

                for (int i = 0; i < targetEnemys.Length; i++)
                {
                    if (targetEnemys[i].transform.position == targetWorldPos && targetEnemys[i].transform.tag == "Enemy") 
                    {
                        EnemyTest targetStat = targetEnemys[i].transform.GetComponent<EnemyTest>();
                        targetStat.hp -= 8;
                        Debug.Log(targetStat.hp);
                        Debug.Log(targetEnemys[i].transform.name);
                        SkillManager.Inst.isUsingSkill = false;

                    }
                 
                }
            }
        }
     */
}
