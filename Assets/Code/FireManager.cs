
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Invetory))]

public class FireManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager _gm;

    private bool isFiring;
    private bool isFireOn;



    [SerializeField] private Transform firePoint;
    [SerializeField] private List<FoodFire> pooling;
    [SerializeField] private Invetory _inv;

    [SerializeField] [Range(5, 50)] private float speed;

    private int idBullet;
    private int count;
    private int iDFoodFire;
    [SerializeField] private int lastId;
    private int countObj;


    public int bulletsSize;

    private void Start()
    {

        _gm = FindObjectOfType(typeof(GameManager)) as GameManager;
        _inv = FindObjectOfType(typeof(Invetory)) as Invetory;

        _inv.SetFruitInv(_gm.DefaultFruit());

        bulletsSize = 6;

        iDFoodFire = countObj = lastId = 0;

        InputButton.ChangeFoodBtn += SetLastIdFromInput;
        HudManager.SetFood += SetLastIdFromInput;


        isFireOn = true;

    }

    private void OnDestroy()
    {
        InputButton.ChangeFoodBtn -= SetLastIdFromInput;
        HudManager.SetFood -= SetLastIdFromInput;
    }

    // Update is called once per frame
    void Update()
    {
        isFiring = (Input.GetKeyDown(KeyCode.Space) && isFireOn);

        Fire();
    }

    private void Fire()
    {
        if (!isFiring) return;

        if (idBullet != iDFoodFire)
        {

            idBullet = iDFoodFire;
        }





        ThrowFood(_inv.GetFruitInv(lastId));


    }

    private void ThrowFood(Scriptables scrip)
    {


        Vector3 pos = new Vector3(firePoint.transform.position.x, firePoint.transform.position.y, firePoint.transform.position.z + 3.0f);

        countObj = 0;

        foreach (FoodFire item in pooling)
        {
            // responsável por contar os ob
            if (pooling.Count > 0 && item.GetIdListPos() == idBullet)
            {
                countObj++;
            }

        }




        if (count < 25 && countObj < bulletsSize)
        {
            InstanciaObj(scrip, pos);



        }
        else
        {





            for (int i = 0; i < pooling.Count; i++)
            {
                if (isFiring)
                {


                    if (pooling[i].gameObject.activeInHierarchy == false && pooling[i].GetIdListPos() == lastId)
                    {
                        pooling[i].transform.gameObject.SetActive(true);
                        pooling[i].transform.position = pos;
                        pooling[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed);


                        isFiring = false;
                    }

                    Debug.Log("IdFood: " + lastId + "\n" + " Ativo? " + pooling[i].gameObject.activeSelf);

                }


            }









        }





        if (isFireOn)
            StartCoroutine(TimeShoot());


    }

    private void InstanciaObj(Scriptables scrip, Vector3 pos)
    {
        GameObject localFood = scrip.GetGo();


        localFood = Instantiate(localFood, pos, Quaternion.identity);
        localFood.transform.Rotate(Vector3.zero);


        localFood.gameObject.tag = "Untagged";


        localFood.AddComponent<Rigidbody>();
        localFood.AddComponent<BoxCollider>();

        localFood.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speed);
        localFood.GetComponent<Rigidbody>().useGravity = false;
        localFood.GetComponent<BoxCollider>().isTrigger = true;

        localFood.GetComponent<BoxCollider>().size = new Vector3(1.5f, 2.5f, 1.5f);
        localFood.AddComponent<FoodFire>();


        localFood.GetComponent<FoodFire>().SetId(iDFoodFire, scrip.GetIdFruitScriptable());

        PoolingMethod(localFood.GetComponent<FoodFire>());
    }

    private void PoolingMethod(FoodFire tmp)
    {

        pooling.Insert(count, tmp);
        count++;

    }

    public void IncreaseIdFood()
    {

        Debug.Log("adicionei nna lista e aumentei o idfood");

        iDFoodFire++;

        lastId = iDFoodFire;



    }

    public int GetLastId() => lastId;
    public void SetIDFoodFire(Scriptables _Fruit)
    {
        if (_Fruit != null)
        {

            for (int i = 0; i < pooling.Count; i++)
            {
                Debug.LogError(" : " + pooling[i].GetIdInstanceLocal() + "\n" + " è igual : " + _Fruit.GetIdFruitScriptable());

                if (pooling[i].GetIdInstanceLocal() == _Fruit.GetIdFruitScriptable())
                {
                    lastId = pooling[i].GetIdListPos();

                    Debug.Log("procurei na lista e achei");


                    if (lastId != iDFoodFire) // pode ser removido depois
                        Debug.LogError(" lastId é: " + lastId + "\n" + " Atualizou com: " + pooling[i].GetIdInstanceLocal());


                }

            }

        }
        else
        {
            Debug.LogError(" Essa fruta não está no Inventário ");
        }


    }

    public void SetLastIdFromInput(int _)
    {
        // seta o id do botão apertado pra buscar a comida certa

        Debug.Log("Apertei botão e troquei de comida");
        lastId = _;
    }

    private IEnumerator TimeShoot()
    {

        isFireOn = false;

        yield return new WaitForSeconds(0.5f);

        isFireOn = true;

    }

}
