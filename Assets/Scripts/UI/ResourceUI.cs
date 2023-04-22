using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI woodCount;
    [SerializeField] private TextMeshProUGUI coalCount;

    private void Start()
    {
        woodCount.text = "Wood: " + PlayerStateMachine.Instance.WoodCount.ToString();
        coalCount.text = "Coal: " + PlayerStateMachine.Instance.CoalCount.ToString();
    }

    private void Update()
    {
        woodCount.text = "Wood: " + PlayerStateMachine.Instance.WoodCount.ToString();
        coalCount.text = "Coal: " + PlayerStateMachine.Instance.CoalCount.ToString();
    }
}
