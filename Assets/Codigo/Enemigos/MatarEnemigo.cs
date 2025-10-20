using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatarEnemigo : MonoBehaviour
{
    [SerializeField]private ParticleSystem particle;
    [SerializeField]private GameObject moneda;
    [SerializeField]private float impulzo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Botas")
        {
            AudioManager.Instance.ReproducirSFX(6);
            Jugador_Controlador.Instance.GravedadY = impulzo;
            Instantiate(moneda, transform.position, Quaternion.identity);
            Instantiate(particle, transform.position + Vector3.up, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
