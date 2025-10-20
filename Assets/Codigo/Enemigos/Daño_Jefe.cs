using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daño_Jefe : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Jefe_Controlador.Instance.MatarJefe();
        }
    }
}
