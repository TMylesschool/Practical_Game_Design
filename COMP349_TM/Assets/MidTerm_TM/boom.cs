using UnityEngine;
using System.Collections;
using TMPro;

public class boom : MonoBehaviour
{
    public TextMeshPro scoreGT;

    void Start()
    {
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreGT = scoreGO.GetComponent<TextMeshPro>();
        scoreGT.text = "0";
    }

    void Update()
    {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject collidedWith = coll.gameObject;

        if (collidedWith.tag == "Apple")
        {
            Apple apple = collidedWith.GetComponent<Apple>(); // Get Apple script (for value)

            if (apple != null && apple.value < 0) // Black apple (explosive or harmful)
            {
                Destroy(this.gameObject); // Destroy Basket
            }
            else
            {
                Destroy(collidedWith); // Destroy apple normally

                int score = int.Parse(scoreGT.text);
                score += (apple != null) ? apple.value * 100 : 100; // Support different apple scores
                scoreGT.text = score.ToString();

                if (score > HighScore.score)
                {
                    HighScore.score = score;
                }
            }
        }
    }
}
