using System;
using System.Collections.Generic; 
using System.Linq;

namespace AdventOfCode
{
  class Program
  {
    public static int edge = 392;
    public static int rightOffset = 2;
    public static int leftOffset = 1;
    public static string result = "";

    static void Main(string[] args)
    {
        Day9();
	}

    public static void Day92() {
        List<string> input = GetInput(9).Split("\n").ToList();
        int counter = 0;
        List<RopeGrid> ropeGrids = new List<RopeGrid>();
        // create a ropeGrid for each of 10 knots
        // 0 is head, 9 is tail
        for (int b = 0; b < 10; b++) {
            RopeGrid ropeGrid = new RopeGrid();
            ropeGrid.Grid = new List<List<string>>();
            ropeGrid.TailCoordinates = new KeyValuePair<int, int>(500,500);
            ropeGrid.HeadCoordinates = new KeyValuePair<int, int>(500,500);
            ropeGrid.PreviousTailCoordinates = new KeyValuePair<int, int>(500,500);
            ropeGrid.PreviousHeadCoordinates = new KeyValuePair<int, int>(500,500);
            ropeGrid.Visited = new List<List<int>>();
            ropeGrids.Add(ropeGrid);
            // setup visited dictionary
            for (int i = 0; i < ropeGrid.Cols; i++) {
                ropeGrid.Visited.Add(new List<int>());
            }
            // add starting position to visited
            ropeGrid.Visited[ropeGrid.TailCoordinates.Key].Add(ropeGrid.TailCoordinates.Value);
        }
      
        for (int i = 0; i < input.Count; i++) {
            string move = input[i];
            System.Console.WriteLine("~~~ {0} ~~~",move);
            int num = int.Parse(move.Remove(0,2));
            move = move.Remove(1,move.Length-1);
            for (int a = 0; a < num; a++) {
                System.Console.WriteLine("~~Move #{0}~~",a);
                for (int b = 0; b < ropeGrids.Count; b++) {
                    ropeGrids[b].PreviousHeadCoordinates = new KeyValuePair<int, int>(ropeGrids[b].HeadCoordinates.Key, ropeGrids[b].HeadCoordinates.Value);
                    ropeGrids[b].PreviousTailCoordinates = new KeyValuePair<int, int>(ropeGrids[b].TailCoordinates.Key, ropeGrids[b].TailCoordinates.Value);
                    System.Console.WriteLine("Tail #: {0} TailCoordinates: {3},{4}. Previous HeadCoordinates: {1},{2}. Previous TailCoordinates: {5},{6}",b,ropeGrids[b].PreviousHeadCoordinates.Key,
                    ropeGrids[b].PreviousHeadCoordinates.Value,ropeGrids[b].PreviousTailCoordinates.Key,ropeGrids[b].PreviousTailCoordinates.Value,ropeGrids[b].PreviousTailCoordinates.Key,ropeGrids[b].PreviousTailCoordinates.Value);
                    if (b == 0) {
                        if (move == "R") {
                        ropeGrids[b].CalcHeadRight();
                        }
                        else if (move == "D") {
                            ropeGrids[b].CalcHeadDown();
                        }
                        else if (move == "L") {
                            ropeGrids[b].CalcHeadLeft();
                        }
                        else {
                            ropeGrids[b].CalcHeadUp();
                        }
                        System.Console.WriteLine("Head #: {0} New HeadCoordinates: {1},{2}",b,ropeGrids[b].HeadCoordinates.Key,ropeGrids[b].HeadCoordinates.Value);
                        ropeGrids[b].CalcTail();
                    }
                    
                    else {
                        // check to see if previous tail changed
                        if ((ropeGrids[b-1].PreviousTailCoordinates.Key != ropeGrids[b-1].TailCoordinates.Key) 
                            || (ropeGrids[b-1].PreviousTailCoordinates.Value != ropeGrids[b-1].TailCoordinates.Value)) {
                            ropeGrids[b].HeadCoordinates = new KeyValuePair<int, int>(ropeGrids[b-1].TailCoordinates.Key, ropeGrids[b-1].TailCoordinates.Value);
                            System.Console.WriteLine("Tail #: {0} New HeadCoordinates {1},{2}",b,ropeGrids[b].HeadCoordinates.Key,ropeGrids[b].HeadCoordinates.Value);
                        }
                        else {
                            break;
                        }
                        ropeGrids[b].CalcTail();
                    }
                }
               //RopeGrid.PrintGridv2(ropeGrids);
            }
        }
         foreach (var a in ropeGrids[9].Visited) {
            foreach (var b in a) {
                counter++;
            }
        }
        System.Console.WriteLine(counter); 
    }

