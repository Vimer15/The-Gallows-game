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


namespace hugman
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string wordToGuess;
        private char[] guessedLetters;
        private int attempts;
        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
        }
        public void StartNewGame()
        {
            string[] words = { "ШОРПО","ЛАГМАН","БОРШ","КЕБАБ","ПЛОВ","МАНТЫ","ГУЛЯШ","ПЕЛЬМЕНИ", "КУРТАБ", "САДЖ" };
            foreach (var child in grid.Children)
            {
                if (child is Button button)
                {
                    button.IsEnabled = true;
                    button.Background = Brushes.White;
                }
            }
            part1_1.Opacity = 0;
            part1_2.Opacity = 0;
            part1_3.Opacity = 0;
            part2.Opacity = 0;
            part3.Opacity = 0;
            part4.Opacity = 0;
            part4_2.Opacity = 0;
            part4_3.Opacity = 0;
            part5_1.Opacity = 0;
            part5_2.Opacity = 0;
            Random random = new Random();
            wordToGuess = words[random.Next(0, words.Length)];
            guessedLetters = new char[wordToGuess.Length];

            for (int i = 0; i < wordToGuess.Length; i++)
            {
                guessedLetters[i] = '-';
            }
            attempts = 6;
            UpdateUI();

        }

        private void UpdateUI()
        {
            WordLabel.Content = new string(guessedLetters);
        }

        private void CheckLetter(char letter, Button button)
        {
            bool letterGuessed = false;

            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (wordToGuess[i] == letter)
                {
                    button.Background = Brushes.Green;
                    guessedLetters[i] = letter;
                    letterGuessed = true;
                }
            }

            if (!letterGuessed)
            {
                attempts--;
                button.Background = Brushes.Red;
                button.IsEnabled = false;
                if (attempts == 5)
                {
                    part1_1.Opacity = 100;
                    part1_2.Opacity = 100;
                    part1_3.Opacity = 100;
                }
                if (attempts == 4)
                {
                    part2.Opacity = 100;
                }
                if (attempts == 3)
                {
                    part3.Opacity = 100;
                }
                if (attempts == 2)
                {
                    part4.Opacity = 100;
                    part4_2.Opacity = 100;
                    part4_3.Opacity = 100;
                }
                if (attempts == 1)
                {
                    part5_1.Opacity = 100;
                }
                if (attempts == 0)
                {
                    part5_2.Opacity = 100;
                }

            }

            UpdateUI();

            if (new string(guessedLetters) == wordToGuess)
            {
                MessageBox.Show("Поздравляю! Вы отгадали слово!");
                StartNewGame();
            }
            else if (attempts == 0)
            {
                MessageBox.Show($"Вы проиграли! Загаданное слово: {wordToGuess}");
                StartNewGame();
            }
        }

        private void LetterButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            char letter = button.Content.ToString()[0];
            CheckLetter(letter, button);
        }
    }
}
