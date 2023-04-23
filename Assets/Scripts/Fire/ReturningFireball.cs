using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturningFireball : MonoBehaviour
{
    public float MaxDistance { get; set; } = 1.0f;
    public float Angle { get; set; } = 0.0f;
    public float Lifetime { get; set; } = 1.0f;
    public Vector2 Displacement { get; set; } = Vector2.zero;

    private double TimeCreated { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        TimeCreated = Time.timeAsDouble;
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

        float normalizedTime = (float)(currentTime - TimeCreated) / Lifetime;
        float distanceMultiplier = -4.0f * Mathf.Pow(normalizedTime - 0.5f, 2.0f) + 1.0f;
        Vector2 distanceVec = new(Mathf.Cos(Mathf.Deg2Rad * Angle), Mathf.Sin(Mathf.Deg2Rad * Angle));

        transform.localPosition = MaxDistance * distanceMultiplier * distanceVec + Displacement;
        transform.localRotation = Quaternion.Euler(0f, 0f, Angle + 45.0f + (normalizedTime < 0.5f ? 0.0f : 180.0f));
    }
}
