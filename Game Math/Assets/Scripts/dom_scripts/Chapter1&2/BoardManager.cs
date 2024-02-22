using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    void Start(){
    }

    //method to call from the event in the BattleTransition animation
    public void NewBoard()
    {
        SpawnBoard.Instance.SpawnNewBoard();
    }

    //resizing the backgrund
    public void ResizeSpriteToScreen(){
        SpriteRenderer backgroundSprite = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();

        Vector3 temporaryScale = transform.localScale;

        float width = backgroundSprite.sprite.bounds.size.x;
        float height = backgroundSprite.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        temporaryScale.x = worldScreenWidth / width;
        temporaryScale.y = worldScreenHeight / height;

        transform.localScale = temporaryScale;
    }
}
