using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using System.Collections.Generic;
public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject logo;
    [SerializeField] private AnimationCurve scaleCurve;
    private float time;
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    //doesnt work or something
    //private void Update()
    //{
    //    time += Time.deltaTime;
    //    if (time >= 0.4285f)
    //    {
    //        StartCoroutine(nameof(AnimateLogo));
    //        time = 0;
    //    }
    //}
    //
    //private IEnumerator AnimateLogo()
    //{
    //    float time = 0;
    //    while (time < 1f)
    //    {
    //        logo.transform.localScale = scaleCurve.Evaluate(time * 0.4285f) * Vector3.one;
    //        time += Time.deltaTime;
    //        yield return new WaitForEndOfFrame();
    //    }
    //}
}
