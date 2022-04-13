using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cezar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string RU = "абвгдежзийклмнопрстуфхцчшщъыьэюя";

        public static string Error()
        {
            return "Ошибка при вводе данных! Введите заново!";
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        //метод, чтобы печатало группами по 6 символов и без мусора
        public static string Output(string str)
        {
            int sch = 1;
            //удаляем лишнее говно
            for (int i = 0; i < str.Length; i++)
            {
                if (!((Convert.ToInt32(str[i]) >= 97 && Convert.ToInt32(str[i]) <= 122) || 
                    (Convert.ToInt32(str[i]) >= 1072 && Convert.ToInt32(str[i]) <= 1103)))
                {
                    str = str.Remove(i, 1);
                    i--;
                }
            }
            //после каждого 6 символа ставим пробел
            for (int i = 0; i < str.Length; i++, sch++)
            {
                if (sch == 7)
                {
                    str = str.Insert(i, " ");
                    sch = 0;
                }
            }
            return str;
        }

        //зашифровка
        public static string Encrypt(string str, int key)
        {
            //перевод в нижний регистр
            str = str.ToLower();
            //замена ё на е
            str = str.Replace('ё', 'е');
            //переводим входной текст в массив чаров
            char[] mas = new char[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                //записываем символ из строки в чар
                char ch = str[i];
                //if - английский алфавит, else if - русский алфавит(код буквы в юникоде при конвертации в int)
                if (Convert.ToInt32(str[i]) >= 97 && Convert.ToInt32(str[i]) <= 122)
                {
                    //получаем символ со сдвигом ключа
                    int a = Convert.ToInt32(ch) + (key % 26);
                    //если оно больше, чем последний символ алфавита, отнимаем мощность алфавита(анг -26, ру -32)
                    if (a > 122)
                    {
                        a = a - 26;
                    }
                    else if (a < 97)
                    {
                        a = a + 26;
                    }
                    ch = Convert.ToChar(a);
                    str = str.Remove(i, 1).Insert(i, ch.ToString());
                }
                else if (Convert.ToInt32(str[i]) >= 1072 && Convert.ToInt32(str[i]) <= 1103)
                {
                    int a = Convert.ToInt32(ch) + (key % 32);
                    if (a > 1103)
                    {
                        a = a - 32;
                    }
                    else if (a < 1072)
                    {
                        a = a + 32;
                    }
                    ch = Convert.ToChar(a);
                    str = str.Remove(i, 1).Insert(i, ch.ToString());
                }
            }

            return str;
        }

        //то же самое, что зашифровка, но вычитаем
        public static string Decrypt(string str, int key)
        {
            str = str.ToLower();
            //замена ё на е
            str = str.Replace('ё', 'е');
            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                if (Convert.ToInt32(str[i]) >= 97 && Convert.ToInt32(str[i]) <= 122)
                {
                    int a = Convert.ToInt32(ch) - (key % 26);
                    if (a < 97)
                    {
                        a = a + 26;
                    }
                    else if (a > 122)
                    {
                        a = a - 26;
                    }
                    ch = Convert.ToChar(a);
                    str = str.Remove(i, 1).Insert(i, ch.ToString());
                }
                else if (Convert.ToInt32(str[i]) >= 1072 && Convert.ToInt32(str[i]) <= 1103)
                {
                    int a = Convert.ToInt32(ch) - (key % 32);
                    if (a < 1072)
                    {
                        a = a + 32;
                    }
                    else if (a > 1103)
                    {
                        a = a - 32;
                    }
                    ch = Convert.ToChar(a);
                    str = str.Remove(i, 1).Insert(i, ch.ToString());
                }
            }

            return str;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                int key = Convert.ToInt32(textBox3.Text);
                textBox2.Text = "";
                textBox2.Text = Output(Encrypt(textBox1.Text, key));
            }
            catch
            {
                textBox2.Text = Error();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                int key = Convert.ToInt32(textBox3.Text);
                textBox2.Text = "";
                textBox2.Text = Output(Decrypt(textBox1.Text, key));
            }
            catch
            {
                textBox2.Text = Error();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            //табличная частота символов русского алфавита в любом длинном тексте
            double[] chastota = new double[32];

            chastota[0] = 0.062;//а
            chastota[1] = 0.014;//б
            chastota[2] = 0.038;//в
            chastota[3] = 0.013;//г
            chastota[4] = 0.025;//д
            chastota[5] = 0.072;//е
            chastota[6] = 0.007;//ж
            chastota[7] = 0.016;//з
            chastota[8] = 0.062;//и
            chastota[9] = 0.010;//й
            chastota[10] = 0.028;//к
            chastota[11] = 0.035;//л
            chastota[12] = 0.026;//м
            chastota[13] = 0.053;//н
            chastota[14] = 0.090;//о
            chastota[15] = 0.023;//п
            chastota[16] = 0.040;//р
            chastota[17] = 0.045;//с
            chastota[18] = 0.053;//т
            chastota[19] = 0.021;//у
            chastota[20] = 0.002;//ф
            chastota[21] = 0.002;//х
            chastota[22] = 0.003;//ц
            chastota[23] = 0.012;//ч
            chastota[24] = 0.006;//ш
            chastota[25] = 0.003;//щ
            chastota[26] = 0.007;//ъ
            chastota[27] = 0.016;//ы
            chastota[28] = 0.007;//ь
            chastota[29] = 0.003;//э
            chastota[30] = 0.006;//ю
            chastota[31] = 0.018;//я

            string str = textBox1.Text;
            str = str.ToLower();
            //считаем сколько символов в тексте
            int sch = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (Convert.ToInt32(str[i]) >= 1072 && Convert.ToInt32(str[i]) <= 1103) sch++;
            }
            //считаем количество каждого символа в тексте
            int[] masi = new int[32];
            for (int i = 0; i < str.Length; i++)
            {
                for (int j = 0; j < RU.Length; j++)
                {
                    if (str[i] == RU[j])
                    {
                        masi[j] = masi[j] + 1;
                    }
                }
            }
            //для каждого ключа находим разницу квадратов частот по методу наименьших квадратов(это уже долго писать)
            //при встрече объясню
            double[] Keys_options = new double[32];

            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    int symb = j + i;
                    if (symb > 31)
                    {
                        symb -= 32;
                    }
                    Keys_options[i] += Math.Pow(((double)masi[symb] / sch) - chastota[j], 2);
                }
            }
            int key = 0;
            //ищем минимальную разность частот = > ключ будет равен индексу минимального элемента в массиве разностей
            double min = 931245223; //рандомное большое число, чтобы минимум всегда был меньше
            for (int i = 1; i < 32; i++)
            {
                if (min > Keys_options[i])
                {
                    min = Keys_options[i];
                    key = i;
                }
            }
            textBox3.Text = key.ToString();
            textBox2.Text = "";
            textBox2.Text = Output(Decrypt(textBox1.Text, key));
        }
    }
}
