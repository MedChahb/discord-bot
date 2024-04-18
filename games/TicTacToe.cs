using DSharpPlus.Entities;


namespace DiscordBot.games
{
    public class TicTacToe
    {
        public char[,] XOPlacements = new char[3, 3];
        public bool isFinish = false;
        public bool isFirstRound = true;

        public DiscordMember starter;
        public DiscordMember versus;

        public TicTacToe()
        {
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    this.XOPlacements[j,i] = '•';
                }
            }
        }

        public void PlaceChar(char c, int col, int row) => this.XOPlacements[col, row] = c;
        public void PlaceX(int col, int row) => this.XOPlacements[col, row] = 'X';
        public void PlaceO(int col, int row) => this.XOPlacements[col, row] = 'O';

        public void CheckIfFinished()
        {

        }

        public void SetStarter(DiscordMember pl) => this.starter = pl;
        public void SetVersus(DiscordMember pl) => this.versus = pl;

        public void Finish() => this.isFinish = true;
        public void HasStarted() => this.isFirstRound = false;

        public string DisplayXOGrid()
        {
            string grid = "";

            for (int j = 0; j < 3; j++)
            {
                grid += $"{XOPlacements[j, 0]} | {XOPlacements[j, 1]} | {XOPlacements[j, 2]}\n";
                if (j != 2)
                    grid += "------\n";
            }
            return grid;
        }
    }
}
