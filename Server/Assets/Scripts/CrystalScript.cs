using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScript : MonoBehaviour
{
    [SerializeField] AudioSource GrabCrystal;
    [SerializeField] List<GameObject> ObjectsToActivate = new List<GameObject>();
    bool grabbedCrystal = false;
    void Start()
    {
        foreach (GameObject obj in ObjectsToActivate)
        {
            BoxCollider2D collider = obj.GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }
    private void Update()
    {
        if(!GrabCrystal.isPlaying && grabbedCrystal)
        {
            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            GrabCrystal.Play();
            grabbedCrystal = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            foreach (GameObject obj in ObjectsToActivate)
            {
                Collider2D collider = obj.GetComponent<Collider2D>();
                if (collider != null)
                {
                    collider.enabled = true;
                }
            }
        }
    }
}
