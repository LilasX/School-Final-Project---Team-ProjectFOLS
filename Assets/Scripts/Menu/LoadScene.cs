using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private AsyncOperation async; //Contenu du chargement.

    public void BtnLoadScene(int i) //pas de parametres = charge la scene suivante.
    {
        if (async != null) return; //Arrete l'execution si async n'est pas nul

        StartCoroutine(IELoadSceneInt(i));
    }

    public void BtnLoadScene(string s) // s = nom de la scene
    {
        if (async != null) return;

        StartCoroutine(IELoadSceneString(s));
    }

    IEnumerator IELoadSceneInt(int i)
    {
        yield return new WaitForSeconds(0.25f);
        Debug.Log("Play");
        async = SceneManager.LoadSceneAsync(i);
    }

    IEnumerator IELoadSceneString(string s)
    {
        yield return new WaitForSeconds(0.25f);
        Debug.Log("Play");
        async = SceneManager.LoadSceneAsync(s);
    }

}


