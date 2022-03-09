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