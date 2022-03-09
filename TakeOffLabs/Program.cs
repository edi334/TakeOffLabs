// See https://aka.ms/new-console-template for more information

using TakeOffLabs;

int golferCount, scoreCount;

golferCount = Console.Read();

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

scoreCount = Console.Read();

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

//scores.GroupBy(s => s.GolferId).