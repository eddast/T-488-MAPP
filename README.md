# Course Repository for T-488-MAPP
## By Edda Steinunn Rúnarsdóttir
#### Includes course weekly projects under ./Projects and some code done in class from lecture material under ./Lecture Codes

## Week 1: Xamarin.iOS project - Visual Demonstration and Information
In the first week of the coure T-488-MAPP an Xamarin.iOS project was implemented in C# via Visual Studio. The project's function was built throughout one week, with some new functionalities every day.  Git was used as a version control tool throughout the week, hence this repository contains change history of this project throughout this entire week, making prior functionalities that may have been omitted available.

### Application Structure and Purpose
The application is a tabbed application with two tabs and it fetches information about movies from an external web service and displays that information appropriately. Each tab contains a view with it's own navigation controller.

### First tab: Search
The first tab is the tab user is automatically navigated to once app launches. The first tab prompts user to write in a query substring of some movie, then displays a button on which the user can click. Once clicked, the tab becomes unclickable - both literally and visually - and a load spinner appears to indicate background process. The screen remains in this state while resources (movie information) are retrieved from the server:

![alt text](https://image.ibb.co/j6NvSw/One.jpg)

Once view has loaded, user is navigated to the next screen (a view is placed on the navigation stack) which is a table view structure displaying all movies that matched user query string (of course, if a query string is empty or no results were found, this view is an empty table view). Each non-empty cell is clickable and navigates user to another view (placed on the navigation stack) which displays images and details of movie which have already been fetched in the load screen before displaying the table view:

![alt text](https://image.ibb.co/izKmZb/Two.jpg)

### Second tab: Top Rated

This tab loads top rated movies from the external web service and initially displays a load spinner indicating background process. Once resources have been retrieved, the top rated movies are displayed in the same table view as the once that displays the search results but with one major difference - this view is not placed on the navigation stack like it does when search is conducted, but replaces the loading view. The table cells are clickable like in the search view and once clicked show movie details. Everytime a user navigates from this tab to another, and then back into this tab to the root view (table view), the information is retrived again meanwhile the same load screen with a load spinner is displayed:

![alt text](https://image.ibb.co/m3BB0G/Three.jpg)

### Known Limitations

Due to lack of time and too specified service model of the external web service's API, the potential cross-platform shareable code of this model could not be placed into the portable class library. This was because the image downloader service (located in the MovieDownload namespace) was included in this service model and could not reside in the PCL due to platform-specific references. I will attempt to seperate the image downloader service from the API service model and solve this before starting week two's Android project. _(Edit: Fixed 03.12.17)_

## Week 2: Xamarin.Droid project - Visual Demonstration and Information
Note that images displaying app's functionality in this section are from tests conducted on an actual android phone (Samsung Galaxy Alpha) and not by an emulator. Also note that the app's minimum software requirement is Android 4.0.3, namely API level 15 but it's target version is Android with API level 26.

### Application Structure and Purpose
The application has essentially the same function as week one's iOS app with minor changes: again a tabbed application was created with two tabs whose purpose is to fetch information about movies from an external web service and displays that information appropriately. Toolbar was used to generate the tab bar function and fragments were loaded into each tabs by a main fragment activity.

### First tab: Search
The first tab is the tab user is automatically navigated to once app launches. The first tab prompts user to write in a query substring of some movie, then displays a button on which the user can click. Once clicked, the tab becomes unclickable - both literally and visually - and a load spinner appears to indicate background process. The screen remains in this state while resources (movie information) are retrieved from the server:

![alt text](https://image.ibb.co/ier69b/Search_View.jpg)

Once resources have been retrieved, user is navigated to the next screen (i.e. a new activity starts on top of the fragment activity which can be retracted via back button present on android phones) which includes a list view displaying all movies that matched user query string. Each item (movie) in that list is clickable and navigates user to another screen (again, starts a new activity) which displays images and details of that movie:

![alt text](https://image.ibb.co/fgqYpb/ListView.jpg)

### Second tab: Top Rated
This tab loads top rated movies from the external web service and initially displays a load spinner indicating background process. Once resources have been retrieved, an empty list view this fragment contains is filled with movies retrieved. The list items are clickable like in the search view and once clicked show movie details. Everytime a user navigates into the top rated tab the information is retrived again, displaying the fragment's load spinner appropriately:

![alt text](https://image.ibb.co/dNmdNw/Top_Rated_Tab.jpg)

### Some enhancements from the iOS app
Four major enhancements emerge in the android app: the method of retrieving images, the user interface, the user experience and code sharing.

- **Glide** was now used to retrieve images (movie posters and movie backdrop images) which increases efficiency greatly as it asynchorniously retrieves images as activity is displayed on screen, thus reducing loading time a bit.

- The **User Experience** is much better in the andorid app than in the iOS app. In addition to naming the app **AMDb** for **App Movie DataBase** a "logo" for the app was designed. Moreover a fixed color pallette was used for the overall look of the app and a launch screen was provided, conforming to the pallette and including the logo. These changes made a an obvious difference in the app appearence from the raw and default look of the iOS app. See launch screen:

![alt text](https://image.ibb.co/hqxnXw/launchscreen.jpg)

- The **User Interface** was greatly enchanced. Now error messages are displayed when user inputs no string and when user receives no results. Also, user can now click on an information icon in the initial screen of the app for information on it's functionality:
![alt text](https://image.ibb.co/mCH4Cw/UIexp1.jpg)
![alt text](https://image.ibb.co/cJKZCw/UIexp2.jpg)
![alt text](https://image.ibb.co/i19Ueb/UIexp3.jpg)

- In addition to this to enhance the UI, now **average rating** of a movie is now displayed below title in the form of stars and numeric rate. This is because most potential users that got to try the app when in progress found it imperative to be able to get some idea of the movie quality in movie inspection based apps like mine. Since displaying the average rating required no extra time complexity nor too much work this was added to the detail view for a better user experience.

- This week's app provides a better **Code sharing**. First of all the known limitation for the iOS app was fixed (service not being in the portable class library). In addition to this, this week's project provides a base for a better MovieModel object in the PCL. This is because the iOS version of the MovieModel depended greatly on a MovieInfo object it took in which I realized made my MovieModel overbloated and less efficient. Therefore, the MovieModel now isolates all factors it needs from the MovieInfo object it takes in and uses those factors in isolation when needed. The android portion of the project never depends on the MovieModel's MovieInfo property. Please note that although my intention was to entirely remove the MovieInfo property from the MovieModel class, it remains due to the iOS part of the project needing to compile.
