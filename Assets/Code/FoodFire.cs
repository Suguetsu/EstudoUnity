
using UnityEngine;

public class FoodFire : MonoBehaviour
{

    [SerializeField] private int idListPos;
    [SerializeField] private int idInstace;



    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Animal":

                IDamageable<int> animal = other.GetComponent<IDamageable<int>>();
                animal.Damage(idInstace);


                break;
            case "Player":



                break;

            case "Coletavel":


                IDamageable<int> _ = other.GetComponent<IDamageable<int>>();
                Colectable fruit = other.GetComponent<Colectable>();
                fruit.Change();


                _.Damage(0);





                break;

        }

        if (other.gameObject.tag != "Untagged")
            transform.gameObject.SetActive(false);
    }

    public void SetId(int listPos, int instance)
    {
        idListPos = listPos;    //posição na lista

        idInstace = instance;   //id de identificação

    }
    public int GetIdListPos() => idListPos;
    public int GetIdInstanceLocal() => idInstace;


    private void OnBecameInvisible()
    {
        transform.gameObject.SetActive(false);
    }


}
