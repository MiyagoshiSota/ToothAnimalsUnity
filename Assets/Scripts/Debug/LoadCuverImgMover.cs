using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadCuverImgMover : MonoBehaviour
{
    [SerializeField] private Vector2 _velocity;
    private RectTransform rtf;

    private bool first = true;
    private bool wait = false;

    // Start is called before the first frame update
    void Start()
    {
        rtf = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!wait)
        {
            if (first)
            {
                if (rtf.anchoredPosition.x >= 2200)
                {
                    wait = true;
                    Invoke(nameof(resetPos), 2);
                }
                else
                {
                    rtf.anchoredPosition += _velocity * Time.deltaTime;
                }
            }
            else
            {
                if (rtf.anchoredPosition.x >= 46)
                {
                    StartCoroutine(LoadSceneAsync("TutrialScene"));
                }
                else
                {
                    rtf.anchoredPosition += _velocity * Time.deltaTime;
                }
            }
        }
    }

    private void resetPos()
    {
        rtf.anchoredPosition = new Vector2(-2400, 0);
        wait = false;
        first = false;
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
