/* Copyright (c) 2022 Sven Mathing
 * Licensed under the EUPL, Version 1.2 or as soon they will be approved by
 * the European Commission - subsequent versions of the EUPL (the "Licence"); You may not use this work except
 * in compliance with the Licence. You may obtain a copy of the Licence at:
 * http://joinup.ec.europa.eu/software/page/eupl Unless required by applicable law or agreed to in writing,
 * software distributed under the Licence is distributed on an "AS IS" basis, WITHOUT WARRANTIES OR CONDITIONS
 * OF ANY KIND, either express or implied. See the Licence for the specific language governing permissions and
 * limitations under the Licence.
 */
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;


namespace smaTimer {
    public partial class smaTimer_Form : Form
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        Font myFont;
        Boolean timerStop = false;


        public smaTimer_Form()
        {
            InitializeComponent();
            byte[] fontData = Properties.Resources.LanaPixel;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.LanaPixel.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.LanaPixel.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            myFont = new Font(fonts.Families[0], 32.0F);
            label2.Text = "01:30";
            //startTimer(0,30);

        }

        private void startTimer(int minutes, int seconds = 0) {
            if (minutes>-1 && seconds < 60 && seconds>-1){
                for (int i = minutes; i >-1 ; i--) {
                    for (int j = seconds; j > -1; j--) {
                        label2.Text = time2string(i, j);
                        this.Update();
                        wait(1000);
                        if (timerStop == true) {
                            return;
                        }
                    }
                    seconds = 59;
                }
            }
            label2.Text = time2string(0, 0);
            button1.Text = "Start";
        }

        private string time2string(int minutes, int seconds=0) {
            string result = "00:00";
            result = "" + minutes.ToString("D2") + ":" + seconds.ToString("D2");
            return result;
        }

        /// <summary>
        /// wait without locking the UserInterface
        /// </summary>
        /// <param name="milliseconds">time to wait</param>
        private void wait(int milliseconds) {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
            };

            while (timer1.Enabled) {
                Application.DoEvents();
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            if (sender.Equals(button1)) {
                if (((Button)sender).Text.Equals("Start")) {
                    button1.Text = "Stop";
                    timerStop = false;
                    startTimer((int)numericUpDown1.Value, (int)numericUpDown2.Value);
                }else {
                    button1.Text = "Start";
                    timerStop = true;
                    //label2.Text = time2string(0);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            //label1.Font = myFont;
            label2.Font = myFont;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            // Specify that the link was visited.
            this.linkLabel1.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://opengameart.org/content/lanapixel-localization-friendly-pixel-font");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            // Specify that the link was visited.
            this.linkLabel2.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://rrze-pp.github.io/rrze-icon-set/");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
        
            // Specify that the link was visited.
            this.linkLabel3.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://joinup.ec.europa.eu/collection/eupl/eupl-text-eupl-12");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            // Specify that the link was visited.
            this.linkLabel4.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://github.com/SvenMathing/");
        }
    }
}
