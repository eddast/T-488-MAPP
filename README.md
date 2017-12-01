# Course Repository for T-488-MAPP
## By Edda Steinunn Rúnarsdóttir
#### Includes course weekly projects under ./Projects and some code done in class from lecture material under ./Lecture Codes

## Week 1: Xamarin.iOS project
In week one an Xamarin.iOS project was implemented in C# via Visual Studio. The project's function was built throughout one week, with some new functionalities every day. This repository contains change history of this project. The application is a tabbed application with two tabs and it fetches information from an external web service and displays that information.

### Visual Demonstration
#### First tab: Search
The first tab is the tab user is automatically navigated to once app launches. The first tab prompts user to write in a query substring of some movie, then displays a button on which the user can click. Once clicked, the tab becomes unclickable - both literally and visually - and a load spinner appears to indicate background process. The screen remains in this state while resources (movie information) are retrieved from the server:

![alt text](https://image.ibb.co/j6NvSw/One.jpg)

Once view has loaded, user is navigated to the next screen (a view is placed on the navigation stack) which is a table view structure displaying all movies that matched user query string (of course, if a query string is empty or no results were found, this view is an empty table view). Each non-empty cell is clickable and navigates user to another view (placed on the navigation stack) which displays images and details of movie which have already been fetched in the load screen before displaying the table view:

![alt text](https://image.ibb.co/izKmZb/Two.jpg)

#### Second tab: Top Rated
This tab loads top rated movies from the external web service and displays it in the same 
![alt text](https://image.ibb.co/m3BB0G/Three.jpg)
