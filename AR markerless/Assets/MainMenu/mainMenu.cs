using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Pastikan untuk mengimpor namespace ini

public class MainMenu : MonoBehaviour
{
    public void TombolKeluar()
    {
        Application.Quit();
        Debug. Log("Game Close");
    }

    public void Mainkan()
    {
        SceneManager. LoadScene("kursi");
    }

    public void Informasi()
    { 
        SceneManager. LoadScene("informasi");   
    }

    public void Credit()
    {
        SceneManager.LoadScene("credit");
    }  
}

