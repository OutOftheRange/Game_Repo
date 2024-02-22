using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPLogic : MonoBehaviour
{
    public static HPLogic Instance;
    [SerializeField] private Animator animeUI;
    private Image myImage;
    public Sprite brokenHpSprite;
    public int HPvalue = 3;
    
    void Awake()
    {
        Instance = this;
    }

    public void TakeDamage()
    {
        HPvalue--;
        animeUI.SetTrigger("PlayerDamaged");
        if (HPvalue>=0) ChangeImage();
        StartCoroutine(HPCheck());
    }
    private void ChangeImage()
    {
        myImage = transform.GetChild(HPvalue).gameObject.GetComponent<Image>();
        myImage.sprite = brokenHpSprite;
    }

    IEnumerator HPCheck()
    {
        yield return new WaitForSeconds(2f);
        if (HPvalue <= 0) WinLossLogic.Instance.GameOver();
    }
}
