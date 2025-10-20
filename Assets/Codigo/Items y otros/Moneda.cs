using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    [SerializeField] private GameObject MonedaRomper;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.Instance.ReproducirSFX(5);
            GameManager.Instance.SumarMoneda();
            CanvasManager.Instance.MonedaCanvas();
            Instantiate(MonedaRomper,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
