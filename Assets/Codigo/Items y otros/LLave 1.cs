using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LLave1 : MonoBehaviour
{
    [SerializeField] private GameObject Particula;
    private void OnTriggerEnter(Collider other)
    {
        if(SceneManager.GetActiveScene().name == "Nivel 1")
        {
            Instantiate(Particula,transform.position,Quaternion.identity);
            AudioManager.Instance.ReproducirSFX(4);
            PlayerPrefs.SetString("1", "Nieve");
            Destroy(gameObject);
        }
        if (SceneManager.GetActiveScene().name == "Nivel 2")
        {
            Instantiate(Particula, transform.position, Quaternion.identity);
            AudioManager.Instance.ReproducirSFX(4);
            PlayerPrefs.SetString("2", "Fuego");
            Destroy(gameObject);
        }
    }
}
