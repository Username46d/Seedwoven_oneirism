using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthManager : MonoBehaviour
{
    public void Register(Vector3Int position)
    {
        Plants plant = TIleManager.Instance.getPlant(position);
        Debug.Log("Выполнен дебаг лог по позиции " + position + " текущая стадия " + plant.currentStadia);
        if (plant.currentStadia < plant.maxStages)
        {
            plant.currentStadia += 1;
            Debug.Log("Растение выросло на стадию " + plant.currentStadia);
            GameManager.Instance?.RegisterSeed(plant.position, plant.growtTime);
            //TIleManager.Instance?.AddTile(plant.growtTiles[plant.currentStadia], position);
            TIleManager.Instance?.AddTile(position);
        }
        else
        {
            //TIleManager.Instance?.OpenTiles(position, 1);
            plant.Apply();
        }
    }
}
