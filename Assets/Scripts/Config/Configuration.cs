using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class Configuration : MonoBehaviour
{
    public TextMeshProUGUI tiempoAyudaPublico;
    public TextMeshProUGUI tiempoPreguntas;

    public TextMeshProUGUI cantidadPreguntas;

    int tAyudaPublico = 30;
    int tPreguntas = 120;

    int cPreguntas = 20;

    void Start()
    {

        PlayerPrefs.SetInt("canridadPreguntas", 20);

        if (PlayerPrefs.HasKey("tiempoAyudaPublico"))
        {
            tAyudaPublico = PlayerPrefs.GetInt("tiempoAyudaPublico");
        }

        if (PlayerPrefs.HasKey("tiempoPreguntas"))
        {
            tPreguntas = PlayerPrefs.GetInt("tiempoPreguntas");
        }

        if (PlayerPrefs.HasKey("canridadPreguntas"))
        {
            cPreguntas = PlayerPrefs.GetInt("canridadPreguntas");
        }

        tiempoAyudaPublico.text = tAyudaPublico.ToString();
        tiempoPreguntas.text = tPreguntas.ToString();
        cantidadPreguntas.text = cPreguntas.ToString();
    }
    
    public void SetConfig()
    {
        PlayerPrefs.SetInt("tiempoAyudaPublico", tAyudaPublico);
        PlayerPrefs.SetInt("tiempoPreguntas", tPreguntas);
        PlayerPrefs.SetInt("canridadPreguntas", cPreguntas);

        SceneManager.LoadScene(1);
    }

    public void ChangeTiempoAyudaPublico(int i)
    {
        tAyudaPublico += i;

        if(tAyudaPublico < 0)
        {
            tAyudaPublico = 0;
        }

        tiempoAyudaPublico.text = tAyudaPublico.ToString();
    }

    public void ChangeTiempoPreguntas(int i)
    {
        tPreguntas += i;

        if(tPreguntas < 0)
        {
            tPreguntas = 0;
        }

        tiempoPreguntas.text = tPreguntas.ToString();
    }

    public void ChangeCantidadPreguntas(int i)
    {
        cPreguntas += i;

        if (cPreguntas < 0)
        {
            cPreguntas = 0;
        }

        if (cPreguntas > 20)
        {
            cPreguntas = 20;
        }

        cantidadPreguntas.text = cPreguntas.ToString();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
