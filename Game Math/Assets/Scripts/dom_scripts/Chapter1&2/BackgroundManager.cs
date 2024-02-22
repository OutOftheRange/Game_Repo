using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    void Update()
    {
        //ResizeSpriteToScreen();
    }

    public void ResizeSpriteToScreen(){
        SpriteRenderer backgroundSprite = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        Vector3 backgroundSize = this.gameObject.transform.GetChild(0).lossyScale;

        //Vector3 temporaryScale = transform.localScale;

        Debug.Log(backgroundSize);

        float width = backgroundSprite.sprite.bounds.size.x;
        float height = backgroundSprite.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = Camera.main.orthographicSize / Screen.height * Screen.width;

        backgroundSize.x = worldScreenWidth / width;
        backgroundSize.y = worldScreenHeight / height;

        //Debug.Log(worldScreenHeight);
        //Debug.Log(worldScreenWidth);
        //Debug.Log(height);
        //Debug.Log(width);
        //Debug.Log(temporaryScale.x);
        //Debug.Log(temporaryScale.y);
        //Debug.Log(backgroundSize);

        //backgroundSize = temporaryScale;
        //Debug.Log(backgroundSize);
    }
}
