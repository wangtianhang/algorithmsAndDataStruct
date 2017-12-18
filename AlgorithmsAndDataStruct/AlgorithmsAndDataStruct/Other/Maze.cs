using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

public class Maze
{
    public static void Test()
    {
        Random.SetSeed(0);

        int width = 300;
        int height = 200;
        
        string time = Debug.GetTime();
        string fileName = "maze" + time + ".bmp";
        string fullPath = Directory.GetCurrentDirectory() + "/" + fileName;
        string pathFileName = "mazePath" + time + ".bmp";
        GenMaze(width, height, fullPath, pathFileName);
    }

    public static void GenMaze(int width, int height, string filePath, string pathFilePath)
    {
        Maze maze = new Maze();

        MazeCell[][] cellMatrix = maze.GenerateByPrim(height, width);
        //List<string> mazeData = new List<string>();
        for (int i = 0; i < height; i++)
        {
            string oneline = "";
            for (int j = 0; j < width; ++j)
            {
                MazeCell iter = cellMatrix[i][j];
                oneline += (iter.m_leftCanThrought == 1).ToString() + (iter.m_upCanThrought == 1) + (iter.m_rightCanThrought == 1) + (iter.m_downCanThrought == 1) + "\t";
            }
            //mazeData.Add(oneline);
            Debug.Log(oneline);
        }

        //File.WriteAllLines(fileName, mazeData.ToArray());

        Bitmap bitmap = Draw(cellMatrix, height, width);
        bitmap.Save(filePath);
        List<MazeCell> path = maze.GothroughMazeByBacktracing(cellMatrix, height, width);
        foreach(var iter in path)
        {
            Debug.Log("pathPoint " + iter.m_r + " " + iter.m_c);
        }
        Bitmap pathMap = DrawPath(cellMatrix, height, width, bitmap, path);
        pathMap.Save(pathFilePath);

        Debug.Log("Maze end " + filePath);
        Debug.Log("Maze end2 " + pathFilePath);
    }

    class MazeCell
    {
        //public int m_x = 0;
        //public int m_y = 0;
        public int m_r = 0;
        public int m_c = 0;
        public int m_leftCanThrought = 0; // index 0
        public int m_upCanThrought = 0; // index 1
        public int m_rightCanThrought = 0; // index 2
        public int m_downCanThrought = 0; // index 3
        public int m_isVisited = 0; // index 4

        //public bool m_goThroughHasVisited = false;
        //public bool m_isGothrough = false;
        public bool m_hasThroughLeft = false;
        public bool m_hasThroughUp = false;
        public bool m_hasThroughRight = false;
        public bool m_hasThroughDown = false;
    }