 public static void Day9() {
        List<string> input = GetInput(9).Split("\n").ToList();
        RopeGrid ropeGrid = new RopeGrid();
        ropeGrid.Grid = new List<List<string>>();
        ropeGrid.TailCoordinates = new KeyValuePair<int, int>(500,500);
        ropeGrid.HeadCoordinates = new KeyValuePair<int, int>(500,500);
        ropeGrid.Visited = new List<List<int>>();
        int counter = 0;

        // setup visited dictionary
         for (int i = 0; i < ropeGrid.Cols; i++) {
            ropeGrid.Visited.Add(new List<int>());
        }
        ropeGrid.Visited[ropeGrid.TailCoordinates.Key].Add(ropeGrid.TailCoordinates.Value);

        for (int i = 0; i < input.Count; i++) {
            string move = input[i];
            System.Console.WriteLine("~~~ {0} ~~~",move);
            int num = int.Parse(move.Remove(0,2));
            move = move.Remove(1,move.Length-1);
            System.Console.WriteLine("move: {0}. num: {1}",move,num);
            for (int a = 0; a < num; a++) {
                ropeGrid.PreviousHeadCoordinates = new KeyValuePair<int, int>(ropeGrid.HeadCoordinates.Key, ropeGrid.HeadCoordinates.Value);
                if (move == "R") {
                    ropeGrid.CalcHeadRight();
                }
                else if (move == "D") {
                    ropeGrid.CalcHeadDown();
                }
                else if (move == "L") {
                    ropeGrid.CalcHeadLeft();
                }
                else {
                    ropeGrid.CalcHeadUp();
                }
                ropeGrid.CalcTail();
                //ropeGrid.PrintGrid();
            }
        }

        foreach (var a in ropeGrid.Visited) {
            foreach (var b in a) {
                counter++;
            }
        }
        System.Console.WriteLine(counter);
    }
     public static int Day82 () {
        var input = GetInput(8).Split("\n").ToList();
        List<List<int>> rows = SetupGrid(input);
        List<List<int>> cols = SetupColumns(rows);
        List<List<int>> innerGrid = SetUpInnerGrid(rows);
        Grid grid = new Grid(innerGrid,rows,cols);
        return CalcScenicScore(grid);
    }

      public static int CalcScenicScore (Grid grid) {
        int currentHighest = 0;
        int topScore = 0;
        int downScore = 0;
        int rightScore = 0;
        int leftScore = 0;
        int currentScore = 0;
        for (int i = 0; i < grid.innerGrid.Count; i++) {
            List<int> row = grid.innerGrid[i];
            // debug
            result = string.Join(",", row);
            System.Console.WriteLine("Row: " + result);
            for (int a = 0; a < row.Count; a++) {
                int currentNum = row[a];
                System.Console.WriteLine("~~~~~ CurrentNum: " + currentNum + "~~~~~");
                // Compare up
                System.Console.WriteLine("Checking top");
                topScore = TreesSeenTopOrLeft(grid.cols[a+1], currentNum, i);
                System.Console.WriteLine("Counter top: " + topScore);
                // Compare down
                System.Console.WriteLine("Checking bottom");
                downScore = TreesSeenBottomOrRight(grid.cols[a+1], currentNum, i);
                // Compare right
                System.Console.WriteLine("Checking right");
                rightScore = TreesSeenBottomOrRight(grid.fullGrid[i+1], currentNum, a);
                // Compare left
                System.Console.WriteLine("Checking left");
                leftScore = TreesSeenTopOrLeft(grid.fullGrid[i+1], currentNum, a);
                System.Console.WriteLine("Counter left: " + leftScore);
                currentScore = topScore * downScore * rightScore * leftScore;
                if (currentScore > currentHighest) {
                    currentHighest = currentScore;
                }
                topScore = 0;
                downScore = 0;
                rightScore = 0;
                leftScore = 0;
                currentScore = 0;
            }
        }
        return currentHighest;
    }
     public static int TreesSeenTopOrLeft(List<int> row, int currentNum, int currentIndex) {
        int counter = 0;
        List<int> restOfRow = new List<int> (row);
        result = string.Join(",", restOfRow);
        System.Console.WriteLine("restOfRow pre: " + result);
        int startIndex = currentIndex + leftOffset;
        System.Console.WriteLine("startIndex: " + startIndex);
        int numToRemove = restOfRow.Count - startIndex;
        System.Console.WriteLine("numToRemove: " + numToRemove);
        restOfRow.RemoveRange(startIndex, numToRemove);
        result = string.Join(",", restOfRow);
        System.Console.WriteLine("restOfRow post: " + result);
        for (int b = restOfRow.Count-1; b >= 0; b--) {
            int compareNum = restOfRow[b];
            System.Console.WriteLine("compareNum " + compareNum);
            counter++;
            if (currentNum > compareNum) {
                continue;
            }
            if (currentNum <= compareNum) {
                break;
            }
        }
        return counter;
    }
 
    public static int TreesSeenBottomOrRight(List<int> row, int currentNum, int currentIndex) {
            int counter = 0;
            List<int> restOfRow = new List<int> (row);
            restOfRow.RemoveRange(0, currentIndex + rightOffset);
            for (int b = 0; b < restOfRow.Count; b++) {
                int compareNum = restOfRow[b];
                counter++;
                if (currentNum > compareNum) {
                    continue;
                }
                if (currentNum <= compareNum) {
                    break;
                }
            }
            return counter;
        }

