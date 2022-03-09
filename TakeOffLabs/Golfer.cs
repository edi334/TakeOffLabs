namespace TakeOffLabs;

public class Golfer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ClubName { get; set; }
    
    public double SkillLevel { get; set; }

    public Golfer(int id, string name, string clubName)
    {
        Id = id;
        Name = name;
        ClubName = clubName;
        SkillLevel = 0;
    }
}