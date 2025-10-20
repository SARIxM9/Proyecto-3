using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] Check;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.CambiarPosicion(transform.position);
            CheckActual();
        }
    }
    public void CheckActual()
    {
        CheckPoint[] checks = FindObjectsOfType<CheckPoint>();
        for (int i = 0; i < checks.Length; i++)
        {
            checks[i].Check[0].SetActive(false);
            checks[i].Check[1].SetActive(true);
        }

        Check[0].SetActive(true);
        Check[1].SetActive(false);
    }
}
