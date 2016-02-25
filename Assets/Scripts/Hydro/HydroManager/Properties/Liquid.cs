using System;
using UnityEngine;

[Serializable]
public class Liquid
{
    [SerializeField]
    private Physics physics;
    public Physics Physics
    {
        get
        {
            return physics;
        }
    }

    [SerializeField]
    private Color color;
    public Color Color
    {
        get
        {
            return color;
        }
    }
}