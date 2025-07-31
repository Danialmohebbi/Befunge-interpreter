# 🧠 Befunge Interpreter

A simple interpreter for running **Befunge-93** programs using `.NET`.

---

## 📘 How to Use

Before running a Befunge program, make sure of the following:

- ✅ You're using **[Befunge-93](https://codeberg.org/catseye/Befunge-93/src/branch/master/doc/Befunge-93.markdown)**, **not** [Befunge-98](https://esolangs.org/wiki/Funge-98).
  - Befunge-93 has a smaller instruction set and runs on a fixed-size 2D grid.
- ✅ Your code file:
  - Contains **no more than 25 lines**
  - Has **no more than 80 characters per line**
  - Is saved in the **current working directory**

### ▶️ Run the Program
To execute a Befunge program, run:

```bash
dotnet run [FileName]
```

Replace `[FileName]` with the name of your `.txt` file (e.g. `hello-world.txt`).

---

## 📄 Sample: `hello-world.txt`

```befunge
>              v
v"Hello World!"<
>:v
^,_@
```

### 🔍 Explanation

1. `>` moves the instruction pointer **right**.
2. It hits `v` and moves **down**, then encounters `<` and turns **left**.
3. The `"` enters **string mode** and pushes `"Hello World!"` to the stack as ASCII values (in reverse).
4. Execution enters this loop:
   ```befunge
   >:v
   ^,_@
   ```
   - `:` duplicates the top value of the stack
   - `,` pops and prints it as an ASCII character
   - `_` pops a value and chooses direction based on whether it’s 0
   - `@` exits the program

### 🧠 Equivalent Logic in Python

```python
while not stack.is_empty():
    print(chr(stack.pop()), end="")
```

---

## 📎 Notes

- Befunge’s instruction pointer moves in **2D**, so visualizing flow is important.
- Stack-based control and flow direction change commands (`^`, `v`, `<`, `>`) are key to Befunge logic.
- Always validate your program within **Befunge-93 constraints** before running.

---

Happy coding in two dimensions! 🎉
