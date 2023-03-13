using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace tilitoli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button[,] buttons = new Button[4, 4];
        
        public MainWindow()
        {
            InitializeComponent();
            InitializeTiles();
            RandomizeTiles();
        }

        private void RandomizeTiles()
        {
            Random random = new Random();
            RoutedEventArgs x = new();
            for (int i = 0; i < 1000; i++)
            {
                Button_Click(grid_main.Children[random.Next(16)], x);
            }
        }

        private void InitializeTiles()
        {
            int num = 0;
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    Button button = new Button();
                    button.Click += new RoutedEventHandler(Button_Click);
                    button.Foreground = Brushes.Red;
                    Grid.SetColumn(button, j);
                    Grid.SetRow(button, i);
                    if (i == buttons.GetLength(0)-1 && j == buttons.GetLength(1)-1)
                    {
                        buttons[i, j] = button;
                        grid_main.Children.Add(button);
                        break;
                    }
                    ImageBrush brush = new ImageBrush();
                    brush.ImageSource = new ImageSourceConverter().ConvertFromString($"pics/V{num + 1}.jpg") as ImageSource;
                    button.Background = brush;
                    button.Content = $"{num}";
                    buttons[j, i] = button;
                    grid_main.Children.Add(button);
                    num++;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if(button.Content == null)
            {
                return;
            }
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    if (buttons[i,j] == button)
                    {
                        for (int k = -1; k < 2; k++)
                        {
                                if (i + k > -1 && i + k < buttons.GetLength(0) && j > -1 && j < buttons.GetLength(1))
                                {
                                    if (buttons[i+k,j].Content == null)
                                    {
                                        Button save = button;
                                        Button empty = buttons[i + k, j];
                                        Grid.SetColumn(button, i + k);
                                        Grid.SetRow(button, j);
                                        Grid.SetColumn(empty, i);
                                        Grid.SetRow(empty, j);
                                        button = empty;
                                        buttons[i, j] = empty;
                                        buttons[i + k, j] = save;
                                        return;
                                    }
                                }
                        }
                        for (int l = -1; l < 2; l++)
                        {
                            if (i> -1 && i < buttons.GetLength(1) && j + l > -1 && j + l < buttons.GetLength(1))
                            {
                                if (buttons[i, j + l].Content == null)
                                {
                                    Button save = button;
                                    Button empty = buttons[i, j + l];
                                    Grid.SetColumn(button, i);
                                    Grid.SetRow(button, j + l);
                                    Grid.SetColumn(empty, i);
                                    Grid.SetRow(empty, j);
                                    button = empty;
                                    buttons[i, j] = empty;
                                    buttons[i, j + l] = save;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
