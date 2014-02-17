using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForCoursera
{
    public class Percolation
    {
        Cell[] cellArray;
        int cellsCount;

   
        public Percolation(int N)              // create N-by-N grid, with all sites blocked
        {
		    cellArray = new Cell[N^2 + 2];
            cellsCount = N;
		    for (int i = 0; i < cellArray.Length; i++)
            {
                cellArray[i] = new Cell(i);
            }
            cellArray[0].isOpen = true;
            cellArray[cellArray.Length - 1].isOpen = true;
        }
   
        public void open(int i, int j)         // open site (row i, column j) if it is not already
        {
            int number = RealNumber(i,j);		
            cellArray[number].isOpen = true;
            if (number <= cellsCount) Connect(number, 0);
                else Connect(number, number - cellsCount);
            if (number > cellArray.Length - 2 - cellsCount) Connect(number, cellArray.Length - 1);
                else Connect(number, number + cellsCount);
            if (number % cellsCount != 1) Connect(number, number - 1);
            if (number % cellsCount != 0) Connect(number, number + 1);
        }
   
        public bool isOpen(int i, int j)    // is site (row i, column j) open?
        {
	 	    return cellArray[RealNumber(i,j)].isOpen;
        }

        public bool isFull(int i, int j)    // is site (row i, column j) full?
        {
            if (CellRoot(RealNumber(i, j)) == CellRoot(0)) return true;
                else return false;
        }
       
        public bool percolates()            // does the system percolate?
        {
            if (CellRoot(0) == CellRoot(cellArray.Length - 1)) return true;
                else return false;
        }
   
        int CellRoot(int number)
        {
		    int rootNumber = cellArray[number].parent;
		    while (rootNumber != cellArray[rootNumber].parent)
		    {
			    rootNumber = cellArray[rootNumber].parent;
		    }
		    return rootNumber;
        }
   
        void Connect(int num1, int num2)
        {
            if (cellArray[num1].isOpen == cellArray[num2].isOpen == true)
                {
                int firstRoot = CellRoot(num1);
                int secondRoot = CellRoot(num2);
                if (firstRoot == secondRoot) return;
                if (cellArray[firstRoot].height > cellArray[secondRoot].height)
                {
                    cellArray[secondRoot].parent = firstRoot;
                    cellArray[firstRoot].height += cellArray[secondRoot].height;
                } else
                {
                    cellArray[firstRoot].parent = secondRoot;
                    cellArray[secondRoot].height += cellArray[firstRoot].height;
                }
            }
        }

        int RealNumber(int i, int j)
        {
            if ((i < 1)||(j < 1)||(i > cellsCount)||(j > cellsCount))
                throw new Exception("BOOM");
            return (i-1) * cellsCount + j;
        }
    }

    struct Cell
    {
	    public int parent;
	    public int height;
	    public bool isOpen;
	    public Cell(int number)
	    {
		    parent = number;
		    height = 1;
		    isOpen = false;
	    }
    }

}
