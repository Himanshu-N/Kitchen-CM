using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] Transform plateVisual;
    [SerializeField] PlatesCounter platesCounter;
    [SerializeField] Transform counterTopPoint;

    private List<GameObject> plateVisualGameObjectList;

    private void Start()
    {
        plateVisualGameObjectList = new List<GameObject>();
        platesCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject lastPlateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(lastPlateGameObject);
        Destroy(lastPlateGameObject);
    }

    private void PlateCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisual, counterTopPoint);

        float plateVisualOffset = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateVisualOffset * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
