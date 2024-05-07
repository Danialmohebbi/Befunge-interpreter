using static Utilities;

namespace DataStructures
{
    class Node{
        public Node? next;
        public int value;

        public Node(int val, Node? nxt=null){
            value = val;
            next = nxt;
        }
    }

class Stack{


    Node? head;

    public void push(int i){
        if (head == null){
            head = new Node(i);
            return;
        }

        head = new Node(i, head);
    }

    public int pop(){
        if (head == null){
            return 0; 
        }

        int popped_value = head.value;
        head = head.next;
        return popped_value;
    }
}

/// <summary>
/// this class represents the main 2D array which will be used for reading the instructions.
/// </summary>
class Field{
    public char[,] instruction_field = new char[25,80];    
    int[] x_len = new int[25];
    int[] y_len = new int[80];
    int pointer_x;
    int pointer_y;
    public int direction_x;
    public int direction_y;
    (int, int) RIGHT = (1, 0);
    (int, int) LEFT = (-1, 0);
    (int, int) DOWN = (0, 1);
    (int, int) UP = (0, -1);
    (int,int)[] directions = {(1,0), (-1,0), (0,1), (0,-1)};
    
    Stack stack = new Stack();

    Random r = new();


    public Field(){
        (direction_x, direction_y) = RIGHT;
        (pointer_x, pointer_y) = (0,0);
    }

    /// <summary>
    /// parse a line into a row in the 2D array.
    /// </summary>
    /// <param name="line">2D array row number</param>
    /// <param name="input">the string to be parsed into instructions</param>
    /// <returns>if the line contains more than 25 characters</returns>
    public void Parse(int line, string input)
    {
        if (input.Length > 80){
            throw new ArgumentException("All lines should contain at most 80 charachters!");
        }
        for (int i = 0; i < input.Length; i++)
        {
            instruction_field[line, i] = input[i];

        }
    }
    /// <summary>
    /// read the inputed file for instructions
    /// </summary>
    /// <param name="fileName">file name</param>
    /// <returns>if number of lines more than 25 or the file doesn't exist throws an exception</returns>
    public void processFile(string fileName){

        List<string> readInstructions = new();
        
        if (!File.Exists(fileName)){
            throw new Exception(fileName + " does not exist!");
        }

        using StreamReader reader = new(fileName);
        while (reader.ReadLine() is string line){
            readInstructions.Add(line);

        }
        if (readInstructions.Count > 25){
            throw new Exception("The file contains more than 25 lines!");
        }

        for (int i = 0; i < readInstructions.Count; i++){
            Parse(i, readInstructions[i]);
        }

        
    }
    /// <summary>
    /// get the next token/instructions to process
    /// </summary>
    /// <returns>char</returns>
    public char nextToken(){
        var token = instruction_field[pointer_y,pointer_x]; 
        return token;
    }
    /// <summary>
    /// change the direction that the pointer will be moving 
    /// </summary>
    /// <param name="op">the instruction to be evaluated</param>
    /// <returns>null</returns>
    public void changeDirection(char op){
        switch (op)
        {
            case '>': (direction_x, direction_y) = RIGHT;break;            
            case '<': (direction_x, direction_y) = LEFT;break;
            case 'v': (direction_x, direction_y) = DOWN;break;
            case '^': (direction_x, direction_y) = UP;break;
            case '?': 
            (direction_x, direction_y) = directions[r.Next(0,4)];break;
            default:break;
        }
    }
    /// <summary>
    /// evaluate if a direction change is needed
    /// </summary>
    /// <param name="op">the instruction to be evaluated</param>
    /// <param name="stack">the stack to be used for evaluation</param>
    /// <returns>null</returns>
    public void evaluateDirection(char op, ref Stack stack){
        int value;
        switch (op){
            case '_': 
            value = stack.pop();
            if (value == 0){
                (direction_x, direction_y) = RIGHT;
            }
            else {
                (direction_x,direction_y) = LEFT;
            }
            break;
            case '|':
            value = stack.pop();
            if (value == 0){
                (direction_x, direction_y) = DOWN;
            }else {
                (direction_x, direction_y) = UP;
            }
            break;
            default: break;
        }
    }
    /// <summary>
    /// update the pointers accordingly to get the next instruction
    /// </summary>
    /// <param name="x">choose how to update the x of the pointer</param>
    /// <param name="y">choose how to update the y of the pointer</param>
    /// <returns>null</returns>
    public void updatePointers(int x = 0, int y = 0){
        pointer_x  = (pointer_x + x + 80) % 80;
        pointer_y = (pointer_y + y + 25) % 25;
        }
    /// <summary>
    /// contains the main loop and process the instructions in the field 
    /// </summary>
    /// <returns>null</returns>
    public void eval(){
        
        bool running = true;

        while (running){
            char currentInstruction = nextToken();
            if (int.TryParse(Convert.ToString(currentInstruction), out int value))
            {
                stack.push(value);
            }
            else if ("+-/*%!`".Contains(currentInstruction)){
                Utilities.evaluateArithmatics(currentInstruction, ref stack);
            }
            else if (".,".Contains(currentInstruction)){
                Utilities.output(currentInstruction, ref stack);
            }
            else if (">v^?<".Contains(currentInstruction)){
                changeDirection(currentInstruction);
            }
            else if(currentInstruction == '#'){
                updatePointers(direction_x, direction_y);
            }
            else if ("|_".Contains(currentInstruction)){
                evaluateDirection(currentInstruction, ref stack);
            }
            else if ("&~".Contains(currentInstruction)){
                Utilities.evaluateInput(currentInstruction, ref stack, ref running);
            }
            else if (currentInstruction == '"'){
                updatePointers(direction_x,direction_y);
                while (true){
                    char newInstruction = nextToken();
                    if (newInstruction == '"'){
                        updatePointers();
                        break;
                    }
                    stack.push(newInstruction);
                    updatePointers(direction_x, direction_y);
                }
            }
            else if (":\\$".Contains(currentInstruction)){
                Utilities.evaluateStack(currentInstruction, ref stack);
            }
            else if (currentInstruction == 'g'){
                int y = stack.pop();
                int x = stack.pop();
                if (0 <= y && y <= 25 && x <= 80 && 0 <= x){
                    char getValue = instruction_field[y,x];
                    stack.push(getValue);
                }else{
                    stack.push(0);
                }
            }else if (currentInstruction == 'p'){
                int y = stack.pop();
                int x = stack.pop();
                int val = stack.pop();
                instruction_field[y,x] = (char) val;
            }
            else if (currentInstruction == '@'){
                break;
            }

            updatePointers(direction_x, direction_y);

        }
        Console.WriteLine();
    }
    }

}
