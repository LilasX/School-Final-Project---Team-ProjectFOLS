using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public Animator animator;

    private AsyncOperation async;

    private GameManager gameManager;

    public void BtnLoadScene(int i) //pas de parametres = charge la scene suivante.
    {
        if (async != null) return;
        animator.SetTrigger("FadeOut");
        StartCoroutine(IELoadSceneInt(i));
    }

    public void BtnLoadScene(string s) // s = nom de la scene
    {
        if (async != null) return;
        animator.SetTrigger("FadeOut");
        StartCoroutine(IELoadSceneString(s));
    }

    IEnumerator IELoadSceneInt(int i)
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("Play");
        async = SceneManager.LoadSceneAsync(i);
    }

    IEnumerator IELoadSceneString(string s)
    {
        yield return new WaitForSeconds(1f);
        
        async = SceneManager.LoadSceneAsync(s);
    }

    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == gameManager.player)
        {
            BtnLoadScene("ProjectFOLS");
        }
    }

    //public void OnFadeCompleteString(string s)
    //{
    //    Debug.Log(sceneToLoad_string);
    //    // async = 
    //    SceneManager.LoadScene(s);
    //}

    //public void OnFadeCompleteInt()
    //{
    //    // async = 
    //    Debug.Log(sceneToLoad_int);
    //    SceneManager.LoadScene(sceneToLoad_int);
    //}
}


