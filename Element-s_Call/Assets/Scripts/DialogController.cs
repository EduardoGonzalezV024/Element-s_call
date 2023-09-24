using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    [SerializeField] private AudioSource[] characters;
    [SerializeField] private DialogClass[] audios;

    private int cont = 0;
    private int characterIndex = 0;

    private void Awake()
    {
        Invoke("playDialog", audios[cont].initialDelay);
    }
    void Update()
    {
        if (!characters[characterIndex].isPlaying)
        {
            Invoke("playDialog", audios[cont].initialDelay);
        }
    }

    void playDialog()
    {
        CancelInvoke();
        characterIndex = audios[cont].characterIndex;
        characters[characterIndex].PlayOneShot(audios[cont].clip);
        cont++;
    }
}
