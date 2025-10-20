using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Murcielago : MonoBehaviour
{
    [SerializeField] private float radio;
    [SerializeField] private bool Rayo;
    [SerializeField] private RaycastHit hit;

    void Update()
    {
        Esfera();
    }

    private void OnDrawGizmos()
    {
        
    }

    public void Esfera()
    {
        Rayo = Physics.SphereCast(transform.position, radio, Vector3.zero, out hit);
    }
}
