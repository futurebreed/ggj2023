## Overview of the rootimentary level format

The rootimentary level format is a simple text-based format for describing levels. It is designed to be easy to read and write, and to be easy to parse. It is not efficient nor is it the most "Unity" like. But it works :)

## Level format

Levels should be stored in a folder called `Levels` in the `StreamingAssets` folder. They should be txt files, following the naming convention Level{Number}.txt. For example, the first level should be called Level1.txt, the second Level2.txt, and so on. Level0.txt will be loaded by the
MazeTest/GridGenerator prefab in debug mode for now.

The text files themselves are simple, comma delimited text files. They should be a power of two in order for the grid to be square (currently 32x16). Each line of the file represents a row of the grid. Each cell is represented by a single character. The following characters are supported:

* 'E' - Empty cell
* 'C' - Debug cube
* 'S' - Debug sphere
* 'X' - Maze exit
* 'D' - Dirt
* 'R' - Rock

An example level looks like:

```
D,D,D,D,D,D,D,D,D,D,D,D,D,E,E,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D
D,D,D,D,D,D,D,D,D,D,D,D,D,E,E,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D
D,D,D,D,D,D,D,D,D,D,D,D,D,E,E,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D
D,D,D,D,D,D,D,D,D,D,D,D,D,E,E,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D
D,D,D,D,D,R,D,D,D,D,D,D,D,E,E,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D
D,D,D,D,D,D,D,D,D,D,D,D,D,E,E,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D
D,D,D,D,D,D,D,D,D,D,D,D,D,E,E,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D
D,D,D,D,D,D,D,D,D,D,D,D,D,E,E,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D
D,D,D,D,D,D,D,D,D,D,D,D,D,E,E,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D
D,D,D,D,D,D,D,D,D,D,D,D,D,E,E,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D
D,D,D,D,D,D,D,D,D,D,D,D,D,E,E,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D
D,D,D,D,D,D,D,D,D,D,D,D,D,E,E,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D
D,D,D,D,D,D,D,D,R,D,D,D,D,E,E,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D
D,D,D,D,D,D,D,D,D,D,D,D,D,E,E,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D
D,D,D,D,D,D,D,D,D,D,D,D,D,E,E,D,D,D,D,D,D,D,D,D,D,D,R,D,D,D,D,D
D,D,D,D,D,D,D,D,D,D,D,D,D,X,X,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D,D
```

![example-level-sc](example-level-sc.png)