using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using User = Player;

public class ZombieController : MonoBehaviour
{
    public bool ForceAttack = false;
    public bool ForceWalk = false;
    public float MoveSpeed = 0.5f;
    public GameObject Player;

    private bool move = true;
    private bool dead = false;
    private Vector3 direction = Vector3.zero;
    private Animator animations;

    void Start()
    {
        animations = GetComponent<Animator>();
        GetComponent<Rigidbody>().freezeRotation = true;
        MoveSpeed += Random.Range(0.00f, 0.25f);
    }

    void Update()
    {
        if (dead)
            return;

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

        var body = GetComponent<Rigidbody>();
        var distance = Vector3.Distance(body.position, Player.transform.position);

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