    public static void Day8 () {
        var input = GetInput(8).Split("\n").ToList();
        List<List<int>> rows = SetupGrid(input);
        foreach (var row in rows) {
            result = string.Join(",", row);
            System.Console.WriteLine(result);
        }
        List<List<int>> cols = SetupColumns(rows);
        foreach (var row in cols) {
            result = string.Join(",", row);
            System.Console.WriteLine(result);
        }
        List<List<int>> innerGrid = SetUpInnerGrid(rows);
        System.Console.WriteLine("\n");
         foreach (var row in innerGrid) {
            result = string.Join(",", row);
            System.Console.WriteLine(result);
        }
        Grid grid = new Grid(innerGrid,rows,cols);
        int answer = CheckForVisibles(grid);
        System.Console.WriteLine("counter: " + answer);
        answer += edge;
        System.Console.WriteLine(answer);
    }

    public static int CheckForVisibles (Grid grid) {
        int counter = 0;
        bool visible = false;
        for (int i = 0; i < grid.innerGrid.Count; i++) {
            List<int> row = grid.innerGrid[i];
            // debug
            result = string.Join(",", row);
            System.Console.WriteLine("Row: " + result);
            for (int a = 0; a < row.Count; a++) {
                if (visible) {
                    counter++;
                }
                visible = false;
                int currentNum = row[a];
                System.Console.WriteLine("~~~~~ CurrentNum: " + currentNum + "~~~~~");
                // Compare up
                System.Console.WriteLine("Checking top");
                visible = IsVisibileFromLeftOrTop(grid.cols[a+1], currentNum, i);
                System.Console.WriteLine(visible);
                if (visible) {
                    System.Console.WriteLine("Visible from top");
                    continue;
                }
                // Compare down
                System.Console.WriteLine("Checking bottom");
                visible = IsVisibileFromRightOrBottom(grid.cols[a+1], currentNum, i);
                System.Console.WriteLine(visible);
                if (visible) {
                    System.Console.WriteLine("Visible from bottom");
                    continue;
                }
                // Compare right
                System.Console.WriteLine("Checking right");
                visible = IsVisibileFromRightOrBottom(grid.fullGrid[i+1], currentNum, a);
                System.Console.WriteLine(visible);
                if (visible) {
                    System.Console.WriteLine("Visible from right");
                    continue;
                }
                // Compare left
                System.Console.WriteLine("Checking left");
                visible = IsVisibileFromLeftOrTop(grid.fullGrid[i+1], currentNum, a);
                System.Console.WriteLine(visible);
                if (visible) {
                    System.Console.WriteLine("Visible from left");
                    continue;
                }
            }
        }
        if (visible) {
            counter++;
        }
        return counter;
    }
    public static bool IsVisibileFromLeftOrTop(List<int> row, int currentNum, int currentIndex) {
        List<int> restOfRow = new List<int> (row);
        int startIndex = currentIndex + leftOffset;
        int numToRemove = restOfRow.Count - startIndex;
        restOfRow.RemoveRange(startIndex, numToRemove);
        for (int b = restOfRow.Count-1; b >= 0; b--) {
            int compareNum = restOfRow[b];
            if (currentNum > compareNum) {
                continue;
            }
            if (currentNum <= compareNum) {
                return false;
            }
        }
        return true;
    }
    public static bool IsVisibileFromRightOrBottom(List<int> row, int currentNum, int currentIndex) {
        List<int> restOfRow = new List<int> (row);
        restOfRow.RemoveRange(0, currentIndex + rightOffset);
        for (int b = 0; b < restOfRow.Count; b++) {
            int compareNum = restOfRow[b];
            if (currentNum > compareNum) {
                continue;
            }
            if (currentNum <= compareNum) {
                return false;
            }
        }
        return true;
    }

    public static List<List<int>> SetUpInnerGrid(List<List<int>> rows) {
        List<List<int>> innerGrid = new List<List<int>>();
        for (int i = 0; i < rows.Count; i++) {
            // check if first or last row
            if (i == 0 || i == rows.Count-1) {
                continue;
            }
            List<int> row = rows[i];
            List<int> innerGridRow = new List<int>();
            for (int a = 0; a < row.Count; a++) {
                // check if first or last num
                if (a == 0 || a == row.Count-1) {
                    continue;
                }
                innerGridRow.Add(row[a]);
            }
            innerGrid.Add(innerGridRow);
        }
        return innerGrid;
    }

    public static List<List<int>> SetupGrid (List<string> input) {
        List<List<int>> rows = new List<List<int>>();
         // setup grid
        for (int i = 0; i < input.Count; i++) {
            string inputRow = input[i];
            List<int> treeRow = new List<int>();
            // add each height to current treeRow
            for (int a = 0; a < inputRow.Length; a++) {
                int currentHeight = int.Parse(inputRow[a].ToString());
                treeRow.Add(currentHeight);
            }
            rows.Add(treeRow);
        }
        return rows;
    }

