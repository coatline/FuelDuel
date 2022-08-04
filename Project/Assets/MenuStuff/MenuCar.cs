using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuCar : MonoBehaviour
{
    public TMP_Text title;
    public Button bt;
    public Button bttwo;
    public TMP_Text FORDUMDUMS;
    public AudioClip engine;
    AudioSource a;

    void Start()
    {
        a = GetComponent<AudioSource>();
        a.PlayOneShot(engine);
        dookie = Random.Range(.3f, .6f);
    }

    float dookie;

    void Update()
    {
        if (transform.position.y <= -35)
        {
            bttwo.gameObject.SetActive(true);
            transform.position = new Vector3(Random.Range(3, -3), 13, 0);
        }
        else if (transform.position.y <= -15)
        {
            title.gameObject.SetActive(true);
        }
        if (transform.position.y <= -25)
        {
            FORDUMDUMS.gameObject.SetActive(true);
        }
        if (transform.position.y <= -30)
        {
            bt.gameObject.SetActive(true);
        }
        transform.Translate(0, dookie, 0);
    }
}
