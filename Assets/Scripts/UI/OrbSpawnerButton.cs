using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrbSpawnerButton : MonoBehaviour
{
    [SerializeField] GameObject orb;
    public int orbAmout;

    public void SpawnOrb()
    {
        if (orbAmout > 0)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(orb, mousePosition, Quaternion.identity);
            --orbAmout;
        }
    }
}
