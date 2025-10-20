using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Vector3 PosicionInicial;
    public int Moneda;
    [SerializeField] private ParticleSystem Morir;
    
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CanvasManager.Instance.MonedaCanvas();
        PosicionInicial = Jugador_Controlador.Instance.transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ResetearJugador()
    {
        StartCoroutine(Resetear());
    }

    public void CambiarPosicion(Vector3 posicion)
    {
        PosicionInicial = posicion;
    }

    IEnumerator Resetear()
    {
        AudioManager.Instance.ReproducirSFX(2);
        Jugador_Controlador.Instance.gameObject.SetActive(false);
        Jugador_Controlador.Instance.Cine.enabled = false;
        CanvasManager.Instance.MorirAnimacion();
        Instantiate(Morir,Jugador_Controlador.Instance.transform.position,Quaternion.identity);
        yield return new WaitForSeconds(1f);
        CanvasManager.Instance.VidaCanvas();
        Corazon.Instance.VidasReset();
        Jugador_Controlador.Instance.transform.position = PosicionInicial;
        Jugador_Controlador.Instance.gameObject.SetActive(true);
        Jugador_Controlador.Instance.Cine.enabled = true;
    }

    public void SumarMoneda()
    {
        Moneda++;
    }

    public void Mundo2(Transform Punto2)
    {
        StartCoroutine(A(Punto2));
    }

    IEnumerator A(Transform Mundo2)
    {
        Jugador_Controlador.Instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Jugador_Controlador.Instance.transform.position = Mundo2.position;
        Jugador_Controlador.Instance.gameObject.SetActive(true);
    }


}
