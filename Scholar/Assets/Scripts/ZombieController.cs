using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using User = Player;

public class ZombieController : MonoBehaviour
{
    public bool ForceAttack = false;
    public bool ForceWalk = false;
    public float MoveSpeed = 0.5f;
    public Player Player;
    public StateController StateController;

    private bool move = true;
    private bool dead = false;
    private Vector3 direction = Vector3.zero;
    private Animator animations;

    void Start()
    {
        animations = GetComponent<Animator>();
        MoveSpeed += Random.Range(0.00f, 0.25f);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public bool IsDead()
    {
        return dead;
    }

    void Update()
    {
        if (dead)
        { 
            return;
        }

        var body = GetComponent<Rigidbody>();
        var distance = Vector3.Distance(body.position, Player.transform.position);
        
        body.constraints = RigidbodyConstraints.FreezeRotation;

        if (ForceAttack)
        {
            animations.Play("attack");
            return;
        }

        if (ForceWalk)
        {
            animations.Play("walk");
            return;
        }

       

        direction = Player.transform.position - body.position;
        direction.Normalize();

        if (move)
        {
            if (Vector3.Distance(Player.transform.position, body.position) <= 50.0f)
            {
                var targetPosition = Player.transform.position;
                targetPosition.y = transform.position.y;
                transform.LookAt(targetPosition);

                body.position = Vector3.MoveTowards(body.position, Player.transform.position, MoveSpeed * Time.deltaTime);
            }
        }

        if (distance <= 3.0)
        {
            Player.TakeDamage(1);
            animations.Play("attack");
            move = false;

            StartCoroutine(User.Wait(2.0f, () => move = true));
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            animations.Play("fall");
            move = false;
            dead = true;
        }
    }
}
