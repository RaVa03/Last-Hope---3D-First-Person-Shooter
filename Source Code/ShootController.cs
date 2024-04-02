using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public AudioSource gunshot;

    public void Fire()
    {
        gunshot.Play();
    }

    public void CanShoot()
    {
        GameController.instance.canShoot = true;
    }
}
