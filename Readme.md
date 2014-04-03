WordSearchProg - Generates a fiendishly difficult word search matrix
====================================================================

Features
--------
This is a word search game in which the target word will appear in the grid exactly once. The grid is filled only with letters from within the target word.


Library Usage
----------------------------------------------------------

```csharp

var MyGenerator = new WordSearchLib.WordSearchGenerator("dog", 3, 3);
WordSearchGenerator.PrintGrid(MyGenerator.GenerateMatrix().Matrix);
```

Console Usage
----------------------------------------------------------

```csharp
>WordSearchProg.exe dog 6 6
o g o d d o
g d d o d g
g o o d g g
o d o o d g
o d g d d g
o d d g g d
```
Limitations and caveats
-----------------------
Generation of grid uses a backtracking algorithm rather than brute force. Haven't encountered any issues as of yet, but I don't vouch for it!

Author
------
Gareth C