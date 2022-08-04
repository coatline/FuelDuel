using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public TMP_Text timerText;
    public GameObject spill;
    Animator an;
    public TMP_Text healthText;
    public TMP_Text scoreText;
    Rigidbody2D rb;
    public float speed = 20;
    public int health = 25;
    public bool ouch;
    bool canBeHurt = true;
    float timer;
    float timeThatCannotBeHurt = 2;
    bool canControl = true;
    float timertwo;
    float timeSliding = .8f;
    int laps;
    Quaternion q;
    DeathController dc;
    float gameTimer = 60;
    bool starting = true;
    float startTimer = 3.5f;
    public TMP_Text startingText;
    public GameObject[] triggers;
    int touchedInOrder;
    bool canScore = false;
    public GameObject arrows;
    AudioSource a;
    public AudioClip click;
    public AudioClip slip;
    public AudioClip bang;
    public AudioClip engine;
    bool done;
    bool doneo;
    bool donet;
    bool doneth;
    public Button nextLevelButton;

    void Start()
    {
        a = GetComponent<AudioSource>();
        dc = FindObjectOfType<DeathController>();
        an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (starting)
        {
            startTimer -= Time.deltaTime;

            if (startTimer <= 3.5f && startTimer > 2.5f)
            {
                startingText.text = "3";
                if (!done)
                {
                    a.loop = false;
                    a.PlayOneShot(click);
                    done = true;
                }
            }
            else if (startTimer <= 2.5f && startTimer > 1.5f)
            {
                if (!doneo)
                {
                    a.loop = false;
                    a.PlayOneShot(click);
                    doneo = true;
                }
                startingText.text = "2";
            }
            else if (startTimer <= 1.5f && startTimer > .5f)
            {
                if (!donet)
                {
                    a.loop = false;
                    a.PlayOneShot(click);
                    donet = true;
                }
                startingText.text = "1";
            }
            else if (startTimer <= .5f && startTimer > 0)
            {
                if (!doneth)
                {
                    a.loop = false;
                    a.PlayOneShot(click);
                    doneth = true;
                }
                a.loop = true;
                startingText.text = "GO!";
            }
            else
            {
                starting = false;
                startingText.gameObject.SetActive(false);
                arrows.gameObject.SetActive(false);
            }
            return;
        }

        if (health <= 0 || gameTimer <= 0)
        {
            a.Stop();
            //SceneManager.LoadScene(0);
            canControl = false;
            var scene =SceneManager.GetActiveScene();
            if (scene.buildIndex != 5)
            {
                nextLevelButton.gameObject.SetActive(true);
            }
            scoreText.gameObject.SetActive(false);
            dc.SetStats(laps, gameTimer, health);
            transform.position = new Vector3(100, 0, 0);
        }

        if (health >= 5 && gameTimer > 0)
        {
            gameTimer -= Time.deltaTime;
            timerText.text = "Time left: " + gameTimer.ToString("F2");
        }

        if (!canBeHurt && timer <= 2 && health >= 5)
        {
            timer += Time.deltaTime;
        }
        else if (timer > timeThatCannotBeHurt && !canBeHurt)
        {
            timer = 0;
            canBeHurt = true;
            an.Play("Drive1");
        }

        if (!canControl && timertwo <= timeSliding && health >= 5)
        {
            timertwo += Time.deltaTime;
        }
        else if (timertwo > timeSliding && !canControl)
        {
            transform.rotation = q;
            canControl = true;
            timertwo = 0;
            rb.drag = 1.5f;
            an.applyRootMotion = true;
            an.Play("Drive1");
        }


        if (ouch)
        {
            Hurt();
        }

        if (canControl == false) { return; }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rb.angularVelocity = 0;
            rb.velocity = transform.up * Time.deltaTime * speed;
            if (!a.isPlaying)
            {
                a.PlayOneShot(engine);
            }
        }
        else
        {
            a.Stop();
            a.loop = false;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -3 - (speed / 30));
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, 3 + (speed / 30));
        }
    }
    private void LateUpdate()
    {
        Camera.main.transform.position = transform.position - new Vector3(0, 0, 10);
    }



    void Hurt()
    {
        health -= 5;
        speed += 17f;
        ouch = false;
        canBeHurt = false;
        healthText.text = "Fuel: " + health;
        an.Play("Hurt");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (canBeHurt && collision.gameObject.CompareTag("Wall"))
        {
            a.PlayOneShot(bang);
            Hurt();
            var lostFuel = Instantiate(spill);
            lostFuel.transform.position = transform.position - transform.up / 4;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trigger"))
        {

            if (touchedInOrder == 0 && collision.gameObject == triggers[0])
            {
                touchedInOrder = 1;
            }
            else if (touchedInOrder == 1 && collision.gameObject == triggers[0])
            {
                canScore = false;
                touchedInOrder = 0;
                print("trigger 1 danved");
            }
            if (touchedInOrder == 1 && collision.gameObject == triggers[1])
            {
                touchedInOrder = 2;
            }
            else if (touchedInOrder == 2 && collision.gameObject == triggers[1])
            {
                canScore = false;
                touchedInOrder = 0;
            }
            if (touchedInOrder == 2 && collision.gameObject == triggers[2])
            {
                touchedInOrder = 3;
            }
            else if (touchedInOrder == 3 && collision.gameObject == triggers[2])
            {
                canScore = false;
                touchedInOrder = 0;
            }
            if (touchedInOrder == 3 && collision.gameObject == triggers[3])
            {
                touchedInOrder = 4;
                canScore = true;
            }
            else if (touchedInOrder == 4 && collision.gameObject == triggers[3])
            {
                touchedInOrder = 0;
                canScore = false;
            }

        }

        if (collision.gameObject.CompareTag("Spill"))
        {
            q = transform.rotation;
            rb.drag = 0;
            canControl = false;
            an.applyRootMotion = false;
            an.Play("Slide");
            a.loop = false;
            a.PlayOneShot(slip);
            a.loop = true;
        }
        else if (collision.gameObject.CompareTag("Finish") && canScore)
        {
            canScore = false;
            touchedInOrder = 0;
            laps++;
            scoreText.text = "Score: " + laps;
            a.loop = false;
            a.PlayOneShot(click);
            a.loop = true;
        }
    }
}
