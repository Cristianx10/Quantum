﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
public void PlayGame(){
SceneManager.LoadScene("Inslevel1");
}
public void QuitGame(){
    Application.Quit();
}

public void Nextinslevel1(){
SceneManager.LoadScene("Nivel");
}
public void Nextinslevel2(){
SceneManager.LoadScene("Nivel2");
}
}
