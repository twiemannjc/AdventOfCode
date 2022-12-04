﻿using System;
using System.Collections.Generic; 

namespace AdventOfCode
{
  class Program
  {
    static void Main(string[] args)
    {
        //Day1();
        //Day2();
        //Day2Part2();
        //Day3Part1();
        //Day3Part2();
        Day4();
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
}