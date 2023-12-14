//using atakmap.cpp_cli.util;
//using MapEngine.Interop.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Net.Http;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Xml;
//using WinTak.Framework.Messaging;
//using WinTak.CursorOnTarget.Services;
//using TAKEngine.Core;
//using WinTak.CursorOnTarget.Services;
//using WinTak.CursorOnTarget.Services;
//using WinTak.Display;
//using WinTak.Framework;
//using WinTak.Framework.Docking;
//using WinTak.Framework.Docking.Attributes;

namespace WinTAK_Simple_Usage_Plugin
{
    [WinTak.Framework.Docking.Attributes.DockPane(ID, "Simple Plugin DockPane", Content = typeof(PluginUserControl))]
    internal class Plugin_DockPane : WinTak.Framework.Docking.DockPane
    {
        private int _counter;
        internal const string ID = "Example_Plugin_DockPane";
        private double _lon;
        private double _lat;
        private bool _isActive;
        public static string PanToWhiteHouseWithRadialUID { get; set; } = string.Empty;

        private WinTak.CursorOnTarget.Services.ICotMessageSender _messageSender;
        private WinTak.Framework.Messaging.IMessageHub _messageHub;
        private WinTak.CursorOnTarget.Services.ICotMessageReceiver _messageReceiver;
        private IEnumerable<WinTak.Location.Providers.ILocationProvider> _locationProviders;
        private WinTak.Framework.ILogger _logger;

        public double Lon
        {
            get { return _lon; }
            set { SetProperty(ref _lon, value); }
        }

        public double Lat
        {
            get { return _lat; }
            set { SetProperty(ref _lat, value); }
        }

        public ICommand CounterCommand
        {
            get;
            private set;
        }

        public ICommand MessageBoxCaptionCommand
        {
            get;
            private set;
        }

        public ICommand NotificationCommand
        {
            get;
            private set;
        }

        public int Counter
        {
            get { return _counter; }
            set { SetProperty(ref _counter, value); }
        }
        public ICommand MessageBoxCommand
        {
            get;
            private set;
        }

        public ICommand FocusMapAreaRadialCommand
        {
            get;
            private set;
        }

        public ICommand FocusMapAreaCommand
        {
            get;
            private set;
        }

        public ICommand ICotMessageSenderCommand
        {
            get;
            private set;
        }

        /*        [ImportMany(AllowRecomposition = true)]
                IEnumerable<WinTak.Location.Providers.ILocationProvider> LocationProviders
                {
                    get { return _locationProviders; }
                    set { _locationProviders = value; }
                }

                [ImportMany(AllowRecomposition = true)]
                WinTak.CursorOnTarget.Services.ICotMessageSender messageSender
                {
                    get { return _messageSender; }
                    set { _messageSender = value; }
                }

                [ImportMany(AllowRecomposition = true)]
                WinTak.CursorOnTarget.Services.ICotMessageReceiver messageReceiver
                {
                    get { return _messageReceiver; }
                    set { _messageReceiver = value; }
                }

                [ImportMany(AllowRecomposition = true)]
                WinTak.Framework.Messaging.IMessageHub messageHub
                {
                    get { return _messageHub; }
                    set { _messageHub = value; }
                }*/

        /*        public Plugin_DockPane()
                {
                    var command = new ExecutedCommand();
                    command.Executed += OnCommandExecuted;
                    NotifyCommand = command;

                    var _messageboxcommand = new ExecutedCommand();
                    _messageboxcommand.Executed += MessageBoxCommandExecuted;
                    MessageBoxCommand = _messageboxcommand;

                    var _messageboxcaptioncommand = new ExecutedCommand();
                    _messageboxcaptioncommand.Executed += MessageBoxCaptionExecuted;
                    MessageBoxCaptionCommand = _messageboxcaptioncommand;

                    var _notificationcommand = new ExecutedCommand();
                    _notificationcommand.Executed += NotificationExecuted;
                    NotificationCommand = _notificationcommand;

                    var _iCotMessageSenderCommand = new ExecutedCommand();
                    _iCotMessageSenderCommand.Executed += ICotMessageSenderExecuted;
                    ICotMessageSenderCommand = _iCotMessageSenderCommand;

                    var _focusMapAreaCommand = new ExecutedCommand();
                    _focusMapAreaCommand.Executed += FocusMapAreaExecuted;
                    FocusMapAreaCommand = _focusMapAreaCommand;

                    var _focusMapAreaRadialCommand = new ExecutedCommand();
                    _focusMapAreaRadialCommand.Executed += FocusMapAreaWithRadialExecuted;
                    FocusMapAreaRadialCommand = _focusMapAreaRadialCommand;

                    _messageSender = messageSender;
                    messageReceiver.MessageReceived += OnMessageReceived;

                    _messageHub = messageHub;
                }*/


