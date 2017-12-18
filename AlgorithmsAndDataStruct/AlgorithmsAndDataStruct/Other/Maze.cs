using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

public class Maze
{
    public static void Test()
    {
        Maze maze = new Maze();
        int width = 300;
        int height = 200;
        MazeCell[][] cellMatrix = maze.GenerateByPrim(height, width);
        List<string> mazeData = new List<string>();
        for (int i = 0; i < height; i++)
        {
            string oneline = "";
            for (int j = 0; j < width; ++j)
            {
                MazeCell iter = cellMatrix[i][j];
                oneline += (iter.m_leftCanThrought == 1).ToString() + (iter.m_upCanThrought == 1) + (iter.m_rightCanThrought == 1) + (iter.m_downCanThrought == 1) + "\t";
            }
            mazeData.Add(oneline);
            Debug.Log(oneline);
        }
        
        string fileName = "mazeData" + Debug.GetTime() + ".txt";
        File.WriteAllLines(fileName, mazeData.ToArray());

        Draw(cellMatrix, height, width);

        Debug.Log("Maze end");
    }

    public class MazeCell
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
    }

    public static void Draw(MazeCell[][] cellList, int num_rows, int num_cols)
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
        string fileName = "maze" + Debug.GetTime() + ".bmp";
        bitmap.Save(fileName);
    }

    //基于随机Prim的迷宫生成算法
    public MazeCell[][] GenerateByPrim(int num_rows, int num_cols)
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
}

