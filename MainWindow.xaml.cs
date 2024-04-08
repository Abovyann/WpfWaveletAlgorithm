using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            WaveletAlgorithm algorithm = new WaveletAlgorithm();
            algorithm.Run();
            UpdateGridDisplay(algorithm.grid);
        }

        private void UpdateGridDisplay(int[,] grid)
        {
            List<int> cellValues = new List<int>();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    cellValues.Add(grid[i, j]);
                }
            }
            itemsControl.ItemsSource = cellValues;
        }


        // WaveletAlgorithm Class Definition
        class WaveletAlgorithm
        {
            // Define the grid size and barrier locations
            const int GridSize = 10;
            public readonly int[,] grid = new int[GridSize, GridSize];
            readonly List<(int, int)> barriers = new List<(int, int)> { (3, 3), (3, 4), (3, 5) };

            // Source and target coordinates
            (int, int) source = (0, 0);
            (int, int) target = (9, 9);

            public void Run()
            {
                InitializeGrid();
                if (FindPath())
                {
                    // Console.WriteLine("Path found!"); // Remove or comment out 
                    // PrintGrid(); // Remove or comment out
                }
                else
                {
                    // Console.WriteLine("No path found."); // Remove or comment out
                }
            }

            void InitializeGrid()
            {
                // Set barriers
                foreach (var (x, y) in barriers)
                {
                    grid[x, y] = -1;
                }
                // Set source
                grid[source.Item1, source.Item2] = 1;
            }

            bool FindPath()
            {
                Queue<(int, int)> queue = new Queue<(int, int)>();
                queue.Enqueue(source);
                while (queue.Count > 0)
                {
                    var (x, y) = queue.Dequeue();
                    int value = grid[x, y];
                    // Check neighbors
                    foreach (var (dx, dy) in new[] { (0, 1), (1, 0), (0, -1), (-1, 0) })
                    {
                        int newX = x + dx;
                        int newY = y + dy;
                        if (newX >= 0 && newX < GridSize && newY >= 0 && newY < GridSize && grid[newX, newY] == 0)
                        {
                            grid[newX, newY] = value + 1;
                            if ((newX, newY) == target) return true; // Target reached
                            queue.Enqueue((newX, newY));
                        }
                    }
                }
                return false; // No path found
            }
        }
    }
}
