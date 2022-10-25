using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public Transform player;
    public string objName;
    public string scene;

    // Update is called once per frame

    void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        Debug.Log(dist);

        if (dist < 1) {
            SceneManager.LoadScene(scene);
        }
    }

    /*
    void OnTriggerEnter(Collider c)
    {
        Debug.Log("Hello");
        Debug.Log(c.gameObject.name);
        if (c.gameObject.name == objName)
        {
            SceneManager.LoadScene(scene);
        }
    }
    */
}
