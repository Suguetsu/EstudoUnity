using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerState { live, dead }

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static event System.Action GetRandomFruit;


    [SerializeField] private Scriptables[] _itens = new Scriptables[0];

    [Header("Score Settings")]
    [SerializeField] private GameObject canvas;
    [SerializeField] private HudManager _hud;

    private int globalScore;
    private int globalLevel;





    [Header("Animals Settings")]
    [SerializeField] private Animals[] _animals;
    [SerializeField] private GameObject wayPointsParent;
    [SerializeField] private List<Transform> wayPointPos;
    [SerializeField] private List<GameObject> animalsPrefab;
    [SerializeField] float speedAnimals;
    [SerializeField] PhysicMaterial matPhysics;
    [SerializeField] public float timeSpown;

    public GameObject test;

    private int sizeInv;
    private Scriptables currentFood;

    [Header("Live Settings")]
    [SerializeField] private int myLives;







    void Start()
    {
        for (int i = 0; i < wayPointsParent.transform.childCount; i++)
        {
            wayPointPos.Add(wayPointsParent.transform.GetChild(i)); // pega a posição dos WayPoints


        }

        for (int i = 0; i < _animals.Length; i++)
        {

            animalsPrefab.Add(_animals[i].animalObj());  // pega os gameObjects



            // Instantiate(canvas, animalsPrefab[i].transform);

        }

        StartCoroutine(SpownAnimal());

        test = Instantiate(test, gameObject.transform.localPosition, Quaternion.identity);


        SendScoreHud(0);

        myLives = 3;
    }



    private void Update()
    {


        if (speedAnimals < 0)
        {
            speedAnimals *= -1;
        }
    }

    // Update is called once per frame

    private IEnumerator SpownAnimal()
    {
        yield return new WaitForSeconds(timeSpown);

        SetRandom();
    }

    private void SetRandom()
    {
        int random = (int)Random.Range(0, animalsPrefab.Count);
        int posRandom = (int)Random.Range(0, wayPointPos.Count);



        if (animalsPrefab[random].transform.rotation.y == 0)
        {
            GameObject temp = Instantiate(animalsPrefab[random], wayPointPos[posRandom]);

            GetRandomFruit?.Invoke();

            temp.AddComponent<AnimalsObject>();
            temp.GetComponent<AnimalsObject>().SetFrui(currentFood);        //<< falta arrumar aqui
            temp.GetComponent<AnimalsObject>().RigidBodySetSpeed(speedAnimals);

            temp.GetComponent<BoxCollider>().material = matPhysics;

            temp.transform.rotation = Quaternion.Euler(0, 180, 0);
            temp.transform.gameObject.tag = "Animal";


            GameObject temp2 = Instantiate(canvas, temp.transform);




            animalsPrefab.RemoveAt(random);
            animalsPrefab.Insert(random, temp);
        }
        else if (!animalsPrefab[random].activeInHierarchy)
        {

            animalsPrefab[random].gameObject.SetActive(true);

            animalsPrefab[random].transform.SetParent(wayPointPos[posRandom].transform);
            animalsPrefab[random].transform.localPosition = Vector3.zero;
            animalsPrefab[random].transform.rotation = Quaternion.Euler(0, 180, 0);

            GetRandomFruit?.Invoke();

            animalsPrefab[random].GetComponent<AnimalsObject>().SetFrui(currentFood);   //<< falta arrumar aqui
            animalsPrefab[random].GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 180, 0);
            animalsPrefab[random].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speedAnimals);
        }

        test = animalsPrefab[random].gameObject;

        StartCoroutine(SpownAnimal());
    }

    public void RandomCollectable(Transform t)
    {
        GameObject temp;
        int id = 0;

        if (Random.value > 0.8f)
        {
            id = 5;
        }
        else if (Random.value > 0.5f)
        {
            id = 4;
        }
        else if (Random.value > 0.2f)
        {

            id = 3;
        }


        if (id >= _itens.Length)
        {
            id = Random.Range(0, _itens.Length);
        }



        if (id <= Random.Range(0, _itens.Length))
        {
            temp = Instantiate(_itens[id].GetGo(), t.position + new Vector3(0, 2.0f, 0), Quaternion.identity);
            temp.AddComponent<BoxCollider>();
            temp.AddComponent<BoxCollider>().size = Vector3.one * 2;
            temp.GetComponent<BoxCollider>().isTrigger = true;
            temp.AddComponent<Colectable>();
            temp.GetComponent<Colectable>().SetFruitObj(_itens[id]);
            temp.gameObject.tag = "Coletavel";


            Color32 a = Color.blue;
            temp.GetComponent<MeshRenderer>().material.color = a;



        }

    }

    public void GetInventorySize(int getNumber) => sizeInv = getNumber;


    public Scriptables DefaultFruit() => _itens[0];
    public void SearchItem(Scriptables food)
    {
        foreach (Scriptables item in _itens)
        {
            if (item.GetIdFruitScriptable() == food.GetIdFruitScriptable())
            {
                currentFood = food;
            }
        }
    }


    #region ScoreSettings
    public void SendScoreHud(int _)
    {

        globalScore += _;

        if (globalScore > 99)
        {
            globalLevel = 2;
        }
        else
        if (globalScore > 199)
        {
            globalLevel = 3;
        }
        else
        {
            globalLevel = 1;
        }



        _hud.SetScoreLevel(_, globalLevel);


    }

    #endregion

    #region LifeManger

    public void DamageLifePlayer(int _)
    {
        myLives -= _;


        _hud.SetIconLife(_);

    }

    #endregion

}
