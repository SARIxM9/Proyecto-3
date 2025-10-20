using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe_Controlador : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float impulzo;
    [SerializeField] private GameObject Fin;
    public enum Estado
    {
        inicio,
        atque1,
        atque2,
        fin,
    }
    public Estado Jefe;

    public static Jefe_Controlador Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void MatarJefe()
    {
        Jugador_Controlador.Instance.GravedadY = impulzo;
        Jefe++;
        animator.SetTrigger("Daño");
        AudioManager.Instance.ReproducirSFX(1);

        if(Jefe == Estado.fin)
        {
            animator.SetTrigger("Fin");
            AudioManager.Instance.ReproducirSFX(0);
            StartCoroutine(Final());
        }

        switch(Jefe)
        {
            case Estado.atque1:
                animator.SetBool("Fase1", true);
                break; 
            case Estado.atque2:
                animator.SetBool("Fase1", false);
                animator.SetBool("Fase2", true);
                break;
        }
    }

    IEnumerator Final()
    {
        yield return new WaitForSeconds(10);
        Fin.SetActive(true);
    }
}
