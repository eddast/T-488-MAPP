# Course Repository for T-488-MAPP
## By Edda Steinunn Rúnarsdóttir
#### Includes course weekly projects under ./Projects and some code done in class from lecture material under ./Lecture Codes

## Week 1: Xamarin.iOS project - Visual Demonstration and Information
In the first week of the coure T-488-MAPP an Xamarin.iOS project was implemented in C# via Visual Studio. The project's function was built throughout one week, with some new functionalities every day. This repository contains change history of this project throughout this entire week and prior functionalities that may have been omitted. The application is a tabbed application with two tabs and it fetches information about movies from an external web service and displays that information appropriately.

### First tab: Search
The first tab is the tab user is automatically navigated to once app launches. The first tab prompts user to write in a query substring of some movie, then displays a button on which the user can click. Once clicked, the tab becomes unclickable - both literally and visually - and a load spinner appears to indicate background process. The screen remains in this state while resources (movie information) are retrieved from the server:

![alt text](https://image.ibb.co/j6NvSw/One.jpg)

Once view has loaded, user is navigated to the next screen (a view is placed on the navigation stack) which is a table view structure displaying all movies that matched user query string (of course, if a query string is empty or no results were found, this view is an empty table view). Each non-empty cell is clickable and navigates user to another view (placed on the navigation stack) which displays images and details of movie which have already been fetched in the load screen before displaying the table view:

![alt text](https://image.ibb.co/izKmZb/Two.jpg)

### Second tab: Top Rated

This tab loads top rated movies from the external web service and initially displays a load spinner indicating background process. Once resources have been retrieved, the top rated movies are displayed in the same table view as the once that displays the search results but with one major difference - this view is not placed on the navigation stack like it does when search is conducted, but replaces the loading view. The table cells are clickable like in the search view and once clicked show movie details. Everytime a user navigates from this tab to another, and then back into this tab to the root view (table view), the information is retrived again meanwhile the same load screen with a load spinner is displayed:

![alt text](https://image.ibb.co/m3BB0G/Three.jpg)

### Known Limitations

Due to lack of time and too specified service model of the external web service's API, the potential cross-platform shareable code of this model could not be placed into the portable class library. I will attempt to handle this before starting week two's project.
