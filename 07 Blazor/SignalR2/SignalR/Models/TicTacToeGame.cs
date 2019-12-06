using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR.Models
{
    public class TicTacToeGame
    {
        char[,] fields;

        public TicTacToeGame(int id, string connectionId)
        {
            this.Id = id;
            fields = new char[,] { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };
            this.Status = GameStatus.Initialized;
            this.Next = 'X';
            this.ConnectionX = connectionId;
        }

        public int Id { get; protected set; }
        public string ConnectionX { get; protected set; }
        public string ConnectionO { get; protected set; }

        public enum GameStatus { Initialized, Running, Ended };

        public GameStatus Status { get; protected set; }

        public char Winner { get; protected set; }

        public char Next { get; protected set; }

        public char Join(string connectionId)
        {
            if (Status != GameStatus.Initialized)
                return ' ';

            Status = GameStatus.Running;
            this.ConnectionO = connectionId;

            return 'O';
        }
        
        public void SetField(int x, int y, char ch)
        {
            if (Status != GameStatus.Running)
                throw new Exception("invalid status");

            if (ch != Next)
                throw new Exception("Not your turn");

            Next = (ch == 'O') ? 'X' : 'O';

            if ((x < 0) || (x >= 3) || (y < 0) || (y >= 3))
                throw new Exception("Invalid field");

            if (fields[x, y] != ' ')
                throw new Exception("Field is not empty");

            fields[x, y] = ch;

            if (((fields[0, 0] == ch) && (fields[1, 1] == ch) && (fields[2, 2] == ch)) ||
                ((fields[0, 2] == ch) && (fields[1, 1] == ch) && (fields[2, 0] == ch)))
            {
                Status = GameStatus.Ended;
                Winner = ch;
            }
            else
                for (int i=0; i<3; i++)
                    if (((fields[i, 0] == ch) && (fields[i, 1] == ch) && (fields[i, 2] == ch)) ||
                        ((fields[0, i] == ch) && (fields[1, i] == ch) && (fields[2, i] == ch)))
                    {
                        Status = GameStatus.Ended;
                        Winner = ch;
                    }

            if (Status != GameStatus.Ended)
            {
                var ended = true;
                foreach (var f in fields)
                    if (f == ' ')
                        ended = false;

                if (ended)
                {
                    Status = GameStatus.Ended;
                    Winner = ' ';
                }
            }
        }

        public static Dictionary<int, TicTacToeGame> Games = new Dictionary<int, TicTacToeGame>();
    }

}