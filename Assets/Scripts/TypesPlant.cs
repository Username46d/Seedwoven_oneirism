using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tiles/TileData")]
public class TypesPlant : ScriptableObject
{
    public RuleTile ruleTile;
    public bool isPlantable;
    public float growtMultiplier;
    public int bonusScore;
}
