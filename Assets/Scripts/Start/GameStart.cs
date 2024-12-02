using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField] private string scenename;
    [SerializeField] private GameObject titleCallObj;
    private bool _nextScene;
    private AudioSource _titleAudio;
    private float _timer;
    private void Start()
    {
        _nextScene = false;
        _titleAudio = titleCallObj.GetComponent<AudioSource>();
        
        MarmotSenser.Instance.SendWriteUpNum("0");
    }

    private void Update()
    {
        if (_timer > 3)
        {
            string value = MarmotSenser.Instance.GetValue();
            string[] tmpAr = value.Split(',');

            if (int.Parse(tmpAr[1]) > 150 && !_nextScene)
            {
                _nextScene = true;
                _titleAudio.Play();
                Invoke(nameof(LoadNextSceneAsync), 2);
            }else if (Input.GetKeyDown(KeyCode.Space))
            {
                _titleAudio.Play();
                Invoke(nameof(LoadNextSceneAsync), 2);
            }
        }
        else
        {
            _timer += Time.deltaTime;
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