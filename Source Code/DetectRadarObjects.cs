using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectRadarObjects : MonoBehaviour
{
    public Image image;

    void Start()
    {
        Radar.RegisterRadarObject(this.gameObject, image);
    }
    void OnDestroy()
    {
        Radar.RemoveRadarObject(this.gameObject);
    }
}
