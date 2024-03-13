using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;



class Stack
{

    // fields

    public int[] a;
    int count;

    // constructor

    public Stack(int max_size)
    {
        a = new int[max_size];
        count = 0;
    }

    // methods

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

    static void input2array(char[,] array, int line, string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            array[line, i] = input[i];

        }
    }

            static void Main(string[] args)

    {

        int pointer_x = 0;
        int pointer_y = 0;
        char[,] magic = new char[25, 80];
        int direction_x = 1;
        int direction_y = 0;
        //string input = ">              v\nv\"Hello World!\"<\n>:v\n^,_@";
        //string input = "\"!dlroW olleH\",,,,,,,,,,,,@";
        string input = "89*,52*5*2*1+,92*6*,92*6*,52*1+52**1+,48*,52*8*7+,52*1+52**1+,52*1+52**4+,v\nv                                                                         <\n>52*52**8+,52*52**,48*1+,@";
        //string input = "&#::_.@#";
        string[] input2 = input.Split("\n");
        for (int i = 0; i < input2.Length; i++)
        {
            input2array(magic, i, input2[i]);
        }
        for (int i = 0; i < magic.GetLength(0); i++)
        {
            for (int j = 0; j < magic.GetLength(1); j++)
            {
                Console.Write("[" + magic[i, j] + "]");
            }
            Console.WriteLine();
        }
        /*magic[0, 0] = '>'; magic[1, 0] = ' '; magic[2, 0] = ' '; magic[3, 0] = ' ';
        magic[4, 0] = ' '; magic[5, 0] = 'v'; magic[5, 1] = '<'; magic[4, 1] = '"';
        magic[3, 1] = 'i'; magic[2, 1] = 'h'; magic[1, 1] = '"'; magic[0, 1] = 'v';
        magic[0, 2] = ':';
        magic[0, 3] = ',';
        magic[0, 4] = ',';
        magic[0, 5] = ',';
        magic[0, 6] = '@';*/
        /* magic[0, 0] = '>'; magic[1, 0] = ' '; magic[2, 0] = ' '; magic[3, 0] = ' ';
        magic[4, 0] = ' '; magic[5, 0] = ' '; magic[6, 0] = ' '; magic[7, 0] = ' ';
        magic[8, 0] = ' '; magic[9, 0] = ' '; magic[10, 0] = ' '; magic[11, 0] = ' ';
        magic[12, 0] = ' '; magic[13, 0] = ' '; magic[14, 0] = ' '; magic[15, 0] = 'v';
        magic[0, 1] = 'v'; magic[1, 1] = '"'; magic[2, 1] = 'H'; magic[3, 1] = 'e';
        magic[4, 1] = 'l'; magic[5, 1] = 'l'; magic[6, 1] = 'o'; magic[7, 1] = ' ';
        magic[8, 1] = 'W'; magic[9, 1] = 'o'; magic[10, 1] = 'r'; magic[11, 1] = 'l';
        magic[12, 1] = 'd'; magic[13, 1] = '!'; magic[14, 1] = '"'; magic[15, 1] = '<';
        magic[0, 2] = '>'; magic[1, 2] = ':'; magic[2, 2] = 'v';
        magic[0, 3] = '^'; magic[1, 3] = ','; magic[2, 3] = '_'; magic[3,3] = '@'; */

        Stack holder = new Stack(100);
        while(true) {
            
            /*Console.Write("Pointer x: " + pointer_x);
            Console.Write(" ");
            Console.Write("Pointer y: " + pointer_y);
            Console.Write(" ");*/
            char new_item = magic[pointer_y, pointer_x];
            /*Console.WriteLine("Command: " + new_item);
            Console.WriteLine();*/
            if (int.TryParse(Convert.ToString(new_item), out int value))
            {
                holder.push(value);
            }
            else if (new_item == '+')
            {
                holder.push(holder.pop() + holder.pop());
            }
            else if (new_item == '>') {
                (direction_x, direction_y) = (1, 0);
            }
            else if (new_item == '<')
            {
                (direction_x, direction_y) = (-1, 0);
            }
            else if (new_item == '^')
            {
                (direction_x, direction_y) = (0, -1);
            }
            else if (new_item == '@') {
                /*
                Console.WriteLine(holder.pop());
                Console.Write("CHAT WE ARE DOING IT WOOOOOOOOOOOO!");*/
                break;
            }
            else if (new_item == '&')
            {
                string? input5 = Console.ReadLine();
                if (input5 is null || input5 == "")
                {
                    break;
                }
                holder.push(int.Parse(input5));
            }
            else if (new_item == 'v')
            {
                (direction_x, direction_y) = (0, 1);
            }
            else if (new_item == '*')
            { 
                holder.push(holder.pop() * holder.pop());
            }
            else if (new_item == '-')
            {
                holder.push(holder.pop() - holder.pop());

            }else if (new_item == '/')
            {
                holder.push(holder.pop() / holder.pop());

            }else if (new_item == '%')
            {
                holder.push(holder.pop() % holder.pop());
            }else if (new_item == '!')
            {  
                if (holder.pop() == 0)
                {
                    holder.push(1);
                }else
                {
                    holder.push(0);
                }
            }else if (new_item == '~')
            {
                string? input4 = Console.ReadLine();
                if (input4 is null || input4 == "")
                {
                    break;
                }
                holder.push(input4[0]);
            }
            else if (new_item == '`') {
                int a = holder.pop();
                int b = holder.pop();
                holder.push(b > a ? 1 : 0);
            }else if (new_item == '#')
            {
                pointer_x = (pointer_x + direction_x) % 80;
                pointer_y = (pointer_y + direction_y) % 25;
            }
            else if (new_item == '"')
            {
                pointer_x = (pointer_x + direction_x) % 80;
                pointer_y = (pointer_y + direction_y) % 25;
                while (true)
                {
                    if (magic[pointer_y, pointer_x] == '"')
                    {
                        break;
                    }
                    holder.push(magic[pointer_y, pointer_x]);
                    pointer_x += direction_x;
                    pointer_y += direction_y;
                }
            }else if (new_item == '_')
            {
                int t = holder.pop();
                if (t == 0) {
                    (direction_x, direction_y) = (1, 0);
                }else
                {
                    (direction_x, direction_y) = (-1, 0);
                }
            }
            else if (new_item == '.')
            {
                Console.Write((int)holder.pop());
            }
            else if (new_item == ',') {
                Console.Write((char)holder.pop());
            }else if (new_item == ':')
            {
                int dup = holder.pop();
                holder.push(dup);
                holder.push(dup);
            }
            pointer_x = (pointer_x + direction_x) % 80;
            pointer_y = (pointer_y + direction_y) % 25;

        }
    }





}
