using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBox(int _noOfBoxes)
    {

        for (int i = 0; i < _noOfBoxes; i++)
        {
            var _obj = Instantiate(pizzaBoxPrefab);
            SetBoxPos(_obj);
        }

        if (pizzasCollected >= 1)
        {
            if (characterRig != null)
                characterRig.weight = 1f;

            if (upgradeParticleEff != null)
                upgradeParticleEff.Play();
        }
    }

    public void RemoveBox(int _noOfBoxes)
    {
        if (pizzasCollected >= 1)
        {
            for (int i = 0; i < _noOfBoxes; i++)
            {
                if (pizzasCollected >= 1)
                {
                    Destroy(pizzaBoxeArray[pizzasCollected - 1]);
                    pizzaBoxeArray.RemoveAt(pizzasCollected - 1);
                    pizzasCollected--;
                }
            }
        }

    }

    void SetBoxPos(GameObject _box)
    {
        _box.tag = "CollectedBox";
        _box.transform.SetParent(pizzaBoxPivot);
        _box.transform.localPosition = new Vector3(0f, pizzasCollected * .2f, 0f);
        _box.transform.rotation = Quaternion.Euler(Vector3.zero);
        pizzaBoxeArray.Add(_box.gameObject);


        pizzasCollected++;
    }

    public void EnbaleBoxPhysics(int _indexFrom)
    {
        if (pizzasCollected <= 0)
            return;

        for (int i = pizzasCollected - 1; i >= _indexFrom; i--)
        {
            GameObject _box = pizzaBoxeArray[i];

            _box.transform.parent = null;
            _box.GetComponent<Collider>().isTrigger = false;
            _box.GetComponent<Rigidbody>().isKinematic = false;
            //Destroy(_box, 3f);
            pizzaBoxeArray.RemoveAt(i);
            pizzasCollected--;
        }
    }


    public void PizzaDelivary(GameObject _delivaryPos, int _noOfPizzas)
    {
        if (pizzasCollected >= 1)
        {
            for (int i = 1; i <= _noOfPizzas; i++)
            {
                if (pizzasCollected >= 1)
                {
                    GameObject _box = pizzaBoxeArray[pizzaBoxeArray.Count - 1];
                    pizzaBoxeArray.RemoveAt(pizzaBoxeArray.Count - 1);
                    _box.transform.parent = null;
                    _box.GetComponent<PizzaBox>().moveTowardsPos(_delivaryPos);
                    pizzasCollected--;
                }

            }

            audioManager.Play("MoreCashCollected");

        }
    }

    public void RemoveAllPizzas()
    {
        EnbaleBoxPhysics(0);
    }

}
