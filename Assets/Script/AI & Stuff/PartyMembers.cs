using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMembers : MonoBehaviour
{
    public GameObject target;
    public List<Vector3> positions;
    public int distance_permitted;
    public float speed;
    private Vector3 lastLeaderPosition;
    private Vector2 movement;
    public Animator animator;
    
    private void Start()
    {
        positions.Add(target.transform.position);
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (lastLeaderPosition != positions[positions.Count - 1])
        {
            positions.Add(target.transform.position);
        }        
    
        if (positions.Count >= distance_permitted)
        {
            if (gameObject.transform.position != positions[0])
            {
                transform.position = Vector3.MoveTowards(transform.position, positions[0], Time.deltaTime * speed);
        
                // Calculate the movement direction
                movement = transform.position;

                // Update animator parameters
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.magnitude);
            }
            else
            {
                positions.Remove(positions[0]);
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                
                // Reset animator parameters
                animator.SetFloat("Horizontal", 0f);
                animator.SetFloat("Vertical", 0f);
                animator.SetFloat("Speed", 0f);
            }
        }
        lastLeaderPosition = target.transform.position;      
    }
}