using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Snake
{
    public partial class MainWindow : Window
    {
        private readonly Dictionary<GridValue, ImageSource> gridValToImage = new()
        {
            { GridValue.Empty, Images.Empty },
            { GridValue.Snake, Images.SnakeBody },
            { GridValue.Food, Images.Food }
        };

        private readonly int rows = 10, cols = 10;
        private readonly Image[,] gridImages;
        private readonly GameState gameState;

        public MainWindow()
        {
            InitializeComponent();
            gridImages = SetupGrid();
            gameState = new GameState(rows, cols);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Wyświetlenie planszy po załadowaniu
            Draw();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // Zmieniamy kierunek węża na podstawie klawiszy
            if (e.Key == Key.Up)
            {
                gameState.ChangeDirection(Direction.Up);
            }
            else if (e.Key == Key.Down)
            {
                gameState.ChangeDirection(Direction.Down);
            }
            else if (e.Key == Key.Left)
            {
                gameState.ChangeDirection(Direction.Left);
            }
            else if (e.Key == Key.Right)
            {
                gameState.ChangeDirection(Direction.Right);
            }

            // Ruch węża
            gameState.Move();

            // Rysujemy planszę po każdym ruchu
            Draw();
        }

        private Image[,] SetupGrid()
        {
            Image[,] images = new Image[rows, cols];
            GameGrid.Rows = rows;
            GameGrid.Columns = cols;

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    Image img = new Image
                    {
                        Source = Images.Empty, // Domyślnie wszystkie komórki są puste
                        Margin = new Thickness(1.8)
                    };
                    images[r, c] = img;
                    GameGrid.Children.Add(img);
                }
            }
            return images;
        }

        private void Draw()
        {
            // Po każdym ruchu, rysujemy planszę, aby zaktualizować widok
            DrawGrid();
        }

        private void DrawGrid()
        {
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    // Sprawdzamy stan komórki w gameState.Grid i rysujemy odpowiedni obrazek
                    GridValue gridVal = gameState.Grid[r, c];
                    gridImages[r, c].Source = gridValToImage[gridVal];
                }
            }
        }
    }
}
