using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinTAK_Simple_Usage_Plugin
{
    /// <summary>
    /// Class <c>CoTxmlClass</c>: A simple class using a few XML methods as a string for the <c>ICotMessageSender</c>.
    /// 
    /// <list type="bullet">
    /// 
    /// <item>
    /// <description>Author: C.A Torino</description>
    /// </item>
    /// 
    /// <item>
    /// <description>Version: 1.0.0.0</description>
    /// </item>
    /// 
    /// <item>
    /// <description>Date: 11th December 2023</description>
    /// </item>
    /// 
    /// </list>
    /// 
    /// </summary>
    /// https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/documentation-comments
    internal class CoTxmlClass
    {
        /// <summary>
        /// Sends Emergency XML stream to all connected EUDs.
        /// See
        /// <see cref="" href="url" langword=" keyword" />
        /// </summary>
        /// 
        /// <remarks>
        /// string <c>uid</c> to cancle
        /// </remarks>
        /// 
        /// <value>
        /// Property <c>uid</c> represents the unique ID of the COT to cancle.
        /// </value>
        /// 
        /// <returns>
        /// CancelEmergency XML string with defined <c>uid</c>
        /// </returns>
        /// 
        /// 
        /// <param name="uid">uid to cancle</param>
        public static string XMLCancelEmergency(string uid)
        {
            //XMLCancelEmergency(XMLToSendClass.uid);
            string timeStart = DateTime.UtcNow.ToString("o");
            string stale = DateTime.UtcNow.AddMinutes(5).ToString("o");
            string x =
            "<?xml version = \"1.0\" encoding = \"UTF-8\"?>" +
            $"<event version=\"2.0\" uid=\"{uid}\" type=\"b-a-o-can\" how=\"h-e\" start=\"{timeStart}\" time=\"{timeStart}\" stale=\"{stale}\">" +
            "<detail>" +
            "<emergency cancel=\"true\"/>" +
            "</detail>" +
            "<point le=\"9999999.0\" ce=\"9999999.0\" hae=\"9999999.0\" lon=\"0\" lat=\"0\"/>" +
            "</event>";
            return x;
        }

        public static string uid { get; set; }
        /// <summary>
        /// Sends Emergency XML stream to all connected EUDs.
        /// </summary>
        /// <param name="type">Type to send</param>
        /// <param name="callsign">Callsign</param>
        /// <param name="lon">Longitude</param>
        /// <param name="lat">Latitude</param> 
        public static string XMLStartEmergency(string type, string callsign, string lon, string lat)
        {
            //XMLStartEmergency("911 Alert","Serious","0","0");
            string timeStart = DateTime.UtcNow.ToString("o");
            string stale = DateTime.UtcNow.AddMinutes(5).ToString("o");
            string g = Guid.NewGuid().ToString();//80992df2-e9af-11eb-a4c3-0025907b8f7d
            string x =
            "<?xml version = \"1.0\" encoding = \"UTF-8\"?>" +
            $"<event version=\"2.0\" uid=\"{g}\" type=\"b-a-o-tbl\" how=\"m-g\" start=\"{timeStart}\" time=\"{timeStart}\" stale=\"{stale}\">" +
            "<detail>" +
            $"<link uid=\"SERVER\" relation=\"p-p\" production_time=\"{timeStart}\" type=\"a-f-G-U-C\" />" +
            $"<contact callsign=\"{callsign}\" />" +
            $"<emergency type=\"{type}\" />" +
            "</detail>" +
            $"<point le=\"9999999.0\" ce=\"9999999.0\" hae=\"9999999.0\" lon=\"{lon}\" lat=\"{lat}\" />" +
            "</event>";
            uid = g;
            return x;
        }

        /// <summary>
        /// Sends Cot XML stream to all connected EUDs.
        /// </summary>
        /// <param name="type">Type to send</param>
        /// <param name="color">Team color</param>
        /// <param name="role">Team role</param>
        /// <param name="callsign">Callsign</param>
        /// <param name="lon">Longitude</param>
        /// <param name="lat">Latitude</param> 
        public static string XMLPresence(string type, string color, string role, string callsign, string lon, string lat)
        {
            //XMLPresence("a-f-G-U-C-I","Yellow","Team Lead","Presence","0","0");
            string timeStart = DateTime.UtcNow.ToString("o");
            string stale = DateTime.UtcNow.AddMinutes(5).ToString("o");
            string g = Guid.NewGuid().ToString();//80992df2-e9af-11eb-a4c3-0025907b8f7d
            string x =
            "<?xml version = \"1.0\" encoding = \"UTF-8\"?>" +
            $"<event version=\"2.0\" uid=\"{g}\" type=\"{type}\" how=\"h-g-i-g-o\" start=\"{timeStart}\" time=\"{timeStart}\" stale=\"{stale}\">" +
            "<detail>" +
            $"<contact callsign=\"{callsign}\" />" +
            $"<__group name=\"{color}\" role=\"{role}\" />" +
            "</detail>" +
            $"<point le=\"9999999.0\" ce=\"9999999.0\" hae=\"9999999.0\" lon=\"{lon}\" lat=\"{lat}\" />" +
            "</event>";
            return x;
        }

        /// <summary>
        /// Sends Cot XML stream to all connected EUDs.
        /// </summary>
        /// <param name="type">Type to send</param>
        /// <param name="callsign">Callsign</param>
        /// <param name="lon">Longitude</param>
        /// <param name="lat">Latitude</param>
        public static string XMLSendCot(string type, string callsign, string lon, string lat)
        {
            //XMLSendCot("a-f-G","Daisy","0","0");
            //type = a-f-G
            //type = a-h-G-U-C-E
            string timeStart = DateTime.UtcNow.ToString("o");
            string stale = DateTime.UtcNow.AddMinutes(5).ToString("o");
            string g = Guid.NewGuid().ToString();//80992df2-e9af-11eb-a4c3-0025907b8f7d
            string x =
            "<?xml version = \"1.0\" encoding = \"UTF-8\"?>" +
            $"<event version=\"2.0\" uid=\"{g}\" type=\"{type}\" how=\"h-g-i-g-o\" start=\"{timeStart}\" time=\"{timeStart}\" stale=\"{stale}\">" +
            "<detail>" +
            $"<contact callsign=\"{callsign}\" />" +
            "</detail>" +
            $"<point le=\"9999999.0\" ce=\"9999999.0\" hae=\"9999999.0\" lon=\"{lon}\" lat=\"{lat}\" />" +
            "</event>";
            return x;
        }

        /// <summary>
        /// Sends GeoChat XML stream to all connected EUDs.
        /// </summary>
        /// <param name="msg">text to send</param>
        public static string XMLGeoChatALL(string msg)
        {
            string timeStart = DateTime.UtcNow.ToString("o");
            string stale = DateTime.UtcNow.AddMinutes(5).ToString("o");
            string g = Guid.NewGuid().ToString();//80992df2-e9af-11eb-a4c3-0025907b8f7d
            string x =
            "<?xml version = \"1.0\" encoding = \"UTF-8\"?>" +
            $"<event version=\"2.0\" uid=\"{g}\" type=\"b-t-f\" how=\"h-g-i-g-o\" start=\"{timeStart}\" time=\"{timeStart}\" stale=\"{stale}\">" +
            "<detail>" +
            "<__chat id=\"All Chat Rooms\" chatroom=\"All Chat Rooms\" groupOwner=\"false\">" +
            "<chatgrp uid0=\"SERVER-UID\" uid1=\"All Chat Rooms\" id=\"SERVER-UID\" />" +
            "</__chat>" +
            "<link uid=\"Server Admin\" relation=\"p-p\" type=\"a-f-G-U-C-I\" />" +
            $"<remarks time=\"{timeStart}\" source=\"Server Admin\" to=\"All Chat Rooms\">{msg}</remarks>" +
            "<__serverdestination />" +
            "<marti>" +
            "<dest>" +
            "<callsign />" +
            "</dest>" +
            "</marti>" +
            "</detail>" +
            "<point le=\"9999999.0\" ce=\"9999999.0\" hae=\"9999999.0\" lon=\"0\" lat=\"0\" />" +
            "</event>";
            return x;
        }

        /// <summary>
        /// Sends GeoChat Welcome message XML stream to all connected EUDs.
        /// </summary>
        public static string XMLGeoChat(string uid, string callsign)
        {
            string timeStart = DateTime.UtcNow.ToString("o");
            string stale = DateTime.UtcNow.AddMinutes(5).ToString("o");
            string g = Guid.NewGuid().ToString();//80992df2-e9af-11eb-a4c3-0025907b8f7d
            string x =
            "<?xml version = \"1.0\" encoding = \"UTF-8\"?>" +
            $"<event version=\"2.0\" uid=\"GeoChat.SERVER-UID.{callsign}.{g}\" type=\"b-t-f\" how=\"h-g-i-g-o\" start=\"{timeStart}\" time=\"{timeStart}\" stale=\"{stale}\">" +
            "<detail>" +
            $"<__chat id=\"{uid}\" parent=\"RootContactGroup\" chatroom=\"{callsign}\" groupOwner=\"True\">" +
            $"<chatgrp uid0=\"SERVER-UID\" uid1=\"{uid}\" id=\"{uid}\"/>" +
            "</__chat>" +
            "<link uid=\"SERVER-UID\" relation=\"p-p\" type=\"a-f-G-U-C-I\"/>" +
            $"<remarks time=\"{timeStart}\" source=\"SERVER\" to=\"{uid}\">Welcome to the C# Testing Server; C.A Torino</remarks>" +
            "<__serverdestination/>" +
            "<marti>" +
            "<dest>" +
            "<callsign/>" +
            "</dest>" +
            "<dest/>" +
            "<dest/>" +
            "</marti>" +
            "</detail>" +
            "<point le=\"9999999.0\" ce=\"9999999.0\" hae=\"9999999.0\" lon=\"0\" lat=\"0\"/>" +
            "</event>";
            return x;
        }
    }
}
