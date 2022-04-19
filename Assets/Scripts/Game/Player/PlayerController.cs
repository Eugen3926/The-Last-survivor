using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class PlayerController
{
    public void Move(float joyX, float joyZ, Rigidbody hero)
    {        
        Vector3 dir = new Vector3(joyX * 0.05f, 0f, joyZ * 0.05f);
        Rotate(hero.transform, dir);
        hero.velocity = new Vector3(joyX * 4f, 0f, joyZ * 4f);
        //Player.heroTransform.position += dir;        
    }    

    public void Rotate(Transform hero, Vector3 dir) {
        hero.rotation = Quaternion.LookRotation(dir);        
    }

    public void Damage(Image healthBar, Image armorhBar)
    {
        if (armorhBar.gameObject.activeSelf)
        {
            armorhBar.fillAmount -= 0.21f;
        }
        else {
            healthBar.fillAmount -= 0.21f;
        }
        
    }

    public void BonusEffect(string name)
    {
        switch (name)
        {
            case "Star(Clone)":
                StarEffect();
                break;
            case "Armor(Clone)":
                ArmorEffect();
                break;
            case "Health(Clone)":
                HealthEffect();
                break;
            default:
                break;
        }
    }

    private void StarEffect() {
        LevelController.playerScore += 300;
    }

    private void ArmorEffect()
    {
       /* Image armorBar = LevelController.heroTransform.parent.GetChild(1).GetChild(3).GetComponent<Image>();
        GameObject armorBarBackGround = armorBar.transform.parent.GetChild(2).gameObject;
        if (!armorBarBackGround.activeSelf)
        {
            armorBarBackGround.SetActive(true);
            armorBar.gameObject.SetActive(true);
        }
        armorBar.fillAmount = 1;*/
    }

    private void HealthEffect()
    {
        /*Image healthBar = LevelController.heroTransform.parent.GetChild(1).GetChild(1).GetComponent<Image>();
        healthBar.fillAmount = 1f;*/
    }
}
