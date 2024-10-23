using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MenuScene : MonoBehaviour
{
    GameObject background;
    GameObject title;
    GameObject press;
    GameObject p1;
    GameObject p2;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        background = GameObject.Find("background");
        title = GameObject.Find("Title");
        press = GameObject.Find("Press");
        p1 = GameObject.Find("P1Controls");
        p2 = GameObject.Find("P2Controls");

        DG.Tweening.Sequence mySequence = DOTween.Sequence();
        mySequence.AppendInterval(1)
            //.Append(background.GetComponent<SpriteRenderer>().DOFade(0, 1))
            .Append(title.GetComponent<Text>().DOText("Dino Duel", 1))
            .Insert(1, press.GetComponent<Text>().DOText("Press Space to Play", 3))
            .Insert(1, p1.GetComponent<Text>().DOText("Player 1 Controls \nw = jump \na = left \ns = shoot \nd = right", 2))
            .Insert(1, p2.GetComponent<Text>().DOText("Player 2 Controls \nUp = jump \nLeft = left \nDown = shoot \nRight = right", 2));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
