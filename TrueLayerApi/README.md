# About
	Current Api is developed using .Net Core 3.1 and C#

## Tools
	Current Api is developed using .Net 5.0 and C#
	Need to Install .Net 5 if not installed already https://dotnet.microsoft.com/en-us/download/dotnet/5.0
	Need to install Docker Desktop to run image created in the container.

## Api Packages 
	Automapper.Extensions.Microsoft.DependencyInjection
	Serilog.AspNetCore
	Newtonsoft.Json
	Microsoft.Extensions.Caching.Memory

### Unit Test Project Packages
	Microsoft.Logging.Abstractions
	Moq

## SET UP
	Open the solution file using Visual studio 2019.

## Execution Steps
### Ref Term TrueLayerApi - TApi
### Ref Term ShakepeareApi - SApi

	- Build the application.
	- In case of any missing packages, either install the package from Nuget store or try restoring nuget packages 
	- Run the application in debug mode.
	- Once opened in browser please select Get method under Pokermon, and select try it out.
	- Enter the test string and hit Execute to view the result.
	- If tested with the same test string, TApi shows the information from cache.
	- If used a different test string, TApi will try to retreive information from SApi.
	- Please note that TApi might be blocked and receive no data as there is a cap on number of calls to SApi.

## Enhancements
	- To add different environment appsettings to enable replacing variables corresponding to different environments(DEV,UAT,PROD).
	- Gracefully implement retry to hit api incase of service unavailability(avoiding being blocked)
	- If deployed to Cloud to configure horizontal scaling up / vertical scaling up to meet demand when users count increases challenging performance of Api.
	- Look for a way to move sensitive appsettings to Vault and retreive from there.
	- Only allow Authenticated users to access True Layer Api(implement authentication with Jwt tokens)
	- Only Allow Authorized users to access certain methods depending on their claims (implement authorization policies either at controller level or methods)
	- Currently assumed that the data is static and not changed when implementing caching, can look at scenarios where data MIGHT changed dynamically and implement in application
		to acknowledge such scenarios to display latest information to users.
	- Write integration tests to check api availability and status code returned.
	- If there is a database to write integration tests to check availabilty of database and connectivity
