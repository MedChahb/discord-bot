using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.games
{
    public class TicTacToe
    {
        public Bitmap boardImage { get; set; }
        public TicTacToe()
        {
            // Create a new image
            this.boardImage = new Bitmap(300, 300);

            // Draw the Tic Tac Toe board
            using (Graphics g = Graphics.FromImage(boardImage))
            {
                g.Clear(Color.White);
                Pen linePen = new Pen(Color.Black, 10);

                // Draw vertical lines
                for (int i = 1; i < 3; i++)
                {
                    g.DrawLine(linePen, new Point(i * 100, 0), new Point(i * 100, 300));
                }

                // Draw horizontal lines
                for (int i = 1; i < 3; i++)
                {
                    g.DrawLine(linePen, new Point(0, i * 100), new Point(300, i * 100));
                }
            }
        }

          /*// Place X at position (0, 0)
            PlaceSymbol(boardImage, 0, 0, 'X');

            // Place O at position (1, 1)
            PlaceSymbol(boardImage, 1, 1, 'O');

            // Save or display the image
            boardImage.Save("XO.jpg");*/

        public void PlaceSymbol(int row, int col, char symbol)
        {
            using (Graphics g = Graphics.FromImage(boardImage))
            {
                Font font = new Font("Arial", 60, FontStyle.Bold);

                // Determine position to place the symbol
                int cellSize = boardImage.Width / 3;
                int margin = 20;
                int x = col * cellSize + margin;
                int y = row * cellSize + margin;

                // Draw the symbol (X or O)
                if (symbol == 'X')
                {
                    g.DrawString("X", font, Brushes.Red, new Point(x, y));
                }
                else if (symbol == 'O')
                {
                    g.DrawString("O", font, Brushes.Blue, new Point(x, y));
                }
            }
        }

        public void SaveGameState() => boardImage.Save("XO.jpg");
    }
}
