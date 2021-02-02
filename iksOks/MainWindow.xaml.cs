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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iksOks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MarkType[] mRezultati;
        private bool mPlayer1Turn; // Player1=X,Player2=O
        private bool mGameEnded;
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        private void NewGame()
        {
            mRezultati = new MarkType[9];
            for (var i = 0; i < mRezultati.Length; i++)
                mRezultati[i] = MarkType.Free;
            mPlayer1Turn = true;
            Container.Children.Cast<Button>().ToList().ForEach(button=>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });
            mGameEnded = false;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }
            var button=(Button)sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);
            var index = column + (row * 3);
            if (mRezultati[index] != MarkType.Free)
                return;
            if (mPlayer1Turn)
                mRezultati[index] = MarkType.Cross;
            else
                mRezultati[index] = MarkType.Nought;
            button.Content = mPlayer1Turn ? "X" : "O";   // isto kao else if
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;
            // ako je true onda postaje false i obrnuto
            mPlayer1Turn ^= true;

            CheckForWinner();

          
        }

        private void CheckForWinner()
        {
            // Row 0
            if(mRezultati[0]!=MarkType.Free && (mRezultati[0] & mRezultati[1] & mRezultati[2]) == mRezultati[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }
            // Row 1
            if (mRezultati[3] != MarkType.Free && (mRezultati[3] & mRezultati[4] & mRezultati[5]) == mRezultati[3])
            {
                mGameEnded = true;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }
            // Row 2
            if (mRezultati[6] != MarkType.Free && (mRezultati[6] & mRezultati[7] & mRezultati[8]) == mRezultati[6])
            {
                mGameEnded = true;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            // Column 0
            if (mRezultati[0] != MarkType.Free && (mRezultati[0] & mRezultati[3] & mRezultati[6]) == mRezultati[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }
            // Column 1
            if (mRezultati[1] != MarkType.Free && (mRezultati[1] & mRezultati[4] & mRezultati[7]) == mRezultati[1])
            {
                mGameEnded = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }
            // Colum 2
            if (mRezultati[2] != MarkType.Free && (mRezultati[2] & mRezultati[5] & mRezultati[8]) == mRezultati[2])
            {
                mGameEnded = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }
            // Diagonal 0
           
            if (mRezultati[0] != MarkType.Free && (mRezultati[0] & mRezultati[4] & mRezultati[8]) == mRezultati[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }
            // Diagonal 1
            if (mRezultati[2] != MarkType.Free && (mRezultati[2] & mRezultati[4] & mRezultati[6]) == mRezultati[2])
            {
                mGameEnded = true;
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            if (!mRezultati.Any(result => result == MarkType.Free))
            {
                mGameEnded = true;
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Crimson;
                    
                });
            }
        }
    }
}
