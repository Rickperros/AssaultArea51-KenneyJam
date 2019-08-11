using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{

    public AlienManager manager;

    public ParticleSystem AbductionParticles;
    public ParticleSystem ExplosionParticles;
    public CarOscillation Car;
    public int HP = 100;
    public float MoveSpeed;
    public float AbducingSpeed;
    public float TimeOnCar;
    public float TimeExploding;

    enum STATES { TOCAR, ONCAR, ABDUCING, LEAVING, EXPLODING, DYING };
    private STATES currentState;

    private float onCarTimer = 0;
    private float onExplodingTimer = 0;
    private Vector3 spawnPosition;
    private Animation animator;

    private void Start()
    {
        animator = GetComponent<Animation>();
        spawnPosition = transform.position;
        ChangeState(STATES.TOCAR);
    }

    private void ChangeState (STATES newState)
    {
        switch (currentState)
        {
            case STATES.TOCAR:
                break;
            case STATES.ONCAR:
                break;
            case STATES.ABDUCING:
                var abustionEmission = AbductionParticles.emission;
                abustionEmission.enabled = false;
                break;
            case STATES.LEAVING:
                break;
            case STATES.EXPLODING:
                break;
            case STATES.DYING:
                break;
        }

        switch (newState)
        {
            case STATES.TOCAR:
                break;
            case STATES.ONCAR:
                Car.Stop();
                onCarTimer = 0;
                break;
            case STATES.LEAVING:
                Car.transform.parent = transform;
                HP = 99999;
                break;
            case STATES.DYING:
                manager.AlienOnScene = false;
                Car.Restart();
                break;
            case STATES.ABDUCING:
                var abustionEmission = AbductionParticles.emission;
                abustionEmission.enabled = true;
                break;
            case STATES.EXPLODING:
                var explosionEmission = ExplosionParticles.emission;
                explosionEmission.enabled = true;
                animator.Play("Alien Dying");
                onExplodingTimer = 0;
                break;
        }

        currentState = newState;
    }

    private void Update()
    {

        ExitConditions();

        switch (currentState)
        {
            case STATES.TOCAR:
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(Car.transform.position.x, Car.transform.position.y + 8, 0), MoveSpeed * Time.deltaTime);
                break;
            case STATES.ONCAR:
                onCarTimer += Time.deltaTime;
                break;
            case STATES.LEAVING:
                transform.position = Vector3.MoveTowards(transform.position, spawnPosition, MoveSpeed * Time.deltaTime);
                break;
            case STATES.DYING:
                transform.position += Vector3.up * 10 * Time.deltaTime;
                break;
            case STATES.ABDUCING:
                Car.transform.position = Vector3.MoveTowards(Car.transform.position, transform.position, AbducingSpeed * Time.deltaTime);
                break;
            case STATES.EXPLODING:
                onExplodingTimer += Time.deltaTime;
                break;
        }
    }

    private void ExitConditions ()
    {
        if (HP < 0 && currentState != STATES.EXPLODING && currentState != STATES.DYING)
            ChangeState(STATES.EXPLODING);

        switch (currentState)
        {
            case STATES.TOCAR:
                if (Mathf.Abs(transform.position.x - Car.transform.position.x) < 1)
                    ChangeState(STATES.ONCAR);
                break;
            case STATES.ONCAR:
                if (onCarTimer > TimeOnCar)
                    ChangeState(STATES.ABDUCING);
                break;
            case STATES.LEAVING:
                if (transform.position == spawnPosition)
                {
                    manager.AlienOnScene = false;
                    gameObject.SetActive(false);
                }
                break;
            case STATES.DYING:
                break;
            case STATES.ABDUCING:
                if (Mathf.Abs(transform.position.y - Car.transform.position.y) < 4)
                    ChangeState(STATES.LEAVING);
                break;
            case STATES.EXPLODING:
                if (onExplodingTimer > TimeExploding)
                    ChangeState(STATES.DYING);
                break;
        }
    }
}