    public static List<List<int>> SetupColumns (List<List<int>> rows) {
        List<List<int>> cols = new List<List<int>>();
        var row = rows[0];
        for (int i = 0; i < row.Count; i++) {
            // for each num in row
            List<int> treeCol = new List<int>();
            // roll through grid rows building columns
            for (int a = 0; a < rows.Count; a++) {
                List<int> currentRow = rows[a];
                for (int b = 0; b < currentRow.Count; b++) {
                    // 3
                    if (i == b) {
                        treeCol.Add(currentRow[b]);
                    }
                    else if (b < i) {
                        continue;
                    }
                    else if (b > i) {
                        break;
                    }
                }
            }
            cols.Add(treeCol);
        }
        return cols;
    }
    public static void Day7(int day) {
        System.Console.WriteLine("\n\n");
        var inputList = GetInput(day).Split("\n").ToList();
        inputList = Cleanup(inputList);
        inputList.RemoveAt(0);
        string changeDirectory = "$ cd";
        string list = "$ ls";
        string directory = "dir ";
        double fileSize = 0;
        double runningSum = 0;
        string currentDir = "/";
        string lastDir = "/";
        List<string> history = new List<string>();  
        history.Add("/");
        List<string> parentOrder = new List<string>();  
        parentOrder.Add("/");
        List<string> specialOrder = new List<string>();  
        specialOrder.Add("/");
        Dictionary<string,Dictionary<string,double>> fileSystem = new Dictionary<string, Dictionary<string, double>>();
        fileSystem.Add("/",new Dictionary<string, double>());
        fileSystem["/"].Add("fileSum",0);
        string currentLine = "";
        string currentOrder = "";

        // loop through each line
        for (int i = 0; i < inputList.Count; i++) {
            currentLine = inputList[i];
            string firstFour = currentLine.Substring(0,4);
            string restOfLine = currentLine.Substring(4,currentLine.Length-4);
            System.Console.WriteLine("\nCurrent line: " + currentLine);    

            if (firstFour == changeDirectory) {
                System.Console.WriteLine("*************cd hit************");
                // set lastDir to last directory we came from, regardless of .. or x
                lastDir = history[history.Count-1];
                // check fileSystem for parent key
                if (!fileSystem.ContainsKey(currentDir)) {
                    // key doesnt exist, add new parent record
                    fileSystem.Add(currentDir,new Dictionary<string, double>());
                    fileSystem[currentDir].Add("fileSum",0);
                }   
                if (runningSum != 0) {         
                     // add runningSum to the fileSum of the lastDir we just came from    
                    System.Console.WriteLine("Adding: {0} to fileSum on dir: {1}",runningSum,currentDir);        
                    fileSystem[currentDir]["fileSum"] += runningSum;
                }
                runningSum = 0;

                //////// cd ..          
                if (restOfLine == " ..") {
                    currentDir = parentOrder[parentOrder.Count-2];
                    parentOrder.RemoveAt(parentOrder.Count-1);
                }
                //////// cd dir 
                else {
                    currentDir = restOfLine.Substring(1,restOfLine.Length-1);
                    specialOrder.Add(currentDir);
                    // add child record
                    if (!fileSystem[lastDir].ContainsKey(currentDir)) {
                        System.Console.WriteLine("Adding child record: {0} to parent: {1}",currentDir,lastDir);
                        fileSystem[lastDir].Add(currentDir,0);
                    }          
                    parentOrder.Add(currentDir);
                }   
                // set current
                history.Add(currentDir);
                System.Console.WriteLine("LastDir "+lastDir);
                System.Console.WriteLine("CurrentDir "+currentDir);
         
                currentOrder = "";
                    foreach (string a in parentOrder) {
                        currentOrder += a + ",";
                }
                System.Console.WriteLine("New parentOrder: "+currentOrder);
                
            }
                // file size num
                else if (double.TryParse(firstFour[0].ToString(),out fileSize)) {
                    string[] size = currentLine.Split(' ');
                    fileSize = double.Parse(size[0]);
                    // add fileSize to runningSum
                    runningSum += fileSize;
                    System.Console.WriteLine("Found file. runningSum is: " + runningSum);
                }
            
        }
        
        System.Console.WriteLine("Final lastDir: {0}. currentDir: {1}. runningSum: {2}",lastDir,currentDir,runningSum);
        // final row
        if (runningSum != 0) {
            lastDir = currentDir;
             // check if parent records have the currentDir (before change)
            if (!fileSystem.ContainsKey(lastDir)) {
                // key doesnt exist, add new parent record
                fileSystem.Add(lastDir,new Dictionary<string, double>());
                fileSystem[lastDir].Add("fileSum",0);
            }
            fileSystem[lastDir]["fileSum"] = runningSum;
        }
        
        runningSum = 0;
        List <double> totals = new List<double>();      
        double final = 0;
        double rn = 0;
        for (int i = specialOrder.Count-1; i >=0; i--) {
            string directoryKey = specialOrder[i];
            System.Console.WriteLine("directoryKey: {0}", directoryKey);
            foreach (var line in fileSystem[directoryKey]) {
                 if (line.Key == "fileSum") {
                    rn += line.Value;
                } 
                else {
                    double childFileSum = fileSystem[line.Key]["fileSum"];
                    rn += childFileSum;
                    fileSystem[directoryKey]["fileSum"] = rn;
                }
            }
            totals.Add(rn);
            rn = 0;
        }

        foreach (var t in totals) {
            if (t <= 100000) {
               // System.Console.WriteLine(t);
                final += t;
            }
        } 
        System.Console.WriteLine("Final: " + final);
        var fileSystemSize = 70000000;
        var outerMostDir = totals[totals.Count-1];
        var sizeOfUpdate = 30000000;
        var lackingCapacity = fileSystemSize - outerMostDir;
        var neededRoom = sizeOfUpdate - lackingCapacity;
        System.Console.WriteLine("Total drive space: {0}. Outer most drive: {1}. Size of update: {2}. Lacking capacity: {3}. Needed room: {4}",
            fileSystemSize,outerMostDir,sizeOfUpdate,lackingCapacity,neededRoom);
        var deletes = new List<double>();
        foreach (var tot in totals) {
            if (tot >= neededRoom) {
                deletes.Add(tot);
            }
        }
        deletes.Sort();
        deletes.Reverse();
        foreach (var d in deletes){
            System.Console.WriteLine(d);
        }
    } 
      
