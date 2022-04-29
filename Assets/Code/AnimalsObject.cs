
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]

public class AnimalsObject : MonoBehaviour, IDamageable<int>
{
    // Start is called before the first frame update
    private Rigidbody _rb;
    private bool isAlive;
    private Scriptables obj;
    private AnimalHud _hud;
    private GameManager _gm;

    private bool isWrongFruit;
    private int speed;






    void OnEnable()
    {


        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = false;


        isAlive = true;


        CancelInvoke();
        Invoke("BecameInvisible", 8);








    }

    void Start()
    {


        transform.GetChild(1).gameObject.SetActive(true);
        _hud = transform.GetChild(1).GetComponent<AnimalHud>();

        if (obj != null)
            _hud.SetImage(obj.GetIcon());

        //Debug.Log("Falta definir oque será feito quando o objeto for diferente do jogdado");
        //Debug.Log("Falta definir uma ação  determinar os leveis, a pontuação");
        //Debug.Log("Falta definir uma ação  definir os obstaculos por level");
        //Debug.Log("Falta definir como setar a fruta do inventario de acordo com a fruta que o jogador pegou");






        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        _gm = FindObjectOfType(typeof(GameManager)) as GameManager;

        speed = 5;
    }


    public void RigidBodySetSpeed(float speed)
    {

        if (speed > 0)
        {
            speed *= -1;

        }

        if (speed <= -20)
        {
            speed = -20;
        }


        _rb.velocity = new Vector3(0, 0, speed);

    }
    public bool GetisAlive() => isAlive;

    public void Damage(int ID)
    {


    //    Debug.Log("ID entrada: " + ID + "id do objeto: " + obj.GetIdFruitScriptable());


        if (ID == obj.GetIdFruitScriptable())
        {
            transform.gameObject.SetActive(false);
            _gm.RandomCollectable(this.transform);
            _gm.SendScoreHud(1);

        }
        else if (!isWrongFruit)
        {
            isWrongFruit = true;
            _rb.velocity *= speed;


            _gm.SendScoreHud(10);
        }





    }

    private void BecameInvisible()
    {

        // congela a rotação do rigidy body
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        transform.gameObject.SetActive(false);

    }



    public void SetFrui(Scriptables fruit)
    {
        obj = fruit;

        if (obj != null && _hud != null)
            _hud.SetImage(obj.GetIcon());

    }


}
