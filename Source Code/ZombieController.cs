using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public int health = 20;
    public int maxHealth = 20;
    Animator anim;
    GameObject player;
    public GameObject ragdoll;
    public AudioSource[] kickSounds;
    public float walkingSpeed;
    public float runningSpeed;
    public float damageAmount = 5;
    public float damageAmount2 = 15;

    NavMeshAgent agent;
    enum State { idle, wander, attack, chase };
    State state = State.idle;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        if (GameController.instance != null)
            player = GameController.instance.player;

    }

    void TurnOffTriggers()
    {
        anim.SetBool("walking", false);
        anim.SetBool("attacking", false);
        anim.SetBool("running", false);
    }

    float DistanceToPlayer()
    {
        if (GameController.instance.gameOver)
            return Mathf.Infinity;
        return Vector3.Distance(player.transform.position, this.transform.position);
    }

    void PlayKicksAudio()
    {
        AudioSource audioSource = new AudioSource();
        int index = Random.Range(1, kickSounds.Length);
        audioSource = kickSounds[index];
        kickSounds[index] = kickSounds[0];
        if(GameController.instance.gameOver==false)
            audioSource.Play();
        kickSounds[0] = audioSource;
    }
    public void DamagePlayerSmallZombie()
    {
        if (player != null)
        {
            player.GetComponent<FPController>().TakeHit(damageAmount);
            PlayKicksAudio();
        }
    }
    public void DamagePlayerMiddleZombie()
    {
        if (player != null)
        {
            player.GetComponent<FPController>().TakeHit(damageAmount2);
            PlayKicksAudio();
        }
    }
    void Update()
    {
        if (player == null && GameController.instance.gameOver == false)
        {
            player = GameObject.FindWithTag("Player");
            return;
        }
        switch (state)
        {
            case State.idle:
                if (DistanceToPlayer() < 20)
                    state = State.chase;
                else if (Random.Range(0, 3) < 2)
                    state = State.wander;
                break;
            case State.wander:
                if (!agent.hasPath) 
                {
                    float nextX = this.transform.position.x + Random.Range(-7, 7) + 1;
                    float nextZ = this.transform.position.z + Random.Range(-7, 7) + 1;
                    float nextY = Terrain.activeTerrain.SampleHeight(new Vector3(nextX, 0, nextZ));
                    Vector3 nextDestination = new Vector3(nextX, nextY, nextZ);
                    agent.SetDestination(nextDestination);
                    agent.speed = walkingSpeed;
                    agent.stoppingDistance = 0;
                    TurnOffTriggers();
                    anim.SetBool("walking", true);
                }
                if (DistanceToPlayer() < 20)
                    state = State.chase;
                break;
            case State.chase:
                if (GameController.instance.gameOver)
                {
                    TurnOffTriggers();
                    state = State.wander;
                    return;
                }
                agent.SetDestination(player.transform.position);
                agent.stoppingDistance = 2;
                TurnOffTriggers();
                agent.speed = runningSpeed;
                anim.SetBool("running", true);
                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {
                    state = State.attack;
                }
                if (DistanceToPlayer() > 25)
                {
                    state = State.wander;
                    agent.ResetPath();
                }
                break;
            case State.attack:
                if (GameController.instance.gameOver)
                {
                    TurnOffTriggers();
                    state = State.wander;
                    return;
                }
                TurnOffTriggers();
                anim.SetBool("attacking", true);
                this.transform.LookAt(player.transform.position);
                if (DistanceToPlayer() > agent.stoppingDistance + 2)
                    state = State.chase;
                break;

        }
    }

}
