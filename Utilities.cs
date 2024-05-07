using DataStructures;

    static class Utilities{
        /// <summary>
        /// this is a helper method to evaluate the following arithmatic opreations:
        /// +,-,/,*,%,!.
        /// </summary>
        /// <param name="op">the instruction to be evaluated</param>
        /// <param name="stack">the stack to be used for evaluation</param>
        /// <returns>null</returns>
    static public void evaluateArithmatics(char op, ref Stack stack){

        int value_1 = 0;
        int value_2 = 0;
        switch(op){
            case '+': stack.push(stack.pop() + stack.pop()); break;
            case '-': 
            value_1 = stack.pop();
            value_2 = stack.pop();
            stack.push(value_2 - value_1); break;
            case '*': stack.push(stack.pop() * stack.pop()); break;
            case '/': 
            value_1 = stack.pop();
            value_2 = stack.pop();
            if (value_1 == 0){
                stack.push(0);
            }else{
                stack.push(value_2 / value_1);
            }
            break;
            case '%':
            value_1 = stack.pop();
            value_2 = stack.pop();
            stack.push(value_1 % value_2); break;
            case '!':
            value_1 = stack.pop();
            if (value_1 == 0){
                stack.push(1);
            }else{
                stack.push(0);
            }
            break;
            case '`':
            int a = stack.pop();
            int b = stack.pop();
            stack.push(b > a ? 1 : 0);
            break;
            default:
            break;
        }
    }

    /// <summary>
    /// this is a helper to process the command line arguments
    /// </summary>
    /// <param name="args">command line arguments</param>
    /// <returns>String Filename</returns>
    static public string processArguments(string[] args){
        if (args.Length != 1){
            Console.WriteLine("dotnet run FileName");
            throw new("Please enter a filename!");
        }
        return args[0];
    }

    /// <summary>
    /// this is a helper method to output an integer or char which has the following commands:
    /// . output top stack value as an int
    /// , output top stack value as an char
    /// </summary>
    /// <param name="op">the instruction to be evaluated</param>
    /// <param name="stack">the stack to be used for evaluation</param>
    /// <returns>null</returns>
    static public void output(char op, ref Stack stack){
        switch (op)
        {
            case '.': Console.Write((int)stack.pop() + " "); break;
            case ',': Console.Write((char) stack.pop()); break;
            default:break;
        }
    }


    /// <summary>
    /// this is a helper method to evaluate an input, which has the following commands:
    /// & for integer input
    /// ~ for char input
    /// </summary>
    /// <param name="op">the instruction to be evaluated.</param>
    /// <param name="stack">the stack to be used for evaluation.</param>
    /// <param name="running">to stop the program if needed.</param>
    /// <returns>null</returns>
    static public void evaluateInput(char op, ref Stack stack, ref bool running){
        switch(op)
        {
            case '&':
            string? input_integer = Console.ReadLine();
            if (input_integer is null || input_integer == ""){
                running = false;
                break;
            }
            stack.push(int.Parse(input_integer));
            break;
            case '~': 
            string? str = Console.ReadLine();
            if (str == "" || str is  null){
                running = false;
                break;
            }
            stack.push(str[0]); 
            break;

        }
    }

    /// <summary>
    /// this is a helper method that does the following elementary opreations on the stack:
    /// $ pops an element
    /// : duplicates the top element on the stack
    /// \ swaps the top two elements on the stack
    /// </summary>
    /// <param name="op">the instruction to be evaluated.</param>
    /// <param name="stack">the stack to be used for evaluation.</param>
    /// <returns>null</returns>
    static public void evaluateStack(char op, ref Stack stack){
        switch (op)
        {
            case '$': stack.pop(); break;
            case '\\':
            int value_1 = stack.pop();
            int value_2 = stack.pop();
            stack.push(value_1);
            stack.push(value_2);
            break;
            case ':':
            int value_3 = stack.pop();
            stack.push(value_3);
            stack.push(value_3); 
            break;
            default:
            break;
        }
    }

    }
