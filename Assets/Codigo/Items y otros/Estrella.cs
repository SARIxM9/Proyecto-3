using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Estrella : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private bool siguiente;
    [SerializeField] private string escena;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.SetTrigger("Activar");
            CanvasManager.Instance.SiguienteNivel(siguiente,escena);
        }
    }
}
