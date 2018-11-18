using UnityEngine;
using System.Collections;

public class Animation_LevelComplete : MonoBehaviour {

    private float duration = 1.0f;
    private float alpha = 0f;
    Color tempColor;
  
    void FixedUpdate()
    {
        LerpAlpha();
    }

    void LerpAlpha()
    {
        tempColor = gameObject.GetComponent<GUITexture>().color;
        float lerp = Mathf.PingPong(Time.time, duration) / duration;

        tempColor.a = Mathf.Lerp(0f, 1.0f, lerp);
        gameObject.GetComponent<GUITexture>().color = tempColor;
    }
}