    public static List<string> Cleanup (List<string> inputList) {
        for (int i = 0; i < inputList.Count; i++) {
            string currentLine = inputList[i];
            string firstFour = currentLine.Substring(0,4);
            if (firstFour == "$ cd" && !currentLine.Contains("..") ) {
                var result = inputList.Where(x => x.Contains(currentLine));
                if (result.Count() > 1) {
                    System.Console.WriteLine(inputList[i]);
                    inputList[i] = "$ cd " + RandomString(8);
                    System.Console.WriteLine(inputList[i]);
                } 
            }
        }
        System.Console.WriteLine("cat cleanup don");
         for (int i = 0; i < inputList.Count; i++) {
            string currentLine = inputList[i];
            string firstFour = currentLine.Substring(0,4);
            if (firstFour == "$ cd" && !currentLine.Contains("..") ) {
                var result = inputList.Where(x => x.Contains(currentLine));
                if (result.Count() > 1) {
                                        System.Console.WriteLine(inputList[i]);
                    inputList[i] = "$ cd " + RandomString(8);
                    System.Console.WriteLine(inputList[i]);
                } 
            }
        }
        return inputList;
    }
   private static Random random = new Random();
    public static string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public static int Day6Part2() {
        string input = GetInput(6);
        string fourteenLast = "";
        List <string> lastFourteen = new List<string>();
        bool unique = false;
        for (int i = 0; i < input.Length; i++) {
            // add current letter to lastFour list
            lastFourteen.Add(input[i].ToString());
            fourteenLast = "";
            foreach (string letter in lastFourteen) {
                fourteenLast += letter;
            }
            System.Console.WriteLine("lastFour is: "+fourteenLast);
            for (int a = 0; a < fourteenLast.Length; a++) {
                string otherThirteen = fourteenLast;
                otherThirteen = otherThirteen.Remove(a,1);
                if (otherThirteen.Contains(fourteenLast[a])) {
                    unique = false;
                    break;
                }
                else if (lastFourteen.Count >= 14) {
                    unique = true;
                }         
            }
            if (unique) {
                return i+1;
            }
            if (lastFourteen.Count >= 14) {
                lastFourteen.RemoveAt(0);
            }
        }
        return 0;
    }
    public static int Day6() {
        string input = GetInput(6);
        string fourLast = "";
        List <string> lastFour = new List<string>();
        bool unique = false;
        // loop thru input string
        for (int i = 0; i < input.Length; i++) {
            // add current letter to lastFour list
            lastFour.Add(input[i].ToString());
            fourLast = "";
            foreach (string letter in lastFour) {
                fourLast += letter;
            }
            // check lastFour for uniqueness
            for (int a = 0; a < fourLast.Length; a++) {
                string otherThree = fourLast;
                otherThree = otherThree.Remove(a,1);
                // if lastFour not unique, break out of check
                if (otherThree.Contains(fourLast[a])) {
                    unique = false;
                    break;
                }
                // lastFour is unique
                else if (lastFour.Count >= 4) {
                    unique = true;
                }
               
            }
            if (unique) {
                return i+1;
            }

           // remove first letter in lastFour
            if (lastFour.Count >= 4) {
                lastFour.RemoveAt(0);
            }
        }
        return 0;
    }

