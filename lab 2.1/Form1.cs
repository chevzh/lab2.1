using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_2._1
{
    public partial class Form1 : Form
    {
        string leftop = ""; // Левый операнд
        string operation = ""; // Знак операции
        string rightop = ""; // Правый операнд

        public Form1()
        {
            InitializeComponent();


            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if (c is Button)
                {
                    if (c.Name.Equals(plusButton.Name) || c.Name.Equals(minusButton.Name) || c.Name.Equals(multiplyButton.Name) || c.Name.Equals(divideButton.Name) || c.Name.Equals(clearButton.Name) || c.Name.Equals(equalsButton.Name))
                    {
                        ((Button)c).MouseEnter += Control_Buttons_Mouse_Enter;
                        ((Button)c).MouseLeave += Control_Buttons_Mouse_Leave;
                    }
                    else
                    {
                        ((Button)c).MouseEnter += Num_Mouse_Enter;
                        ((Button)c).MouseLeave += Num_Mouse_Leave;
                    }

                    ((Button)c).Click += Button_Click;

                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            //if (!((Button)sender).Name.Equals(equalsButton.Name) && !((Button)sender).Name.Equals(clearButton.Name))
            //{
                string s = ((Button)sender).Text;

                calcInput.Text += s;

                int num;
                // Пытаемся преобразовать его в число
                bool result = Int32.TryParse(s, out num);
                // Если текст - это число
                if (result == true)
                {
                    // Если операция не задана
                    if (operation == "")
                    {
                        // Добавляем к левому операнду
                        leftop += s;
                    }
                    else
                    {
                        // Иначе к правому операнду
                        rightop += s;
                    }
                }
                // Если было введено не число
                else
                {
                    // Если равно, то выводим результат операции
                    if (s == "=")
                    {
                        Update_RightOp();
                        calcInput.Text += rightop;
                        operation = "";
                    }
                    // Очищаем поле и переменные
                    else if (s == "Clear")
                    {
                        leftop = "";
                        rightop = "";
                        operation = "";
                        calcInput.Text = "";
                    }
                    // Получаем операцию
                    else
                    {
                        // Если правый операнд уже имеется, то присваиваем его значение левому
                        // операнду, а правый операнд очищаем
                        if (rightop != "")
                        {
                            Update_RightOp();
                            leftop = rightop;
                            rightop = "";
                        }
                        operation = s;
                    }
                }
           // }

        }

        // Обновляем значение правого операнда
        private void Update_RightOp()
        {
            int num1 = Int32.Parse(leftop);
            int num2 = Int32.Parse(rightop);
            // И выполняем операцию
            switch (operation)
            {
                case "+":
                    rightop = (num1 + num2).ToString();
                    break;
                case "-":
                    rightop = (num1 - num2).ToString();
                    break;
                case "*":
                    rightop = (num1 * num2).ToString();
                    break;
                case "/":
                    rightop = (num1 / num2).ToString();
                    break;
            }
        }

        private void Control_Buttons_Mouse_Enter(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.GreenYellow;
        }

        private void Control_Buttons_Mouse_Leave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.Transparent;
        }

        private void Num_Mouse_Enter(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.LightGray;
        }

        private void Num_Mouse_Leave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.WhiteSmoke;
        }



        // Task 2

        List<int> elementsList = new List<int>();
        int element;

        private void generateButton_Click(object sender, EventArgs e)
        {
            int count = int.Parse(elementCount.Text);

            Random rnd = new Random();

            listView1.Clear();
            elementsList.Clear();

            for(int i = 0; i < count; i++)
            {
                element = rnd.Next(100);
                listView1.Items.Add(element.ToString());
                elementsList.Add(element);
            }
            
        }

        private void sortAscButton_Click(object sender, EventArgs e)
        {
            elementsList.Sort(descSort);
            writeToList();
        }

        private void sortDescButton_Click(object sender, EventArgs e)
        {
            elementsList.Sort();
            writeToList();
        }

        private int descSort(int x, int y)
        {
            if (x > y) return -1;
            if (x < y) return 1;
            else return 0;                      
        }

        private void writeToList()
        {
            listView1.Clear();

            foreach (var element in elementsList)
            {
                listView1.Items.Add(element.ToString());
            }
        }
    }
}

