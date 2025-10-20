using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoverEnemigo : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform[] punto;
    [SerializeField] private Animator animator;
    [SerializeField] private int index;
    [SerializeField] private float Esperar;
    [SerializeField] private float Detectar;
    [SerializeField] private float EnemigoAtaque;
    private float EnemigoAtaqueVariable;
    private float EsperarVariable;

    public enum Maquina
    {
        Patrulla,
        Perseguir,
        Regresar,
        Atacar
    }
    public Maquina Estado;

    private void Start()
    {
        EsperarVariable = Esperar;
        EnemigoAtaqueVariable = EnemigoAtaque;
    }

    void Update()
    {
        switch (Estado)
        {
            case Maquina.Patrulla:

                agent.SetDestination(punto[index].position);

                if (agent.remainingDistance <= 0.1f)
                {
                    if (EsperarVariable <= 0)
                    {
                        animator.SetBool("Esperar", false);
                        index++;
                        EsperarVariable = Esperar;
                        if (index >= punto.Length)
                        {
                            index = 0;
                        }
                    }
                    else
                    {
                        EsperarVariable -= Time.deltaTime;
                        animator.SetBool("Esperar", true);
                    }
                }
                if (Vector3.Distance(transform.position,Jugador_Controlador.Instance.transform.position) <= Detectar)
                {
                    Estado = Maquina.Perseguir;
                }
                break;

            case Maquina.Perseguir:

                animator.SetBool("Esperar", false);
                agent.SetDestination(Jugador_Controlador.Instance.transform.position);
                if(Vector3.Distance(transform.position, Jugador_Controlador.Instance.transform.position) > Detectar)
                {
                    Estado = Maquina.Regresar;
                }
                if (Vector3.Distance(transform.position, Jugador_Controlador.Instance.transform.position) <= 1)
                {
                    Estado = Maquina.Atacar;
                }
                break;

            case Maquina.Regresar:

                agent.SetDestination(punto[index].position);
                if (agent.remainingDistance <= 0.5f)
                {
                    Estado = Maquina.Patrulla;
                }
                break;

            case Maquina.Atacar:

                transform.LookAt(Jugador_Controlador.Instance.transform.position);
                transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

                agent.velocity = Vector3.zero;
                agent.isStopped = true;
                animator.Play("Atacar");
                if(EnemigoAtaqueVariable <= 0)
                {
                    EnemigoAtaqueVariable = EnemigoAtaque;
                    agent.isStopped = false;
                    Estado = Maquina.Perseguir;
                }
                else
                {
                    EnemigoAtaqueVariable -= Time.deltaTime;    
                }
                break;

        }
    }
}
