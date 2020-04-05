using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    private int count;
    private int totalGO;

    public Text countText;
    public Text winText;
    public float mvtSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.enabled = false;
        totalGO = GameObject.FindGameObjectsWithTag("Collectable").Length;
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0.0f, vertical);
        rb.AddForce(direction * mvtSpeed);
        
        if (count >= totalGO)
        {
            winText.enabled = true;
            winText.text = "YOU WIN!";
            
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Collectable"))
        {
            //other.gameObject.SetActive(false);
            Destroy(other.gameObject);
            count++;
            SetCountText();
        }
        
    }
    void SetCountText()
    {
        countText.text = "Count : " + count.ToString();
    }
}
