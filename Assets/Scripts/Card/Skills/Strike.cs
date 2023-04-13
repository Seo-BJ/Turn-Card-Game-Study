using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityEditor.PlayerSettings;

public class Strike : PlayerSkill
{
    [FormerlySerializedAs("_dicenumber")] public int dicenumber = 0;
    [FormerlySerializedAs("_isSelected")] public bool isSelected = false;
    public override void AttackEnemy(int damage, int enemyHp)
    {
        base.AttackEnemy(damage, enemyHp);
        Debug.Log(damage);
        Debug.Log(enemyHp);
    }
    public override int SelectTarget(Vector3 pos)
    {
        return base.SelectTarget(pos);

    }
    
    //private Coroutine attackCoroutine = null;
    public override void UseSkill(Dice dice)
    {
        if(!isSelected)
        {
            isSelected = true;
            SkillManager.Inst.isUsingSkill = true;
            dicenumber = dice.diceNumber;
        }
    }
    void Update()
    {
        if (isSelected)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Vector2 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector2.zero);
                
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("적발견");
                    GameObject targetEnemy = hit.collider.gameObject;
                    targetEnemy.GetComponent<EnemyUI>().stat.EnemyCurrentHp -= dicenumber;
                    
                    isSelected = false;
                    dicenumber= 0;
                    SkillManager.Inst.isUsingSkill = false;
                    targetEnemy.GetComponent<EnemyUI>().targetImage.SetActive(false);
                }
            }
        }
    }



    /*
    public override void UseSkill(int diceNumber)
    {
        if(!isSelected)
        {
            isSelected = true;
            Debug.Log(isSelected);
            attackCoroutine = StartCoroutine(CoStrike(diceNumber));
        }
    }
    IEnumerator CoStrike(int diceNumber)
    {
        while (true)
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.tag == "Enemy")
                {
                    Debug.Log("적발견");
                    int enemyHP = hitObject.GetComponent<EnemyStat>()._enemyHP;
                    AttackEnemy(diceNumber, enemyHP);
                    isSelected = false;
                    yield break;
                }
            }
            
            yield return null;
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && isSelected)
        {
            StopCoroutine(attackCoroutine); 
            isSelected = false;
            Debug.Log("코루틴 종료");
        }
    }
     */


    /*
    IEnumerator Test(int damage)
    {
        Debug.Log("코루틴시작");
        return test2();
        return test1(_targetPos, damage);
        Debug.Log("코루틴종료");
    }
    
    IEnumerator Method1()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _enemyHP = SelectTarget(Utils.MousePos);
        }
        yield return null;
    }
    IEnumerator Method2(int damage)
    {
        AttackEnemy(damage, _enemyHP);
        yield return null;
    }
    IEnumerator test1(Vector3 pos, int damage)
    {
        Debug.Log(damage);
        
        RaycastHit2D hit;
        hit = Physics2D.Raycast(pos, Vector2.zero);
        if (hit.collider != null)
        {
            EnemyStat targetEnemyStat = hit.collider.GetComponent<EnemyStat>();
            int targetEnemyHP = targetEnemyStat._enemyHP;
            targetEnemyHP -= damage;
            if (targetEnemyHP < 0)
            {
                targetEnemyHP = 0;
            }
            yield return null;

        }
        Debug.Log("체력감소");
        
    }
    IEnumerator test2()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Utils.MousePos);
            _targetPos = Utils.MousePos;
            yield return null;
        }      
    }
  */












}
