namespace AdventOfCode
{
    public class Solver15 : ISolver
    {
        public Solver15()
        {
        }
        public async Task<string> Solve1(string input)
        {
            var map = GetMap(input);

            var result = AStarPathFinding(map);

            return result?.ToString() ?? "Cheapest route not found :(";
        }

        public static int[][] GetMap(string input)
        {
            var rows = input.GetRows().Select(r => r.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

            return rows;
        }

        public static int? AStarPathFinding(int[][] map)
        {
            var xSize = map[0].Length - 1;
            var ySize = map.Length - 1;

            var start = new Tile
            {
                Y = 0,
                X = 0,
                OwnCost = map[0][0]
            };

            var finish = new Tile
            {
                X = xSize,
                Y = ySize,
                OwnCost = map[ySize][xSize]
            };

            start.SetDistance(finish.X, finish.Y);

            var activeTiles = new PriorityQueue<Tile, int>();
            activeTiles.Enqueue(start, 0);

            var visitedTiles = new HashSet<(int, int)>();

            while (activeTiles.Count > 0)
            {
                var checkTile = activeTiles.Dequeue();

                if (checkTile.X == finish.X && checkTile.Y == finish.Y)
                {
                    // We found cheepest route :)
                    //Console.WriteLine(checkTile.Path);
                    return checkTile.TotalCost;
                }

                visitedTiles.Add(checkTile.Pos);

                var walkableTiles = GetWalkableTiles(map, checkTile, finish, xSize, ySize);

                foreach (var walkableTile in walkableTiles)
                {
                    //We have already visited this tile so we don't need to do so again!
                    if (visitedTiles.Contains(walkableTile.Pos))
                        continue;

                    //It's already in the active list, but that's OK, maybe this new tile has a better value (e.g. We might zigzag earlier but this is now straighter). 

                    var existingTile = activeTiles.UnorderedItems.Select(e => e.Element).FirstOrDefault(x => x.X == walkableTile.X && x.Y == walkableTile.Y);
                    if (existingTile != null)
                    {
                        if (existingTile.CostDistance > walkableTile.CostDistance)
                        {
                            activeTiles.Enqueue(walkableTile, walkableTile.CostDistance);
                        }
                    }
                    else
                    {
                        //We've never seen this tile before so add it to the list. 
                        activeTiles.Enqueue(walkableTile, walkableTile.CostDistance);
                    }
                }
            }

            return null;
        }

        public static int[][] EnlargeMap(int[][] map)
        {
            var largerMap = new int[map.Length * 5][];

            for (var i = 0; i < largerMap.Length; i++)
            {
                largerMap[i] = new int[map.Length * 5];
            }

            foreach (var sx in Enumerable.Range(0, 5))
            {
                foreach (var sy in Enumerable.Range(0, 5))
                {
                    for (var i = 0; i < map.Length; i++)
                    {
                        for (var j = 0; j < map.Length; j++)
                        {
                            var x = sx * map.Length + i;
                            var y = sy * map.Length + j;
                            var newVal = map[i][j] + sx + sy;
                            largerMap[x][y] = newVal > 9 ? newVal - 9 : newVal;
                        }
                    }
                }
            }

            return largerMap;
        }

        private static List<Tile> GetWalkableTiles(int[][] map, Tile currentTile, Tile targetTile, int xSize, int ySize)
        {
            var directions = new List<Tile>()
            {
                new Tile { X = currentTile.X, Y = currentTile.Y - 1, Parent = currentTile },
                new Tile { X = currentTile.X, Y = currentTile.Y + 1, Parent = currentTile },
                new Tile { X = currentTile.X - 1, Y = currentTile.Y, Parent = currentTile },
                new Tile { X = currentTile.X + 1, Y = currentTile.Y, Parent = currentTile },
            };

            var possibleTiles = directions
                    .Where(tile => tile.X >= 0 && tile.X <= xSize)
                    .Where(tile => tile.Y >= 0 && tile.Y <= ySize)
                    .ToList();

            foreach (var tile in possibleTiles)
            {
                tile.SetDistance(targetTile.X, targetTile.Y);
                tile.OwnCost = map[tile.X][tile.Y];
                tile.Cost = currentTile.Cost + tile.OwnCost;
            }

            return possibleTiles;
        }


        public async Task<string> Solve2(string input)
        {
            var map = GetMap(input);

            map = EnlargeMap(map);

            var result = AStarPathFinding(map);

            return result?.ToString() ?? "Cheapest route not found :(";
        }
    }

    class Tile
    {
        public (int x, int y) Pos => (X, Y);
        public int OwnCost { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Cost { get; set; }
        public int Distance { get; set; }
        public int CostDistance => Cost + Distance;
        public Tile Parent { get; set; }
        public void SetDistance(int targetX, int targetY)
        {
            this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
        }

        public int TotalCost => OwnCost + Parent?.TotalCost ?? 0;

        public string Path => Parent?.Path + OwnCost;
    }
}
