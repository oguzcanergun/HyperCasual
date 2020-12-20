using System;
using UnityEngine;
using Backbones;

public class CustomNameAttribute : PropertyAttribute
{
    public readonly string[] names;
    public CustomNameAttribute()
    {
        this.names = Enum.GetNames(typeof(BallColour));
    }
}
