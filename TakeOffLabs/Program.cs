// See https://aka.ms/new-console-template for more information

using TakeOffLabs;

void SubTask1(List<Golfer> golfers)
{
    var set = new HashSet<string>(golfers.Select(s => s.ClubName).ToList());
    foreach (var clubName in set)
    {
        Console.Write(clubName + ": ");
        golfers
            .Where(g => g.ClubName == clubName)
            .OrderByDescending(g => g.SkillLevel)
            .ToList()
            .ForEach(g => Console.Write(g.Name + " "));
        Console.Write("\n");
    }

    Console.Write("\n");
}

void SubTask2(List<Golfer> golfers)
{
    var set = new HashSet<string>(golfers.Select(s => s.ClubName).ToList());
    var topSkilled = golfers.OrderByDescending(g => g.SkillLevel).Take(set.Count).ToList();

    var solvedClubs = new List<SolvedClub>();
    foreach (var clubName in set)
    {
        var solved = false;
        var topGolfer = topSkilled.Find(g => g.ClubName == clubName);
        if (topGolfer != null)
        {
            solved = true;
        }
        solvedClubs.Add(new SolvedClub(clubName, solved));
    }
    
    var needGolfers = solvedClubs
        .Where(s => s.Solved == false)
        .ToList();

    var canBeMovedGolfers = topSkilled
        .Where(g => needGolfers.Find(sc => sc.ClubName == g.ClubName) == null)
        .OrderByDescending(g => g.SkillLevel)
        .ToList();
    
    needGolfers.ForEach(sc =>
    {
        var golfer = canBeMovedGolfers.FirstOrDefault();
        if (golfer != null)
        {
            golfer.ClubName = sc.ClubName;
            canBeMovedGolfers.Remove(golfer);
        }
    });
}

void SubTask3(List<Golfer> golfers)
{
    var set = new HashSet<string>(golfers.Select(s => s.ClubName).ToList());
    var topSkilled = golfers.OrderByDescending(g => g.SkillLevel).Take(set.Count).ToList();

    var solvedClubs = new List<SolvedClub>();
    foreach (var clubName in set)
    {
        var solved = false;
        var topGolfer = topSkilled.Find(g => g.ClubName == clubName);
        if (topGolfer != null)
        {
            solved = true;
        }
        solvedClubs.Add(new SolvedClub(clubName, solved));
    }
    
    var needGolfers = solvedClubs
        .Where(s => s.Solved == false)
        .ToList();

    var canBeMovedGolfers = topSkilled
        .Where(g => needGolfers.Find(sc => sc.ClubName == g.ClubName) == null)
        .OrderByDescending(g => g.SkillLevel)
        .ToList();
    
    var minClub = needGolfers.Select(sc =>
    {
        var golfer = golfers
            .Where(g => g.ClubName == sc.ClubName)
            .MaxBy(g => g.SkillLevel);
        return new
        {
            MaxSkillLevel = golfer.SkillLevel,
            golfer.ClubName
        };
    }).MinBy(o => o.MaxSkillLevel);

    double min = canBeMovedGolfers[0].SkillLevel;
    var movedGolfer = canBeMovedGolfers[0];
    for (int i = 0; i < canBeMovedGolfers.Count - 1; ++i)
    {
        var dif = canBeMovedGolfers[i].SkillLevel - canBeMovedGolfers[i + 1].SkillLevel;
        if (dif < min)
        {
            min = dif;
            movedGolfer = canBeMovedGolfers[i];
        }
    }

    movedGolfer.ClubName = minClub.ClubName;
}

int golferCount, scoreCount;

golferCount = Int32.Parse(Console.ReadLine());

var golfers = new List<Golfer>();

for (int i = 0; i < golferCount; ++i)
{
    var line = Console.ReadLine();
    var input = line.Split(" ");
    int id = -1;
    try
    {
        id = Int32.Parse(input[0]);
    }
    catch
    {
        Console.Write($"Unable to parse {input[0]}");
    }

    var golfer = new Golfer(id, input[1], input[2]);
    golfers.Add(golfer);
}

scoreCount = Int32.Parse(Console.ReadLine());

var scores = new List<Score>();

for (int i = 0; i < scoreCount; ++i)
{
    var line = Console.ReadLine();
    var input = line.Split(" ");
    int golferId = -1, value = 0;
    try
    {
        value = Int32.Parse(input[0]);
        golferId = Int32.Parse(input[1]);
    }
    catch
    {
        Console.Write($"Error while parsing");
    }

    var score = new Score(value, golferId);
    scores.Add(score);
}

var query = scores.GroupBy(
    s => s.GolferId,
    s => s.Value,
    (golferId, values) => new
    {
        Key = golferId,
        Average = (double) (values.Sum() / values.Count())
    });

foreach (var group in query)
{
    var golfer = golfers.Find(g => g.Id == group.Key);
    if (golfer != null)
    {
        golfer.SkillLevel = group.Average;
    }
}

SubTask1(golfers);

SubTask2(golfers);

SubTask1(golfers);

SubTask3(golfers);

SubTask1(golfers);