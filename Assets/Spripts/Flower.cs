using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowerColor
{
    White,
    Yellow,
    LightOrange,
    Orange,
    DarkOrange,
    Red,
    Pink,
    Violet,
    DarkBlue,
    Blue,
    LightBlue,
    Green,
    LightGreen
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
