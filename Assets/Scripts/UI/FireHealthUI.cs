using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireHealthUI : MonoBehaviour
{
    [SerializeField] private Slider fireHealthSlider;

    private void Start()
    {
        fireHealthSlider.value = FireController.Instance.FireHealth;
    }

    private void Update()
    {
        fireHealthSlider.value = FireController.Instance.FireHealth;
    }
}
