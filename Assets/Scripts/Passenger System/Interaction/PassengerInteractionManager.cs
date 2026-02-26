using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassengerInteractionManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PassengerSystem PassengerSystem;
    [SerializeField] private GameObject CanvasUI;
    [SerializeField] private Button NormalTicketBtn;
    [SerializeField] private Button OverpricedTicketBtn;
    [SerializeField] private Button KickOutBtn;
    [SerializeField] private TextMeshProUGUI PassengerMessage;

    [Header("Parameters")]
    [SerializeField] private float InBetweenPassengersTime = 5f;

    private Passenger currentPassenger;

    private void Start()
    {
        NextPassengerInteraction();
    }

    private void OnNormalTicketClicked()
    {
        NextPassengerInteraction();
    }

    private void OnOverpricedTicketClicked()
    {
        float rnd = Random.value;
        if (currentPassenger.tolerance <= rnd)
        {
            ChangeMessage(currentPassenger.overpricedGoodReactionMessage);
        }
        else
        {
            ChangeMessage(currentPassenger.overpricedBadReactionMessage);
        }

        NextPassengerInteraction();
    }

    private void OnKickOutClicked()
    {
        ChangeMessage(currentPassenger.kickOutReactionMessage);
        NextPassengerInteraction();
    }

    private void NextPassengerInteraction()
    {
        StartCoroutine(NextPassengerRoutine());
    }

    private IEnumerator NextPassengerRoutine()
    {
        SetButtonsInteractable(false);

        if (currentPassenger != null)
        {
            PassengerSystem.DequeueFirstPassenger();
            yield return new WaitForSeconds(InBetweenPassengersTime);
        }

        currentPassenger = PassengerSystem.GetFirstPassenger();

        if (currentPassenger != null)
            ChangeMessage(currentPassenger.initialMessage);

        SetButtonsInteractable(true);
    }

    private void ChangeMessage(string newMessage)
    {
        PassengerMessage.text = newMessage;
    }

    private void SetButtonsInteractable(bool value)
    {
        NormalTicketBtn.interactable = value;
        OverpricedTicketBtn.interactable = value;
        KickOutBtn.interactable = value;
    }

    private void OnEnable()
    {
        NormalTicketBtn.onClick.AddListener(OnNormalTicketClicked);
        OverpricedTicketBtn.onClick.AddListener(OnOverpricedTicketClicked);
        KickOutBtn.onClick.AddListener(OnKickOutClicked);
    }

    private void OnDisable()
    {
        NormalTicketBtn.onClick.RemoveAllListeners();
        OverpricedTicketBtn.onClick.RemoveAllListeners();
        KickOutBtn.onClick.RemoveAllListeners();
    }

}
