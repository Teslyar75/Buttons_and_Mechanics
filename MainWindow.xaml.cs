using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Buttons_and_Mechanics
{
    public partial class MainWindow : Window
    {
        private Random random = new Random();
        private Button[] buttons;
        private char[] sequence;
        private int sequenceLength;
        private int currentIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
            InitializeButtons();
            GenerateSequence();
            UpdateSequenceDisplay();
            PickRandomButtonAndChangeColor();
        }

        private void InitializeButtons()
        {
            buttons = new Button[]
            {
                button1,  button2,  button3,
                button4,  button5,  button6,
                button7,  button8,  button9,
                button10, button11, button12,
                button13, button14, button15,
                button16, button17, button18,
                button19, button20, button21,
                button22, button23, button24,
                button25, button26, button27,
                button28, button29, button30,
                button31, button32, button33,
                button34, button35, button36,
                button37, button38, button39,
                button40, button41, button42,
                button43, button44, button45,
            };

            foreach (Button button in buttons)
            {
                button.Background = new SolidColorBrush(Color.FromRgb(255, 229, 108));
                button.Tag = button.Background;
            }
        }

        private void GenerateSequence()
        {
            char[] allowedLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            sequenceLength = random.Next(10, 20);
            sequence = new char[sequenceLength];

            for (int i = 0; i < sequenceLength; i++)
            {
                sequence[i] = allowedLetters[random.Next(0, allowedLetters.Length)];
            }
        }

        private void PickRandomButtonAndChangeColor()
        {
            char currentLetter = sequence[currentIndex];

            foreach (Button button in buttons)
            {
                button.Background = (Brush)button.Tag;
            }

            Button currentButton = buttons.FirstOrDefault(btn => btn.Content.ToString() == currentLetter.ToString());
            if (currentButton != null)
            {
                currentButton.Background = Brushes.Red;
            }
        }

        private void UpdateSequenceDisplay()
        {
            sequenceDisplay.Text = " ";
            for (int i = currentIndex; i < sequenceLength; i++)
            {
                sequenceDisplay.Text += sequence[i] + " ";
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(e.Key.ToString())) && e.Key.ToString().Length == 1)
            {
                char pressedKey = e.Key.ToString()[0];
                if (pressedKey == sequence[currentIndex])
                {
                    currentIndex++;

                    // Удаляем уже набранный символ из текстового окна
                    sequenceDisplay.Text = sequenceDisplay.Text.Substring(2);

                    if (currentIndex < sequenceLength)
                    {
                        PickRandomButtonAndChangeColor();
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Congratulations! You completed the sequence!\nDo you want to continue?", "Sequence Completed", MessageBoxButton.YesNoCancel);

                        if (result == MessageBoxResult.Yes)
                        {
                            currentIndex = 0;
                            GenerateSequence();
                            UpdateSequenceDisplay();
                            PickRandomButtonAndChangeColor();
                        }
                        else if (result == MessageBoxResult.Cancel || result == MessageBoxResult.No)
                        {
                            this.Close();
                        }
                    }
                }
            }
            else if (e.Key == Key.Escape)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to continue or exit?", "Exit or Continue", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.No)
                {
                    this.Close();
                }
            }
        }
    }
}
