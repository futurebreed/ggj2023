using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid 
{
    // the overall width and height of the grid
    private int _width, _height;

    public int Width => _width;
    public int Height => _height;

    private GridCell[,] _cells;

    public Grid(int width, int height)
    {
        _width = width;
        _height = height;
        _cells = new GridCell[width, height];
    }

    public void SetCell(int x, int y, GridCell cell)
    {
        _cells[x, y] = cell;
    }

    public GridCell GetCell(int x, int y)
    {
        return _cells[x, y];
    }

    public TCell GetCell<TCell>(int x, int y) where TCell : GridCell
    {
        return (TCell)_cells[x, y];
    }
}

