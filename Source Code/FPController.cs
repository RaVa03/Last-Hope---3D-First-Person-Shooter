using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.SceneManagement;

public class FPController : MonoBehaviour
{
    //public static FPController instance;
    public GameObject cam;
    public GameObject playerPrefab;
    public GameObject bloodPrefab;
    //public GameObject pauseMenuRef;
    public GameObject eve;
    public Animator anim;
    public Slider healthbar;
    public TextMeshProUGUI bulletReserves;
    public TextMeshProUGUI bulletInClip;
    public AudioSource[] footsteps;
    public AudioSource jump;
    public AudioSource land;
    public AudioSource ammoPickup;
    public AudioSource faPickup;
    public AudioSource emptyGun;
    public AudioSource reloadAudio;
    public Transform direction;
    Rigidbody rb;
    CapsuleCollider capsule;
    Quaternion cameraRot;
    Quaternion characterRot;
    float speed = 0.1f;
    float xSensitivity = 2;//for cam rotation up down
    float ySensitivity = 2;//for character rot left right
    float minimumX = -90; //down
    float maximumX = 90; //up
    float x, z;
    //Inventory
    public int ammo = 10;
    int maxAmmo = 100;
    public int health = 100;
    int maxHealth = 100;
    public int clip = 10;
    int maxClip = 10;
    bool walkingSounds = false;
    bool prevGrounded = true;
    float xRot;
    float yRot;


    void Start()
    {

        yRot = Input.GetAxis("Mouse X") * ySensitivity;
        //rot around x axis-> mouse up-down
        xRot = Input.GetAxis("Mouse Y") * xSensitivity;
        rb = this.GetComponent<Rigidbody>();
        capsule = this.GetComponent<CapsuleCollider>();
        cameraRot = cam.transform.localRotation;
        characterRot = this.transform.localRotation;

        health = maxHealth;

        healthbar.value = health;
        bulletReserves.text = ammo + "";
        bulletInClip.text = clip + "";
        GameController.instance.menuFunctions.GetComponent<MenuFunctions>().SaveState();
    }

    void RemoveZombieFromList(GameObject zombi)
    {
        if (GameController.instance.enemies.Contains(zombi.gameObject))
        {
            GameController.instance.enemies.Remove(zombi.gameObject);
        }
    }

