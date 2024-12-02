using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDestroy : MonoBehaviour
{
    public void GameobjectDestroy(GameObject obj)
    {
        Destroy(obj);
    }
}
