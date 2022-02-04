using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Text ScoreText;
    [SerializeField] private AudioSource CollectSoundEffect;
    [SerializeField] private AudioSource DieSoundEffect;
    [SerializeField] private AudioSource LevelCompleteEffect;

    private Animator animator;
    private Rigidbody2D rigidbody2D;

    private bool finishLineTouched = false;

    private int Cherries = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cherry")
        {
            CollectSoundEffect.Play();
            Destroy(collision.gameObject);
            Cherries++;
            ScoreText.text = "Cherries : " + Cherries;
        }
        else if (collision.gameObject.tag == "Danger")
        {
            DieSoundEffect.Play();
            Die();
        }
        else if (collision.gameObject.tag =="Finish" && !finishLineTouched)
        {
            LevelCompleteEffect.Play();
            finishLineTouched = true;
            Invoke("NextLevel", 2f);
        }
    }

    void Die()
    {
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Die");
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