    public static void Day5() {
        Dictionary<int,List<string>> guide = new Dictionary<int, List<string>>();
        Dictionary<int,int> numIndexes = new Dictionary<int, int>();
        List<string> instructions =  new List<string>();

        SetupDay5(out guide,out numIndexes,out instructions);

        // part 1
        for (int i = 0; i < instructions.Count; i++) {
            string [] numsInstructions = instructions[i].Split(',');
            int numToMove = int.Parse(numsInstructions[0]);
            int from = int.Parse(numsInstructions[1]);
            int to = int.Parse(numsInstructions[2]);
            // iterate for each numToMove
            for (int a = 0; a < numToMove; a++) {
                int fromColLength = guide[from].Count;
                string letterToMove = guide[from][fromColLength-1];
                // remove the last letter in from col
                guide[from].RemoveAt(fromColLength-1);
                // add letter to the to col
                guide[to].Add(letterToMove);
            }
        }

        // get output for part 1
        string output = "";
        System.Console.WriteLine("---Output 1---");
        foreach (var thing in guide){
            output += thing.Value.Last();
        }
        System.Console.WriteLine(output);

        SetupDay5(out guide,out numIndexes,out instructions);

        // part 2
         for (int i = 0; i < instructions.Count; i++) {
            string [] numsInstructions = instructions[i].Split(',');
            int numToMove = int.Parse(numsInstructions[0]);
            int from = int.Parse(numsInstructions[1]);
            int to = int.Parse(numsInstructions[2]);
            int fromIndexToRemoveAt = guide[from].Count-numToMove;
            int fromIndexToEndRemoveAt = guide[from].Count-1;
            // remove the last letters in From col
            var lettersToAdd = new List<string>();
            for (int a = fromIndexToRemoveAt; a < guide[from].Count; a++) {
                // add letter to the To col
                guide[to].Add(guide[from][a]);
            }
            guide[from].RemoveRange(fromIndexToRemoveAt,numToMove);
        }

        System.Console.WriteLine("---Output 2---");
        output = "";
        foreach (var col in guide) {
            output += col.Value.Last();
        }
        System.Console.WriteLine(output);
    
    }
    
    public static void SetupDay5(out Dictionary<int,List<string>> guide, out Dictionary<int,int> numIndexes, out List<string> instructions) {
        string[] input = GetInput(5).Split('\n');
        guide = new Dictionary<int, List<string>>();
        numIndexes = new Dictionary<int, int>();
        instructions = new List<string>();
        int colNumIndex = 0;
        string numLine = "";
        int counter = 1;

        // find number row and number assignments in instructions
        for (int x = 0; x < input.Length; x++) {
            string line = input[x];
            for (int y = 0; y < line.Length; y++) {
                if (!(int.TryParse(line[y+1].ToString(),out colNumIndex))) {
                    // check if instruction line
                    if (line[y]=='m') {    
                        string [] lineItems = line.Split(' ');
                        string instructionToAdd = "";
                        // iterate through items in lineItems to find numbers
                        for (int i = 0; i < lineItems.Length; i++) {
                            // add assignments to instructions
                            if ((int.TryParse(lineItems[i].ToString(),out colNumIndex))) {
                                instructionToAdd += colNumIndex.ToString();
                                if (i != lineItems.Length-1) {
                                    instructionToAdd += ",";  
                                }   
                            }                         
                        }
                        instructions.Add(instructionToAdd);
                    }
                    break;
                }
                numLine = line;
            }
        }   

        // setup numIndex dictionary to track where letters go
        for (int x = 0; x < numLine.Length; x++) {
            if (!(int.TryParse(numLine[x].ToString(),out colNumIndex))) {
                continue;
            }     
            numIndexes.Add(x,counter);
            if (!guide.ContainsKey(counter)){
                guide.Add(counter,new List<string>());
            }       
            counter++;
        }      

        // setup guide
        for (int i = 0; i < input.Length; i++) {
            string line = input[i];
            // loop through each char in line
            for (int a = 0; a < line.Length; a++) {
                 // check if instruction line
                    if (line[a]=='m') {
                        break;
                    }
                // check if letter
                if (Char.IsLetter(line[a])) {
                    // get the index of the letter to find which column (in the guide) to add the letter to
                    int colNum = numIndexes[a];

                    // assign next 3 chars to var
                    string letterToAdd = line[a].ToString();
                    // assign to guide column
                    guide[colNum].Add(letterToAdd);
                }
            }
        }

        // get letters in correct order
        foreach (var thing in guide){
             thing.Value.Reverse();
        }
    }
    public static void Day4() {
        string input = GetInput(4);
        string[] inputs = input.Split(new Char [] {'\n' , ',' });
        int counter = 0;
        string[] elfOneRange = new string[2];
        string[] elfTwoRange = new string[2];
        int elfOneStart= 0;
        int elfOneEnd= 0;
        int elfTwoStart = 0;
        int elfTwoEnd= 0;
        List<string> elfOne = new List<string>();
        List<string> elfTwo = new List<string>();
        bool elfTwoContained = false;
        bool elfOneContained = false;

        // put ranges into strings
        for (int i = 0; i < inputs.Length; i+=2) {
            // reset lists
            elfOne.Clear();
            elfTwo.Clear();

            elfOneRange = inputs[i].Split("-");
            elfOneStart = int.Parse(elfOneRange[0]);
            elfOneEnd = int.Parse(elfOneRange[1]);

            elfTwoRange = inputs[i+1].Split("-");
            elfTwoStart = int.Parse(elfTwoRange[0]);
            elfTwoEnd = int.Parse(elfTwoRange[1]);

            // add all elf one nums into char list
            for (int a = elfOneStart; a <= elfOneEnd; a++) {
                string aa = a.ToString();
                elfOne.Add(aa);
                if (elfOneStart==elfOneEnd) {break;}
            }
            // add all elf twp nums into char list
            for (int b = elfTwoStart; b <= elfTwoEnd; b++) {
                string bb = b.ToString();
                elfTwo.Add(bb);
                if (elfTwoStart==elfTwoEnd) {break;}
            }

            foreach (var num in elfOne) {
               elfTwoContained = true;
                if (!elfTwo.Contains(num)){
                    elfTwoContained = false;
                    break;
                }
            }

            if (elfTwoContained){
                counter++;
                elfTwoContained = true;
            }

            else if (!elfTwoContained) {
                foreach (var num in elfTwo) {
                    elfOneContained = true;
                    if (!elfOne.Contains(num)) {
                        elfOneContained = false;
                        break;
                    }
                }
                  if (elfOneContained){ 
                    counter++;
                }
            }
           
       }
        System.Console.WriteLine("Final count part one: " + counter);
        counter = 0;
        // part 2
        for (int i = 0; i < inputs.Length; i+=2) {
            // reset lists
            elfOne.Clear();
            elfTwo.Clear();
            elfOneRange = inputs[i].Split("-");
            elfOneStart = int.Parse(elfOneRange[0]);
            elfOneEnd = int.Parse(elfOneRange[1]);
            elfTwoRange = inputs[i+1].Split("-");
            elfTwoStart = int.Parse(elfTwoRange[0]);
            elfTwoEnd = int.Parse(elfTwoRange[1]);

            for (int a = elfOneStart; a <= elfOneEnd; a++) {
                string aa = a.ToString();
                elfOne.Add(aa);
                if (elfOneStart==elfOneEnd) {break;}
            }
            for (int b = elfTwoStart; b <= elfTwoEnd; b++) {
                string bb = b.ToString();
                elfTwo.Add(bb);
                if (elfTwoStart==elfTwoEnd) {break;}
            }
            
            foreach (var num in elfOne) {
               elfTwoContained = true;
                if (!elfTwo.Contains(num)){
                    continue;
                }
                else {
                    counter++;
                    elfTwoContained = true;
                    break;
                }
            }

            if (!elfTwoContained) {
                foreach (var num in elfTwo) {
                    if (!elfOne.Contains(num)) {
                        continue;
                    }
                    else {
                        counter++;
                        break;     
                    }
                }                 
            }    
        }
        System.Console.WriteLine("Final count part two: " + counter);
    }

