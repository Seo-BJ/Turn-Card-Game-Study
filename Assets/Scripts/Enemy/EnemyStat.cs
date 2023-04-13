using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EnemyStatSO", menuName = "SO/Enemy Stat SO")]
public class EnemyStat : ScriptableObject
{
    [FormerlySerializedAs("enemyMaxHP")] [SerializeField] private int enemyMaxHp ;
    [FormerlySerializedAs("enemyCurrentHP")] [SerializeField] private int enemyCurrentHp ;

    [SerializeField] private int armor = 0;

    [SerializeField] private int str = 0;
    [SerializeField] private int reg = 0; //슬더스의 민첩

    public int EnemyMaxHp { get { return enemyMaxHp; } set { enemyMaxHp = value; } }
    public int EnemyCurrentHp { get { return enemyCurrentHp; } set { enemyCurrentHp = value; } }
    public int Armor { get { return armor; } set { armor = value; } }
    public int Str { get { return str; } set { str = value; } }
    public int Reg { get { return str; } set { reg = value; } }

}
