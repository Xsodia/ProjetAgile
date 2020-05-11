using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{

    SpriteRenderer spriteRenderer;

    [SerializeField] Sprite[] bonusSprites;
    int bonusIndex;
    /**
     * 0 = add balle to scene
     * 1 = add life point
     *  
     * */
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bonusIndex = Random.Range(0, bonusSprites.Length);
        Debug.Log(bonusIndex + ", " + (bonusSprites.Length - 1) + " , " + bonusSprites.Length);
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = bonusSprites[bonusIndex];
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Paddle"))
        {
            Destroy(gameObject);
            switch (bonusIndex)
            {
                case (0):
                    GameSession.instance.AddBallToScene();
                    break;
                case 1:
                    GameSession.instance.AddLife();
                    break;
                default:
                    break;
            }
        }
    }

}
