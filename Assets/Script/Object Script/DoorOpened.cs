using UnityEngine;

public class DoorOpened : MonoBehaviour
{
    public string DoorOpened_Parameter = "Open";
    public AudioSource Open;
    private int Counter = 0;
    Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Warrior" || other.gameObject.tag == "Archer" || other.gameObject.tag == "Magician")
        {
            if(Counter == 0){
                Open.Play();
                Counter = 1;
            }
            animator.SetBool(DoorOpened_Parameter, true);
        }
    }
}