using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "PlayerStatSO", menuName = "SO/Player Stat SO")]
public class PlayerStat : ScriptableObject
{
    [FormerlySerializedAs("playerMaxHP")] [SerializeField] private int playerMaxHp = 100;
    [FormerlySerializedAs("playerCurrentHP")] [SerializeField] private int playerCurrentHp = 100;

    [SerializeField] private int armor = 0;

    [FormerlySerializedAs("playerMaxMP")] [SerializeField] private int playerMaxMp = 3;
    [FormerlySerializedAs("playerCurrentMP")] [SerializeField] private int playerCurrentMp = 3;

    [SerializeField] private int playerDiceCount = 4;
    [SerializeField] private int playerMaxDiceCount = 5;


    [SerializeField] private int str = 0;
    [SerializeField] private int reg = 0; //�������� ��ø

    [SerializeField] private int gold = 100;
    [SerializeField] private int exp = 0;


    public int PlayerMaxHp { get { return playerMaxHp; } set { playerMaxHp = value; } }
    public int PlayerCurrentHp { get { return playerCurrentHp; } set { playerCurrentHp = value; } }
    public int Armor { get { return armor; } set { armor = value; } }
    public int PlayerMaxMp { get { return playerMaxMp; } set { playerMaxMp = value; } }
    public int PlayerCurrentMp { get { return playerCurrentMp; } set { playerCurrentMp = value; } }
    public int PlayerDiceCount { get { return playerDiceCount; } set { playerDiceCount = value; } }
    public int PlayerMaxDiceCount { get { return playerMaxDiceCount; } set { playerMaxDiceCount = value; } }
    public int Str { get { return str; } set { str = value; } }
    public int Reg { get { return str; } set { reg = value; } }
    public int Gold { get { return gold; } set { gold = value; } }
    public int Exp { get { return exp; } set { exp = value; } }







}

