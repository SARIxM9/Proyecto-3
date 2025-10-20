using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particula : MonoBehaviour
{
    [SerializeField] private float TiempoVida;

    private void Start()
    {
        Destroy(gameObject,TiempoVida);
    }
}
