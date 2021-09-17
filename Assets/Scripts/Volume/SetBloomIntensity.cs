using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SetBloomIntensity : MonoBehaviour
{
    private SettingsData data;

    private Volume volume;
    private Bloom bloom;

    public float baseIntensity = 0;


    void Awake()
    {
        data = SettingsSaveSystem.LoadSettingsData();

        volume = GetComponent<Volume>();
        volume.profile.TryGet(out bloom);

        SetBloom();
    }

    public void SetBloom(float bloomPower = -1){
        if(bloomPower == -1){
            bloom.intensity.value = data.bloomPower * 1/100 * baseIntensity;
        }
        else{
            bloom.intensity.value = bloomPower * 1/100 * baseIntensity;
        }
    }
}
