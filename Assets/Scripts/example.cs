using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class example : MonoBehaviour
{

    // Start is called before the first frame update
    private void Awake()
    {
        print(this.name + "Estoy malo awake");
    }

    void Start()
    {
        print(this.name + "Estoy malo start");
    }

    // Update is called once per frame
    void Update()
    {
        print(this.name + "Estoy malo");
    }

    private void OnDisable()
    {
        print(this.name + "Estoy malo desativado");
    }

    private void OnDestroy()
    {
        print(this.name + "Estoy malo destruido");
    }

    private void OnEnable()
    {
        print(this.name + "Estoy malo ativo");
    }
}
