using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades
{
    public enum UpgradeType { handAttack, splashAttack, rapidAttack, fireBallAttackRate1, fireBallAttackRate2, fireHealth1, fireHealth2, woodValue, fireballDamage, rapidDamage, woodDrop, fireburst}

    private List<UpgradeType> unlockedUpgrades;

    public Upgrades ()
    {
        unlockedUpgrades = new List<UpgradeType>();
    }

    public void UnlockUpgrade(UpgradeType upgrade)
    {
        unlockedUpgrades.Add(upgrade);
    }

    public void RemoveUpgrade(UpgradeType upgrade)
    {
        unlockedUpgrades.Remove(upgrade);
    }

    public bool IsUpgradeUnlocked(UpgradeType upgrade)
    {
        return unlockedUpgrades.Contains(upgrade);
    }
}
