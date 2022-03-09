namespace TakeOffLabs;

public class Score
{
    public int Value { get; set; }
    public int GolferId { get; set; }

    public Score(int value, int golferId)
    {
        Value = value;
        GolferId = golferId;
    }
}