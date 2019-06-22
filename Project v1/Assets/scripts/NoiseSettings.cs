using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class NoiseSettings
{
    
    public enum FilterType {Simple, Ridgid};
    public FilterType filterType;
    [ConditionalHide("filterType", 0)]
    public SimpleNoiseSettings simpleNoiseSettings;
    [ConditionalHide("filterType", 1)]
    public RidgidNoiseSettings ridgidNoiseSettings;

    //all the attributes that go with each respective noiseFilter
    [System.Serializable]
    public class SimpleNoiseSettings
    {
        public enum FilterType { Simple, Ridgid };
        public float strength = 1;
        [Range(-10, 10)]
        public float minValue = 1f;
        [Range(1, 8)]
        public int numLays = 1;
        public float baseRoughness = 1;
        public float roughness = 2;
        public float persistence = .5f;
        public Vector3 center;
    }
    [System.Serializable]
    public class RidgidNoiseSettings : SimpleNoiseSettings
    {
        public float weightMultiplier = .8f;
    }

}
