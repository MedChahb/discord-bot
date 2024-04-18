using DSharpPlus.Entities;
using System;
using System.Runtime.Remoting.Messaging;


namespace DiscordBot.games
{
    public class TicTacToe
    {
        public char[,] XOPlacements = new char[3, 3];

        public bool isNotStarted = true;
        public bool isWin = false;
        public bool isDraw = false;

        public DiscordMember starter;
        public DiscordMember versus;

        public DiscordMember turn;

        public DiscordMember winner;

        public TicTacToe()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.XOPlacements[i,j] = '•';
                }
            }
        }

        // start from 1 to 3
        public void PlaceX(int col, int row)
        {
            this.XOPlacements[row, col] = 'X';
            if (CheckIfFinished() == 1) { this.winner = starter; this.isWin = true; }
            if (CheckIfFinished() == -1) this.isDraw = true;
        }
        public void PlaceO(int col, int row)
        {
            this.XOPlacements[row, col] = 'O';
            if (CheckIfFinished() == 1) {this.winner = versus; this.isWin = true;}
            if (CheckIfFinished() == -1) this.isDraw = true;
        }

        public void ChangeTurn() => this.turn = (turn.Id == starter.Id) ? versus: starter;
        public int CheckIfFinished()
        {
            // Check rows
            for (int i = 0; i < 3; i++)
            {
                if (XOPlacements[i, 0] != '•' && XOPlacements[i, 0] == XOPlacements[i, 1] && XOPlacements[i, 1] == XOPlacements[i, 2])
                    return 1;
            }

            // Check columns
            for (int j = 0; j < 3; j++)
            {
                if (XOPlacements[0, j] != '•' && XOPlacements[0, j] == XOPlacements[1, j] && XOPlacements[1, j] == XOPlacements[2, j])
                    return 1;
            }

            // Check diagonals
            if (XOPlacements[0, 0] != '•' && XOPlacements[0, 0] == XOPlacements[1, 1] && XOPlacements[1, 1] == XOPlacements[2, 2])
                return 1;

            if (XOPlacements[0, 2] != '•' && XOPlacements[0, 2] == XOPlacements[1, 1] && XOPlacements[1, 1] == XOPlacements[2, 0])
                return 1;

            // Check if all cells are filled (draw)
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (XOPlacements[j, i] == '•')
                        return 0; // There is at least one empty cell
                }
            }

            return -1; // If all cells are filled and no player won, it's a draw
        }
        public void SetStarter(DiscordMember pl) => this.starter = pl;
        public void SetVersus(DiscordMember pl) => this.versus = pl;
        public void SetFirstTurn(DiscordMember pl) => this.turn = pl;
        public void HasStarted() => this.isNotStarted = false;
        public bool isCellEmpty(int col, int row) { return this.XOPlacements[row, col] == '•'; }
        public bool Draw() { return this.isDraw; }
        public bool Win()  { return this.isWin; }
        public string DisplayXOGrid()
        {
            string grid = "";

            for (int j = 0; j < 3; j++)
            {
                grid += $"{XOPlacements[j, 0]} | {XOPlacements[j, 1]} | {XOPlacements[j, 2]}\n";
                if (j != 2)
                    grid += "---------\n";

                Console.WriteLine(grid);
            }
            return grid;
        }
    }
}
