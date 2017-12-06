using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var day = "3";

            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (day == null)
            {
                Console.WriteLine("Enter the day.");
                day = Console.ReadLine();
            }

            if (day == "1")
            {
                const string input = "892195969991735837915273868729548694237967495115412399373194562526947585337233793568278265279199883197167634791293177986152566236718332617536487236879747167999983363832257912445756887314879229925864477761357139855548522513798899853896612387146687716264599943289416326727256525173953861534244979466587895429399159924916364476319573895566795393368411672387263615582128377676293612892723762237191146714286233543514411813323197995953854871628225358543514157867372265718724276911699514971458844849349726276329135118243155698271218844347387457343656446381799296893222256198484465873714311777937421161581798189554141474236239447612421883232173914183732126332838194648583472419154369952477422666389517569944428464617457124369349242479612422673241361777576466946622932243728551273284837934497511114334421486262244982914734452113946361245377351849815584855691778894798219822463298387771923329337634394654439458564233259451453345316753241438267739439225497515276522424441532462541528195782818326918562247278496495764435386667383577543385186827269732261223156824351164841648424564925198783625721396988984481558391866483955533972212164693898955412719161648411279149413443192896864258215498543827458438871355879336892721675937111952479183496982825163456282747678364612135596373533447719867384667516572262124225585623974278833981365494628646614588114147473559138853453189448624976774641922469183942857695986376428944876851497914443873513862319484181787593572987444669767939526294424531262999564948571142342741129862311311313166798363442745792896227642881893134498151552326647933689596516859342242244584714818773791567187322217164347852843751875979415198165627534263527828414549217234322361937785185174993256753483876378332521824515977173397535784236923629636713469151526399149548322849831431526219478653861754364155275865511643923249858589466142474763778413826829226663398467569555747267195129525138917561785436449855933951538973995881954521124753369223898312843734771532342383282987422334196585128526526324291777689689492346231786335851551413876834969878";
                Day1(input);
            }
            else if (day == "2")
            {
                const string input = @"116	1259	1045	679	1334	157	277	1217	218	641	1089	136	247	1195	239	834
269	1751	732	3016	260	6440	5773	4677	306	230	6928	7182	231	2942	2738	3617
644	128	89	361	530	97	35	604	535	297	599	121	567	106	114	480
105	408	120	363	430	102	137	283	123	258	19	101	181	477	463	279
873	116	840	105	285	238	540	22	117	125	699	953	920	106	113	259
3695	161	186	2188	3611	2802	157	2154	3394	145	2725	1327	3741	2493	3607	4041
140	1401	110	119	112	1586	125	937	1469	1015	879	1798	122	1151	100	926
2401	191	219	607	267	2362	932	2283	889	2567	2171	2409	1078	2247	2441	245
928	1142	957	1155	922	1039	452	285	467	305	506	221	281	59	667	232
3882	1698	170	5796	2557	173	1228	4630	174	3508	5629	4395	180	5100	2814	2247
396	311	223	227	340	313	355	469	229	162	107	76	363	132	453	161
627	1331	1143	1572	966	388	198	2068	201	239	176	1805	1506	1890	1980	1887
3390	5336	1730	4072	5342	216	3823	85	5408	5774	247	5308	232	256	5214	787
176	1694	1787	1586	3798	4243	157	4224	3603	2121	3733	851	2493	4136	148	153
2432	4030	3397	4032	3952	2727	157	3284	3450	3229	4169	3471	4255	155	127	186
919	615	335	816	138	97	881	790	855	89	451	789	423	108	95	116";
                Day2(input);
            }
            else if (day == "3")
            {
                const int input = 325489;
                Day3(input);
            }

            Console.WriteLine("Program complete.");
            Console.ReadKey();
        }

        // ************* Day 1 *************
        private static void Day1(string input)
        {
            input = input.Trim();

            // Part 1
            {
                var sum = 0;
                var lastNumber = int.Parse(input.Last().ToString());
                foreach (var c in input)
                {
                    var currentNumber = int.Parse(c.ToString());
                    if (currentNumber == lastNumber)
                        sum += currentNumber;
                    lastNumber = currentNumber;
                }
                Console.WriteLine($"Sum for part 1: {sum}");
            }

            // Part 2
            {
                var sum = 0;
                var halfLength = input.Length / 2;
                for (var i = 0; i < input.Length; i++)
                {
                    var currentNumber = int.Parse(input[i].ToString());
                    var halfwayIndex = halfLength + i;
                    halfwayIndex -= halfwayIndex >= input.Length ? input.Length : 0;
                    var halfwayNumber = int.Parse(input[halfwayIndex].ToString());
                    if (currentNumber == halfwayNumber)
                        sum += currentNumber;
                }
                Console.WriteLine($"Sum for part 2: {sum}");
            }
        }

        // ************* Day 2 *************
        private static void Day2(string input)
        {
            var lines = input.Split(Environment.NewLine);
            var numbers = lines.Select(l => l.Split("	").Select(int.Parse).ToList()).ToList();
            Day2(numbers);
        }

        private static void Day2(IEnumerable<IEnumerable<int>> input)
        {
            var inputList = input.Select(i => i as IList<int> ?? i.ToList()).ToList();

            // Part 1
            {
                var sum = 0;
                foreach (var line in inputList)
                {
                    var largest = int.MinValue;
                    var smallest = int.MaxValue;
                    foreach (var number in line)
                    {
                        if (number > largest)
                            largest = number;
                        if (number < smallest)
                            smallest = number;
                    }
                    if (largest < smallest)
                        continue;
                    sum += largest - smallest;
                }
                Console.WriteLine($"Sum for part 1: {sum}");
            }

            // Part 2
            {
                var sum = 0;
                foreach (var line in inputList)
                {
                    var equalNumbers = 0;
                    for (var i = 0; i < line.Count; i++)
                    {
                        var n1 = line[i];
                        for (var j = 0; j < line.Count; j++)
                        {
                            var n2 = line[j];

                            if (i == j)
                                continue;
                            if (n1 < n2)
                                continue;

                            if (n2 == n1)
                            {
                                equalNumbers++;
                                if (equalNumbers % 2 == 0)
                                    sum += 1;
                                continue;
                            }

                            if (n1 % n2 == 0)
                                sum += n1 / n2;
                        }
                    }
                }
                Console.WriteLine($"Sum for part 2: {sum}");
            }
        }

        // ************* Day 3 *************
        private enum Direction
        {
            East,
            North,
            West,
            South
        }

        private class Coordinate
        {
            public int X { get; }
            public int Y { get; }

            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }

            public Coordinate(Coordinate coordinate, Direction direction)
            {
                X = coordinate.X;
                Y = coordinate.Y;
                switch (direction)
                {
                    case Direction.East:
                        X += 1;
                        break;
                    case Direction.North:
                        Y += 1;
                        break;
                    case Direction.West:
                        X -= 1;
                        break;
                    case Direction.South:
                        Y -= 1;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Direction is not a valid value.");
                }
            }

            public int GetRelevantXorY(Direction direction)
            {
                switch (direction)
                {
                    case Direction.East:
                    case Direction.West:
                        return X;
                    case Direction.North:
                    case Direction.South:
                        return Y;
                    default:
                        throw new IndexOutOfRangeException("Direction is not a valid value.");
                }
            }
        }

        private static void Day3(int input)
        {
            var currentLocation = new Coordinate(0, 0);
            var map = new Dictionary<int, Coordinate> { { 1, currentLocation } };
            var direction = Direction.East;
            var maxLevel = 0;

            // Make us our spiral.
            for (var i = 2; i <= input; i++)
            {
                // Move us and add this number to the map.
                currentLocation = new Coordinate(currentLocation, direction);
                map.Add(i, currentLocation);

                // Get the current level we are on.
                var currentLevel = Math.Abs(currentLocation.GetRelevantXorY(direction));

                // If we've hit the peak, change direction.
                if (currentLevel >= maxLevel)
                {
                    // Special case: East can move 1 past max. If we are currently 1 past max, increase max and change.
                    if (direction == Direction.East && currentLevel > maxLevel)
                    {
                        maxLevel++;
                        direction = Direction.North;
                        continue;
                    }

                    // If we reach this, we're at exactly the max and still moving East.
                    if (direction == Direction.East)
                        continue;

                    // Increment direction.
                    direction++;
                    if (!Enum.IsDefined(typeof(Direction), direction))
                        direction = Direction.East;
                }
            }

            var inputCoordinate = map[input];
            var distance = Math.Abs(inputCoordinate.X) + Math.Abs(inputCoordinate.Y);
            Console.WriteLine($"From {input} back to center takes {distance} moves.");
        }
    }
}
