using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class Jugador_Controlador : MonoBehaviour
{
    [Header("Jugador y otros")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform Robot;
    [SerializeField] private Transform Camara;
    public CinemachineBrain Cine;
    [Header("Fuerzas")]
    [SerializeField] private Vector3 direccion; 
    [SerializeField] private float velocidad; 
    [SerializeField] private float salto;
    [SerializeField] private float rotacion;
    [SerializeField,Range(1,10)] private float GravedadAumentar;
    public float GravedadY;
    [Header("Fuerzas")]
    [SerializeField] private Animator animator;
    [Header("STUN")]
    [SerializeField] private bool Stun;
    [SerializeField] private float TiempoStun;
    public float TiempoStunVariable;
    [SerializeField] private Vector2 FuerzaStun;



    public static Jugador_Controlador Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        Movimiento();
        Animaciones();
    }
    
    public void Movimiento()
    {
        if (!Stun)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            direccion = transform.forward * vertical + transform.right * horizontal;
            direccion.Normalize();


            direccion *= velocidad;

            Gravedad();

            //Por que ahora funciona bien????
            //Vector3 todo = new Vector3(direccion.x * velocidad, GravedadY, direccion.z * velocidad);

            Rotar();


            controller.Move(direccion * Time.deltaTime);
            //controller.Move(todo * Time.deltaTime);
        }
        else
        {
            if (TiempoStunVariable <= 0)
            {
                Stun = false;
            }
            else
            {
                GravedadY += Physics.gravity.y * Time.deltaTime;

                TiempoStunVariable -= Time.deltaTime;

                Vector3 todo = (Robot.transform.forward * FuerzaStun.x + Robot.transform.up * GravedadY);
                controller.Move(todo * Time.deltaTime);
            }
        }
    }


    //Olvide como rotar
    public void Rotar()
    {
        if(direccion.x != 0 || direccion.z != 0)
        {
            transform.rotation = Quaternion.Euler(0,Camara.rotation.eulerAngles.y,0);
            Quaternion mira = Quaternion.LookRotation(new Vector3(direccion.x,0,direccion.z));
            Robot.transform.rotation = Quaternion.Slerp(Robot.transform.rotation,mira , rotacion * Time.deltaTime);
        }
    }

    public void Gravedad()
    {
        GravedadY += Physics.gravity.y * GravedadAumentar * Time.deltaTime;

        if (controller.isGrounded)
        {
            GravedadY = -0.5f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GravedadY = salto;
            }
        }
        direccion.y = GravedadY;
    }

    public void Animaciones()
    {
        animator.SetBool("Piso", controller.isGrounded);

        animator.SetFloat("Run", Mathf.Abs(direccion.x) + Mathf.Abs(direccion.z));
    }

    public void Aturdir()
    {
        AudioManager.Instance.ReproducirSFX(8);
        Stun = true;
        TiempoStunVariable = TiempoStun;
        GravedadY = FuerzaStun.y;
        Vector3 todo = new Vector3 (0f,GravedadY,0f);  
        controller.Move(todo * Time.deltaTime);
    }
}
