using System;
using System.ComponentModel;
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

class Field{
    public void Instruction_field_printer(char[,] field)
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

    public void Field_initialization(char[,] field, int line, string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            field[line, i] = input[i];

        }
    }
}
}
