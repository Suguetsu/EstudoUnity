
using UnityEngine;



[CreateAssetMenu(menuName = "New Animal", fileName = "Wild")]

public class Animals : ScriptableObject
{
    public enum animalType { Dog, Chicken, Cow, Horse, Bull, Fox }

    [SerializeField] private GameObject obj;
    [SerializeField] private animalType wild;
    [SerializeField] private Scriptables preferredFood;

    public GameObject animalObj() => obj;
    public animalType animalKind => wild;
    public string GetFood() => preferredFood.GetTipo().ToString();
}
