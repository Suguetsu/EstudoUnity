
using System.Collections.Generic;
using UnityEngine;

// Player Inventory
[RequireComponent(typeof(FireManager))]



public class Invetory : MonoBehaviour
{
    public static event System.Action<int, Sprite> SendFruitToHud;



    [SerializeField] private List<Scriptables> inventoryFruit_List;

    FireManager _fireM;



    private GameManager _gm;





    private void Start()
    {


        _gm = FindObjectOfType(typeof(GameManager)) as GameManager;
        _fireM = GetComponent<FireManager>();

        GameManager.GetRandomFruit += GetRandomFruit;
        Colectable.SetInInventory += SetFruitInv;


        Invoke("UpdateListHud", 0.1f);
    }


    private void OnDestroy()
    {
        GameManager.GetRandomFruit -= GetRandomFruit;
        Colectable.SetInInventory -= SetFruitInv;

    }

    public Scriptables GetFruitInv(int id) => inventoryFruit_List[id];

    public void SetFruitInv(Scriptables inv)
    {


        if (inventoryFruit_List.Contains(inv))
        {

            Debug.Log("Já tem! ");

            _fireM.SetIDFoodFire(inv);


            return;
        }

        inventoryFruit_List.Add(inv);



        if (_fireM != null)
            _fireM.IncreaseIdFood();




        UpdateListHud();
    }

    private void UpdateListHud()
    {

        if (inventoryFruit_List.Count > 0)
            for (int i = 0; i < inventoryFruit_List.Count; i++)
            {

                SendFruitToHud?.Invoke(i, inventoryFruit_List[i].GetIcon());

            }

    }

    public void RemoveFruitInv(Scriptables inv) => inventoryFruit_List.Remove(inv);
    public bool FindObj(Scriptables inv)
    {

        if (inventoryFruit_List.Contains(inv)) return true;


        return false;

    }
    public int GetSize() => inventoryFruit_List.Count;
    public void GetRandomFruit()
    {
        int foodRandom = (int)Random.Range(0, GetSize());
        _gm.SearchItem(inventoryFruit_List[foodRandom]);



    }






}