        [ImportingConstructor]
        public Plugin_DockPane(WinTak.CursorOnTarget.Services.ICotMessageSender messageSender,
            WinTak.CursorOnTarget.Services.ICotMessageReceiver messageReceiver,
            WinTak.Framework.ILogger logger,
            WinTak.Framework.Notifications.INotificationLog notification,
            WinTak.Framework.Messaging.IMessageHub messageHub)
        {
            var counterCommand = new ExecutedCommand();
            counterCommand.Executed += OnCounterExecuted;
            CounterCommand = counterCommand;

            var messageboxcommand = new ExecutedCommand();
            messageboxcommand.Executed += MessageBoxCommandExecuted;
            MessageBoxCommand = messageboxcommand;

            var messageboxcaptioncommand = new ExecutedCommand();
            messageboxcaptioncommand.Executed += MessageBoxCaptionExecuted;
            MessageBoxCaptionCommand = messageboxcaptioncommand;

            var notificationcommand = new ExecutedCommand();
            notificationcommand.Executed += NotificationExecuted;
            NotificationCommand = notificationcommand;

            var iCotMessageSenderCommand = new ExecutedCommand();
            iCotMessageSenderCommand.Executed += ICotMessageSenderExecuted;
            ICotMessageSenderCommand = iCotMessageSenderCommand;

            var focusMapAreaCommand = new ExecutedCommand();
            focusMapAreaCommand.Executed += FocusMapAreaExecuted;
            FocusMapAreaCommand = focusMapAreaCommand;

            var focusMapAreaRadialCommand = new ExecutedCommand();
            focusMapAreaRadialCommand.Executed += FocusMapAreaWithRadialExecuted;
            FocusMapAreaRadialCommand = focusMapAreaRadialCommand;

            _messageSender = messageSender;
            messageReceiver.MessageReceived += OnMessageReceived;

            _messageHub = messageHub;

            _logger = logger;
        }

        private void PanToWhiteHouse()
        {
            var message = new WinTak.Common.Messaging.FocusMapMessage(new TAKEngine.Core.GeoPoint(38.8977, -77.0365)) { Behavior = WinTak.Common.Events.MapFocusBehavior.PanOnly };
            _messageHub.Publish(message);
        }

        private void PanToWhiteHouseWithRadial()
        {
            string uid = PanToWhiteHouseWithRadialUID; // UID of existing Map Object
            var message = new WinTak.Common.Messaging.PanToMapObjectMessage(uid) { ShowRadialMenu = true };
            _messageHub.Publish(message);
        }

        private void OnMessageReceived(object sender, WinTak.Common.CoT.CoTMessageArgument args)
        {
            // Handle CoT Message         
           WinTak.UI.Notifications.Notification.NotifyInfo("COT Message", $"{args.Message}");
           PanToWhiteHouseWithRadialUID = args.Uid;         
        }


        public new bool IsActive
        {
            get { return _isActive; }
            set
            {
                SetProperty<bool>(ref _isActive, value, "IsActive");

                if (value)
                {
                    WinTak.Display.MapViewControl.MapMouseMove += MapViewControl_MapMouseMove;
                }
                else
                {
                    WinTak.Display.MapViewControl.MapMouseMove -= MapViewControl_MapMouseMove;
                }
            }
        }

        private void OnCounterExecuted(object sender, EventArgs e)
        {
            Counter++;
        }

        private void MapViewControl_MapMouseMove(object sender, WinTak.Display.MapMouseEventArgs e)
        {
            if (true)
            {              
                Lat = e.WorldLocation.Latitude;
                Lon = e.WorldLocation.Longitude;
            }
        }

        private void MessageBoxCommandExecuted(object sender, EventArgs e)
        {
            _logger.Info("| (WinTAK_Simple_Usage_plugin) MessageBox Show : This is a messagebox show using WinTak's UI");
            WinTak.UI.Controls.MessageBox.Show("This is a messagebox show using WinTak's UI");
        }

        private void MessageBoxCaptionExecuted(object sender, EventArgs e)
        {
            //atakmap.cpp_cli.util.Logger.log(atakmap.cpp_cli.util.Logger.Level.Info, "Button Clicked");
            _logger.Info("| (WinTAK_Simple_Usage_plugin) MessageBox Show Caption : This is a messagebox show with a caption using WinTak's UI");
            WinTak.UI.Controls.MessageBox.Show("This is a messagebox show with a caption using WinTak's UI", "Test caption", MessageBoxButton.OK);
        }

        private void NotificationExecuted(object sender, EventArgs e)
        {
            _logger.Info("| (WinTAK_Simple_Usage_plugin) Notification NotifyInfo : This is a NotifyInfo Notification using WinTak's UI");
            WinTak.UI.Notifications.Notification.NotifyInfo("WinTak's UI info notification", "This is a NotifyInfo Notification using WinTak's UI");
        }

