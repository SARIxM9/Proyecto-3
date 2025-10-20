using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.SetTrigger("Activar");
            AudioManager.Instance.Fin();
            CanvasManager.Instance.SalirFIN();
        }
    }
}
