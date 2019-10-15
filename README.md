# Food Truck Finder
Food Truck Finder is an application used to find the closest food truck to your San Francisco Location. It is an ASP.NET Core application.

# Using Food Truck Finder

First download the source and run it in Visual Studio.  You will be taken to a page which will allow you to enter an addess.  Click the find food truck button to find closest food trucks!

### Client Application
This app was built using .NET Core 3 and Razor Pages.  There is a welcome page and a map results page.  Food Truck Finder uses data that it gets from the server and add pins to Bing Maps so that you can see the location of each food truck.

### Client Tradeoffs
+ It does not make sure you put in a San Fran address even though the data we have is for San Fransico only.
+ It gets the nearest food trucks, however there might be multiple food trucks at the exact same location.  To solve for that not, the results are distinct based on that location so you could lose some locations if they are identical.  In the future it should create layers in the map to show all.

### Server Application
This app was built using .NET Core 3.  The bulk of the work to get the food trucks was moved to a Domain service.  It utilizes a data set of food trucks, thier permits as well as their locations.  It calls to get the dataset, caches it for subsequent requests and then queries that dataset for food trucks closets to the address location that was passed to it.

### Server Tradeoffs
+ This app uses MemoryCache to cache the results and caches them for 30 minutes.  If this data were to grow too large it would be better to organize and store it in a better caching mechanism like Resis, for example.
+ If similar locations are passed in it will always do the lookup to get the results.  It should be able to cache these results as well so that the lookup is not needed if the request locations are close enough to each other.

### Overall Tradeoffs
+ This should have been deployed through DevOps, but instead it was pushed directly from Visual Studio.
+ Typically we would separate Client apps from Server apps so they are not tied to each other from a development standpoint.  That way they could be developed independently.
