using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(BoxCollider))]
public class LifeController : MonoBehaviour, IDamageable<int>
{

    private GameManager _gm;


    [SerializeField] private GameObject objPlayer; // usado para fazer o player piscar quanto atingido
    [SerializeField] private SkinnedMeshRenderer _render; // usado para fazer o player mudar de cor quando atingido
    [SerializeField] private BoxCollider _cCollider;

    void Start()
    {

        _gm = FindObjectOfType(typeof(GameManager)) as GameManager;
        _cCollider.isTrigger = true;
    }




    private bool isDamage;
    private bool isInvunerable;
    private float time;


    public void Damage(int hit)
    {

        Color cor = Color.red;
        cor = new Color(cor.r, cor.g, cor.b, 0.25f);

        for (int i = 0; i < _render.materials.Length; i++)
        {
            _render.materials[i].color = Color.red;
        }


        _gm.DamageLifePlayer(hit);



    }


    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Animal")
        {

            other.gameObject.SetActive(false);

            if (isInvunerable) return;

            isDamage = true;

        }

        if (isDamage)
        {
         

            this.Damage(1);
            isInvunerable = true;
            isDamage = false;


            StartCoroutine(InvunerableTime());



        }

    }

    private void Update()
    {
        time = time < 3 && isInvunerable ? time += 1 * (Time.deltaTime) : time;


    }

    private IEnumerator InvunerableTime()
    {
        //Debug.Log("Executando coroutine " + time);

        if (isInvunerable && time < 3)
        {

            yield return new WaitForSeconds(0.1f);
            objPlayer.gameObject.SetActive(!objPlayer.activeInHierarchy);



            StartCoroutine(InvunerableTime());
        }
        else
        {



            for (int i = 0; i < _render.materials.Length; i++)
            {
                _render.materials[i].color = Color.white;
            }

            time = 0;

            isInvunerable = false;
            objPlayer.gameObject.SetActive(true);
       

            StopAllCoroutines();
        }

    }

}
