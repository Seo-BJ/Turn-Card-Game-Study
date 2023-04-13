using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [FormerlySerializedAs("_targetImage")] public GameObject targetImage;
    [SerializeField] Image hpBackGround;
    [SerializeField] Sprite hpBackSprite;
    [SerializeField] Sprite armorBackSprite;

    [SerializeField] Image hpMeter;
    [SerializeField] Sprite hpMeterSprite;
    [SerializeField] Sprite armorMeterSprite;

    public EnemyStat stat;
    [SerializeField] TMP_Text currentHp;
    [FormerlySerializedAs("MaxHp")] [SerializeField] TMP_Text maxHp;
    [SerializeField] TMP_Text armor;




    void Start()
    {
        
        //hpMeter.fillAmount = (float)stat.PlayerCurrentHp / (float)stat.PlayerMaxHp;
        HandleHpTextUI();
    }
    private void OnMouseEnter()
    {
        if (SkillManager.Inst.isUsingSkill)
        {
            targetImage.SetActive(true);
        }

    }
    private void OnMouseExit()
    {
        if (SkillManager.Inst.isUsingSkill)
        {
            targetImage.SetActive(false);
        }
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
        hpMeter.fillAmount = (float)stat.EnemyCurrentHp / (float)stat.EnemyMaxHp;
    }
    private void HandleHpTextUI()
    {
        currentHp.text = stat.EnemyCurrentHp.ToString();
        maxHp.text = stat.EnemyMaxHp.ToString();

        armor.text = stat.Armor.ToString();
  
    }
}
