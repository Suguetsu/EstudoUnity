using UnityEngine.EventSystems;
using UnityEngine;

// responsável por pegar o click do jogador para trocar de tiro
public class InputButton : MonoBehaviour, IPointerClickHandler
{
    public static event System.Action<int> ChangeFoodBtn;
    public static event System.Action<Transform> Outline;


    public int inputFood;


    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        // chamar  a troca de comida.
        ChangeFoodBtn?.Invoke(inputFood);
        Outline?.Invoke(this.transform);

        Debug.Log("Chamando troca de botões\n N?" + inputFood);



    }

    public void SetInput(int idBtn) => inputFood = idBtn;


}
