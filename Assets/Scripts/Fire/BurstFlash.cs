using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstFlash : MonoBehaviour
{
    public float MaxSize { get; set; } = 1.0f;
    public float RotationSpeed { get; set; } = 90.0f;
    public float Lifetime { get; set; } = 1.0f;

    private double TimeCreated { get; set; }
    private float CurrentRotation { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        TimeCreated = Time.timeAsDouble;
        CurrentRotation = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        double currentTime = Time.timeAsDouble;
        if (currentTime > TimeCreated + Lifetime)
        {
            Destroy(gameObject);
            return;
        }

        float normalizedTime = (float) (currentTime - TimeCreated) / Lifetime;
        float scaleMultiplier = -4.0f * Mathf.Pow(normalizedTime - 0.5f, 2.0f) + 1.0f;
        transform.localScale = scaleMultiplier * MaxSize * Vector3.one;

        CurrentRotation += RotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0f, 0f, CurrentRotation);
    }
}
