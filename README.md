# UDP Forwarder for Web API

## About ##
The udp-forwarder is an open source library available as a NuGet package [link] for Web APIs. It grabs http requests sent to the API, creates a rich log event in a JSON format and sends it to a remote log service, like Logstash, via UDP.

## Usage ##
To install the udp-forwarder to your WebAPI project: 
* Run "Install-Package udp-forwarder" in the Package Manager Console.
* Add the following to App_Start/WebAPIConfig.cs
```csharp
var serverIp = "0.0.0.0";
var serverPort = 0000;
var logService = new LogService(new UDPService(serverIp, serverPort));
config.MessageHandlers.Add(new LoggingHandler(logService));
```
* Edit the serverIP and serverPort variables to match the setup of your log service.
* OPTIONAL: If for some reason the forwarder cannot send the log to the remote log service, then that log is dismissed. This is the default configuration but there is an optional feature to log these instances to a text file. To enable this feature, add the following key to appSettings of the WebAPI's Web.config file, where the value points to the desired directory where the logs should be stored.
```csharp
<add key="FallbackLogPath" value="c:/logs">
```

* Save and run the API.

## Extensions ##
Wish to send logs with other method than UDP? <br><br>
The upd-forwarder uses an UDP service to send the logs, but the library can be extended to use other services. To do so, fork this project and create a new service that implements the ITransferService interface. Then inject the new service into your instance of the LogServie class in the WebAPIConfig.cs file:
```csharp
var logService = new LogService(new YOUR-NEW-SERVICE(serverIp, serverPort));
```

## License ##
This software is licensed under the [MIT license](https://github.com/TopGunSoftware/udp-forwarder/blob/master/LICENSE).


			