    static Bitmap Draw(MazeCell[][] cellList, int num_rows, int num_cols)
    {
        // 每个格子占用4 * 4个像素
        int size = 8;
        Bitmap bitmap = new Bitmap(num_cols * size + 4, num_rows * size + 4);

        //================画边框==========================
        for (int i = 0; i < num_cols * size + 4; i++)
        {
            for (int j = 0; j < 2; ++j )
            {
                bitmap.SetPixel(i, j, Color.Black);
            }
        }

        for (int i = 0; i < num_cols * size + 4; i++)
        {
            for (int j = num_rows * size + 2; j < num_rows * size + 4; ++j)
            {
                bitmap.SetPixel(i, j, Color.Black);
            }
        }

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < num_rows * size + 4; ++j)
            {
                bitmap.SetPixel(i, j, Color.Black);
            }
        }

        for (int i = num_cols * size + 2; i < num_cols * size + 4; i++)
        {
            for (int j = 0; j < num_rows * size + 4; ++j)
            {
                bitmap.SetPixel(i, j, Color.Black);
            }
        }

        //=================画边框结束=============================
        int offsetX = 2;
        int offsetY = 2;
        for (int i = 0; i < num_rows; i++)
        {
            for (int j = 0; j < num_cols; ++j)
            {
//                 if(i != 0)
//                 {
//                      continue;
//                 }


                MazeCell iter = cellList[i][j];
                if(iter.m_leftCanThrought != 1)
                {
                    for (int x = iter.m_c * size; x < iter.m_c * size + 2; ++x)
                    {
                        for (int y = iter.m_r * size; y < iter.m_r * size + 8; ++y)
                        {
                            //bitmap.SetPixel(offsetX + x, offsetY + y, Color.Red);
                            bitmap.SetPixel(offsetX + x, offsetY + y, Color.Black);
                        }
                    }
                }
                if (iter.m_upCanThrought != 1)
                {
                    for (int x = iter.m_c * size; x < iter.m_c * size + 8; ++x)
                    {
                        for (int y = iter.m_r * size; y < iter.m_r * size + 2; ++y)
                        {
                            //bitmap.SetPixel(offsetX + x, offsetY + y, Color.Blue);
                            bitmap.SetPixel(offsetX + x, offsetY + y, Color.Black);
                        }
                    }
                }
                if (iter.m_rightCanThrought != 1)
                {
                    for (int x = iter.m_c * size + 6; x < iter.m_c * size + 8; ++x)
                    {
                        for (int y = iter.m_r * size; y < iter.m_r * size + 8; ++y)
                        {
                            //bitmap.SetPixel(offsetX + x, offsetY + y, Color.Green);
                            bitmap.SetPixel(offsetX + x, offsetY + y, Color.Black);
                        }
                    }
                }
                if (iter.m_downCanThrought != 1)
                {
                    for (int x = iter.m_c * size; x < iter.m_c * size + 8; ++x)
                    {
                        for (int y = iter.m_r * size + 6; y < iter.m_r * size + 8; ++y)
                        {
                            //bitmap.SetPixel(offsetX + x, offsetY + y, Color.Yellow);
                            bitmap.SetPixel(offsetX + x, offsetY + y, Color.Black);
                        }
                    }
                }

                ///填充四角
                for (int x = iter.m_c * size; x < iter.m_c * size + 2; ++x)
                {
                    for (int y = iter.m_r * size; y < iter.m_r * size + 2; ++y)
                    {
                        bitmap.SetPixel(offsetX + x, offsetY + y, Color.Black);
                    }
                }
                for (int x = iter.m_c * size + 6; x < iter.m_c * size + 8; ++x)
                {
                    for (int y = iter.m_r * size; y < iter.m_r * size + 2; ++y)
                    {
                        bitmap.SetPixel(offsetX + x, offsetY + y, Color.Black);
                    }
                }
                for (int x = iter.m_c * size; x < iter.m_c * size + 2; ++x)
                {
                    for (int y = iter.m_r * size + 6; y < iter.m_r * size + 8; ++y)
                    {
                        bitmap.SetPixel(offsetX + x, offsetY + y, Color.Black);
                    }
                }
                for (int x = iter.m_c * size + 6; x < iter.m_c * size + 8; ++x)
                {
                    for (int y = iter.m_r * size + 6; y < iter.m_r * size + 8; ++y)
                    {
                        bitmap.SetPixel(offsetX + x, offsetY + y, Color.Black);
                    }
                }
                ///填充四角结束
            }
        }

        //string dir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);  
        //string fileName = "maze" + Debug.GetTime() + ".bmp";
        //bitmap.Save(filePath);
        return bitmap;
    }

    static Bitmap DrawPath(MazeCell[][] cellList, int num_rows, int num_cols, Bitmap bitmap, List<MazeCell> path)
    {
        int offsetX = 2;
        int offsetY = 2;
        int size = 8;

        for (int i = 0; i < num_rows; i++)
        {
            for (int j = 0; j < num_cols; ++j)
            {
                MazeCell iter = cellList[i][j];
                if (path.Contains(iter))
                {
                    for (int x = iter.m_c * size + 2; x < iter.m_c * size + 6; ++x)
                    {
                        for (int y = iter.m_r * size + 2; y < iter.m_r * size + 6; ++y)
                        {
                            bitmap.SetPixel(offsetX + x, offsetY + y, Color.Green);
                        }
                    }
                }
            }
        }
        return bitmap;
    }

    //基于随机Prim的迷宫生成算法
    MazeCell[][] GenerateByPrim(int num_rows, int num_cols)
    {
        //------>x
        // |
        // |
        // |
        // Y
        MazeCell[][] cellMatrix = new MazeCell[num_rows][];
        for (int i = 0; i < num_rows; ++i )
        {
            cellMatrix[i] = new MazeCell[num_cols];
        }
        for (int i = 0; i < num_rows; ++i)
        {
            for (int j = 0; j < num_cols; ++j )
            {
                cellMatrix[i][j] = new MazeCell();
                cellMatrix[i][j].m_r = i;
                cellMatrix[i][j].m_c = j;
            }
        }

        // Set starting row and column
        int r = 0;
        int c = 0;
        List<MazeCell> historyStack = new List<MazeCell>();
        historyStack.Add(cellMatrix[0][0]);

        // Trace a path though the cells of the maze and open walls along the path.
        // We do this with a while loop, repeating the loop until there is no history, 
        // which would mean we backtracked to the initial start.
        while (historyStack.Count != 0)
        {
            //random choose a candidata cell from the cell set histroy
            int random = Random.Range(0, historyStack.Count);
            r = historyStack[random].m_r;
            c = historyStack[random].m_c;
            historyStack[random].m_isVisited = 1;
            historyStack.RemoveAt(random);
            StringBuilder check = new StringBuilder();
            // If the randomly chosen cell has multiple edges 
            // that connect it to the existing maze, 
            if(c > 0)
            {
                if(cellMatrix[r][c - 1].m_isVisited == 1)
                {
                    check.Append('L');
                }
                else if (cellMatrix[r][c - 1].m_isVisited == 0)
                {
                    historyStack.Add(cellMatrix[r][c - 1]);
                    cellMatrix[r][c - 1].m_isVisited = 2;
                }
            }
            if(r > 0)
            {
                if(cellMatrix[r-1][c].m_isVisited == 1)
                {
                    check.Append('U');
                }
                else if (cellMatrix[r - 1][c].m_isVisited == 0)
                {
                    historyStack.Add(cellMatrix[r - 1][c]);
                    cellMatrix[r - 1][c].m_isVisited = 2;
                }
            }
            if (c < num_cols - 1)
            {
                if (cellMatrix[r][c + 1].m_isVisited == 1)
                {
                    check.Append('R');
                }
                else if (cellMatrix[r][c + 1].m_isVisited == 0)
                {
                    historyStack.Add(cellMatrix[r][c + 1]);
                    cellMatrix[r][c + 1].m_isVisited = 2;
                }
            }
            if(r < num_rows - 1)
            {
                if (cellMatrix[r + 1][c].m_isVisited == 1)
                {
                    check.Append('D');
                }
                else if (cellMatrix[r + 1][c].m_isVisited == 0)
                {
                    historyStack.Add(cellMatrix[r + 1][c]);
                    cellMatrix[r + 1][c].m_isVisited = 2;
                }
            }

            // select one of these edges at random.
            if(check.Length != 0)
            {
                int random2 = Random.Range(0, check.Length);
                char move_direction = check[random2];
                if(move_direction == 'L')
                {
                    cellMatrix[r][c].m_leftCanThrought = 1;
                    c = c - 1;
                    cellMatrix[r][c].m_rightCanThrought = 1;
                }
                if (move_direction == 'U')
                {
                    cellMatrix[r][c].m_upCanThrought = 1;
                    r = r -1;
                    cellMatrix[r][c].m_downCanThrought = 1;
                }
                if (move_direction == 'R')
                {
                    cellMatrix[r][c].m_rightCanThrought = 1;
                    c = c + 1;
                    cellMatrix[r][c].m_leftCanThrought = 1;
                }
                if (move_direction == 'D')
                {
                    cellMatrix[r][c].m_downCanThrought = 1;
                    r = r + 1;
                    cellMatrix[r][c].m_upCanThrought = 1;
                }
            }
        }

        // Open the walls at the start and finish
        cellMatrix[0][0].m_leftCanThrought = 1;
        cellMatrix[num_rows - 1][num_cols - 1].m_rightCanThrought = 1;

        return cellMatrix;
    }

    List<MazeCell> GothroughMazeByBacktracing(MazeCell[][] matrix, int row, int col)
    {
        List<MazeCell> pathStack = new List<MazeCell>();
        pathStack.Add(matrix[0][0]);
        //MazeCell last = null;
        Debug.Log("push " + 0 + " " + 0);
        while(pathStack.Count != 0)
        {
            MazeCell top = pathStack[pathStack.Count - 1];
            if (top.m_r == row - 1 && top.m_c == col - 1)
            {
                return pathStack;
            }
            else
            {
                //如果当前路径可以通过
                if(top.m_leftCanThrought == 1 && !top.m_hasThroughLeft)
                {
                    top.m_hasThroughLeft = true;
                    if (top.m_c - 1 >= 0)
                    {
                        MazeCell next = matrix[top.m_r][top.m_c - 1];
                        MazeCell last = GetLastCell(pathStack);
                        if (next != last)
                        {
                            Debug.Log("push " + next.m_r + " " + next.m_c);
                            pathStack.Add(next);
                            //last = top;
                        }

                    }
                }
                else if(top.m_upCanThrought == 1 && !top.m_hasThroughUp)
                {
                    top.m_hasThroughUp = true;
                    if (top.m_r - 1 >= 0)
                    {
                        MazeCell next = matrix[top.m_r - 1][top.m_c];
                        MazeCell last = GetLastCell(pathStack);
                        if (next != last)
                        {
                            Debug.Log("push " + next.m_r + " " + next.m_c);
                            pathStack.Add(next);
                            //last = top;
                        }

                    }
                }
                else if(top.m_rightCanThrought == 1 && !top.m_hasThroughRight)
                {
                    top.m_hasThroughRight = true;
                    if (top.m_c + 1 < col)
                    {
                        MazeCell next = matrix[top.m_r][top.m_c + 1];
                        MazeCell last = GetLastCell(pathStack);
                        if (next != last)
                        {
                            Debug.Log("push " + next.m_r + " " + next.m_c);
                            pathStack.Add(next);
                            //last = top;
                        }

                    }
                }
                else if (top.m_downCanThrought == 1 && !top.m_hasThroughDown)
                {
                    top.m_hasThroughDown = true;
                    if (top.m_r + 1 < row)
                    {
                        MazeCell next = matrix[top.m_r + 1][top.m_c];
                        MazeCell last = GetLastCell(pathStack);
                        if (next != last)
                        {
                            Debug.Log("push " + next.m_r + " " + next.m_c);
                            pathStack.Add(next);
                            //last = top;
                        }

                    }
                }
                else
                {
                    MazeCell pop = pathStack[pathStack.Count - 1];
                    pathStack.RemoveAt(pathStack.Count - 1);
                    Debug.Log("pop " + pop.m_r + " " + pop.m_c);
                    
                }
            }
        }
        return null;
    }

    MazeCell GetLastCell(List<MazeCell> pathStack)
    {
        int index = pathStack.Count - 2;
        if(index >= 0)
        {
            return pathStack[pathStack.Count - 2];
        }
        else
        {
            return null;
        }
    }
}

