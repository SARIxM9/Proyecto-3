using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject Interfaz;
    [SerializeField] private GameObject MenuPausa;
    [SerializeField] private GameObject Pausa;
    [SerializeField] private GameObject Opciones;
    [SerializeField] private GameObject Menu;
    [SerializeField] private string Panel;
    [Header("Intefaz")]
    [SerializeField] private TextMeshProUGUI textoVida;
    [SerializeField] private TextMeshProUGUI textoMoneda;
    [SerializeField] private Image Vida;
    [SerializeField] private Sprite[] imagenVida;
    [Header("Animaciones")]
    [SerializeField] private Animation Morir;
    [SerializeField] private Animation Transicion;
    [Header("AudioMixer")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider Mu;
    [SerializeField] private Slider SF;
    [Header("LeanTween")]
    [SerializeField] private GameObject Titulo;
    [SerializeField] private GameObject[] BotonesMenu;
    [SerializeField] private LeanTweenType Tipo1;
    [SerializeField] private LeanTweenType Tipo2;
    [SerializeField] private LeanTweenType Tipo3;
    [Header("System")]
    [SerializeField] private GameObject System;



    public static CanvasManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            VidaCanvas();
            MonedaCanvas();
        }
        else
        {
            AnimacionInicio();
        }

        if (PlayerPrefs.HasKey("Musica"))
        {
            Mu.value = PlayerPrefs.GetFloat("Musica");
        }
        if(PlayerPrefs.HasKey("SFX"))
        {
            SF.value = PlayerPrefs.GetFloat("SFX");
        }
    }

    private void Update()
    {
        Pausar();
    }

    public void VidaCanvas()
    {
        switch (Corazon.Instance.vidasVariable)
        {
            case 5:
                textoVida.text = "5";
                Vida.sprite = imagenVida[4];
                Vida.enabled = true;
                break;
            case 4:
                textoVida.text = "4"; 
                Vida.sprite = imagenVida[3];
                break;
            case 3:
                textoVida.text = "3";
                Vida.sprite = imagenVida[2];
                break;
            case 2:
                textoVida.text = "2";
                Vida.sprite = imagenVida[1];
                break;
            case 1:
                textoVida.text = "1";
                Vida.sprite = imagenVida[0];
                break;
            default:
                textoVida.text = "0";
                Vida.enabled = false;
                break;
        }
       
    }

    public void MorirAnimacion()
    {
        Morir.Play();
    }

    public void MonedaCanvas()
    {
        textoMoneda.text = GameManager.Instance.Moneda.ToString();
    }

    //Metodos de Paneles


    //Interfaz
    public void Pausar()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Interfaz.active == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            Interfaz.SetActive(false);
            MenuPausa.SetActive(true);
        }
    }

    //MenuPausa
    public void Reanudar()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        Interfaz.SetActive(true);
        MenuPausa.SetActive(false);
    }

    public void SalirMenu()
    {
        StartCoroutine(SM());
    }

    public void SalirFIN()
    {
        StartCoroutine(SFin());
    }

    IEnumerator SM()
    {
        Transicion.Play();
        Time.timeScale = 1;
        yield return new WaitForSeconds(1);
        MenuPausa.SetActive(false);
        Menu.SetActive(true);
        SceneManager.LoadScene("Menu");
        AnimacionInicio();
    }

    IEnumerator SFin()
    {
        Transicion.Play();
        Interfaz.SetActive(false);
        yield return new WaitForSeconds(1);
        Cursor.lockState = CursorLockMode.None;
        Menu.SetActive(true);
        SceneManager.LoadScene("Menu");
        AnimacionInicio();
    }

    //Abrir Opciones desde Pausa o Menu
    public void AbrirOpciones(GameObject Desactivar)
    {
        StartCoroutine(AnimacionOpciones1(Desactivar));
    }

    //MenuOpciones

    public void SalirOpciones()
    {
        StartCoroutine(AnimacionOpciones2());
    }

    public void SliderMusica(float volumen)
    {
        audioMixer.SetFloat("Musica", Mathf.Log10(volumen) * 20);
        PlayerPrefs.SetFloat("Musica", volumen);
    }
    public void SliderSFX(float volumen)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volumen) * 20);
        PlayerPrefs.SetFloat("SFX", volumen);
    }

    //Niveles Estrella

    public void SiguienteNivel(bool siguiente,string escena)
    {
        StartCoroutine(Nivel(siguiente,escena));
    }

    IEnumerator Nivel(bool siguiente, string escena)
    {
        Interfaz.SetActive(false);
        Transicion.Play();
        yield return new WaitForSeconds(1);
        if (siguiente)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Interfaz.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(escena);
            Interfaz.SetActive(true);
        }
    }

    //Menu

    public void Jugar()
    {
        StartCoroutine(PasarNivel(Menu,Interfaz,false,"Niveles"));
    }

    IEnumerator PasarNivel(GameObject Desactivar, GameObject Activar,bool siguiente,string escena)
    {
        Transicion.Play();
        yield return new WaitForSeconds(1);

        if (siguiente)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(escena);
        }
        Desactivar.SetActive(false);
        Activar.SetActive(true);
    }

    public void Salir()
    {
        Application.Quit();
    }

    //LeanTween

    public void AnimacionInicio()
    {
        StartCoroutine(AnimacionMenu());
        LeanTween.reset();

        Titulo.transform.localScale = Vector3.zero;
        BotonesMenu[0].transform.position = new Vector2(-300,650);
        BotonesMenu[1].transform.position = new Vector2(-300,450);
        BotonesMenu[2].transform.position = new Vector2(-300,250);

        LeanTween.scale(Titulo, Vector3.one * 1.2f, 1).setEase(Tipo1);
        LeanTween.moveX(BotonesMenu[0],500,0.5f).setEase(Tipo2).setOnComplete(A);
    }
    public void A()
    {
        LeanTween.moveX(BotonesMenu[1], 500, 0.5f).setEase(Tipo2).setOnComplete(B);
    }
    public void B()
    {
        LeanTween.moveX(BotonesMenu[2],500, 0.5f).setEase(Tipo2);
    }

    IEnumerator AnimacionMenu()
    {
        System.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        System.SetActive(true);
    }

    public void AnimacionOpcionesAbrir()
    {
        LeanTween.reset();
        Opciones.transform.localScale = Vector3.zero;
        LeanTween.scale(Opciones, Vector3.one, 0.5f).setEase(Tipo1).setIgnoreTimeScale(true);
    }

    public void AnimacionOpcionesCerrar()
    {
        LeanTween.scale(Opciones, Vector3.zero, 0.5f).setEase(Tipo3).setIgnoreTimeScale(true);
    }

    IEnumerator AnimacionOpciones1(GameObject Desactivar)
    {
        System.SetActive(false);
        AnimacionOpcionesAbrir();
        Panel = Desactivar.name;
        Opciones.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        Desactivar.SetActive(false);
        System.SetActive(true);
    }

    IEnumerator AnimacionOpciones2()
    {
        if (Panel == "Menu")
        {
            System.SetActive(false);
            Menu.SetActive(true);
            AnimacionOpcionesCerrar();
            yield return new WaitForSecondsRealtime(0.5f);
            Opciones.SetActive(false);
            System.SetActive(true);
        }
        else if (Panel == "Pausa")
        {
            System.SetActive(false);
            Pausa.SetActive(true);
            AnimacionOpcionesCerrar();
            yield return new WaitForSecondsRealtime(0.5f);
            Opciones.SetActive(false);
            System.SetActive(true);
        }
    }
}
