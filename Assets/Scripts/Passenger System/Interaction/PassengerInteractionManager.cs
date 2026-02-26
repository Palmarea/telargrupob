using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
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
    [SerializeField] private Image PassengerPortrait;

    [Header("Parameters")]
    [SerializeField] private float InBetweenPassengersTime = 5f;

    [Header("Config")]
    [SerializeField] private SpriteAtlas SpriteAtlas;

    private Passenger currentPassenger;

    public void StartPassengerInteraction()
    {
        NextPassengerInteraction();
    }

    private void OnNormalTicketClicked()
    {
        MoneyManager.Instance.AddMoney(currentPassenger.offeredPay);
        NextPassengerInteraction();
    }

    private void OnOverpricedTicketClicked()
    {
        float rnd = Random.value;
        if (currentPassenger.tolerance <= rnd)
        {
            MoneyManager.Instance.AddMoney(currentPassenger.offeredPay + 0.5f);
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
            currentPassenger = null;
            yield return new WaitForSeconds(InBetweenPassengersTime);
        }

        if (PassengerSystem.CheckQueueState())
        {
            currentPassenger = PassengerSystem.GetFirstPassenger();

            if (currentPassenger != null)
            {
                UpdateInterationUIState(true);
                ChangePortrait();
                ChangeMessage(currentPassenger.initialMessage);
            }

            SetButtonsInteractable(true);
        }
        else
        {
            UpdateInterationUIState(false);
        }
    }

    private void ChangeMessage(string newMessage)
    {
        PassengerMessage.text = newMessage;
    }

    private void ChangePortrait()
    {
        PassengerPortrait.sprite = SpriteAtlasHandling.GetSpriteFromAtlas(SpriteAtlas, currentPassenger.skinId.ToString());
    }

    private void SetButtonsInteractable(bool value)
    {
        NormalTicketBtn.interactable = value;
        OverpricedTicketBtn.interactable = value;
        KickOutBtn.interactable = value;
    }

    public void UpdateInterationUIState(bool state)
    {
        CanvasUI.SetActive(state);
    }

    public bool GetInteractionState() => CanvasUI.activeSelf;

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
