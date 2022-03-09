using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        RestartLevel();
    }
    public void RestartLevel()
    {
        if (Input.GetKeyDown(KeyCode.Escape)|| Hero.Instance.GetLive() < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
        }
            
    }
}
