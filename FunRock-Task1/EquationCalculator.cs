using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunRock_Task1
{
    class EquationCalculator
    {
        Stack<int> StackOperators;
        Stack<int> StackNumbers;
        String operators = "+-/*";
        String postfixStr;

        string stepsToEvaluate;
        bool invalidEquation = false;

        public void CalculateEquation(string theEquation)
        {
            string postfixEquation = infix_to_postfix(theEquation);
            int equationValue = calculatePostfixExpression(postfixEquation);

            if (invalidEquation)
            {
                Console.WriteLine("this is not a valid postfix expresison...");
            }
            else
            {
                Console.WriteLine("posfix equation is > " + postfixEquation + "\n");
                Console.WriteLine(stepsToEvaluate);
                Console.WriteLine("PRINT " + equationValue);
            }
        }

        bool stackIsEmpty(Stack<int> stack)
        {
            bool isEmpty = false;
            if (stack.Count == 0)
            {
                isEmpty = true;
            }
            return isEmpty;
        }

        // convert aninfix equation (1+2*3) to a postfix equation (123-+)
        String infix_to_postfix(String infix)
        {
            postfixStr = "";
            StackOperators = new Stack<int>();

            for (int i = 0; i < infix.Length; i++)
            {
                if (char.IsDigit(infix[i]))
                {
                    postfixStr += infix[i];
                }// if char is a number

                else if (operators.IndexOf(infix[i]) != -1) // current charecter is an operator ( + - * / )
                {
                    if (stackIsEmpty(StackOperators))
                    {
                        StackOperators.Push((int)infix[i]);
                    }// if operators stack is empty
                    else
                    {
                         if (Precedance(infix[i]) >= Precedance((char)StackOperators.First()))
                        {
                            StackOperators.Push((int)infix[i]);
                        }// if char precedance > top of stack

                        else
                        {
                            postfixStr += (char)StackOperators.Pop();
                            
                            if (!stackIsEmpty(StackOperators))
                            {
                                while (Precedance(infix[i]) < Precedance((char)StackOperators.First()))
                                {
                                    postfixStr += (char)StackOperators.Pop();
                                }
                            }// if operators stack is empty
                            else
                            {
                                StackOperators.Push((int)infix[i]);
                            }// if operators stack is not empty

                        }// if char precedance < top of stack

                    }// if operator stack not empty
                }//if operator
            }
            while (!stackIsEmpty(StackOperators))
            {
                postfixStr += ((char)StackOperators.Pop());
            }// if there are still operators in the stack , concatinate the rest
            return postfixStr;
        }//infix to postfix 


        // calculate expressions, takes a postfix expression and calculates the equation.
        int calculatePostfixExpression(String postfix)
        {
            int op2 = 0;
            int op1 = 0;
            int result = 0;
            StackNumbers = new Stack<int>();
            char[] postfixChar = postfix.ToCharArray();

            for (int i = 0; i < postfix.Length; i++)
            {
                if (char.IsDigit(postfixChar[i]))
                {
                    StackNumbers.Push((int)char.GetNumericValue(postfixChar[i]));
                    stepsToEvaluate += "PUSH " + postfixChar[i] + "\n";

                }// if the the current char is a digit , push to numbers stack
                else
                {
                    if (!stackIsEmpty(StackNumbers)) // stack is not empty
                    {
                        op2 = StackNumbers.Pop();
                        
                    }// if the stack has values, pop operand number 2
                    if (!stackIsEmpty(StackNumbers))
                    {
                        op1 = StackNumbers.Pop();
                        
                    }// if the stack still has values, pop operand number 1
                    else
                    {
                        invalidEquation = true;
                    }// not a valid expression

                    // calculate the result then push the result to the numbers stack
                    if (postfixChar[i] == '+')
                    {
                        result = op1 + op2;
                        stepsToEvaluate += "ADD " + "\n";
                    }
                    else if (postfixChar[i] == '-')
                    {
                        result = op1 - op2;
                        stepsToEvaluate += "SUB " + "\n";
                    }
                    else if (postfixChar[i] == '*')
                    {
                        result = op1 * op2;
                        stepsToEvaluate += "MUL " + "\n";
                    }
                    else
                    {
                        result = op1 / op2;
                        stepsToEvaluate += "DIV " + "\n";
                    }
                    StackNumbers.Push(result);
                    stepsToEvaluate += "PUSH " + result + "\n";
                }// char is not a number

            }//looping on the postfix string

            return result;
        }// calculate the equation value


        // function Precedance, checks the Precedance of the operator, valid operators are + - * / 
        int Precedance(char op)
        {
            if (op == '+' || op == '-')
            {
                return 0;
            }// if + or -
            if (op == '*' || op == '/')
            {
                return 1;
            }// if * or /

            return -1;

        }//Precedance function

    }
}
