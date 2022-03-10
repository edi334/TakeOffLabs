namespace TakeOffLabs;

public class SolvedClub
{
    public string ClubName { get; set; }
    public bool Solved { get; set; }

    public SolvedClub(string clubName, bool solved)
    {
        ClubName = clubName;
        Solved = solved;
    }
}