        private void ICotMessageSenderExecuted(object sender, EventArgs e)
        {
            _logger.Info("| (WinTAK_Simple_Usage_plugin) CotMessageSender : This is a GeoChatALL text message on WinTak's UI");
            var Cots = new List<string>
            {
                CoTxmlClass.XMLGeoChatALL("This is a GeoChatALL text message on WinTak's UI"),
                CoTxmlClass.XMLPresence("a-f-G-U-C-I", "Yellow", "Team Lead", "Presence", "29.8587", "-31.0218"),
                CoTxmlClass.XMLStartEmergency("911 Alert Electricity is KAKing", "Serious eskom se DOOS", "29.6006", "-30.3794"),
                //CoTxmlClass.XMLCancelEmergency(CoTxmlClass.uid),
                CoTxmlClass.XMLSendCot("a-f-G-U-C-I", "Team Lead", "0", "0")
             };
            foreach (var cot in Cots)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(cot);
                _messageSender.Send(doc);
                //_messageSender.SendInternal(doc);
            }
        }

        private void FocusMapAreaExecuted(object sender, EventArgs e)
        {
            _logger.Info("| (WinTAK_Simple_Usage_plugin) Focus Map Area Executed : PanToWhiteHouse");
            PanToWhiteHouse();
        }

        private void FocusMapAreaWithRadialExecuted(object sender, EventArgs e)
        {
            _logger.Info("| (WinTAK_Simple_Usage_plugin) Focus Map Area With Radial Executed : PanToWhiteHouseWithRadial");
            PanToWhiteHouseWithRadial();
        }


        private class ExecutedCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public event EventHandler Executed;

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                var handler = Executed;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }


        private static bool _running;

        public static void Start()
        {
            WinTak.UI.Notifications.Notification.NotifyInfo("Start HTTPS", "HTTPS request started");
            if (_running) return;
            Thread updateThread = new Thread(BackgroundUpdaterAsync) { IsBackground = true };
            updateThread.Start();
        }

        private static async void BackgroundUpdaterAsync()
        {
            _running = true;
            //SettingsClass.EnableNoIPUpdater
            while (true)
            {

                using (HttpClient client = new HttpClient())
                {
                    WinTak.UI.Notifications.Notification.NotifyInfo("GetAsync Activated", "GetAsync request Activated");
                    string url = "https://dummyjson.com/products/1";
                    //var httpContent = new StringContent(payload.ToString(), Encoding.UTF8, "application/json");
                    //logsrichTextBox.AppendText("SENT => " + Environment.NewLine);
                    //logsrichTextBox.AppendText(url + Environment.NewLine);
                    //Add Default Request Headers
                    //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {VarClass.At}");
                    try
                    {
                        //using (HttpResponseMessage response = await client.GetAsync(new Uri(url)))
                        using (HttpResponseMessage response = await client.GetAsync(new Uri(url)))
                        {
                            using (HttpContent content = response.Content)
                            {
                                //Read the result and display in Textbox
                                string result = await content.ReadAsStringAsync();//Result string JSON
                                string reasonPhrase = response.ReasonPhrase;//Reason OK, FAIL etc.
                                WinTak.UI.Notifications.Notification.NotifyInfo("Try HTTPS Result", $"{result}");
                                //logsrichTextBox.AppendText("RESULT => " + Environment.NewLine);
                                //logsrichTextBox.AppendText(result + Environment.NewLine);
                                //logsrichTextBox.AppendText(reasonPhrase + Environment.NewLine);
                                //JsonGetDevicesSerializer(result);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, "Could not test eWeLink API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //Logger.WriteLine($" *** Error:{ex.Message} [eWeLink_API_Form] ***");
                        return;
                    }
                }
                /*                    try
                                    {
                                        Notification.NotifyInfo("Try HTTPS Activated", "Try HTTPS request Activated");
                                        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://dynupdate.no-ip.com/nic/update?hostname={0}", SettingsClass.NoIPHost));
                                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://dummyjson.com/products/1"));
                                        request.Proxy = null;
                                        request.UserAgent = string.Format("Plugin No-Ip Updater/2.0 {0}", SettingsClass.NoIPUsername);
                                        request.Timeout = 10000;
                                        request.Headers.Add(HttpRequestHeader.Authorization, string.Format("Basic {0}", Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", SettingsClass.NoIPUsername, SettingsClass.NoIPPassword)))));
                                        request.Method = "GET";

                                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                                        {
                                            Notification.NotifyInfo("HTTPS Response", $"{response.ContentType}");
                                        response.
                *//*                            using (HttpContent content = response)
                                            {
                                                //Read the result and display in Textbox
                                                string result = await content.ReadAsStringAsync();//Result string JSON
                                                string reasonPhrase = response.ReasonPhrase;//Reason OK, FAIL etc.
                                                logsrichTextBox.AppendText("RESULT => " + Environment.NewLine);
                                                logsrichTextBox.AppendText(result + Environment.NewLine);
                                                logsrichTextBox.AppendText(reasonPhrase + Environment.NewLine);
                                                JsonLoginSerializer(result);
                                            }*//*
                                        }
                                    }
                                    catch
                                    {
                                    }*/

                Thread.Sleep(TimeSpan.FromMinutes(10));
            }
            _running = false;
        }



    }
}
