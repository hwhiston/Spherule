using UnityEngine;
using System.Collections;

public class Draggable : MonoBehaviour {

    private Vector3 startingPos;
    private bool beingDragged = false;
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool draggable;

    void Awake()
    {
        startingPos = gameObject.GetComponent<RectTransform>().anchoredPosition3D;
        draggable = true;
    }

    void OnMouseDown()
    {
        if (draggable)
        {
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    void OnMouseDrag()
    {
        if (draggable)
        {
            beingDragged = true;
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }

    void OnMouseUp()
    {
        beingDragged = false;
        gameObject.GetComponent<RectTransform>().anchoredPosition3D = startingPos;
        draggable = true;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.gameObject.GetComponent<Spherule>() != null && c.gameObject.GetComponent<QuantityCounter>() != null && c.gameObject.GetComponent<Spherule>().Type == gameObject.GetComponent<Spherule>().Type)
        {
            draggable = false;
            c.gameObject.GetComponent<QuantityCounter>().numSpheres++;
            gameObject.GetComponent<RectTransform>().anchoredPosition3D = startingPos;
        }
    }
}
