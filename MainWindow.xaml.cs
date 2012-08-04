using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Recognition;//Voice Recognize
using System.Diagnostics;//Process
using System.Speech.Synthesis;//Speaking
using System.Windows.Forms;
using System.Drawing;

namespace VoiceApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SpeechRecognitionEngine Speech = new SpeechRecognitionEngine();
        private SpeechSynthesizer DeviceSpeak = new SpeechSynthesizer();
        Process currentProcess = Process.GetCurrentProcess();
        
        
       
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void recbtn_Click(object sender, RoutedEventArgs e)
        {
                /*Configuring the SpeechRecognitionEngine*/

               // Speech = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

                Speech.SetInputToDefaultAudioDevice(); // Set Deafault microphone to listen voice

                gmBuilder(); // Grammar Loader
               anotherGm();
            
                Speech.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_Speech_SpeechRecognized);

                Speech.RecognizeAsync(RecognizeMode.Multiple); // Start asynchronous, continuous speech recognition.
                
          
         }

        void _Speech_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            

            foreach (RecognizedWordUnit word in e.Result.Words)
            {
                //inputfield.Items.Add(word.Text);
                if (word.Confidence > 0.0f)
                {
                    inputfield.Items.Add(word.Text);
                    switch (word.Text)
                    {
                        case "FireFox":
                            DeviceSpeak.Speak("Starting Firefox");
                            Process.Start("firefox.exe");
                            break;

                        case "Notepad":
                            DeviceSpeak.Speak("Starting Notepad");
                            currentProcess = Process.Start("Notepad.exe");
                            break;

                        case "Test":
                            currentProcess = Process.Start("http://yasiradnan.com");
                            DeviceSpeak.Speak("" + currentProcess);
                            /*DeviceSpeak.Speak("Starting uTorrent");
                                Process.Start("uTorrent.exe");*/
                            break;
                        case "New Tab":
                            DeviceSpeak.Speak("Opening New Tab");
                            SendKeys.SendWait("^{T}");
                            break;

                        case "Close":
                            //DeviceSpeak.Speak("Closing Application" + currentProcess.CloseMainWindow());
                            DeviceSpeak.Speak("Closing Application");
                            //currentProcess.CloseMainWindow();
                            SendKeys.SendWait("%{F4}");
                            break;

                        default:
                            //DeviceSpeak.Speak("Searching"+word.Text);
                            //currentProcess = Process.Start("http://google.com/search?q="+word.Text);
                            break;
                    }
                }

            }

           
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
         //   Current.Shutdown(); //exit from application

        }

        private void gmBuilder()
        {
            Choices cmd = new Choices();
            cmd.Add(new string[] {"Test", "Close", "FireFox", "New Tab","Notepad" });

            /*Create a GrammarBuilder object and append the Choices object.*/
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(cmd);

            /*Create the Grammar instance and load it into the speech recognizer.*/
            Grammar g = new Grammar(gb);
            Speech.LoadGrammar(g);
         }

        private void anotherGm()
        {
            Speech.LoadGrammar(new DictationGrammar());

        }
        private void MouseMove(int x, int y)
        {
            /*Moving Mouse*/
           

            


        }
    }
}
