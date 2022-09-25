using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fromstarttopreloads : MonoBehaviour
{
   public void LoadPreloads()
    {
        SceneManager.LoadScene("Preloads");
    }
}
