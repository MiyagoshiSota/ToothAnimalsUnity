using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(TigerSenser.Instance.GetValue());

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TigerSenser.Instance.SendWriteUpNum("0");
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
            TigerSenser.Instance.SendWriteUpNum("1");
        }
    }
}
