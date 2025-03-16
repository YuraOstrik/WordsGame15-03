namespace WordsGame15._03
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void Generator(string filename)
        {
            try
            {
                string text = "SpaceX said it would review data \"to better understand [the] root cause\" of the most recent explosion and noted it happened after the loss of " +
                    "\"several\" engines.1" +
                    "\r\n\r\nThe Federal Aviation Administration (FAA) said the company would be required to conduct an investigation before it could fly again!" +
                    "\r\n\r\nNasa hopes to use a modified version of the spaceship as a human lunar lander for its Artemis missions to return to the Moon, Isn't they?";

                using (FileStream file = new FileStream(filename, FileMode.Create, FileAccess.Write))
                using (BinaryWriter writer = new BinaryWriter(file))
                {
                    writer.Write(text);
                }

                MessageBox.Show("Файл создан");
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка при создании файла");
            }
        }

        void Task1()
        {
            Generator("../../array1.dat");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task ts1 = Task.Run(Task1);
            ts1.Wait(); 

            string text;
            using (FileStream file = new FileStream("../../array1.dat", FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(file))
            {
                byte[] bytes = reader.ReadBytes((int)file.Length);
                text = System.Text.Encoding.UTF8.GetString(bytes);
            }

            Task<int> first = Task.Run(() => First(text));
            Task<int> second = Task.Run(() => Second(text));
            Task<int> third = Task.Run(() => Third(text));
            Task<int> fourth = Task.Run(() => Fourth(text));
            Task<int> fifth = Task.Run(() => Fifth(text));

            first.ContinueWith(task => Invoke(new Action(() => Update(textBox1, checkBox1, task.Result))),
                TaskScheduler.FromCurrentSynchronizationContext());
            second.ContinueWith(task => Invoke(new Action(() => Update(textBox2, checkBox2, task.Result))),
                TaskScheduler.FromCurrentSynchronizationContext());
            third.ContinueWith(task => Invoke(new Action(() => Update(textBox3, checkBox3, task.Result))),
                TaskScheduler.FromCurrentSynchronizationContext());
            fourth.ContinueWith(task => Invoke(new Action(() => Update(textBox4, checkBox4, task.Result))),
                TaskScheduler.FromCurrentSynchronizationContext());
            fifth.ContinueWith(task => Invoke(new Action(() => Update(textBox5, checkBox5, task.Result))),
                TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void Update(TextBox textBox, CheckBox checkBox, int num)
        {
            if (checkBox.Checked)
            {
                textBox.Text = num.ToString();
            }
            else
            {
                textBox.Text = string.Empty; 
            }
        }

        private int First(string text)
        {
            return text.Count(x => x == '.' || x == '!' || x == '?');
        }
        private int Second(string text)
        {
            return text.Length;
        }
        private int Third(string text)
        {
            char[] chars = { ' ', '\t', '\n', '\r', '.', ',', '!', '?' };
            string[] words = text.Split(chars, StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }
        private int Fourth(string text)
        {
            return text.Count(x => x == '?');

        }
        private int Fifth(string text)
        {
            return text.Count(x => x == '?');
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}


