using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Image hpBackGround;
    [SerializeField] Sprite hpBackSprite;
    [SerializeField] Sprite armorBackSprite;

    [SerializeField] Image hpMeter;
    [SerializeField] Sprite hpMeterSprite;
    [SerializeField] Sprite armorMeterSprite;

    [SerializeField] PlayerStat stat;
    [SerializeField] TMP_Text currentHp;
    [FormerlySerializedAs("MaxHp")] [SerializeField] TMP_Text maxHp;
    [SerializeField] TMP_Text armor;

    [FormerlySerializedAs("currentMP")] [SerializeField] TMP_Text currentMp;
    [FormerlySerializedAs("MaxMP")] [SerializeField] TMP_Text maxMp;



    void Start()
    {
        //hpMeter.fillAmount = (float)stat.PlayerCurrentHp / (float)stat.PlayerMaxHp;
        HandleHpTextUI();
    }


    void Update()
    {
        HandleHpTextUI();

        /*
        if (Input.GetKeyUp(KeyCode.V))
        {
            PlayerController.Inst.GetDamage(10);
            HandleHpRatio();
            Handle_Hp_TextUI();
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            PlayerController.Inst.RestoreHp(10);
            HandleHpRatio();
            Handle_Hp_TextUI();
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            PlayerController.Inst.GainArmor(10);
            HandleHpRatio();
            Handle_Hp_TextUI();
        }
        */
    }
    void ArmorOrHp()
    {
        if (stat.Armor > 0)
        {
            hpMeter.sprite = armorMeterSprite;
            hpBackGround.sprite = armorBackSprite;
        }
        else
        {
            hpMeter.sprite = hpMeterSprite;
            hpBackGround.sprite = hpBackSprite;
        }
    }
    private void HandleHpRatio()
    {
        hpMeter.fillAmount = (float)stat.PlayerCurrentHp / (float)stat.PlayerMaxHp;
    }
    private void HandleHpTextUI()
    {
        currentHp.text = stat.PlayerCurrentHp.ToString();
        maxHp.text = stat.PlayerMaxHp.ToString();

        armor.text = stat.Armor.ToString();
        /*
        currentMP.text = stat.PlayerCurrentMP.ToString();
        MaxMP.text = stat.PlayerMaxMP.ToString();
        */
    }
    
}
