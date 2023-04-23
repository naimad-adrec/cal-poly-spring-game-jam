using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades
{
    public enum UpgradeType { handAttack, splashAttack, rapidAttack, fireBallAttackRate, fireHealth, woodValue, }

    private List<UpgradeType> unlockedUpgrades;
    private List<UpgradeType> lockedUpgrades;

    public Upgrades ()
    {
        unlockedUpgrades = new List<UpgradeType>();
        lockedUpgrades = new List<UpgradeType>();
    }

    public void UnlockUpgrade(UpgradeType upgrade)
    {
        unlockedUpgrades.Add(upgrade);
        lockedUpgrades.Remove(upgrade);
    }

    public bool IsUpgradeUnlocked(UpgradeType upgrade)
    {
        return unlockedUpgrades.Contains(upgrade);
    }
}
