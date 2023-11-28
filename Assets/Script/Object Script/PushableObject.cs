using UnityEngine;

public class PushableObject : MonoBehaviour
{
    public GameObject hint1;
    public GameObject hint2;
    private bool canBePushed = false;
    private bool WrongPerson = false;
    private Rigidbody2D rb;
    private RigidbodyType2D originalRigidbodyType;
    public Vector2 pushForce;
    
    private float timer;

    void Start() {
        timer = 5f;
    }

    void Update() {
        if (timer <= 0f) {
            hint2.SetActive(true);
        }

        if(WrongPerson == true){
            timer -= Time.deltaTime;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalRigidbodyType = rb.bodyType; // Simpan tipe rigidbody asli saat inisialisasi
    }

    private void FixedUpdate()
    {
        if (canBePushed)
        {
            Vector2 pushDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            rb.AddForce(pushDirection.normalized * pushForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Warrior") || collision.gameObject.CompareTag("box")) 
        {
            canBePushed = true;
            if (rb.bodyType == RigidbodyType2D.Static)
            {
                rb.bodyType = originalRigidbodyType; // Ubah tipe rigidbody menjadi tipe aslinya (dynamic)
            }
        }else if (collision.gameObject.CompareTag("Archer") || collision.gameObject.CompareTag("Magician")){
            rb.bodyType = RigidbodyType2D.Static;
            WrongPerson = true;
            hint1.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Warrior") || collision.gameObject.CompareTag("box"))
        {
            canBePushed = false;
            if (rb.bodyType != RigidbodyType2D.Static)
            {
                rb.bodyType = RigidbodyType2D.Static; // Ubah tipe rigidbody menjadi static
            }
        }
    }
}
