using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    RectTransform rectTransform;
    Vector3 defaultPos;
    int maxHeight = 18;
    int diff = 1;
    float timePeriod = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        defaultPos = rectTransform.position;
        StartCoroutine(MoveUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MoveUp()
    {
        while (rectTransform.position.y < defaultPos.y + maxHeight)
        {
            rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y + diff);
            yield return new WaitForSeconds(timePeriod);
        }
        yield return MoveDown();
    }

    IEnumerator MoveDown()
    {
        while (rectTransform.position.y > defaultPos.y)
        {
            rectTransform.position = new Vector3(rectTransform.position.x, rectTransform.position.y - diff);
            yield return new WaitForSeconds(timePeriod);
        }
        yield return MoveUp();
    }


}
