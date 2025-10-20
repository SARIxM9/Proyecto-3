using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    [SerializeField] private Transform[] Puntos;
    [SerializeField] private int indice;
    [SerializeField] private int velocidad;


    private void Update()
    {
        Mover();
    }

    public void Mover()
    {
        transform.position = Vector3.MoveTowards(transform.position, Puntos[indice].position,velocidad * Time.deltaTime);
        Direccion();
    }

    public void Direccion() 
    {
        if (Vector3.Distance(transform.position, Puntos[indice].position) <= 0.1f)
        {
            indice++;
            if (indice >= Puntos.Length)
            {
                indice = 0;
            }
        }
    }
}
