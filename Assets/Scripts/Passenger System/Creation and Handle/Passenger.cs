using System.Text;

[System.Serializable]
public class Passenger
{
    public int skinId;
    public float tolerance;
    public float offeredPay;
    public int arrivalStop;
    public int destinationStop;
    public string initialMessage;
    public string overpricedGoodReactionMessage;
    public string overpricedBadReactionMessage;
    public string kickOutReactionMessage;

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("Passenger:");
        sb.AppendLine($"  SkinId: {skinId}");
        sb.AppendLine($"  Tolerance: {tolerance}");
        sb.AppendLine($"  Offered Pay: {offeredPay}");
        sb.AppendLine($"  ArrivalStop: {arrivalStop}");
        sb.AppendLine($"  DestinationStop: {destinationStop}");
        sb.AppendLine($"  InitialMessage: {initialMessage}");
        sb.AppendLine($"  OverpricedGoodReactionMessage: {overpricedGoodReactionMessage}");
        sb.AppendLine($"  OverpricedBadReactionMessage: {overpricedBadReactionMessage}");
        sb.AppendLine($"  KickOutReactionMessage: {kickOutReactionMessage}");

        return sb.ToString();
    }
}
