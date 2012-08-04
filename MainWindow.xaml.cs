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
using System.Text.RegularExpressions;

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
              // anotherGm();
            
                Speech.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(_Speech_SpeechRecognized);

                Speech.RecognizeAsync(RecognizeMode.Multiple); // Start asynchronous, continuous speech recognition.
                
          
         }

        void _Speech_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            

            foreach (RecognizedWordUnit word in e.Result.Words)
            {
                //inputfield.Items.Add(word.Text);
                    inputfield.Items.Add(word.Text);
                    switch (word.Text)
                    {
                        case "MyComputer":
                            DeviceSpeak.Speak("Opening My Computer");
                            Process.Start("::{20d04fe0-3aea-1069-a2d8-08002b30309d}");
                            break;
                        
                        case "FireFox":
                            DeviceSpeak.Speak("Starting Firefox");
                            currentProcess = Process.Start("firefox.exe");
                            break;

                        case "Notepad":
                            DeviceSpeak.Speak("Starting Notepad");
                            currentProcess = Process.Start("Notepad.exe");
                            break;
                        
                        case "NewTab":
                            DeviceSpeak.Speak("Opening New Tab");
                            SendKeys.SendWait("^{T}");
                            break;
                        case "ControlPanel":
                            DeviceSpeak.Speak("Opening Control Panel");
                            currentProcess = Process.Start("::{21EC2020-3AEA-1069-A2DD-08002b30309d}");
                            break;
                            
                        case "NetworkPlaces":
                            DeviceSpeak.Speak("Opening Network Places");
                            currentProcess = Process.Start("::{208d2c60-3aea-1069-a2d7-08002b30309d}");
                            break;
                        case "StartMenu" :
                            DeviceSpeak.Speak("Opening Start Menu");
                           // currentProcess = Process.Start("::{48e7caab-b918-4e58-a94d-505519c795dc}");
                            break;
                        case "RecycleBin" :
                            DeviceSpeak.Speak("Opening Recycle Bin");
                            currentProcess = Process.Start("::{645FF040-5081-101B-9F08-00AA002F954E}");
                            break;
                        case "MyDocuments" :
                            DeviceSpeak.Speak("Opening My Documents");
                            currentProcess = Process.Start("::{450D8FBA-AD25-11D0-98A8-0800361B1103}");
                            break;
                        case "AdministrativeTools" :
                            DeviceSpeak.Speak("Opening My Documents");
                            currentProcess = Process.Start("::{D20EA4E1-3957-11d2-A40B-0C5020524153}");
                            break;
                        case "TaskManager":
                            DeviceSpeak.Speak("Opening Task Manager");
                            currentProcess = Process.Start("^+{ESC}");
                            break;
                        case "Close":
                            DeviceSpeak.Speak("Closing Application");
                            SendKeys.SendWait("%{F4}");
                            break;
                        
                        default:
                            //DeviceSpeak.Speak("Searching"+word.Text);
                            //currentProcess = Process.Start("http://google.com/search?q="+word.Text);
                            break;
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
            cmd.Add(new string[] {"Close", "FireFox", "NewTab","Notepad","MyComputer","ControlPanel","NetworkPlaces","StartMenu","RecycleBin","MyDocuments","Administrative Tools","TaskManager" });

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
        private void MoveMouse(int x, int y)
        {
            /*this.Cursor = new System.Windows.Input.Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(x, y);
            Cursor.Clip = new Rectangle(this.Location, this.Size);*/
        }
    }
}
