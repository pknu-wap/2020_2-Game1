using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llama_Start : MonoBehaviour
{
    public GameObject llama;
    private bool moved = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Llama") && !moved)
        {
            moved = true;
            StartCoroutine(llamaMove());
        }
    }

    IEnumerator llamaMove()
    {
        float moveGap = 0.055f;
        while (true)
        {
            yield return new WaitForSecondsRealtime(0.2f);

            for (int i = 0; i < 80; i++)
            {
                yield return new WaitForSecondsRealtime(0.05f);
                llama.transform.position = new Vector2(llama.transform.position.x, llama.transform.position.y - moveGap);
                if (i == 79)
                {
                    llama.GetComponent<Animator>().SetBool("isEnd", true);
                }
            }

            yield return new WaitForSecondsRealtime(0.2f);

            for (int i = 0; i < 80; i++)
            {
                yield return new WaitForSecondsRealtime(0.05f);
                llama.transform.position = new Vector2(llama.transform.position.x, llama.transform.position.y + moveGap);
                if (i == 79)
                {
                    llama.GetComponent<Animator>().SetBool("isEnd", false);
                }
            }
        }
    }
}
