﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplat : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 1.5f);
    }
}
