using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOfDays : MonoBehaviour
{
    public float time;
    //public static TimeOfDays instance;
    //public Light DirectionalLight = GameObject.Find("Directional Light").GetComponent<Light>();
    public Light DirectionalLight;
    public LightingPreset Preset;

    public Material Day;
    public Material Night;

    bool day = false;

    public void SetTime(float _time)
    {
        time = _time;
        transform.RotateAround(Vector3.zero, Vector3.right, 30f * _time);
        transform.LookAt(Vector3.zero);
    }

    public void UpdateLighting(float _time)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(_time);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(_time);

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(_time);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((_time * 360f) - 90f, 170f, 0));
        }

        _time *= 24;
        time = _time;
        if (_time <= 6f || _time > 19.5f && !day) //night
        {
            RenderSettings.skybox = Night;
            day = true;


        }
        else if(_time > 6f && _time < 6.5f && day) //day
        {
            RenderSettings.skybox = Day;
            day = false;
        }

    }
}
