using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roundCount;

    private void Start()
    {
        roundCount.text = RoundController.Instance.RoundCount.ToString();
    }

    private void Update()
    {
        roundCount.text = RoundController.Instance.RoundCount.ToString();
    }
}
