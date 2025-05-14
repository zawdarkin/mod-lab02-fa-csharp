using System;
using System.Collections.Generic;

namespace fans
{
    public class State
    {
        public string Identifier { get; set; }
        public Dictionary<char, State> NextStates { get; set; }
        public bool IsFinal { get; set; }
    }

    public class FA1
    {
        private readonly State _startState;
        
        public FA1()
        {
            var stateA = new State { Identifier = "A", IsFinal = false, NextStates = new Dictionary<char, State>() };
            var stateB = new State { Identifier = "B", IsFinal = false, NextStates = new Dictionary<char, State>() };
            var stateC = new State { Identifier = "C", IsFinal = true, NextStates = new Dictionary<char, State>() };
            var stateD = new State { Identifier = "D", IsFinal = false, NextStates = new Dictionary<char, State>() };
            var stateE = new State { Identifier = "E", IsFinal = false, NextStates = new Dictionary<char, State>() };

            stateA.NextStates['0'] = stateD;
            stateA.NextStates['1'] = stateB;
            
            stateB.NextStates['0'] = stateC;
            stateB.NextStates['1'] = stateB;
            
            stateC.NextStates['0'] = stateE;
            stateC.NextStates['1'] = stateC;
            
            stateD.NextStates['0'] = stateE;
            stateD.NextStates['1'] = stateC;
            
            stateE.NextStates['0'] = stateE;
            stateE.NextStates['1'] = stateE;

            _startState = stateA;
        }

        public bool? Run(IEnumerable<char> input)
        {
            var current = _startState;
            foreach (var symbol in input)
            {
                if (!current.NextStates.TryGetValue(symbol, out current))
                    return null;
            }
            return current.IsFinal;
        }
    }

    public class FA2
    {
        private readonly State _stateEvenEven;
        private readonly State _stateOddEven;
        private readonly State _stateEvenOdd;
        private readonly State _stateOddOdd;
        
        public FA2()
        {
            _stateEvenEven = new State { Identifier = "EE", IsFinal = false, NextStates = new Dictionary<char, State>() };
            _stateOddEven = new State { Identifier = "OE", IsFinal = false, NextStates = new Dictionary<char, State>() };
            _stateEvenOdd = new State { Identifier = "EO", IsFinal = false, NextStates = new Dictionary<char, State>() };
            _stateOddOdd = new State { Identifier = "OO", IsFinal = true, NextStates = new Dictionary<char, State>() };

            SetupTransitions();
        }

        private void SetupTransitions()
        {
            // Transitions for 0
            _stateEvenEven.NextStates['0'] = _stateOddEven;
            _stateOddEven.NextStates['0'] = _stateEvenEven;
            _stateEvenOdd.NextStates['0'] = _stateOddOdd;
            _stateOddOdd.NextStates['0'] = _stateEvenOdd;
            
            // Transitions for 1
            _stateEvenEven.NextStates['1'] = _stateEvenOdd;
            _stateOddEven.NextStates['1'] = _stateOddOdd;
            _stateEvenOdd.NextStates['1'] = _stateEvenEven;
            _stateOddOdd.NextStates['1'] = _stateOddEven;
        }

        public bool? Run(IEnumerable<char> input)
        {
            var current0 = _stateEvenEven;
            var current1 = _stateEvenEven;
            
            foreach (var symbol in input)
            {
                if (symbol == '0') current0 = current0.NextStates[symbol];
                else if (symbol == '1') current1 = current1.NextStates[symbol];
                else return null;
            }
            
            return current0 == _stateOddEven && current1 == _stateEvenOdd;
        }
    }

    public class FA3
    {
        private readonly State _initial;
        
        public FA3()
        {
            var stateNo1 = new State { Identifier = "No1", IsFinal = false, NextStates = new Dictionary<char, State>() };
            var stateOne1 = new State { Identifier = "One1", IsFinal = false, NextStates = new Dictionary<char, State>() };
            var stateTwo1 = new State { Identifier = "Two1", IsFinal = true, NextStates = new Dictionary<char, State>() };

            stateNo1.NextStates['0'] = stateNo1;
            stateNo1.NextStates['1'] = stateOne1;
            
            stateOne1.NextStates['0'] = stateNo1;
            stateOne1.NextStates['1'] = stateTwo1;
            
            stateTwo1.NextStates['0'] = stateTwo1;
            stateTwo1.NextStates['1'] = stateTwo1;

            _initial = stateNo1;
        }

        public bool? Run(IEnumerable<char> input)
        {
            var current = _initial;
            foreach (var symbol in input)
            {
                if (!current.NextStates.TryGetValue(symbol, out current))
                    return null;
            }
            return current.IsFinal;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string testString = "01111";
            
            var fa1 = new FA1();
            Console.WriteLine($"FA1 result: {fa1.Run(testString)}");
            
            var fa2 = new FA2();
            Console.WriteLine($"FA2 result: {fa2.Run(testString)}");
            
            var fa3 = new FA3();
            Console.WriteLine($"FA3 result: {fa3.Run(testString)}");
        }
    }
}
