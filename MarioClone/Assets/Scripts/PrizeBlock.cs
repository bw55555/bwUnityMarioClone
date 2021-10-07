using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizeBlock : MonoBehaviour
{
    public AnimationCurve anim;

    public int coinsInBlock = 5;
    // Start is called before the first frame update
    bool jumpAnimationRunning = false;

    public Collider2D collider2d;

    void Awake()
    {
        collider2d = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.contacts[0].point.y <= collider2d.bounds.min.y)
        {
            if (!jumpAnimationRunning)
            {
                StartCoroutine(RunAnimation());
                if (coinsInBlock > 0)
                {
                    SoundManager.Instance.PlayOneShot(SoundManager.Instance.getCoin);
                    IncreaseTextUIScore(100);
                    coinsInBlock -= 1;
                }
            }
        }
        
    }

    IEnumerator RunAnimation()
    {

        // Get starting position of PrizeBlock
        Vector2 startPos = transform.position;
        jumpAnimationRunning = true;
        // Cycle through all the keys in the animation curve
        for (float x = 0; x < anim.keys[anim.length - 1].time; x += Time.deltaTime)
        {

            // Change the block position to what is defined
            // on the AnimationCurve
            transform.position = new Vector2(startPos.x,
                startPos.y + anim.Evaluate(x));

            // Continue looping at next update
            yield return null;
        }
        jumpAnimationRunning = false;
    }

    void IncreaseTextUIScore(int amt)
    {
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();
        int score = int.Parse(textUIComp.text);
        score += amt;
        textUIComp.text = score.ToString();
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