    public static void Day3Part2() {
        string input = System.IO.File.ReadAllText(@"/Users/twiemann/Documents/a_c_3.txt");        
        string[] rucksacks = input.Split("\n");
        List<string> compartments = new List<string>();
        List<char> itemsList = new List<char>();
        var priorities = SetupPriorities();

        for (int i = 0; i < rucksacks.Length; i+=3) {
            foreach (char letter in rucksacks[i]) {
                if (rucksacks[i+1].IndexOf(letter) != -1) {
                    if (rucksacks[i+2].IndexOf(letter) != -1) {
                        itemsList.Add(letter);
                        break;
                    }
                }
            }            
        }

        int total = 0;
        foreach (var item in itemsList) {
            total += priorities[item];
        }
        System.Console.WriteLine(total);
    }

    public static Dictionary<char, int> SetupPriorities() {
        string letters = "abcdefghijklmnopqrstuvwxyz";         
        Dictionary<char, int> priorities = new Dictionary<char, int>();

        // setup priorities
        for (int i = 0; i < letters.Length; i++) {
            priorities.Add(letters[i],i+1);
            char upperLetter = letters[i].ToString().ToUpper().ToCharArray()[0];
            priorities.Add(upperLetter,i+27);
        }
        return priorities;
    }
    
    public static void Day3Part1() {
        string input = System.IO.File.ReadAllText(@"/Users/twiemann/Documents/a_c_3.txt");        
        string[] rucksacks = input.Split("\n");
        List<string> compartments = new List<string>();
        List<char> itemsList = new List<char>();
        string letters = "abcdefghijklmnopqrstuvwxyz";         
        Dictionary<char, int> priorities = new Dictionary<char, int>();

        // setup priorities
        for (int i = 0; i < letters.Length; i++) {
            priorities.Add(letters[i],i+1);
            char upperLetter = letters[i].ToString().ToUpper().ToCharArray()[0];
            priorities.Add(upperLetter,i+27);
        }

        foreach (string sack in rucksacks) {
            string compartmentOne = sack.Substring(0,(sack.Length/2));
            System.Console.WriteLine(compartmentOne);
            string compartmentTwo = sack.Substring((sack.Length/2),(sack.Length/2));
            System.Console.WriteLine(compartmentTwo);
            foreach (char item in compartmentOne) {
                if (compartmentTwo.Contains(item)) {
                    System.Console.WriteLine("Match: " + item);
                    itemsList.Add(item);
                    break;
                }
            }
        }

        int total = 0;
        foreach (var item in itemsList) {
            total += priorities[item];
        }
        System.Console.WriteLine(total);
    }
   public static void Day2Part2() {        
        Dictionary<string, string> translations = new Dictionary<string, string>() {
            {"A","Rock"},
            {"B","Paper"},
            {"C","Scissors"},
            {"X","Lose"},
            {"Y","Draw"},
            {"Z","Win"}
        };
        Dictionary<string, int> rubric = new Dictionary<string, int>() {
            {"Rock",1},
            {"Paper",2},
            {"Scissors",3}
        };     
        Dictionary<string, string> loseRules = new Dictionary<string, string>() {
            {"Rock","Scissors"},
            {"Paper","Rock"},
            {"Scissors","Paper"}
        };      
         Dictionary<string, string> winRules = new Dictionary<string, string>() {
            {"Rock","Paper"},
           {"Paper","Scissors"},
           {"Scissors","Rock"}
        };       
        int score = 0;
        string input = System.IO.File.ReadAllText(@"/Users/twiemann/Documents/a_c_2.txt");        
        string[] stratGuide = input.Split("\n");
        string scenario = "";
        
        foreach (var item in stratGuide) {
            string oppPlay = translations[item.Split(' ')[0]];
            scenario = translations[item.Split(' ')[1]];
            if (scenario == "Win") {
                score += 6;
                score += rubric[winRules[oppPlay]];
            }
             if (scenario == "Lose") {
                score += rubric[loseRules[oppPlay]];
            }
            else if (scenario == "Draw") {
                score += 3;
                score += rubric[oppPlay];
            }
            
        }
        System.Console.WriteLine(score);
    }

