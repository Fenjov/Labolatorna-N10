using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;

namespace Lab10Avalonia
{
    public partial class MainWindow : Window
    {
        private readonly Random _random = new Random();
        
        // Змінні лічильників для обліку кількості за типами
        private int _buttonCount = 0;
        private int _textBoxCount = 0;
        private int _textBlockCount = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Обробник натискання, що динамічно породжує об'єкти
        private void OnGenerateElementClick(object? sender, RoutedEventArgs e)
        {
            // 1. Випадковий вибір цільового контейнера: 0 - Форма, 1 - Панель
            int targetDestination = _random.Next(2);
            
            // 2. Випадковий вибір типу елемента управління: 0 - Кнопка, 1 - Поле вводу, 2 - Мітка
            int elementType = _random.Next(3);

            Control newControl;
            string typeName = "";

            // Динамічне створення екземплярів відповідних класів
            switch (elementType)
            {
                case 0:
                    _buttonCount++;
                    newControl = new Button 
                    { 
                        Content = $"Кнопка #{_buttonCount}",
                        Padding = new Avalonia.Thickness(5, 2)
                    };
                    typeName = "Кнопка";
                    break;
                    
                case 1:
                    _textBoxCount++;
                    newControl = new TextBox 
                    { 
                        Text = $"Поле #{_textBoxCount}", 
                        Width = 100,
                        Height = 30
                    };
                    typeName = "Поле вводу";
                    break;
                    
                case 2:
default:
                    _textBlockCount++;
                    newControl = new TextBlock 
                    { 
                        Text = $"Мітка #{_textBlockCount}", 
                        Foreground = Brushes.DarkBlue,
                        FontWeight = FontWeight.Medium
                    };
                    typeName = "Мітка";
                    break;
            }

            string destinationName = "";
            double xCoord = 0;
            double yCoord = 0;

            // 3. Визначення координат розташування та прив'язка до батьківського елемента
            if (targetDestination == 0)
            {
                // Породження на Формі (головному Canvas вікна)
                // Обмежуємо максимальні межі, щоб елемент помістився
                double maxW = Math.Max(50, FormCanvas.Bounds.Width - 120);
                double maxH = Math.Max(50, FormCanvas.Bounds.Height - 40);
                
                xCoord = _random.Next(15, (int)maxW);
                yCoord = _random.Next(40, (int)maxH);
                
                // Встановлюємо координати об'єкта через прикріплені властивості Canvas
                Canvas.SetLeft(newControl, xCoord);
                Canvas.SetTop(newControl, yCoord);
                
                // Додаємо динамічний об'єкт до списку дочірніх елементів форми
                FormCanvas.Children.Add(newControl);
                destinationName = "Форма";
            }
            else
            {
                // Породження всередині окремої Панелі
                xCoord = _random.Next(10, (int)(MyPanelBorder.Width - 110));
                yCoord = _random.Next(10, (int)(MyPanelBorder.Height - 35));

                Canvas.SetLeft(newControl, xCoord);
                Canvas.SetTop(newControl, yCoord);
                
                // Додаємо об'єкт до внутрішньої панелі
                PanelCanvas.Children.Add(newControl);
                destinationName = "Панель";
            }

            // 4. Оновлення інформаційного табло (Кількість за типами)
            lblStats.Text = $"Кнопки: {_buttonCount}\nПоля вводу: {_textBoxCount}\nМітки: {_textBlockCount}";

            // 5. Виведення інформації про тип об'єкта, його контейнер та точне розташування
            txtLog.Text += $"• {typeName} -> {destinationName} (X: {Math.Round(xCoord)}; Y: {Math.Round(yCoord)})\n";
        }
    }
}