    void DestroyZombie(GameObject hitZombie, GameObject rdPrefab)
    {
        RemoveZombieFromList(hitZombie);
        GameObject newRagdoll = Instantiate(rdPrefab,
            hitZombie.transform.position, hitZombie.transform.rotation);
        newRagdoll.transform.Find("Hips").GetComponent<Rigidbody>().AddForce(direction.forward * 1000);
        Destroy(hitZombie);
    }
    void CreateBloodEffect(RaycastHit hitInfo)
    {
        GameObject bloodEffect = Instantiate(bloodPrefab, hitInfo.point, Quaternion.identity);
        bloodEffect.transform.LookAt(this.transform.position);
        Destroy(bloodEffect, 0.5f);
    }
    void ZombieIsHit()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(direction.position, direction.forward, out hitInfo, 200))
        {

            GameObject hitZombie = hitInfo.collider.gameObject;


            if (hitInfo.collider.name == "Head")
            {
                CreateBloodEffect(hitInfo);
                Collider[] colliderList =
                    hitInfo.collider.transform.GetComponentsInParent<Collider>();
                foreach (Collider hitCollider in colliderList)
                {
                    GameObject hitZombieHead = hitCollider.gameObject;
                    if (hitZombieHead.name == "Ch10Prefab"
                        || hitZombieHead.name == "Ch10Prefab(Clone)"
                        || hitZombieHead.name == "WarzombiePrefab"
                        || hitZombieHead.name == "WarzombiePrefab(Clone)")
                    {
                        GameObject rdPrefab =
                            hitZombieHead.GetComponent<ZombieController>().ragdoll;
                        hitZombieHead.GetComponent<ZombieController>().health = 0;
                        DestroyZombie(hitZombieHead, rdPrefab);
                        break;
                    }
                    if (hitZombieHead.name == "BossZombiePrefab"
                        || hitZombieHead.name == "BossZombiePrefab(Clone)")
                    {
                        CreateBloodEffect(hitInfo);
                        hitZombieHead.GetComponent<BossController>().health =
                            Mathf.Clamp(hitZombieHead.GetComponent<BossController>().health - 10,
                            0, hitZombieHead.GetComponent<BossController>().maxHealth);
                        if (hitZombieHead.GetComponent<BossController>().health <= 0)
                        {
                            GameObject rdPrefab =
                                hitZombieHead.GetComponent<BossController>().ragdoll;
                            DestroyZombie(hitZombieHead, rdPrefab);
                        }
                        break;
                    }
                }
            }
            else if (hitZombie.tag == "ZombieTag")
            {
                CreateBloodEffect(hitInfo);
                hitZombie.GetComponent<ZombieController>().health =
                    Mathf.Clamp(hitZombie.GetComponent<ZombieController>().health - 10,
                    0, hitZombie.GetComponent<ZombieController>().maxHealth);

                if (hitZombie.GetComponent<ZombieController>().health <= 0)
                {

                    GameObject rdPrefab = hitZombie.GetComponent<ZombieController>().ragdoll;
                    DestroyZombie(hitZombie, rdPrefab);

                }
            }
        }
    }

    public void TakeHit(float amount)
    {
        health = (int)Mathf.Clamp(health - amount, 0, maxHealth);
        healthbar.value = health;
        if (health <= 0)
        {
            foreach (GameObject zombieToDestroy in GameController.instance.enemies)
               Destroy(zombieToDestroy);
            GameController.instance.enemies.Clear();
            Vector3 position = new Vector3(this.transform.position.x, Terrain.activeTerrain.SampleHeight(this.transform.position), this.transform.position.z);
            if (eve == null)
                eve = Instantiate(playerPrefab, position, this.transform.rotation);
            eve.GetComponent<Animator>().SetTrigger("death");
            GameController.instance.gameOver = true;
            GameController.instance.deathMenu.SetActive(true);
            GameController.instance.player.SetActive(false);
            GameController.instance.ui.SetActive(false);
            GameController.instance.menuFunctions.SetActive(false);
            GameController.instance.volCanvas.SetActive(false);

        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Destination" && GameController.instance.enemies.Count == 0)
        {

            SceneManager.LoadScene("BossLevel");
            GameController.instance.sceneLoad = "BossLevel";
            GameController.instance.menuFunctions.GetComponent<MenuFunctions>().SaveState();

        }

    }

    void Update()
    {
        bool grounded = IsGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(0, 300, 0);
            jump.Play();
            if (anim.GetBool("walking"))
            {
                CancelInvoke("PlayFootstepAudio");
                walkingSounds = false;
            }
        }
        else if (!prevGrounded && grounded)
            land.Play();
        prevGrounded = grounded;


        if (Input.GetKeyDown(KeyCode.F))
            anim.SetBool("arm", !anim.GetBool("arm"));
        if (Input.GetMouseButtonDown(0) && !anim.GetBool("fire") && anim.GetBool("arm") && GameController.instance.canShoot && GameController.instance.menuFunctions.GetComponent<MenuFunctions>().GameOnPause == false)//PauseMenu.GameOnPause==false)
        {
            if (clip > 0)
            {
                anim.SetTrigger("fire");
                clip = Mathf.Clamp(clip - 1, 0, maxClip);
                bulletInClip.text = clip + "";
                ZombieIsHit();
                GameController.instance.canShoot = false;
            }
            else
                emptyGun.Play();
        }

        if (Input.GetKeyDown(KeyCode.R) && anim.GetBool("arm"))

        {

            anim.SetTrigger("reload");
            reloadAudio.Play();
            int neededAmount = maxClip - clip;
            int availableAmount;

            if (neededAmount < ammo)
                availableAmount = neededAmount;
            else
                availableAmount = ammo;
            ammo -= availableAmount;
            clip += availableAmount;
            bulletReserves.text = ammo + "";
            bulletInClip.text = clip + "";
        }

        if (Mathf.Abs(x) > 0 || Mathf.Abs(z) > 0)
        {
            if (!anim.GetBool("walking"))
            {
                anim.SetBool("walking", true);
                InvokeRepeating("PlayFootstepAudio", 0, 0.4f);
            }
        }
        else if (anim.GetBool("walking"))
        {
            anim.SetBool("walking", false);
            CancelInvoke("PlayFootstepAudio");
            walkingSounds = false;
        }

    }

    void FixedUpdate()
    {
        CalculateMovement();

    }
    void CalculateMovement()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        //rotation around y axis-> mouse left-right
        yRot = yRot + (mouseX * ySensitivity);
        //rot around x axis-> mouse up-down
        xRot = xRot + (mouseY * xSensitivity);

        //clamp the angle
        xRot = Mathf.Clamp(xRot, minimumX, maximumX);

        //calculate the rotation
        cameraRot = Quaternion.Euler(-xRot, 0, 0);
        characterRot = Quaternion.Euler(0, yRot, 0);

        //assign the new rotations to the objects
        cam.transform.localRotation = cameraRot;
        this.transform.localRotation = characterRot;

        x = Input.GetAxis("Horizontal") * speed;
        z = Input.GetAxis("Vertical") * speed;
        transform.position += cam.transform.forward * z + cam.transform.right * x;

    }
    void PlayFootstepAudio()
    {
        AudioSource audioSource = new AudioSource();
        int n = Random.Range(1, footsteps.Length);
        audioSource = footsteps[n];
        footsteps[n] = footsteps[0];
        if(GameController.instance.gameOver==false)
            audioSource.Play();
        footsteps[0] = audioSource;
        walkingSounds = true;

    }
    bool IsGrounded()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, capsule.radius, Vector3.down, out hitInfo,
            (capsule.height / 2f) - capsule.radius + 0.1f))
        {
            return true;
        }
        else
            return false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "AmmoTag" && ammo < maxAmmo)
        {
            ammo = Mathf.Clamp(ammo + 10, 0, maxAmmo);
            bulletReserves.text = ammo + "";
            ammoPickup.Play();
            Destroy(col.gameObject);

        }
        if (col.gameObject.tag == "FATag" && health < maxHealth)
        {
            health = Mathf.Clamp(health + 10, 0, maxHealth);
            healthbar.value = health;
            faPickup.Play();
            Destroy(col.gameObject);

        }
        if (IsGrounded())
        {
            if (anim.GetBool("walking") && !walkingSounds)
                InvokeRepeating("PlayFootstepAudio", 0, 0.4f);
        }
    }
}
