using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Ball"))
        {
            GameSession.instance.DestroyBallFromScene(collider.gameObject);

            if(GameSession.instance.lives <= 1 && GameSession.instance.numberOfActiveBalls < 1)
            {
                SceneManager.LoadScene("Game Over");
            }
            if(GameSession.instance.numberOfActiveBalls < 1)
            {
                GameSession.instance.SubLife();
                GameSession.instance.AddBallToScene();
            }
        }

        if (collider.CompareTag("Bonus"))
        {
            Destroy(collider.gameObject);
        }


    }

}
