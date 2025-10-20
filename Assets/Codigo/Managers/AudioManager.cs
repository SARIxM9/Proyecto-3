using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]private AudioSource[] Musica;
    [SerializeField]private AudioSource[] SFX;

    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ReproducirMusica(6);
    }

    public void ReproducirMusica(int musica)
    {
        Musica[musica].Play();
    }

    public void ReproducirSFX(int sfx)
    {
        SFX[sfx].Play();
    }

    public void Jefe()
    {
        Musica[6].enabled = false;
        Musica[7].enabled = true;
    }

    public void Fin()
    {
        Musica[6].enabled = true;
        Musica[7].enabled = false;
    }
}
