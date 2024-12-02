using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutrialToothManager : MonoBehaviour
{
    [SerializeField] private GameObject tutrialTooth;

    private void Start()
    {
        tutrialTooth.GetComponent<Tooth>().MakeDecayed();
    }

    public void FixDecay()
    {
        tutrialTooth.GetComponent<Tooth>().Heal();
    }
}
