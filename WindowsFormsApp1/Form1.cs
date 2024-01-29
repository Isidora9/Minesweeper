using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Properties;
using System.Diagnostics;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Stopwatch stopWatch;
        List<TimeSpan> highScore = new List<TimeSpan>();

        byte[,] Position = new byte[15, 15];
        Button[,] ButtonList = new Button[15, 15];
        int bombTotal = 40;
        int result = 0;
        bool gameStarted = false;
        Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            GenerateBombs();
            GeneratButtons();
            GeneratePositionValue();
            //SimulateGame();
            StartTimer();
        }

        private void GenerateBombs() 
        {
            int bombTotalTxt = Convert.ToInt16(txtMineCount.Text);

            int bombCount = 0;
            for (int bomb = 0; bomb < bombTotal; bomb++)
            {
                int x = random.Next(0, 15);
                int y = random.Next(0, 15);
                if (Position[x,y] == 0)
                {
                    Position[x,y] = 10;
                    bombCount++;
                }
                else
                {
                    bomb--;
                }
            }
            txtMineCount.Text = bombTotal.ToString();
        }
        private void GeneratePositionValue() 
        {
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    if (Position[x, y] == 10)
                    {
                        continue;
                    }
                    byte bombCount = 0;

                    for (int xCheck = -1; xCheck < 2; xCheck++)
                    {
                        int xPos = xCheck + x;
                        for (int yCheck = -1; yCheck < 2; yCheck++)
                        {
                            int yPos = yCheck + y;
                            if (xPos == x && yPos == y) 
                            {
                                continue;
                            }
                            if (xPos < 0 || xPos > 14 || yPos < 0 || yPos > 14)
                            {
                                continue;
                            }
                            if (Position[xPos,yPos] == 10)
                            {
                                bombCount++;
                            }
                        }
                    }
                    if (bombCount == 0)
                    {
                        Position[x, y] = 20;
                    }
                    else
                    {
                        Position[x, y] = bombCount;
                    }
                }
            }
        }
        private void GeneratButtons() 
        {
            int xLoc = 3;
            int yLoc = 6;
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    Button btn = new Button();
                    btn.Parent = pnlBody;
                    btn.Location = new Point(xLoc, yLoc);
                    btn.Size = new Size(25, 22);
                    btn.Tag = $"{x},{y}";
                    btn.Click += BtnClick;
                    btn.MouseUp+= BtnMouseUp;
                    xLoc += 25;
                    ButtonList[x, y] = btn;
                }
                yLoc += 22;
                xLoc = 3;
            }
        }
        private void BtnClick(object sender, EventArgs e)
        {
            gameStarted = true;
            Button btn = (Button)sender;
            int x = Convert.ToInt32(btn.Tag.ToString().Split(',')[0]);
            int y = Convert.ToInt32(btn.Tag.ToString().Split(',')[1]);
            byte value = Position[x, y];
            if (value == 10)
            {
                btn.Image = Resources.bomb;
                GameOver();
            }
            else if (value == 20)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Enabled = false;
                result++;
                OpenAdjacentEmptyTiles(btn);
            }
            else 
            {
                btn.Image = null;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderColor = SystemColors.ControlDark;
                btn.FlatAppearance.MouseDownBackColor = btn.BackColor;
                btn.FlatAppearance.MouseOverBackColor = btn.BackColor;
                btn.Text = Position[x, y].ToString();
                result++;
            }
            btn.Click -= BtnClick;
            txtResult.Text = result.ToString();
            if (result == ButtonList.Length - bombTotal)
            {
                Win();
            }
        }
        private void BtnMouseUp(object sender, MouseEventArgs e)
        {
            gameStarted = true;
            Button btn = sender as Button;

            if (e.Button == MouseButtons.Right)
            {
                PlaceFlag(btn);
            }
            if (e.Button == MouseButtons.Middle)
            {
                OpenSolvedTiles(btn);
            }
        }
        private void PlaceFlag(Button btn) 
        {
            if (btn.Text == "" && btn.Image == null)
            {
                btn.Image = Resources.flag;
            }
            else if (btn.Image != null)
            {
                btn.Image = null;
            }
        }
        private void OpenSolvedTiles(Button btn) 
        {
            int x = Convert.ToInt32(btn.Tag.ToString().Split(',')[0]);
            int y = Convert.ToInt32(btn.Tag.ToString().Split(',')[1]);

            if (NearFlag(btn) == Position[x, y])
            {
                OpenAdjacentTiles(btn);
            }
        }
        private void OpenAdjacentTiles(Button btn)
        {
            int x = Convert.ToInt32(btn.Tag.ToString().Split(',')[0]);
            int y = Convert.ToInt32(btn.Tag.ToString().Split(',')[1]);
            for (int xCheck = -1; xCheck < 2; xCheck++)
            {
                if (!gameStarted)
                {
                    break;
                }

                int xPos = xCheck + x;
                for (int yCheck = -1; yCheck < 2; yCheck++)
                {
                    if (!gameStarted)
                    {
                        break;
                    }

                    int yPos = yCheck + y;
                    if (xPos == x && yPos == y)
                    {
                        continue;
                    }
                    if (xPos < 0 || xPos > 14 || yPos < 0 || yPos > 14)
                    {
                        continue;
                    }
                    Button btnAdjacent = ButtonList[xPos, yPos];
                    byte value = Position[xPos, yPos];
                    if (value != 10)
                    {
                        btnAdjacent.PerformClick();
                    }
                }
            }
        }

        private int NearFlag(Button btn)
        {
            int flagCount = 0;
            int x = Convert.ToInt32(btn.Tag.ToString().Split(',')[0]);
            int y = Convert.ToInt32(btn.Tag.ToString().Split(',')[1]);
            for (int xCheckFlag = -1; xCheckFlag < 2; xCheckFlag++) 
            {
                int xPosFlag = xCheckFlag + x;
                for (int yCheckFlag = -1; yCheckFlag < 2; yCheckFlag++)
                {
                    int yPosFlag = yCheckFlag + y;
                    if (xPosFlag == x && yPosFlag == y)
                    {
                        continue;
                    }
                    if (xPosFlag < 0 || xPosFlag > 14 || yPosFlag < 0 || yPosFlag > 14)
                    {
                        continue;
                    }
                    Button btnAdjacentIsFlag = ButtonList[xPosFlag, yPosFlag];
                    byte value = Position[xPosFlag, yPosFlag];
                    if (value == 10 && btnAdjacentIsFlag.Image != null)
                    {
                        flagCount++;
                    }
                }
            }
            return flagCount;
        }

        private void OpenAdjacentEmptyTiles(Button btn) 
        {
            int x = Convert.ToInt32(btn.Tag.ToString().Split(',')[0]);
            int y = Convert.ToInt32(btn.Tag.ToString().Split(',')[1]);
            List<Button> emptyButtons = new List<Button>();
            for (int xCheck = -1; xCheck < 2; xCheck++)
            {
                int xPos = xCheck + x;
                for (int yCheck = -1; yCheck < 2; yCheck++)
                {
                    int yPos = yCheck + y;
                    if (xPos == x && yPos == y)
                    {
                        continue;
                    }
                    if (xPos < 0 || xPos > 14 || yPos < 0 || yPos > 14)
                    {
                        continue;
                    }
                    Button btnAdjacent = ButtonList[xPos, yPos];
                    byte value = Position[xPos, yPos];

                    if (value == 20)
                    {
                        if (btnAdjacent.FlatStyle != FlatStyle.Flat)
                        {
                            btnAdjacent.FlatStyle = FlatStyle.Flat;
                            btnAdjacent.FlatAppearance.BorderSize = 0;
                            btnAdjacent.Enabled = false;
                            emptyButtons.Add(btnAdjacent);
                        }
                    }
                    else if (value != 10)
                    {
                        btnAdjacent.PerformClick();
                    }
                }
            }
            foreach (var btnEmpty in emptyButtons)
            {
                OpenAdjacentEmptyTiles(btnEmpty);
                result++;
            }
        }

        private void BtnRestart_Click(object sender, EventArgs e)
        {
            stopWatch.Restart();
            gameStarted = false;
            Button btn = sender as Button;
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    ButtonList[x, y].Dispose();
                }
            }
            pnlBody.Controls.Clear();
            Position = new byte[15, 15];
            result = 0;
            txtResult.Text = result.ToString();

            GeneratButtons();
            GenerateBombs();
            GeneratePositionValue();
            pnlBody.Enabled = true;
        }
        private void GameOver() 
        {
            pnlBody.Enabled = false;
            stopWatch.Stop();
            DialogResult result = MessageBox.Show("Game over!\nDo you wish to start over?", "", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                BtnRestart.PerformClick();
            }
        }
        private void Win() 
        {
            WinTime();
            pnlBody.Enabled = false;
            DialogResult answer = MessageBox.Show("You won!\nDo you wish to start over?", "Congrats!",
MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (answer == DialogResult.Yes)
            {
                BtnRestart.PerformClick();
            }
        }
        private void StartTimer() 
        {
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            txtTimer.Text = String.Format("{0:00}:{1:00}:{2:00}", stopWatch.Elapsed.Minutes, stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10);
        }
        private void WinTime() 
        {
            stopWatch.Stop();
            TimeSpan winningTime = stopWatch.Elapsed;

            for (int i = 0; i < 10; i++)
            {
                if (highScore.Count == i)
                {
                    highScore.Add(winningTime);
                    highScore.ToList().Sort();
                    break;
                }
                if (winningTime < highScore[i])
                {
                    highScore.Insert(i, winningTime);   
                    break;
                }
            }
            if (highScore.Count > 10)
            {
                highScore.RemoveAt(10);
            }
            //string winningTime = String.Format("{0:00}:{1:00}:{2:00}", stopWatch.Elapsed.Minutes, stopWatch.Elapsed.Seconds, stopWatch.Elapsed.Milliseconds / 10);
        }
        private void OpenTile()
        {
            int x = random.Next(0, 15);
            int y = random.Next(0, 15);
            Button btnRandom = ButtonList[x, y];
            btnRandom.PerformClick();
        }
        private void PutFlags() 
        {
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    Button btnCurrent = ButtonList[x, y];

                    if (Position[x, y] >= 1 && Position[x, y] <= 8 && btnCurrent.FlatStyle == FlatStyle.Flat)
                    {
                        int unopenedTiles = 0;
                        List<Button> unopenedTilesList = new List<Button>();

                        for (int xCheck = -1; xCheck < 2; xCheck++)
                        {
                            int xPos = xCheck + x;
                            for (int yCheck = -1; yCheck < 2; yCheck++)
                            {
                                int yPos = yCheck + y;

                                if (xPos == x && yPos == y)
                                {
                                    continue;
                                }
                                if (xPos < 0 || xPos > 14 || yPos < 0 || yPos > 14)
                                {
                                    continue;
                                }
                                Button btnAdjacent = ButtonList[xPos, yPos];

                                if (btnAdjacent.Image == null && btnAdjacent.Text == "" && 
                                    btnAdjacent.Enabled == true && Position[xPos, yPos] == 10)
                                {
                                    unopenedTiles++;
                                    unopenedTilesList.Add(btnAdjacent);
                                }
                            }
                        }
                        if (unopenedTiles == Position[x, y])
                        {
                            foreach (Button btn in unopenedTilesList)
                            {
                                PlaceFlag(btn);
                            }
                        }
                    }
                }
            }

        }
        private void OpenSolvedTiles()
        {
            for (int x = 0; x < 15; x++)
            {
                for (int y = 0; y < 15; y++)
                {
                    Button btnCurrent = ButtonList[x, y];
                    if (Position[x, y] >= 1 && Position[x, y] <= 8 && ButtonList[x,y].FlatStyle == FlatStyle.Flat)
                    {
                        OpenSolvedTiles(ButtonList[x, y]);
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //OpenTile();
            //PutFlags();
            //OpenSolvedTiles();
            //while (pnlBody.Enabled == true)
            //{
            //    PutFlags();
            //    OpenSolvedTiles();
            //}
        }
    }
}
