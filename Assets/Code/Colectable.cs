
using UnityEngine;


public class Colectable : MonoBehaviour, IDamageable<int>, IChangeable
{
    void Start()
    {
        Invoke("InviseBle", 3);
    }

    public static event System.Action<Scriptables> SetInInventory;


    private Scriptables fruit;

    public void SetFruitObj(Scriptables _)
    {
        if (fruit == null)
            fruit = _;
        
    }

    public void SendFruitToInv() => SetInInventory?.Invoke(fruit);

    public void InviseBle() => gameObject.SetActive(false);
    public void Damage(int ID)
    {

        this.gameObject.SetActive(false);
    }

    public void Change()
    {        SendFruitToInv();
       
    }
}
