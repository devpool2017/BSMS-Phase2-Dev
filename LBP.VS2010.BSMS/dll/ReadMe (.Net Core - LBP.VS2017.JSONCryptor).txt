.Net Core
- LBP.VS2017.JSONCryptor.dll

In appsettings.json which is the default configuration file, add the following:
"CryptoSettings": {
    "IsFirstRun": "true",
    "JSONToken": <true value will be generated prior to production, you may use any sample>
  },


How to use?
Console App
- On Main method, instantiate the class and call the function that encrypts JSON File.
Web API
- On StartUp.cs where instantation of configuration happens, instantiate the class and 
call the function that encrypts JSON File.


Note: Decrypt encrypted properties first prior to assigning them to a controller.