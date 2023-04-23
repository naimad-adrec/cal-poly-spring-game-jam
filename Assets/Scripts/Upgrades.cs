using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades
{
    public enum UpgradeType { handAttack, splashAttack, rapidAttack, fireBallAttackRate, fireHealth, woodValue, fireBurst, woodDrop}

    private List<UpgradeType> unlockedUpgrades;

    public Upgrades ()
    {
        unlockedUpgrades = new List<UpgradeType>();
    }

    public void UnlockUpgrade(UpgradeType upgrade)
    {
        unlockedUpgrades.Add(upgrade);
    }

    public bool IsUpgradeUnlocked(UpgradeType upgrade)
    {
        return unlockedUpgrades.Contains(upgrade);
    }
}
