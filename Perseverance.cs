using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace PomodoroApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public partial class Form1 : Form
    {
        private bool isRunning = false;
        private Timer timer;
        private int minutes = 25;
        private int seconds = 0;
        private int pomodoroCount = 1;
        private SoundPlayer alarmSound;

        private Label minutesLabel;
        private Label secondsLabel;
        private Label statusLabel;
        private Label phraseLabel;
        private Button startPauseBtn;

        public Form1()
        {
            InitializeComponent();
            InitializePomodoroTimer();
        }

        private void InitializePomodoroTimer()
        {
            // Initialize Timer
            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += Timer_Tick;

            // Load alarm sound
            alarmSound = new SoundPlayer("sound.wav");

            // Initialize Labels and Button
            minutesLabel.Text = "25";
            secondsLabel.Text = "00";
            statusLabel.Text = "Pomodoro 1";
            phraseLabel.Text = "Discipline is the most important\ningredient of success";
            startPauseBtn.Text = "Start";
            startPauseBtn.Click += StartPauseBtn_Click;
        }

        private void StartPauseBtn_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                PauseTimer();
            }
            else
            {
                StartTimer();
            }
        }

        private void StartTimer()
        {
            isRunning = true;
            startPauseBtn.Text = "Pause";
            timer.Start();
        }

        private void PauseTimer()
        {
            isRunning = false;
            startPauseBtn.Text = "Start";
            timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (seconds == 0)
            {
                if (minutes == 0)
                {
                    CompletePomodoro();
                }
                else
                {
                    minutes--;
                    seconds = 59;
                }
            }
            else
            {
                seconds--;
            }
            UpdateDisplay();
        }

        private void CompletePomodoro()
        {
            timer.Stop();
            isRunning = false;
            startPauseBtn.Text = "Start";
            pomodoroCount++;
            minutes = 25;
            seconds = 0;
            UpdateDisplay();
            statusLabel.Text = "Pomodoro " + pomodoroCount.ToString();
            alarmSound.Play();
            // MessageBox.Show("Tomate un descanso campeón");
        }

        private void UpdateDisplay()
        {
            minutesLabel.Text = minutes < 10 ? "0" + minutes : minutes.ToString();
            secondsLabel.Text = seconds < 10 ? "0" + seconds : seconds.ToString();
        }

        private void InitializeComponent()
        {
            this.minutesLabel = new System.Windows.Forms.Label();
            this.secondsLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.phraseLabel = new System.Windows.Forms.Label();
            this.startPauseBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // minutesLabel
            // 
            this.minutesLabel.AutoSize = true;
            this.minutesLabel.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minutesLabel.Location = new System.Drawing.Point(50, 20);
            this.minutesLabel.Name = "minutesLabel";
            this.minutesLabel.Size = new System.Drawing.Size(53, 36);
            this.minutesLabel.TabIndex = 0;
            this.minutesLabel.Text = "25";
            this.minutesLabel.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // secondsLabel
            // 
            this.secondsLabel.AutoSize = true;
            this.secondsLabel.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondsLabel.Location = new System.Drawing.Point(120, 20);
            this.secondsLabel.Name = "secondsLabel";
            this.secondsLabel.Size = new System.Drawing.Size(53, 36);
            this.secondsLabel.TabIndex = 1;
            this.secondsLabel.Text = "00";
            this.secondsLabel.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.Location = new System.Drawing.Point(60, 70);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(99, 18);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.Text = "Pomodoro 1";
            this.statusLabel.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // phraseLabel
            // 
            this.phraseLabel.AutoSize = true;
            this.phraseLabel.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phraseLabel.Location = new System.Drawing.Point(10, 100);
            this.phraseLabel.Name = "phraseLabel";
            this.phraseLabel.Size = new System.Drawing.Size(245, 32);
            this.phraseLabel.TabIndex = 3;
            this.phraseLabel.Text = "Discipline is the most important\ningredient of success";
            this.phraseLabel.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // startPauseBtn
            // 
            this.startPauseBtn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startPauseBtn.Location = new System.Drawing.Point(50, 150);
            this.startPauseBtn.Name = "startPauseBtn";
            this.startPauseBtn.Size = new System.Drawing.Size(100, 30);
            this.startPauseBtn.TabIndex = 4;
            this.startPauseBtn.Text = "Start";
            this.startPauseBtn.UseVisualStyleBackColor = true;

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 200);
            this.Controls.Add(this.startPauseBtn);
            this.Controls.Add(this.phraseLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.secondsLabel);
            this.Controls.Add(this.minutesLabel);
            this.Name = "Form1";
            this.Text = "Pomodoro Timer";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
