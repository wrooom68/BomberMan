using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHandler : MonoBehaviour
{

    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("DestroyMe",3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyMe()
    {
        PlayerController.bombAvailable = true;
        Destroy(gameObject);
        FindObjectOfType<PlayerController>().BombBlast(transform.position);
    }
}
