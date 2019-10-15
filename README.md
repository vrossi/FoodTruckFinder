# Food Truck Finder
Food Truck Finder is an application used to find the closest food truck to your San Francisco location. It is an ASP.NET Core application.

# Using Food Truck Finder

### Running locally
First download the source and run it in Visual Studio.  You will be taken to a page which will allow you to enter an address.  Click the find food truck button to find closest food trucks!

### Demo App
 [Demo](https://vinnie-rossi-find-food-trucks.azurewebsites.net/)


### Welcome Screen

[<img src="https://vinnierossifoodtrucktest.blob.core.windows.net/images/FoodTruckFinderWelcome.PNG" />](https://vinnie-rossi-find-food-trucks.azurewebsites.net/)

### Maps Screen
[<img src="https://vinnierossifoodtrucktest.blob.core.windows.net/images/FoodTruckFinderMaps.PNG" />](https://vinnie-rossi-find-food-trucks.azurewebsites.net/)

### Client Application
This app was built using .NET Core 3 and Razor Pages.  There is a welcome page and a map results page.  Food Truck Finder uses data that it gets from the server and add pins to Bing Maps so that you can see the location of each food truck.

### Client Tradeoffs
+ It does not make sure you put in a San Francisco address even though the data we have is for San Francisco only.
+ This app, provided that the dataset was greater than just San Francisco, the app could take advantage of location services so that typing in an address isn't required.
+ It gets the nearest food trucks, however there might be multiple food trucks at the exact same location.  To solve for that, note that the results are distinct based on that location so you could lose some locations if they are identical.  In the future it should create layers in the map to show all.

### Server Application
This app was built using .NET Core 3.  The bulk of the work to get the food trucks was moved to a Domain service.  It utilizes a dataset of food trucks, their permits as well as their locations.  It calls to get the dataset, caches it for subsequent requests and then queries that dataset for food trucks closets to the address location that was passed to it.

### Server Tradeoffs
+ This app uses MemoryCache to cache the results and caches them for 30 minutes.  If this data were to grow too large it would be better to organize and store it in a better caching mechanism like Redis, for example.
+ If address locations with similar proximity are passed in it will always do the lookup to get the results.  It should be able to cache these results as well so that the lookup is not needed if the request locations are close enough to each other.
+ The dataset should be scrubed for bad character and ommited if necessary.

### Overall Tradeoffs
+ This should have been deployed through DevOps, but instead it was pushed directly from Visual Studio. **(Friends don't let friends right-click & publish)**
+ Typically we would separate Client apps from Server apps so they are not tied to each other from a development standpoint.  That way they could be developed independently.
+ This should be a mobile app.  It works OK on a phone in the browser but with more time this could be a lot better on a mobile device.
