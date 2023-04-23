using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillTree : MonoBehaviour
{

    // Button Variables
    [SerializeField] private Button fireRateOne;
    [SerializeField] private Button fireHealthOne;
    [SerializeField] private Button fireDamageOne;
    [SerializeField] private Button woodValueOne;
    [SerializeField] private Button rapidUnlock;
    [SerializeField] private Button fireRateTwo;
    [SerializeField] private Button woodDrop;
    [SerializeField] private Button rapidDamage;
    [SerializeField] private Button handUnlock;
    [SerializeField] private Button splashUnlock;
    [SerializeField] private Button fireHealthTwo;
    [SerializeField] private Button fireburstUnlock;

    private Upgrades upgrades;

    private void Update()
    {
        if (rapidUnlock.GetComponent<UpgradeButton>().Purchased == true)
        {
            upgrades.UnlockUpgrade(Upgrades.UpgradeType.rapidAttack);
        }
        if(handUnlock.GetComponent<UpgradeButton>().Purchased == true)
        {
            upgrades.UnlockUpgrade(Upgrades.UpgradeType.handAttack);
        }
        if (splashUnlock.GetComponent<UpgradeButton>().Purchased == true)
        {
            upgrades.UnlockUpgrade(Upgrades.UpgradeType.splashAttack);
        }
        if (fireburstUnlock.GetComponent<UpgradeButton>().Purchased == true)
        {
            upgrades.UnlockUpgrade(Upgrades.UpgradeType.fireBurst);
        }
    }
}
