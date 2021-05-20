using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vaja2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        private MarkType[] chessBoard;

        private bool player1Turn;
        private bool gameFinish;


        private MarkType[] Kopiraj(MarkType[] tmp)
        {
            MarkType[] test;
            test = new MarkType[9];
            for (int i = 0; i < tmp.Length; i++)
            {
                test[i] = tmp[i];
            }
            return test;
        }
        private void NewGame()
        {
            gameFinish = false;
            chessBoard = new MarkType[9];
            for(int i = 0; i < chessBoard.Length; i++)
            {
                chessBoard[i] = MarkType.Free;
            }

            Test.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Red;
                button.FontSize = 70;
            });
            player1Turn = true;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (gameFinish)
            {
                NewGame();
                return;
            }
            Button button = (Button)sender;
            int col = Grid.GetColumn(button);
            int row = Grid.GetRow(button);
            int index = col + (row * 3);

        

            if (chessBoard[index] != MarkType.Free) return;
            chessBoard[index] = player1Turn ? MarkType.Cross : MarkType.Circle;
            button.Content = player1Turn ? "X" : "O";
            if (!player1Turn)
                button.Foreground = Brushes.Blue;

            CheckForWinner();
            if (gameFinish) return;

            player1Turn = false;
            int tmp = BestMove();




            string zacasno = "";
            if (tmp == 0)
            {
                if (chessBoard[tmp] == MarkType.Cross) zacasno = "X";
                else zacasno = "O";
                Button_0.Content = zacasno;
                Button_0.Foreground = Brushes.Blue;
            }
            else if (tmp == 1)
            {
                if (chessBoard[tmp] == MarkType.Cross) zacasno = "X";
                else zacasno = "O";
                Button_1.Content = zacasno;
                Button_1.Foreground = Brushes.Blue;
            }
            else if (tmp == 2)
            {
                if (chessBoard[tmp] == MarkType.Cross) zacasno = "X";
                else zacasno = "O";
                Button_2.Content = zacasno;
                Button_2.Foreground = Brushes.Blue;
            }
            else if (tmp == 3)
            {
                if (chessBoard[tmp] == MarkType.Cross) zacasno = "X";
                else zacasno = "O";
                Button_3.Content = zacasno;
                Button_3.Foreground = Brushes.Blue;
            }
            else if (tmp == 4)
            {
                if (chessBoard[tmp] == MarkType.Cross) zacasno = "X";
                else zacasno = "O";
                Button_4.Content = zacasno;
                Button_4.Foreground = Brushes.Blue;
            }
            else if (tmp == 5)
            {
                if (chessBoard[tmp] == MarkType.Cross) zacasno = "X";
                else zacasno = "O";
                Button_5.Content = zacasno;
                Button_5.Foreground = Brushes.Blue;
            }
            else if (tmp == 6)
            {
                if (chessBoard[tmp] == MarkType.Cross) zacasno = "X";
                else zacasno = "O";
                Button_6.Content = zacasno;
                Button_6.Foreground = Brushes.Blue;
            }
            else if (tmp == 7)
            {
                if (chessBoard[tmp] == MarkType.Cross) zacasno = "X";
                else zacasno = "O";
                Button_7.Content = zacasno;
                Button_7.Foreground = Brushes.Blue;
            }
            else if (tmp == 8)
            {
                if (chessBoard[tmp] == MarkType.Cross) zacasno = "X";
                else zacasno = "O";
                Button_8.Content = zacasno;
                Button_8.Foreground = Brushes.Blue;
            }
            CheckForWinner();

        }


        private int BestMove()
        {
            int mode = Način.SelectedIndex;
            int bestScore = -1000;
            int bestMove = 0; 
            MarkType[] tmp = Kopiraj(chessBoard);
            MarkType znak = new MarkType();
            if (player1Turn)
            {
                znak = MarkType.Cross;
            }
            else
            {
                znak = MarkType.Circle;
            }
            for (int i = 0; i < tmp.Length; i++)
            {
                if (tmp[i] == MarkType.Free)
                {
                    tmp[i] = znak;
                    int score = AlfaBeta(tmp, false, mode, -10000, 10000);
                    tmp[i] = MarkType.Free;
                    if(score > bestScore)
                    {
                        
                        bestScore = score;
                        bestMove = i;
                       
                      
                    }
                }
            }

            chessBoard[bestMove] = znak;
            player1Turn = true;
            return bestMove;
        }

        /*
        private int Minimax(MarkType[] board, bool max, int depth)
        {
            int score = 0;
            int result = EvaluateBoard(board);
            int bestScore;

            if(Math.Abs(result) == 100 || result == 500)
            {
                if(result == 500)
                {
                    return 0;
                }
                return result;
            }
            
            if(depth == 0)
            {
                return result;
            }
            
            if(max)
            {
                bestScore = -100000;
                for(int i = 0; i < board.Length; i++)
                {
                    if(board[i] == MarkType.Free)
                    {
                        board[i] = MarkType.Circle;
                        score = Minimax(board, false, depth-1);
                        board[i] = MarkType.Free;
                        if(score>bestScore)
                        {
                            bestScore = score;
                        }
                    }
                }
                if(bestScore == -100000)
                {
                    return 0;
                }
                return bestScore;
            }
            else
            {
                bestScore = 100000;
                for (int i = 0; i < board.Length; i++)
                {
                    if (board[i] == MarkType.Free)
                    {
                        board[i] = MarkType.Cross;
                        score = Minimax(board, true, depth - 1);s
                        board[i] = MarkType.Free;
                        if (score < bestScore)
                        {
                            bestScore = score;
                        }
                    }
                }
                if (bestScore == 100000)
                {
                    return 0;
                }
                return bestScore;
            }
        }


        */

        private int AlfaBeta(MarkType[] board, bool max, int depth, int alfa, int beta)
        {
            int score = 0;
            int result = EvaluateBoard(board);
            int bestScore;

            if (Math.Abs(result) == 100 || result == 500)
            {
                if (result == 500)
                {
                    return 0;
                }
                return result;
            }

            if (depth == 0)
            {
                return result;
            }

            if (max)
            {
                bestScore = -100000;
                for (int i = 0; i < board.Length; i++)
                {
                    if (board[i] == MarkType.Free)
                    {
                        board[i] = MarkType.Circle;
                        score = AlfaBeta(board, false, depth - 1, alfa, beta);
                        board[i] = MarkType.Free;

                        if (score > bestScore)
                        {
                            bestScore = score;
                            if(score >alfa)
                            {
                                alfa = score;
                                if(alfa >= beta)
                                {
                                    return bestScore;
                                }
                            }
                        }
                    }
                }
                if (bestScore == -100000)
                {
                    return 0;
                }
                return bestScore;
            }
            else
            {
                bestScore = 100000;
                for (int i = 0; i < board.Length; i++)
                {
                    if (board[i] == MarkType.Free)
                    {
                        board[i] = MarkType.Cross;
                        score = AlfaBeta(board, true, depth - 1, alfa, beta);
                        board[i] = MarkType.Free;
                        if (score < bestScore)
                        {
                            bestScore = score;
                            if(score < beta)
                            {
                                beta = score;
                                if(alfa >= beta)
                                {
                                    return bestScore;
                                }
                            }
                        }
                    }
                }
                if (bestScore == 100000)
                {
                    return 0;
                }
                return bestScore;
            }
        }


        private int EvaluateBoard(MarkType[] board)
        {
            MarkType X = MarkType.Cross;
            MarkType O = MarkType.Circle;


            int heuristicX = 0;
            int heuristicO = 0;
            int counterX = 0;
            int counterO = 0;
            int heuristic = 0;

            for (int i = 0; i < 3; i++)
            {

                for (int k = 0; k < 3; k++)
                {
                    if (board[k + i * 3] != MarkType.Free)
                    {
                        if (board[k + i * 3] != X)
                            counterO++;
                        else
                            counterX++;
                        if (counterO != 0 && counterX !=0)
                        {
                            counterO = 0;
                            counterX = 0;
                            break;
                        }

                    }
                }

                if (counterO == 1)
                {
                    heuristicO = 1;
                    counterO = 0;
                }
                else if (counterO == 2)
                {
                    heuristicO = 10;
                    counterO = 0;
                }
                else if (counterO == 3)
                {
                    heuristicO = 100;
                    counterO = 0;
                    return 100;
                }
                if (counterX == 1)
                {
                    heuristicX = -1;
                    counterX = 0;
                }
                else if (counterX == 2)
                {
                    heuristicX = -10;
                    counterX = 0;
                }
                else if (counterX == 3)
                {
                    heuristicX = -100;
                   
                    counterX = 0;
                    return -100;
                }


                heuristic += heuristicO +heuristicX;
                heuristicO = 0;
                heuristicX = 0;

                for (int k = 0; k < 3; k++)
                {
                    if (board[k * 3 + i] != MarkType.Free)
                    {
                        if (board[k * 3 + i] != X)
                            counterO++;
                        else
                            counterX++;
                        if (counterO != 0 && counterX != 0)
                        {
                            counterO = 0;
                            counterX = 0;
                            break;
                        }
                    }
                }


                if (counterO == 1)
                {
                    heuristicO = 1;
                    counterO = 0;
                }
                else if (counterO == 2)
                {
                    heuristicO = 10;
                    counterO = 0;
                }
                else if (counterO == 3)
                {
                    heuristicO = 100;
                    counterO = 0;
                    return 100;
                }
                if (counterX == 1)
                {
                    heuristicX = -1;
                    counterX = 0;
                }
                else if (counterX == 2)
                {
                    heuristicX = -10;
                    counterX = 0;
                }
                else if (counterX == 3)
                {
                    heuristicX = -100;

                    counterX = 0;
                    return -100;
                }

                heuristic += heuristicO + heuristicX;
                heuristicO = 0;
                heuristicX = 0;
            }
            for (int k = 0; k < 3; k++)
            {
                if (board[k * 4] != MarkType.Free)
                {
                    if (board[k * 4] != X)
                        counterO++;
                    else
                        counterX++;
                    if (counterO != 0 && counterX != 0)
                    {
                        counterO = 0;
                        counterX = 0;
                        break;
                    }
                }
            }

            if (counterO == 1)
            {
                heuristicO = 1;
                counterO = 0;
            }
            else if (counterO == 2)
            {
                heuristicO = 10;
                counterO = 0;
            }
            else if (counterO == 3)
            {
                heuristicO = 100;
                counterO = 0;
                return 100;
            }
            if (counterX == 1)
            {
                heuristicX = -1;
                counterX = 0;
            }
            else if (counterX == 2)
            {
                heuristicX = -10;
                counterX = 0;
            }
            else if (counterX == 3)
            {
                heuristicX = -100;

                counterX = 0;
                return -100;
            }

            heuristic += heuristicO + heuristicX;
            heuristicO = 0;
            heuristicX = 0;
            for (int k = 1; k < 4; k++)
            {
                if (board[k * 2] != MarkType.Free)
                {
                    if (board[k * 2] != X)
                        counterO++;
                    else
                        counterX++;
                    if (counterO != 0 && counterX != 0)
                    {
                        counterO = 0;
                        counterX = 0;
                        break;
                    }
                }
            }

            if (counterO == 1)
            {
                heuristicO = 1;
                counterO = 0;
            }
            else if (counterO == 2)
            {
                heuristicO = 10;
                counterO = 0;
            }
            else if (counterO == 3)
            {
                heuristicO = 100;
                counterO = 0;
                return 100;
            }
            if (counterX == 1)
            {
                heuristicX = -1;
                counterX = 0;
            }
            else if (counterX == 2)
            {
                heuristicX = -10;
                counterX = 0;
            }
            else if (counterX == 3)
            {
                heuristicX = -100;

                counterX = 0;
                return -100;
            }

            heuristic += heuristicO + heuristicX;
            heuristicO = 0;
            heuristicX = 0;

            if (!chessBoard.Any(f => f == MarkType.Free))
            {
                heuristic = 500;
            }


            return heuristic;
        }

       

        private void CheckForWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                if (chessBoard[0+i*3] != MarkType.Free && (chessBoard[0+i*3] & chessBoard[1+i*3] & chessBoard[2+i*3]) == chessBoard[0+i*3])
                {
                    gameFinish = true;
                    if(i == 0)
                        Button_0.Background = Button_1.Background = Button_2.Background = Brushes.Green;
                    else if( i == 1)
                        Button_3.Background = Button_4.Background = Button_5.Background = Brushes.Green;
                    else if(i == 2)
                        Button_6.Background = Button_7.Background = Button_8.Background = Brushes.Green;
                }
                if (chessBoard[i] != MarkType.Free && (chessBoard[i] & chessBoard[i+3] & chessBoard[i+6]) == chessBoard[i])
                {
                    gameFinish = true;
                    if (i == 0)
                        Button_0.Background = Button_3.Background = Button_6.Background = Brushes.Green;
                    else if( i == 1)
                        Button_1.Background = Button_4.Background = Button_7.Background = Brushes.Green;
                    else if(i == 2)
                        Button_2.Background = Button_5.Background = Button_8.Background = Brushes.Green;
                }

            }
            if (chessBoard[0] != MarkType.Free && (chessBoard[0] & chessBoard[4] & chessBoard[8]) == chessBoard[0])
            {
                Button_0.Background = Button_4.Background = Button_8.Background = Brushes.Green; 
                gameFinish = true;
            }
            if (chessBoard[2] != MarkType.Free && (chessBoard[2] & chessBoard[4] & chessBoard[6]) == chessBoard[2])
            {
                Button_2.Background = Button_4.Background = Button_6.Background = Brushes.Green; 
                gameFinish = true;
            }
            if (!gameFinish && !chessBoard.Any(f => f == MarkType.Free))
            {
                gameFinish = true;
                Test.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Gray;
                });
            }
        }
    }
}
