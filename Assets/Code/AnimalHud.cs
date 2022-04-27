
using UnityEngine;



public class AnimalHud : MonoBehaviour
{

    [SerializeField] private SpriteRenderer icon;


    private void OnEnable()
    {

        transform.GetChild(0).gameObject.SetActive(true);
        Invoke("BecameInvisible", 4);
    }

    public void SetImage(Sprite img) => icon.sprite = img;

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.rotation = Quaternion.Euler(-transform.rotation.eulerAngles.x, 0, -transform.rotation.eulerAngles.z);
        transform.localPosition = new Vector3(0, 7, 1);


    }



    private void BecameInvisible()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
