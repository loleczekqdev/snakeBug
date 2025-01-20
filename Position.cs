﻿
namespace Snake
{
    internal class Position
    {
        public int Row { get; }
        public int Col { get; }

        public Position(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }
        public Position Translate(Direction direction)
        {
            return new Position(this.Row + direction.RowOffset, this.Col + direction.ColOffset);
        }

        public override bool Equals(object? obj)
        {
            return obj is Position position &&
                   Row == position.Row &&
                   Col == position.Col;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Col);
        }

        public static bool operator ==(Position? left, Position? right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position? left, Position? right)
        {
            return !(left == right);
        }
    }
}
