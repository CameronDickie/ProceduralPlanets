using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilter : INoiseFilter
{
    Noise noise = new Noise();
    NoiseSettings.RidgidNoiseSettings settings;
    public RigidNoiseFilter(NoiseSettings.RidgidNoiseSettings settings)
    {
        this.settings = settings;
    }
    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;
        float weight = 1;

        for (int i = 0; i < settings.numLays; i++)
        {
            float v = 1-Mathf.Abs(noise.Evaluate(point * frequency + settings.center));
            v *= v;
            v *= weight;
            weight = Mathf.Clamp01(v * settings.weightMultiplier);
            //regions that start out low, remain relatively undetailed compared to regions that start higher-up

            noiseValue += amplitude * (v + 1) * 1 / 2;
            frequency *= settings.roughness;
            amplitude *= settings.persistence;
            //when roughness > 1 the frequency will increase with each layer, and when persistence < 1 amplitude will decrease
        }
        noiseValue = Mathf.Max(0, noiseValue - settings.minValue); // we do not want negative noise 
        return noiseValue * settings.strength;
    }
}
