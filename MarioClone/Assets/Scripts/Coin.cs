using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update

    int value = 1;

    void OnCollisionEnter2D(Collision2D col)
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.getCoin);
        IncreaseTextUIScore(100 * value);
        Destroy(gameObject);
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
