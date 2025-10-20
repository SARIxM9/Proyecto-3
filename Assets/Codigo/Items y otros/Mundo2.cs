using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mundo2 : MonoBehaviour
{
    [SerializeField] private Animation animacion1;
    [SerializeField] private Animation animacion2;
    [SerializeField] private Transform PuntoM2;

    private void Start()
    {
        if (PlayerPrefs.HasKey("1"))
        {
            animacion1.Play();
            PlayerPrefs.DeleteKey("1");
        }
        if (PlayerPrefs.HasKey("2"))
        {
            GameManager.Instance.CambiarPosicion(PuntoM2.position);
            GameManager.Instance.Mundo2(PuntoM2);
            animacion2.Play();
            PlayerPrefs.DeleteKey("2");
        }
    }
}
