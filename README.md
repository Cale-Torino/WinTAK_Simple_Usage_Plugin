# WinTAK Simple Usage Plugin

A simple plugin showing a few of the basic components which can be utilized when you develop a plugin for **WinTAK**











# Example Guide

## 1. WinTAK Coding Style

- Please read the Naming Guidelines found here: https://msdn.microsoft.com/en-us/library/ms229042.aspx
- Key Style Guidelines
    - DO NOT use hungarian style notation in C# (m_iSomeInteger fields, bSomeBoolean parameter <= don't want to see that)
    - DO use PascalCasing for class names, method names and public properties
    - DO use camelCasing for parameter names
    - DO use camelCasing with an underscore prefix for private fields (private _someField)
    - DO follow the Microsoft Framework Design Guidelines!

## 2. Focus on a Map Area

Client code can programmatically request **WinTAK** to focus on a GeoPoint(s) (pan and zoom to).

This and other actions can be achieved with the `WinTak.Framework.Messaging.IMessageHub` pub/sub interface.

```C#
class MyPluginClass
{
    private WinTak.Framework.Messaging.IMessageHub _messageHub;

    [ImportingConstructor]
    public MyPluginClass(WinTak.Framework.Messaging.IMessageHub messageHub)
    {
        _messageHub= messageHub;
    }

    private void PanToWhiteHouse()
    {
        var message = new WinTak.Common.Messaging.FocusMapMessage(new GeoPoint(38.8977, -77.0365)){ Behavior = MapFocusBehavior.PanOnly };
        _messageHub.Publish(message);
    }
}
```

## 3. Focus on a MapObject and Showing the Radial Menu

Client code can programmatically request **WinTAK** to focus on a MapObject (pan and zoom to),
as well as show the radial menu.

This and other actions can be achieved with the `WinTak.Framework.Messaging.IMessageHub` pub/sub interface.

```C#
class MyPluginClass
{
    private WinTak.Framework.Messaging.IMessageHub _messageHub;

    [ImportingConstructor]
    public MyPluginClass(WinTak.Framework.Messaging.IMessageHub messageHub)
    {
        _messageHub= messageHub;
    }

    private void PanToWhiteHouse()
    {
        string uid = ... // UID of existing Map Object
        var message = new WinTak.Common.Messaging.PanToMapObjectMessage(uid){ ShowRadialMenu = true };
        _messageHub.Publish(message);
    }
}
```

## 4. Runtime Plugin Loading and Part Recomposition

With **WinTAK 4.0** Plugins can now be loaded or unloaded at runtime.

In previous versions of **WinTAK** a restart was necessary in order to change which plugins were being loaded.

With this feature in mind Plugin developers need to make sure that their plugins can handle runtime loading/unloading by being able to recompose parts.

**WinTAK** plugins use **MEF** for dependency injection.

Most core services of **WinTAK** are brought into a plugin via plugin classes using an `[ImportingConstructor]`.

However, this is not the only way to acquire an exported interface from **WinTAK**.

In addition to `[ImportingConstructor]`'s you can make use of an `[Import]` property. The benefit of [Import] properties is that they can allow for recomposition, and they can be used to ingest interfaces that may not always be available.

```C#
class MyPluginClass
{
    private IEnumerable<WinTak.Location.Providers.ILocationProvider> _locationProviders;

    [ImportingConstructor]
    public MyPluginClass(IEnumerable<WinTak.Location.Providers.ILocationProvider> locationProviders)// This could cause an exception if a plugin is loaded/unloaded that exports an ILocationProvider
    {
        _locationProviders = locationProviders;
    }

    [ImportMany(AllowRecomposition = true)]
    IEnumerable<WinTak.Location.Providers.ILocationProvider> LocationProviders// Because this property allows recomposition it will not cause an exception if a plugin is loaded/unloaded
    {
        get { return _locationProviders; }
        set { _locationProviders = value; }
    }
}
```

As a general rule you should make use of [Import] properties over [ImportingConstructor]'s when Importing a collection of Interfaces.

Plugins can export their own `ILocationProvider`,
`IChatService` and
`IAlertProvider` interfaces as a few examples,
and all of these interfaces must be Imported's as an `IEnumerable` collection of interfaces.

(because there will most likely be more then 1 of each of these interfaces available) 
and therefore should be ingested via `[Import]` properties over an `[ImportingConstructor]`. 

## 5. Sending and Recieving CoT Messages in WinTAK

`WinTak.CursorOnTarget.Services.ICotMessageSender` and `WinTak.CursorOnTarget.Services.ICotMessageReceiver` are the CoT message handling interfaces.
These interfaces are used to send and recieve CoT messages internally.

`ICotMessageReceiver` can be used to inspect any incoming messages that have been received from the network,
or that are being created and sent by other plugins or core WinTAK via the `ICotMessageSender` interface.

`ICotMessageSender` is used to send messages internally so that they can be handled and processed by core **WinTAK** (and other plugins)
as if the message has been received from one of it's network inputs.

These interfaces can be acquired via Dependency Injection by your class's ImportingConstructor or via an Import property.


```C#
class MyPluginClass
{
    private WinTak.CursorOnTarget.Services.ICotMessageSender _messageSender;

    [ImportingConstructor]
    public MyPluginClass(WinTak.CursorOnTarget.Services.ICotMessageSender messageSender, WinTak.CursorOnTarget.Services.ICotMessageReceiver messageReceiver)
    {
        _messageSender = messageSender;
        messageReceiver.MessageReceived += OnMessageReceived;
    }

    private void OnMessageReceived(object sender, WinTak.Common.CoT.CoTMessageArgument args)
    {
        // Handle CoT Message
    }
}
```

Sending a COT example

```C#
/// <summary>
/// Sends Cot XML stream to all connected EUDs.
/// </summary>
/// <param name="type">Type to send</param>
/// <param name="callsign">Callsign</param>
/// <param name="lon">Longitude</param>
/// <param name="lat">Latitude</param>
public static string XMLSendCot(string type, string callsign, string lon, string lat)
{
    //XMLSendCot("a-f-G","C.A Torino","0","0");
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

string coTXMLSendCot = CoTxmlClass.XMLSendCot("a-f-G-U-C-I", "C.A Torino", "0", "0");
var cotXml = new XmlDocument();
cotXml.LoadXml(coTXMLSendCot);
_messageSender.Send(cotXml);
```

To send a CoT message to the network the `WinTak.Common.Services.ICommunicationService` interface is used.

This interface has BroadcastCot and SendCot methods that can be used to either broadcast a CoT message to every node on the network,
or send it to a known Contact on the network.





