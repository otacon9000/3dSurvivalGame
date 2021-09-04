using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField]
    private int _damageAmount = 1;
    
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Fire();
    }
    private void Fire()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 center = new Vector3(0.5f, 0.5f, 0);
            Ray rayOrigin = mainCamera.ViewportPointToRay(center);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {
                Debug.Log(hitInfo.collider.name);
                IDamageable hit = hitInfo.collider.GetComponent<IDamageable>();

                if (hit != null)
                {
                    hit.Damage(_damageAmount);
                }


            }
        }
    }
}
