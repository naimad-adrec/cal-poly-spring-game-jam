using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private int coalCost;
    [SerializeField] private Button currentbutton;
    private bool _purchased = false;

    public bool Purchased { get { return _purchased;  } set { _purchased = value;  } }

    private void Start()
    {
        currentbutton.interactable = true;
    }

    private void Update()
    {
        currentbutton.interactable = _purchased == false &&
            PlayerStateMachine.Instance.CoalCount >= coalCost;
    }

    public void SetPurchaseToTrue()
    {
        PlayerStateMachine.Instance.CoalCount -= coalCost;
        _purchased = true;
    }
}
