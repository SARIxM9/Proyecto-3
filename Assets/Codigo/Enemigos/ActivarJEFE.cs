using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarJEFE : MonoBehaviour
{
    [SerializeField] private GameObject[] Desaparecer;
    [SerializeField] private GameObject JEFE;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject objeto in Desaparecer)
            {
                objeto.SetActive(false);
            }
            JEFE.SetActive(true);
            AudioManager.Instance.Jefe();
            GameManager.Instance.CambiarPosicion(transform.position);
            Destroy(gameObject);
        }
    }
}
