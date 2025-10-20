using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MatarJugador : MonoBehaviour
{
    [SerializeField] private int QuitarVida;
    [SerializeField] private bool MUERTE;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!MUERTE)
            {
                Corazon.Instance.RestarVidas(QuitarVida);
            }
            else
            {
                GameManager.Instance.ResetearJugador();
                CanvasManager.Instance.VidaCanvas();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!MUERTE)
        {
            Corazon.Instance.RestarVidas(QuitarVida);
        }
        else
        {
            GameManager.Instance.ResetearJugador();
            CanvasManager.Instance.VidaCanvas();
        }
    }
}
