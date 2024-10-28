using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorScript : MonoBehaviour
{
    private Camera camer;
    public Vector3 spawnpoint;
    private bool hasmoved = false;
    public string nextlevel;
    public bool isLastScene;
    public bool didscare;
    public GameObject scareobj;
    public AudioSource scaresound;
    void Start()
    {
        camer = Camera.main;
        hasmoved = false;
        didscare = false;
        if (isLastScene) { 
            scareobj.SetActive(false);
        }
    }

    void Update()
    {

        Vector3 p = camer.ScreenToWorldPoint(Input.mousePosition);
        p.z = 0;
        if (hasmoved)
        {

            transform.position = p;
        }
        else {
            if (Vector3.Distance(spawnpoint, p) < 0.8f) { 
                hasmoved = true;
            }
        }
    }

    IEnumerator Do() {
        scaresound.Play();
        scareobj.SetActive(true);
        yield return new WaitForSeconds(3);
        scareobj.SetActive(false);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Bad"))
        {
            if (isLastScene && !didscare) {
                didscare=true;
                StartCoroutine(Do());
            }
            hasmoved = false;
            transform.position = spawnpoint;
        }
        if (col.gameObject.CompareTag("Good"))
        {
            SceneManager.LoadScene(nextlevel);
        }
    }
}
