using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanisterWeapon : MonoBehaviour
{
    public Transform Sight;

    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        { ps.enableEmission = true; }
        else if (Input.GetMouseButtonUp(0))
        { ps.enableEmission = false; }

        transform.LookAt(Sight);
    }

    void OnParticleCollision(GameObject other)
    {

        if (other.GetComponent<Alien>())
            other.GetComponent<Alien>().HP--;
    }

}