    public static void Day2() {   
        Dictionary<string, string> translations = new Dictionary<string, string>() {
            {"A","Rock"},
            {"B","Paper"},
            {"C","Scissors"},
            {"X","Rock"},
            {"Y","Paper"},
            {"Z","Scissors"}
        };
        Dictionary<string, int>  rubric = new Dictionary<string, int>() {
            {"Rock",1},
            {"Paper",2},
            {"Scissors",3}
        };     
         Dictionary<string, string>  rules = new Dictionary<string, string>() {
            {"Rock","Scissors"},
            {"Paper","Rock"},
            {"Scissors","Paper"}
        };       
        int score = 0;
        string input = System.IO.File.ReadAllText(@"/Users/twiemann/Documents/a_c_1.txt");        
        string[] stratGuide = input.Split("\n");
        
        foreach (var item in stratGuide) {
            string oppPlay = translations[item.Split(' ')[0]];
            System.Console.WriteLine("Opp play is " + oppPlay);
			string myPlay = translations[item.Split(' ')[1]];
            System.Console.WriteLine("my play is " + myPlay);
            // tie
            if (oppPlay == myPlay) {
                score += 3 + rubric[myPlay];
            }
            // win
            else if (rules[myPlay] ==  oppPlay) {
                score += 6 + rubric[myPlay];
            }
            // loss
             else {
                score += rubric[myPlay];
            }
            System.Console.WriteLine("Round score: " + score);
        }
    }
 
    public static void Day1 () {
        string input = System.IO.File.ReadAllText(@"/Users/twiemann/Documents/a_c_1_1.txt");     
        int runningTotal = 0;
        int currentHigh = 0;
        int intToAdd = 0;     
        string[] calsArr = input.Split("\n");   
        List<int> scoresList = new List<int>();      
        
        foreach (string cal in calsArr) {
        
            if (int.TryParse(cal, out intToAdd)) {
                runningTotal += intToAdd;
            Console.WriteLine("\nRunning total is: " + runningTotal);
            }
            else {
                if (runningTotal > currentHigh) {
                    currentHigh = runningTotal;
            } 
            scoresList.Add(runningTotal);
            runningTotal = 0;
            }
        }
        
        if (runningTotal > currentHigh) {
            currentHigh = runningTotal;
        }
        System.Console.WriteLine(currentHigh);
        scoresList.Sort();
        int total = scoresList[scoresList.Count-1] + scoresList[scoresList.Count-2] + scoresList[scoresList.Count-3];
        System.Console.WriteLine(total);
    }
    public static string GetInput (int day){
        string path = @"/Users/twiemann/Documents/a_c_" + day.ToString() + ".txt";
        return System.IO.File.ReadAllText(path);
    }
  }
  class Grid {
    public List<List<int>> innerGrid = new List<List<int>>();
    public List<List<int>> fullGrid = new List<List<int>>();
    public List<List<int>> cols = new List<List<int>>();
    public Grid ( List<List<int>> innerGrid, List<List<int>> fullGrid ,List<List<int>> cols){
        this.innerGrid = innerGrid;
        this.fullGrid = fullGrid;
        this.cols = cols;
    }
  }
}