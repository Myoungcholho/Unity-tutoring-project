using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LiftTileChangeText : MonoBehaviour
{
    public TextMeshPro text;
    private LiftTile lifttile;
    void Start()
    {
        lifttile = GetComponent<LiftTile>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = lifttile.Total.ToString();
    }
}
