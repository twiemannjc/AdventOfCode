namespace AdventOfCode
{
    class Monkey 
    {
        public List<double> Items { get; set; } = new List<double>();
        public int InspectedCounter { get; set; } = 0;
        public KeyValuePair<string,string> Operation { get; set; }
        public List<int> Test { get; set; } = new List<int>();
        public List<int> RemovedItems { get; set; } = new List<int>();

        public static List<Monkey> SetupMonkey(List<string> input) {
            List<Monkey> monkeys = new List<Monkey>();
            for (int i = 0; i < input.Count; i++) {
                List<string> monkeySplit = input[i].Split("\n").ToList();
                Monkey monkey = new Monkey();
                for (int a = 0; a < monkeySplit.Count; a++) {
                    string currentLine = monkeySplit[a].Trim().Substring(0,7);
                    System.Console.WriteLine(monkeySplit[a].Trim());
                    // setup Monkey
                    if (currentLine == "Monkey ") {
                        //
                    }
                    else if (currentLine == "Startin") {
                        var itemVals = monkeySplit[a].Trim().Replace("Starting items: ","").Split(", ");
                        foreach (var item in itemVals) {
                            System.Console.WriteLine(item);
                            monkey.Items.Add(int.Parse(item));
                        }
                    }
                    else if (currentLine == "Operati") {
                        var operationVals = monkeySplit[a].Trim().Replace("Operation: new = old ","").Split(" ");
                        monkey.Operation = new KeyValuePair<string, string> (operationVals[0],operationVals[1]);
                        
                    }
                    else if (currentLine == "Test: d") {
                        string test = monkeySplit[a].Trim().Replace("Test: divisible by ","");
                        int divisibleVal = int.Parse(test);
                        monkey.Test.Add(divisibleVal);
                    }
                    else if (currentLine == "If true") {
                        string trueS = monkeySplit[a].Trim().Replace("If true: throw to monkey ","");
                        int trueVal = int.Parse(trueS);
                        monkey.Test.Add(trueVal);
                    }
                    else if (currentLine == "If fals") {
                        string falseS = monkeySplit[a].Trim().Replace("If false: throw to monkey ","");
                        int falseVal = int.Parse(falseS);
                        monkey.Test.Add(falseVal);
                    }
                }
                monkeys.Add(monkey);
            }
            return monkeys;
        }
    }
}