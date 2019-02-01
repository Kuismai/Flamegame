using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTextShow : MonoBehaviour
{
    public GameObject textToShow;
    //public Color textColor = new Color(0f, 0f, 0f, 0f);
    //public Color textColorHide = new Color(0f, 0f, 0f, 0f);
    //public Color textColorShow = new Color(1f, 1f, 1f, 1f);

    private void Awake()
    {
        textToShow.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textToShow.SetActive(true);
            //textToShow.color = Color.Lerp(playerLight.color, playerLightColor, Time.deltaTime * pLightBlendTime);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textToShow.SetActive(false);
        }
    }


}
