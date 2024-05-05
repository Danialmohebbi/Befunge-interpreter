**Befunge Interpreter**  
---  
**How to use**
---
`*` Before proceeding, ensure you are familiar with the `instructions` applicable to
`Befunge 93`, as opposed to `Befunge 98`. The nuances between the two versions
can affect program behavior significantly. 
  
`*` Ensure that the content adheres to the syntax and regulations of `Befunge 93`.
Specifically, verify that the file comprises a maximum of `25 lines`, with each line containing no more than `80 characters`.
Additionally, confirm that the file is located in the `current directory`.  
  
`*` Execute the instructions provided in a file by passing it as a command-line
argument in the following format: `dotnet run [FileName]`. 

  
**Sample hello-world.txt**  
---
```
>              v
v"Hello World!"<
>:v
^,_@
```
`*` The initial `>` instruction sets the direction of the pointer to move right.As the pointer encounters `v`, it changes direction downward and subsequently encounters `<`, making it move left.      
  
`*` Upon encountering `"`, the pointer pushes the `ASCII` codes of the characters `"Hello World!"` onto the stack.  
  
`*` When the pointer encounters `v`, it enters a loop:  
```
>:v
^,_@
```
  
`*` This loop prints `characters` from the stack until it's `empty` and then terminating. It's akin to the following `Python` snippet:
  
```
while not stack.isEmpty():
  print(char(stack.pop()))
```
