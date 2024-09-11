# Real Estate Website
In this project, a website where real estate adverts are published has been created. Website’s language is in Turkish. Users can post and view real estate adverts on the website. Users can become a corporate or individual member of the website. Records in the database can be updated by logging in as an administrator. Different operations such as adding to favourites and sending messages can also be done on the site.

**Technologies Used:** .NET 6.0, C#, Entity Framework, MSSQL Server, HTML, CSS
## Implementing The Project
### 1. Database design
![database-ER](./Readme%20Images/databaseER.png)
### 2.	Create The Project from Visual Studio IDE
   
In Visual Studio, ASP.NET Core Web App(Model-View-Controller) template was chosen and the project was created.

### 3.	Create The Database

ClassLibray named Entity was added to solution. Then all the tables and their attributes are coded. 
After that, two more class libraries were added: DataAccess and Service. Three folders were opened in DataAccess. These are Context, Migrations and Models. Table names are added to RealEstateContext.cs in the Context folder. 
Project Manager Console was opened from Visual Studio. From default project options, RealEstate.DataAccess was chosen. Then the database was created using the following commands:

`Add-Migration RealEstate -o Migrations -Verbose`

`Update-Database`



### 4.	Develope The Program
   
All modules were added to project sequentially. The project was coded in accordance with MVC architecture. Clean code development was taken into account and the SOLID principles were applied while coding.

## Features of The Project
Here are some operations that users can do from the website:
- Personal or corporate register
- Login – Logout
- List adverts
- Show advert details
- Add advert to favourites, list favourites, remove advert from favourites
- List real estate news
- Apply for Corporate Membership (different from the corporate register)
- Send message to admin from contact us page
- Show profile
- Delete advert
- Update price of advert
  
If the user is admin:
- List all adverts, delete advert
- List all users, delete user
- List corporate membership applications
- List all messages
- List all news, upload news, delete news

## Screenshots From The Project
### *Homepage - Part 1*
![image-1](./Readme%20Images/mainpage1.png)

### *Homepage - Part 2*
![image-2](./Readme%20Images/mainpage2.png)

### *Homepage - Part 3*
![image-3](./Readme%20Images/mainpage3.png)

### *Log In Page*
![image-4](./Readme%20Images/loginpage.png)

### *User Profile Page*
![image-5](./Readme%20Images/profilepage.png)

### *User Favourites Page*
![image-6](./Readme%20Images/favouritespage.png)

### *Filtered Advert Viewing - For Sale Listings Page*
![image-7](./Readme%20Images/filteredadverts.png)

### *News List Page*
![image-8](./Readme%20Images/newspage.png)

### *News Details Page*
![image-9](./Readme%20Images/newsdetails.png)

