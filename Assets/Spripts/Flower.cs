using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowerColor
{
    White,
    Red,
    Yellow,
    Blue,
    Green,
    Violet,
    Orange
};

[CreateAssetMenu]
public class Flower : ScriptableObject
{
    public Sprite imageC;
    public Sprite imageL;
    public Sprite imageR;
    public FlowerColor color;
    public string size;
    public string flowerName;
}
