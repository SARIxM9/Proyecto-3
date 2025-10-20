using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [SerializeField] private ParticleSystem Romper;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.Instance.ReproducirSFX(7);
            Corazon.Instance.SumarVidas();
            Instantiate(Romper,Jugador_Controlador.Instance.transform.position + Vector3.up,Quaternion.identity);
            Desaparecer();
        }
    }

    public void Desaparecer()
    {
        Destroy(gameObject);
    }
}
