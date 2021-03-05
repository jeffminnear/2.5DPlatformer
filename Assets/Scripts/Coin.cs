using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;

    private UIManager ui;

    void Start()
    {
        ui = GameObject.Find("UI").GetComponent<UIManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ui.UpdateCoins(value);
            Destroy(gameObject);
        }
    }
}
