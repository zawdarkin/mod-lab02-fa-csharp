using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fans
{
  public class State
  {
    public string Name;
    public Dictionary<char, State> Transitions;
    public bool IsAcceptState;
  }


  public class FA1
  {
    public static State q1 = new State(){
      Name = "q1",
      IsAcceptState = false,
      Transitions = new Dictionary<char, State>()
    };
    public State q2 = new State(){
      Name = "q2",
      IsAcceptState = false,
      Transitions = new Dictionary<char, State>()
    };
    public State q3 = new State(){
      Name = "q3",
      IsAcceptState = true,
      Transitions = new Dictionary<char, State>()
    };
    
    public State q4 = new State(){
      Name = "q4",
      IsAcceptState = false,
      Transitions = new Dictionary<char, State>()
    };

    public State q5 = new State(){
      Name = "q5",
      IsAcceptState = false,
      Transitions = new Dictionary<char, State>()
    };
  
    
    State InitialState = q1;

    public FA1(){
      q1.Transitions['0'] = q4;
      q1.Transitions['1'] = q2;
      q2.Transitions['0'] = q3;
      q2.Transitions['1'] = q2;
      q3.Transitions['0'] = q5;
      q3.Transitions['1'] = q3;
      q4.Transitions['0'] = q5;
      q4.Transitions['1'] = q3;
      q5.Transitions['0'] = q5;
      q5.Transitions['1'] = q5;
    }

    public bool? Run(IEnumerable<char> s)
    {
      State current = InitialState;
      foreach (var c in s)
      {
        current = current.Transitions[c];
        if (current == null) return null;
      }
      return current.IsAcceptState;
    }

  };

  public class FA2
  {
   public static State q1 = new State(){
      Name = "q1",
      IsAcceptState = false,
      Transitions = new Dictionary<char, State>()
    };
    public State q2 = new State(){
      Name = "q2",
      IsAcceptState = true,
      Transitions = new Dictionary<char, State>()
    };
    public State q3 = new State(){
      Name = "q3",
      IsAcceptState = false,
      Transitions = new Dictionary<char, State>()
    };
    
    public State q4 = new State(){
      Name = "q4",
      IsAcceptState = true,
      Transitions = new Dictionary<char, State>()
    };

    public FA2(){
      q1.Transitions['0'] = q2;
      q2.Transitions['0'] = q1;
      q3.Transitions['1'] = q4;
      q4.Transitions['1'] = q3;
    }

    public bool? Run(IEnumerable<char> s)
    {
      State current_0 = q1;
      State current_1 = q3;
      foreach (var c in s)
      {
        if(c == '1') current_1 = current_1.Transitions[c];
        else if (c == '0') current_0 = current_0.Transitions[c];
        else return null;
      }
      return current_1.IsAcceptState && current_0.IsAcceptState;
    }
  }
  
  public class FA3
  {
    public static State q1 = new State(){
      Name = "q1",
      IsAcceptState = false,
      Transitions = new Dictionary<char, State>()
    };
    public State q2 = new State(){
      Name = "q2",
      IsAcceptState = false,
      Transitions = new Dictionary<char, State>()
    };
    public State q3 = new State(){
      Name = "q3",
      IsAcceptState = true,
      Transitions = new Dictionary<char, State>()
    };

    State InitialState = q1;
    
    public FA3(){
      q1.Transitions['0'] = q1;
      q1.Transitions['1'] = q2;
      q2.Transitions['0'] = q1;
      q2.Transitions['1'] = q3;
      q3.Transitions['0'] = q3;
      q3.Transitions['1'] = q3;
    }

    public bool? Run(IEnumerable<char> s)
    {
      State current = InitialState;
      foreach (var c in s)
      {
        current = current.Transitions[c];
        if (current == null) return null;
      }
      return current.IsAcceptState;
    }
  };

  class Program
  {
    static void Main(string[] args)
    {
      String s = "01111";
      FA1 fa1 = new FA1();
      bool? result1 = fa1.Run(s);
      Console.WriteLine(result1);
      FA2 fa2 = new FA2();
      bool? result2 = fa2.Run(s);
      Console.WriteLine(result2);
      FA3 fa3 = new FA3();
      bool? result3 = fa3.Run(s);
      Console.WriteLine(result3);
    }
  }
}