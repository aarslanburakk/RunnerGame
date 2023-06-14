using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPotion : MonoBehaviour
{
    public static ButtonPotion potionInstence;
    [SerializeField] Text countText;
    int count = 0;
    private void Start()
    {
        potionInstence = this;
        if (InventoryManager.instance.items.Count > 0)
        {
            count = InventoryManager.instance.items.Count;
            countText.text = count.ToString();
        }
        
    }
    public void UseRedPotion()
    {
        if (HealthBar.hitPoint == HealthBar.maxHitPoint)
        {
            return;
        }
        else
        {

            CountDecrease();
            HealthBar.instance.UseHeal();
        }

    }

    public void UseGreenPotion()
    {
        InventoryManager.instance.items.Remove(PickGreenObject());
        CountDecrease();
    }

    GameObject PickGreenObject()
    {
        GameObject pickedObject = null;
        bool active = false;
        foreach (GameObject go in InventoryManager.instance.items)
        {
            if (go.CompareTag("Green Potion"))
            {
                active = true;
                pickedObject = go;
            }
            else if (active == false)
            {
                pickedObject = null; ;
            }
        }
        return pickedObject;
    }

    GameObject PickRedObject()
    {
        GameObject pickedObject = null;
        bool active = false;
        foreach (GameObject go in InventoryManager.instance.items)
        {
            if (go.CompareTag("Red Potion"))
            {
                active = true;
                pickedObject = go;
            }
            else if (active == false)
            {
                pickedObject = null; ;
            }
        }
        return pickedObject;
    }
    public void CountIncrease()
    {
        if (count < 3)
        {
            count += 1;
            countText.text = "" + count;
        }
    }
    public void CountDecrease()
    {
        count -= 1;
        countText.text = "" + count;
        if (count == 0)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
        }
    }
    public void IfPlayerDead()
    {     
        gameObject.transform.parent.gameObject.SetActive(false);     

    }

}
