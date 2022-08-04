using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public AudioClip Click;
    AudioSource a;

    private void Start()
    {
        a = GetComponent<AudioSource>();
    }

    public void ChangeScene(int index)
    {
        a.PlayOneShot(Click);
        SceneManager.LoadScene(index);
    }

}
