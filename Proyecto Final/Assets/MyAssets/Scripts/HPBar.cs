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

    int HP = -1;

    [SerializeField]
    [FloatSlider(0.1f,0.5f)]
    float HPReductionAnimationStep = 0.1f;

    [SerializeField]
    [FloatSlider(0.5f, 3)]
    float HPReductionAnimationDuration = 1f;

    [SerializeField]
    GameObject TurnImage;

    [SerializeField]
    TMP_Text nameLabel;

    [SerializeField]
    Image BuffImage;

    [SerializeField]
    TMP_Text BuffText;

    [SerializeField]
    Image SelectedTarget;

    RPGActor myActor;

    public void SetActor(RPGActor actor)
    {
        myActor = actor;
        HP = actor.GetHealth();
        Bar.value = actor.GetHealth() / 100.0f;
        nameLabel.text = actor.Name;
        HPText.SetText(actor.GetHealth().ToString());
    }
    public void SetSelected(bool selected)
    {
        if(TurnImage != null)
        {
            TurnImage.SetActive(selected);
        }
    }

    public IEnumerator SetDamage(int newHp)
    {
        
        float reductionStep = Mathf.Abs(HP - newHp) / (HPReductionAnimationDuration / HPReductionAnimationStep);
        while (Bar.value * 100 > newHp)
        {
            if (HP > newHp)
            {
                Bar.value -= reductionStep / 100.0f;
            }
            else
            {
                Bar.value += reductionStep / 100.0f;
            }
            yield return new WaitForSeconds(HPReductionAnimationStep);
        }
        HP = newHp;
        HPText.SetText(HP.ToString());        
    }

    public void SetHealth(int hp)
    {
        HPText.SetText(hp.ToString());
        Bar.value = hp / 100.0f;
    }

    public void SetName(string n)
    {
        nameLabel.text = n;
    }

    public void DisplayBuff(Buff b)
    {
        
        BuffImage.gameObject.SetActive(true);
        BuffText.SetText(b.Duration.ToString());
    }

    public void DeactivateBuff()
    {
        BuffImage.gameObject.SetActive(false);
    }

    public void SetAsTarget(bool t)
    {
        SelectedTarget.gameObject.SetActive(t);
    }
}
