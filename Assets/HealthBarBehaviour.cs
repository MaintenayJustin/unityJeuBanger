using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[Serializable]
public struct ColorThreshold
{
    public Color Color;
    public float Threshold;
}
public class HealthBarBehaviour : MonoBehaviour
{
    public int Percent;
    public ColorThreshold[] Colors;
    private Image _image;
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
    }
    private void OnValidate()
    {
        SetHealth(Percent, 1);
    }
    public void SetHealth(int currentHealth, int maxHealth)
    {
        float ratio = currentHealth / maxHealth;
        // _image.fillAmount = ratio;

    }
}
