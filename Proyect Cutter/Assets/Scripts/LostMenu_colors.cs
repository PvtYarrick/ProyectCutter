﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostMenu_colors : MonoBehaviour {

    private float floor = 0.3f;
    private float ceiling = 5.0f;

    // Use this for initialization
    void Start () {
        float emission = floor + Mathf.PingPong(Time.time, ceiling - floor);
    }
	
	// Update is called once per frame
	void Update () {
        Renderer renderer = GetComponent<Renderer>();
        Material mat = renderer.material;

        float emission = Mathf.PingPong(Time.time, 1.0f);
        Color baseColor = Color.cyan; //Replace this with whatever you want for your base color at emission level '1'

        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

        mat.SetColor("_EmissionColor", finalColor);
    }
}
