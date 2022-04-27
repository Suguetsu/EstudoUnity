
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class Scriptables : ScriptableObject
{
    [SerializeField]  private int id
    {
        get
        {

            return GetInstanceID();
        }


    }


    
   public string nameObj;

    public enum kind
    {
        food, drink
    }


    [SerializeField] private GameObject obj;
    [SerializeField] private kind tipoItem;
    [SerializeField] private Sprite icon;


    public GameObject GetGo() => obj;
    public kind GetTipo() => tipoItem;
    public Sprite GetIcon() => icon;
    public int GetIdFruitScriptable() => id;

}
