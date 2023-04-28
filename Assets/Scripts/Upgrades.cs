using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades
{
    public enum UpgradeType { handAttack, splashAttack, rapidAttack, fireBallAttackRate1, fireBallAttackRate2, fireHealth1, fireHealth2, woodValue, fireballDamage, rapidDamage, woodDrop, fireburst}

    private HashSet<UpgradeType> unlockedUpgrades;

    public Upgrades ()
    {
        unlockedUpgrades = new HashSet<UpgradeType>();
    }

    public void UnlockUpgrade(UpgradeType upgrade)
    {
        if (!unlockedUpgrades.Contains(upgrade))
            unlockedUpgrades.Add(upgrade);
    }

    public void RemoveUpgrade(UpgradeType upgrade)
    {
        if (unlockedUpgrades.Contains(upgrade))
            unlockedUpgrades.Remove(upgrade);
    }

    public bool IsUpgradeUnlocked(UpgradeType upgrade)
    {
        return unlockedUpgrades.Contains(upgrade);
    }
}
