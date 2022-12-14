namespace AdventOfCode
{
    class RopeGrid 
    {
        public int Cols { get; set; } = 2000;
        public List<List<string>> Grid { get; set; }
        public KeyValuePair<int,int> TailCoordinates { get; set; }
        public KeyValuePair<int,int> HeadCoordinates { get; set; }
        public KeyValuePair<int,int> PreviousHeadCoordinates { get; set; }
        public KeyValuePair<int,int> PreviousTailCoordinates { get; set; }
        public KeyValuePair<int,int> Distance { get; set; }
        public List<List<int>> Visited { get; set; }

        public void CalcHeadRight () {
            int newY = HeadCoordinates.Value;
            newY++;
            HeadCoordinates = new KeyValuePair<int, int> (HeadCoordinates.Key, newY);   
        }
        public void CalcHeadLeft () {
            int newY = HeadCoordinates.Value;
            newY--;
            HeadCoordinates = new KeyValuePair<int, int> (HeadCoordinates.Key, newY);
        }
        public void CalcHeadUp () {
            int newX = HeadCoordinates.Key;
            newX++;
            HeadCoordinates = new KeyValuePair<int, int> (newX, HeadCoordinates.Value);
        }

        public void CalcHeadDown () {
            int newX = HeadCoordinates.Key;
            newX--;
            HeadCoordinates = new KeyValuePair<int, int> (newX, HeadCoordinates.Value);
        }

         public void CalcTail () {
            Distance = new KeyValuePair<int, int>(HeadCoordinates.Key - TailCoordinates.Key, HeadCoordinates.Value - TailCoordinates.Value);
            System.Console.WriteLine("Distance: {0}, {1}. TailCoords: {2}, {3}", Distance.Key, Distance.Value, TailCoordinates.Key, TailCoordinates.Value);
            // left 
            if (Distance.Key == 0 && Distance.Value < -1) {
                int newY = TailCoordinates.Value;
                newY--;
                TailCoordinates = new KeyValuePair<int, int> (TailCoordinates.Key, newY);
            }
            // RIGHT 
             else if (Distance.Key == 0 && Distance.Value > 1) {
                int newY = TailCoordinates.Value;
                newY++;
                TailCoordinates = new KeyValuePair<int, int> (TailCoordinates.Key, newY);
            }
            // up
            else if (Distance.Key > 1 && Distance.Value == 0) {
                int newX = TailCoordinates.Key;
                newX++;
                TailCoordinates = new KeyValuePair<int, int> (newX, TailCoordinates.Value);
            } 
            // down
            else if (Distance.Key < -1 && Distance.Value == 0) {
                int newX = TailCoordinates.Key;
                newX--;
                TailCoordinates = new KeyValuePair<int, int> (newX, TailCoordinates.Value);
            }
            // 1,2 good
            // 2,1 good
            // 2,2 good
            else if ((Distance.Key == 1 && Distance.Value > 1)
                    || (Distance.Key > 1 && Distance.Value == 1)
                    || (Distance.Key > 1 && Distance.Value > 1)) {
                int newX = TailCoordinates.Key;
                int newY = TailCoordinates.Value;
                newX++;
                newY++;
                TailCoordinates = new KeyValuePair<int, int> (newX, newY);
            }
            // 1,-2 good
            // 2,-1 good
            // 2,-2
            else if ((Distance.Key == 1 && Distance.Value < -1)
                    || (Distance.Key > 1 && Distance.Value == -1)
                    || (Distance.Key > 1 && Distance.Value < -1)) {
                int newX = TailCoordinates.Key;
                int newY = TailCoordinates.Value;
                newX++;
                newY--;
                TailCoordinates = new KeyValuePair<int, int> (newX, newY);
            }
            // -1,2 good
            // -2,1 good
            // -2,2
            else if ((Distance.Key == -1 && Distance.Value > 1)
                    || (Distance.Key < -1 && Distance.Value == 1)
                    || (Distance.Key < -1 && Distance.Value > 1)) {
                int newX = TailCoordinates.Key;
                int newY = TailCoordinates.Value;
                newX--;
                newY++;
                TailCoordinates = new KeyValuePair<int, int> (newX, newY);
            }
            // -1,-2 good
            // -2,-1 good
            // -2,-2
            else if ((Distance.Key == -1 && Distance.Value < -1)
                    || (Distance.Key < -1 && Distance.Value == -1)
                    || (Distance.Key < -1 && Distance.Value < -1)) {
                int newX = TailCoordinates.Key;
                int newY = TailCoordinates.Value;
                newX--;
                newY--;
                TailCoordinates = new KeyValuePair<int, int> (newX, newY);
            }
            CheckVisited();
            System.Console.WriteLine("New tail: {0}, {1}", TailCoordinates.Key, TailCoordinates.Value);
        } 

         public void CheckVisitedv2() {
            if (!Visited[HeadCoordinates.Key].Contains(HeadCoordinates.Value)) {
                Visited[HeadCoordinates.Key].Add(HeadCoordinates.Value);
            }
        }

        public void CheckVisited() {
            if (!Visited[TailCoordinates.Key].Contains(TailCoordinates.Value)) {
                Visited[TailCoordinates.Key].Add(TailCoordinates.Value);
            }
        }

        public void PrintGrid() {
            Grid = new List<List<string>>();
            // setup grid
            for (int e = 0; e < Cols; e++) {
                Grid.Add(new List<string>());
                for (int a = 0; a < Cols; a++) {
                    Grid[e].Add(".");
                }
            }
            int offset = Cols - 1;
            int tXOutput = offset - TailCoordinates.Key;
            int hXOutput = offset - HeadCoordinates.Key;
            Grid[tXOutput][TailCoordinates.Value] = "T";
            Grid[hXOutput][HeadCoordinates.Value] = "H";

            // setup grid and print inital output
            string output = "";
            for (int d = 0; d < Cols; d++) {
                for (int a = 0; a < Cols; a++) {
                    output += Grid[d][a];
                }
                output += "\n";
            }
            System.Console.WriteLine(output);
        }

        public static void PrintGridv2(List<RopeGrid> ropeGrids) {
            var headGrid = new RopeGrid();
            headGrid.HeadCoordinates = ropeGrids[0].HeadCoordinates;
            headGrid.TailCoordinates = ropeGrids[0].TailCoordinates;
            headGrid.Grid = new List<List<string>>();
            var printGrid = headGrid.Grid;
            var printCols = headGrid.Cols;
            // setup grid
            for (int e = 0; e < printCols; e++) {
                printGrid.Add(new List<string>());
                for (int a = 0; a < printCols; a++) {
                    printGrid[e].Add(".");
                }
            }

            int offset = printCols - 1;
            int hXOutput = 0;
            //printGrid[hXOutput][headGrid.HeadCoordinates.Value] = "H";
            for (int a = ropeGrids.Count - 1; a >= 1; a--) {
                int tXOutput = offset - ropeGrids[a].TailCoordinates.Key;
                hXOutput = offset - ropeGrids[a].HeadCoordinates.Key;
                // print tail
                // print head
                printGrid[hXOutput][ropeGrids[a].HeadCoordinates.Value] = a.ToString();
            }
            hXOutput = offset - headGrid.HeadCoordinates.Key;
            printGrid[hXOutput][headGrid.HeadCoordinates.Value] = "H";

            // setup grid and print inital output
            string output = "";
            for (int d = 0; d < printCols; d++) {
                for (int a = 0; a < printCols; a++) {
                    output += printGrid[d][a];
                }
                output += "\n";
            }
            System.Console.WriteLine(output);
        }
        
    }
}