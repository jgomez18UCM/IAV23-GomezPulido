using BehaviorDesigner.Runtime.ObjectDrawers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    Slider Bar;

    [SerializeField]
    TMP_Text HPText;

    float HP;

    [SerializeField]
    [FloatSlider(0.1f,0.5f)]
    float HPReductionAnimationStep = 0.1f;

    [SerializeField]
    [FloatSlider(0.5f, 3)]
    float HPReductionAnimationDuration = 1f;

    [SerializeField]
    GameObject SelectedImage;

    [SerializeField]
    TMP_Text nameLabel;
    // Start is called before the first frame update
    void Start()
    {
        HP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelected(bool selected)
    {
        if(SelectedImage != null)
        {
            SelectedImage.SetActive(selected);
        }
    }

    public IEnumerator SetDamage(float newHp)
    {
        
        float reductionStep = (HP - newHp) / (HPReductionAnimationDuration / HPReductionAnimationStep);
        while (Bar.value * 100.0f > newHp)
        {
            
            Bar.value -= reductionStep / 100.0f;
            HP -= reductionStep;
            HPText.SetText(HP.ToString());
            yield return new WaitForSeconds(HPReductionAnimationStep);
        }
        
        
    }

    public void SetName(string n)
    {
        nameLabel.text = n;
    }
}
