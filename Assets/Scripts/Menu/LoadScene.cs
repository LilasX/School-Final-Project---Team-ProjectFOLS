using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{

    public Animator animator;

    private AsyncOperation async;

    private GameManager gameManager;

    public string  sceneName;

    [SerializeField] private GameObject loaderCanvas;
    [SerializeField] private Image progressBar;
    private float target;

    private bool allowScene;

    public void BtnLoadScene(int i) //pas de parametres = charge la scene suivante.
    {
        if (async != null) return;
        animator.SetTrigger("FadeOut");
        StartCoroutine(IELoadSceneInt(i));
    }

    public async void BtnLoadScene(string s) // s = nom de la scene
    {
        progressBar.fillAmount = 0;

        if (async != null) return;
        animator.SetTrigger("FadeOut");

        var scene = SceneManager.LoadSceneAsync(s);
        scene.allowSceneActivation = false;

        loaderCanvas.SetActive(true);

        do
        {
            Time.timeScale = 1; //Make sure it doesn't mess with the pause in other scenes
            await Task.Delay(100);
            progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, 1f, 6 * Time.deltaTime);
        } while (scene.progress < 0.9f);

        await Task.Delay(1000);

        scene.allowSceneActivation = true;
        loaderCanvas.SetActive(false);
        //StartCoroutine(IELoadSceneString(s));
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
        async.allowSceneActivation = false;
        loaderCanvas.SetActive(true);

        do
        {
            
        } while (async.progress < 0.9f);
    }

    IEnumerator IELoadSceneStringT(string s)
    {
        yield return new WaitForSeconds(1f);

        
    }

    private void Start()
    {
        gameManager = GameManager.instance;
        //Time.timeScale = 1;
    }

    void Update()
    {
       //progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, 3 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject == gameManager.player)
        {
            BtnLoadScene(sceneName);
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


