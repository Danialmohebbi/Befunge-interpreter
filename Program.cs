using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;
using DataStructures;
class Program
{
    static void Main(string[] args)
    {
        Field field = new();
        int pointer_x = 0;
        int pointer_y = 0;
        char[,] instruction_field = new char[25, 80];
        int[] x_len = new int[25];
        int[] y_len = new int[80];
        int direction_x = 1;
        int direction_y = 0;

        List<string> instructions = new();
        using StreamReader reader = new("text.txt");
        while (reader.ReadLine() is string line){
            instructions.Add(line);
        }  


        for (int i = 0; i < instructions.Count; i++)
        {
            x_len[i] = instructions[i].Length;
            field.Field_initialization(instruction_field, i, instructions[i]);
        }
        //field.Instruction_field_printer(instruction_field);
        for (int i = 0; i < y_len.Length; i++)
        {
            int last = 0;
            bool found = false;
            for (int j = 0; j < 25; j++)
            {
                if (instruction_field[j,i] != '\0'){
                    
                    last = j;
                    found = true;
                }
            }
            if (last != 0){
                y_len[i] = last + Convert.ToInt32(found);
            }
        }
        /*foreach (var item in y_len)
        {
            Console.WriteLine("Column " + item);
        }*/
        Stack value_stack = new Stack();
        List<(int, int)> dirs = new();
        dirs.Add((1,0));
        dirs.Add((-1,0));
        dirs.Add((0,1));
        dirs.Add((0,-1));
        //Instruction_field_printer(instruction_field);
        while (true) {
            //field.Instruction_field_printer(instruction_field);
            /*Console.Write("Pointer x: " + pointer_x);
            Console.Write(" ");
            Console.Write("Pointer y: " + pointer_y);
            Console.Write(" ");*/
            char current_instruction = instruction_field[pointer_y, pointer_x];
            /*  Console.WriteLine("Command: " + current_instruction);
            Console.WriteLine();*/
            if (int.TryParse(Convert.ToString(current_instruction), out int value))
            {
                value_stack.push(value);
            }else if(current_instruction == '|'){
                int popped_value = value_stack.pop();
                if (popped_value == 0){
                    (direction_x, direction_y) = (0,1);
                }else{
                    (direction_x, direction_y) = (0,-1);
                }
            }
            else if (current_instruction == '+')
            {
                value_stack.push(value_stack.pop() + value_stack.pop());
            }
            else if (current_instruction == '>') {
                (direction_x, direction_y) = (1, 0);
            }
            else if (current_instruction == '<')
            {
                (direction_x, direction_y) = (-1, 0);
            }
            else if (current_instruction == '^')
            {
                (direction_x, direction_y) = (0, -1);
            }
            else if (current_instruction == '@') {
                break;
            }else if (current_instruction == '\\'){
                int value_1 = value_stack.pop();
                int value_2 = value_stack.pop();
                value_stack.push(value_1);
                value_stack.push(value_2);
            }
            else if (current_instruction == '&')
            {
                string? input_integer = Console.ReadLine();
                if (input_integer is null || input_integer == "")
                {
                    break;
                }
                value_stack.push(int.Parse(input_integer));
            }
            else if (current_instruction == '$'){
                value_stack.pop();
            }else if (current_instruction == 'g'){
                int y = value_stack.pop();
                int x = value_stack.pop();
                try
                {
                    value_stack.push(instruction_field[y,x]);
                }
                catch (System.Exception)
                {
                    
                    value_stack.push(0);
                }
                
            }else if (current_instruction == 'p'){
                int y = value_stack.pop();
                int x = value_stack.pop();
                char v = (char) value_stack.pop();
                instruction_field[y,x] = v;
            }
            else if (current_instruction == 'v')
            {
                (direction_x, direction_y) = (0, 1);
            }
            else if (current_instruction == '*')
            {
                value_stack.push(value_stack.pop() * value_stack.pop());
            }
            else if (current_instruction == '-')
            {
                int value_popped_1 = value_stack.pop();
                int value_popped_2 = value_stack.pop(); 
                value_stack.push(value_popped_2 - value_popped_1);

            }else if (current_instruction == '/')
            {
                int y = value_stack.pop();  
                int x = value_stack.pop();
                try
                {
                    value_stack.push(y/x);
                }
                catch (System.Exception)
                {
                    value_stack.push(0);
                }
                value_stack.push(0);

            }else if (current_instruction == '%')
            {
                value_stack.push(value_stack.pop() % value_stack.pop());
            }else if (current_instruction == '!')
            {  
                if (value_stack.pop() == 0)
                {
                    value_stack.push(1);
                }else
                {
                    value_stack.push(0);
                }
            }else if (current_instruction == '?'){
                Random random = new();
                int rnd = random.Next(0,4);
                (direction_x, direction_y) = dirs[rnd];
            }
            else if (current_instruction == '~')
            {
                string? input_character = Console.ReadLine();
                if (input_character is null || input_character == "")
                {
                    break;
                }
                value_stack.push(input_character[0]);
            }
            else if (current_instruction == '`') {
                int a = value_stack.pop();
                int b = value_stack.pop();
                value_stack.push(b > a ? 1 : 0);
            }else if (current_instruction == '#')
            {
                pointer_x = (pointer_x + direction_x + x_len[pointer_y]) % x_len[pointer_y];
                if (y_len[pointer_x] == 0){
                                pointer_y = (pointer_y + direction_y + y_len[pointer_x]) % 1;
                }else{
            pointer_y = (pointer_y + direction_y + y_len[pointer_x]) % y_len[pointer_x];}            }
            else if (current_instruction == '"')
            {
                pointer_x = (pointer_x + direction_x + x_len[pointer_y]) % x_len[pointer_y];
                if (y_len[pointer_x] == 0){
                                pointer_y = (pointer_y + direction_y + y_len[pointer_x]) % 1;
                }else{
            pointer_y = (pointer_y + direction_y + y_len[pointer_x]) % y_len[pointer_x];}
                while (true)
                {
                    //Console.WriteLine($"{pointer_x}, {pointer_y}");
                    if (instruction_field[pointer_y, pointer_x] == '"')
                    {
                        break;
                    }
                    value_stack.push(instruction_field[pointer_y, pointer_x]);
                pointer_x = (pointer_x + direction_x + x_len[pointer_y]) % x_len[pointer_y];
                if (y_len[pointer_x] == 0){
                                pointer_y = (pointer_y + direction_y + y_len[pointer_x]) % 1;
                }else{
            pointer_y = (pointer_y + direction_y + y_len[pointer_x]) % y_len[pointer_x];}
                }
            }else if (current_instruction == '_')
            {
                int t = value_stack.pop();
                if (t == 0) {
                    (direction_x, direction_y) = (1, 0);
                }else
                {
                    (direction_x, direction_y) = (-1, 0);
                }
            }
            else if (current_instruction == '.')
            {
                Console.Write((int)value_stack.pop());
            }
            else if (current_instruction == ',') {
                Console.Write((char)value_stack.pop());
            }else if (current_instruction == ':')
            {
                int duplicate = value_stack.pop();
                value_stack.push(duplicate);
                value_stack.push(duplicate);
            }
            //Console.WriteLine(pointer_x + direction_x + x_len[pointer_y]);
            pointer_x = (pointer_x + direction_x + x_len[pointer_y]) % x_len[pointer_y];
                if (y_len[pointer_x] == 0){
                                pointer_y = (pointer_y + direction_y + y_len[pointer_x]) % 1;
                }else{
            pointer_y = (pointer_y + direction_y + y_len[pointer_x]) % y_len[pointer_x];}
        }
        
        Console.WriteLine();
    }





}
