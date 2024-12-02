using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSE : MonoBehaviour
{
    [SerializeField] private GameObject messageSEHipo;
    [SerializeField] private GameObject messageSETiger;

    private AudioSource AudioSourceHipo;
    private AudioSource AudioSourceTiger;

    private void Start()
    {
        AudioSourceHipo = messageSEHipo.GetComponent<AudioSource>();
        AudioSourceTiger = messageSETiger.GetComponent<AudioSource>();
    }

    public void Play(string who)
    {
        if (who == "Hipo")
        {
            Invoke(nameof(PlayMessageSEHipo), 1f);
        }
        else{
            Invoke(nameof(PlayMessageSETiger), 1f);
        }
    }

    private void PlayMessageSEHipo()
    {
        AudioSourceHipo.Play();
    }

    private void PlayMessageSETiger()
    {
        AudioSourceTiger.Play();
    }
}
