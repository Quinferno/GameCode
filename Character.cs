using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Attackers))]
public class Character : MonoBehaviour, ITakeHit, IDie
{
    public static List<Character> All = new List<Character>();

    private Controller controller;
    private Attackers attacker;
    private Animator animator;
    private new Rigidbody rigidbody;
    private int currentHealth;

    [SerializeField] private float moveSpeed = 5f;
    
    [SerializeField] private int damage = 1;

    [SerializeField] private int maxHealth = 10;
        
    public event Action<int, int> OnHealthChanged = delegate { };
    public event Action<IDie> OnDied = delegate { };
    public int Damage { get { return damage; } }

    private void Awake()
    {
        attacker = GetComponent<Attackers>();
        animator = GetComponentInChildren<Animator>();//Different solution needed if character has more than one animator
        rigidbody = GetComponent<Rigidbody>();

    }

    internal void SetController(Controller controller)
    {
        this.controller = controller;
    }

    private void Update()
    {
        Vector3 direction = controller.GetDirection(); 
        if (direction.magnitude > 0.4f)//if combined y and x input is too low, prevents movement (creating a controller stick deadzone)
        {
            var velocity = (direction * moveSpeed).With(y: rigidbody.velocity.y);//replaces y value with rigid body velocity.y value
            rigidbody.velocity = velocity;

            transform.position += direction * Time.deltaTime * moveSpeed;
            transform.forward = direction * 360f;

            animator.SetFloat("Speed", direction.magnitude);//Speed is set to magnitude
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
        if (controller.attackPressed)
        {
            if (attacker.CanAttack)
            {
                animator.SetTrigger("Attack");
            }
        }

    }
      
    
    private void OnEnable()
    {
        currentHealth = maxHealth;

        if (All.Contains(this) == false)
            All.Add(this);
    }

    private void OnDisable()
    {
        if (All.Contains(this))
            All.Remove(this);
    }

    public void TakeHit(IAttack hitBy)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= hitBy.Damage;
        OnHealthChanged(currentHealth, maxHealth);
                
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDied(this);
    }
}
