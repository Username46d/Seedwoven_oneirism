using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[CreateAssetMenu(fileName = "Plants", menuName = "Plants/Plants Type")]
public class Plants : ScriptableObject
{
    public GameObject growtTile;
    public string descript;
    public int price;
    public Vector3Int position;
    public int growtTime = 3;
    public TypesFlower typesFlower;
    public TypesFlower[] CombinationFlowers;
    public Sized sized = new Sized(0.2f, 1f);
    public int maxStages = 1;
    public int currentStadia = 0;
    public int combinatiedIndex = 1;

    
    public virtual void Apply(){ return; }
    public virtual void Apply(int combinationCoef) { return; }
}
public struct Sized
{
    public float startSize;
    public float endSize;

    public Sized(float s, float e) => (startSize, endSize) = (s, e);
}
public enum TypesFlower
{
    BluePoint,
    PurplePoint,
    RedScore,
    TileOpenFlower,
    YellowOpenFlower,
    SpecialFlower
}
