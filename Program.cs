using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;



class Stack
{
    public int[] a;
    
    int count;

    public Stack(int max_size)
    {
        a = new int[max_size];
        count = 0;
    }


    public bool is_empty() =>
        count == 0;

    public void push(int i)
    {
        a[count] = i;
        count += 1;
    }

    public int pop()
    {
        if (is_empty()){
            return 0;
        }
        count -= 1;
        return a[count];
    }

    public void print()
    {
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(a[count]);
        }
    }
}


class Program
{

    static void Field_initialization(char[,] field, int line, string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            field[line, i] = input[i];

        }
    }

    static void Instruction_field_printer(char[,] field)
    {
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                Console.Write("[" + field[i, j] + "]");
            }
            Console.WriteLine();
        }
    }

            static void Main(string[] args)

    {

        int pointer_x = 0;
        int pointer_y = 0;
        char[,] instruction_field = new char[25, 80];
        int direction_x = 1;
        int direction_y = 0;
        //hello word
        //string input = ">              v\nv\"Hello World!\"<\n>:v\n^,_@"; 
        //string input = "\"!dlroW olleH\",,,,,,,,,,,,@";
        string input = "89*,52*5*2*1+,92*6*,92*6*,52*1+52**1+,48*,52*8*7+,52*1+52**1+,52*1+52**4+,v\nv                                                                         <\n>52*52**8+,52*52**,48*1+,@";
        //cat work in progress
        //string input = "~:1+!#@_,";
        string[] instructions = input.Split("\n");
        for (int i = 0; i < instructions.Length; i++)
        {
            Field_initialization(instruction_field, i, instructions[i]);
        }

        Stack value_stack = new Stack(100);
        //Instruction_field_printer(instruction_field);
        while (true) {
            
            /*Console.Write("Pointer x: " + pointer_x);
            Console.Write(" ");
            Console.Write("Pointer y: " + pointer_y);
            Console.Write(" ");*/
            char current_instruction = instruction_field[pointer_y, pointer_x];
            /*Console.WriteLine("Command: " + new_item);
            Console.WriteLine();*/
            if (int.TryParse(Convert.ToString(current_instruction), out int value))
            {
                value_stack.push(value);
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
                value_stack.push(value_stack.pop() - value_stack.pop());

            }else if (current_instruction == '/')
            {
                value_stack.push(value_stack.pop() / value_stack.pop());

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
            }else if (current_instruction == '~')
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
                pointer_x = (pointer_x + direction_x) % 80;
                pointer_y = (pointer_y + direction_y) % 25;
            }
            else if (current_instruction == '"')
            {
                pointer_x = (pointer_x + direction_x) % 80;
                pointer_y = (pointer_y + direction_y) % 25;
                while (true)
                {
                    if (instruction_field[pointer_y, pointer_x] == '"')
                    {
                        break;
                    }
                    value_stack.push(instruction_field[pointer_y, pointer_x]);
                    pointer_x += direction_x;
                    pointer_y += direction_y;
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
            pointer_x = (pointer_x + direction_x) % 80;
            pointer_y = (pointer_y + direction_y) % 25;

        }
        //Console.ReadLine();
    }





}
