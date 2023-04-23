using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private int coalCost;
    [SerializeField] private Button currentbutton;
    private bool purchased = false;

    private void Start()
    {
        currentbutton.interactable = true;
    }

    private void Update()
    {
        if (purchased == false)
        {
            if (PlayerStateMachine.Instance.CoalCount >= coalCost)
            {
                currentbutton.interactable = true;
            }
            else
            {
                currentbutton.interactable = false;
            }
        }
    }

    public void SetPurchaseToTrue()
    {
        purchased = true;
    }
}
