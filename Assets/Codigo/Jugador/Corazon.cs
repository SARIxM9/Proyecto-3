using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corazon : MonoBehaviour
{
    [SerializeField] private int vidas;
    public int vidasVariable;
    [SerializeField] private float Invencible;
    [SerializeField] private float InvencibleVariable;
    [SerializeField] private GameObject[] RobotPartes;

    public static Corazon Instance;

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

    private void Start()
    {
        VidasReset();
    }

    private void Update()
    {
        if (InvencibleVariable > 0)
        {
            InvencibleVariable -= Time.deltaTime;
            for(int i = 0; i < RobotPartes.Length; i++)
            {
                if (Mathf.Floor(InvencibleVariable * 5) % 2 == 0)
                {
                    RobotPartes[i].SetActive(true);
                }
                else
                {
                    RobotPartes[i].SetActive(false);
                }
                if(InvencibleVariable <= 0)
                {
                    RobotPartes[i].SetActive(true);
                }
            }
        }
    }

    public void VidasReset()
    {
        vidasVariable = vidas;
        CanvasManager.Instance.VidaCanvas();
    }

    public void RestarVidas(int restar)
    {
        if (InvencibleVariable <= 0)
        {
            vidasVariable -= restar;
            CanvasManager.Instance.VidaCanvas();
            if (vidasVariable <= 0)
            {
                GameManager.Instance.ResetearJugador();
                CanvasManager.Instance.VidaCanvas();
            }
            else
            {
                Jugador_Controlador.Instance.Aturdir();
                InvencibleVariable = Invencible;
            }
        }
        
    }

    public void SumarVidas()
    {
        vidasVariable++;
        if (vidasVariable > vidas)
        {
            VidasReset();
        }
        CanvasManager.Instance.VidaCanvas();
    }
}
