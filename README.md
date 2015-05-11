# UDP Forwarder for Web API

## About ##
The udp-forwarder is an open source software available as a NuGet package for Web APIs.  It grabs http requests sent to the API, creates a rich log on JSON format and sents to a remote log service like Logstash via UDP.

## Usage ##
To install the udp-forwarder to your WebAPI project: 
* Run "Install-Package udp-forwarder" in the Package Manager Console.
* Add the following to App_Start/WebAPIConfig.cs.
```csharp
var serverIp = "0.0.0.0";
var serverPort = 0000;
var logService = new LogService(new UDPService(serverIp, serverPort));
config.MessageHandlers.Add(new LoggingHandler(logService));
```
* Edit the serverIP and serverPort variables to match the setup of your log service.
* OPTIONAL: The udp-forwarder can log errors if they occur. In order to enable this feature, add the following key to appSettings of the WebAPI's Web.config file, where the value points to the desired directory where the logs should be stored.
```csharp
<add key="FallbackLogPath" value="c:/logs">
```


* Save and run the API.

## Extensions ##
Wish to send logs with other methods than UDP? <br><br>
The upd-forwarder uses an UDP service to send the packages, but can be extended with other services. To do so, fork this project and create a new service that implements ITransferService. Then inject the new service by changing the logService in WebAPIConfig.cs:
```csharp
var logService = new LogService(new YOUR-NEW-SERVICE(serverIp, serverPort));
```

## Using Logstash as remote server ##
If [logstash](http://logstash.net/) is used to receive the logs on the remote server, the following configuration can be used to make it work seamlessly with the udp-forwarder:
```ruby
input {
	udp {
		port => 0000
		charset => "UTF-8"
	}
}

filter {
	json {
		source => "message"
	}
}
```
Where the port should be set to an unused udp port on the server.


## License ##
This software is licensed under the [MIT license](https://github.com/TopGunSoftware/udp-forwarder/blob/master/LICENSE).


			