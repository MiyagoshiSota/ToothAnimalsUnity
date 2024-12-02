using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    [SerializeField] private string scenename;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Q)){
            LoadNextSceneAsync();
        }
    }

    public void LoadNextSceneAsync()
    {
        StartCoroutine(LoadSceneAsync(scenename));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
