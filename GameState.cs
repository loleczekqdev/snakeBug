using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class GameState
    {
        public int Rows { get; }
        public int Cols { get; }
        public GridValue[,] Grid { get; }
        public Direction Dir { get; private set; }
        public int Score { get; private set; }
        public bool GameOver { get; private set; }

        public readonly LinkedList<Position> snakePositions = new LinkedList<Position>();
        private readonly Random random = new Random();

        public GameState(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Grid = new GridValue[rows, cols];
            Dir = Direction.Right;

            AddSnake();  // Dodanie węża do gry
            AddFood();   // Dodanie jedzenia
        }

        private void AddSnake()
        {
            int r = Rows / 2;

            // Dodanie węża do planszy w wybranej pozycji (początkowa pozycja węża)
            for (int c = 1; c <= 3; c++)
            {
                Grid[r, c] = GridValue.Snake;  // Wstawienie węża do komórek
                snakePositions.AddFirst(new Position(r, c));  // Dodanie pozycji węża do listy
            }
        }

        private IEnumerable<Position> EmptyPositions()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    if (Grid[r, c] == GridValue.Empty)
                    {
                        yield return new Position(r, c);
                    }
                }
            }
        }

        private void AddFood()
        {
            // Dodanie jedzenia w pustym miejscu
            List<Position> empty = new List<Position>(EmptyPositions());
            if (empty.Count == 0) return;  // Jeżeli brak miejsca na planszy, nic nie dodawaj

            Position pos = empty[random.Next(empty.Count)];
            Grid[pos.Row, pos.Col] = GridValue.Food;  // Dodanie jedzenia do planszy
        }


        public Position HeadPosition() => snakePositions.First.Value;

        public Position TailPosition() => snakePositions.Last.Value;

        public IEnumerable<Position> SnakePositions() => snakePositions;

        private void AddHead(Position pos)
        {
            snakePositions.AddFirst(pos);
            Grid[pos.Row, pos.Col] = GridValue.Snake;
        }

        private void RemoveTail()
        {
            Position tail = snakePositions.Last.Value;
            Grid[tail.Row, tail.Col] = GridValue.Empty;
            snakePositions.RemoveLast();
        }

        public void ChangeDirection(Direction dir)
        {
            Dir = dir;
        }

        private bool OutsideGrid(Position pos)
        {
            return pos.Row < 0 || pos.Row >= Rows || pos.Col < 0 || pos.Col >= Cols;
        }

        private GridValue WillHit(Position newHeadPos)
        {
            if (OutsideGrid(newHeadPos)) return GridValue.Outside;
            if (newHeadPos == TailPosition()) return GridValue.Empty;

            return Grid[newHeadPos.Row, newHeadPos.Col];
        }

        public void Move()
        {
            Position newHeadPos = HeadPosition().Translate(Dir);
            GridValue hit = WillHit(newHeadPos);

            if (hit == GridValue.Outside || hit == GridValue.Snake)
            {
                GameOver = true;
            }
            else if (hit == GridValue.Empty)
            {
                RemoveTail();
                AddHead(newHeadPos);
            }
            else if (hit == GridValue.Food)
            {
                Score++;
                AddHead(newHeadPos);
                AddFood();
            }
        }


    }

}
