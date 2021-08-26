## &copy; BoostUp - Logo
![BoostUp Logo 4](https://user-images.githubusercontent.com/64086041/126073441-3b69f1a2-180e-406e-8f8c-e3e9171c06c5.png)

## Hosted on:

**Azure -** https://boostup.azurewebsites.net/

## :eyeglasses: Project Introduction

**BoostUp Website** is created as defense project for **ASP.NET Core MVC** course at [SoftUni](https://softuni.bg/ "SoftUni") (June 2021).

BoostUp website is a professional community for companies, employees and recruiters. Place for connecting professionals to each other and engaging them on topics they care about.
BoostUp is a platform, where companies can introduce their businesses, employees can take their career path and professional development to the next level and recruiters can share their vital experience, while searching for the right candidates.

## :pencil2: Overview

**BoostUp** is a place, where companies can share their company details, job offers and applications, users can find the best job for them or just share their knowledge and work experience through posts and comments. Recruiters can publish and manage their own job offers and review the received applications.

## :pencil: Project Description

### 1. Users

**BoostUp** supports users with profile page, storing basic information about them like: name, address, profile picture, details for education, experiense etc. Every user could become recruiter using a link near to their profile. To become recruiter it is required to add an e-mail, where they can receive details if someone apply to their jobs. Recruiters are able to add jobs to every single company. 

Also users are able to become employees of a single company.

User profiles can be viewed using "Users" button in the navbar, where there is an option for searching by name.

### 2. Companies

All users aer able to add companies. For this purpose they should fill a form with basic details for the company.
> :warning: **Note**: Once successfully added a company, it is forwarded to the admin user, which can approve or rejecte it.

After approval the company will be shown in all companies page and statistics of BoostUp will be updated with this company.
Companies can be accessing using "Companies" button in the navbar, where there is an options for searching by name or year founded, sorting and filtering. Companies details button is provided in every company form.
Using company details page, an recrutier can post a job to the particular company.
Company details page, in addition to showing some details, also provides the numbers of employees joined this company and jobs posted, and also links to all employees profiles and jobs related to the company.

### 3. Jobs

Jobs are posted only by recruiters and are connected to a single company.
Jobs can be accessing using "Jobs" button in the navbar, where there is an options for searching by job title, sorting and filtering. Jobs details button is provided in every company form.
Jobs details page, in addition to showing some details, also provides the numbers of employees joined this company and jobs posted, and also links to all employees profiles and jobs related to the company.
The recruiter has the option to edit his own jobs accessing them throught "Mine jobs" button in navbar.

## :hammer: Built With
- [ASP.NET CORE 5.0](https://github.com/dotnet/aspnetcore) MVC pattern
- ASP.NET CORE razor pages
- ASP.NET CORE view components
- ASP.NET CORE areas
- [EntityFrameworkCore](https://github.com/dotnet/efcore) 
- [In-Memmory Cache](https://github.com/aspnet/Caching)
- MSSQL Server
- [AutoMapper](https://github.com/AutoMapper/AutoMapper)
- HTML & CSS
- Bootstrap v4.3.1
- [MyTested.AspNetCore.Mvc](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc)
- [xUnit](https://github.com/xunit/xunit)
- Web Api controllers + AJAX real-time Requests
- jQuery

## Code coverage
![Screenshot_1](https://user-images.githubusercontent.com/64086041/129948942-4c8e9b1b-99e0-4619-94a5-88aa4fa47ec9.png)

## :floppy_disk: Database Diagram
![DataBase - Diagram 20210807](https://user-images.githubusercontent.com/64086041/128595832-dafa6a4e-4716-4961-a0d5-f06df9b7d626.png)

## Initial StartUp
If you need admin account seeded you should add section Admin in appsettings.json file with 2 properies: Email and Password, like shown below:

![Screenshot_3](https://user-images.githubusercontent.com/64086041/129956970-0ffec20e-e3d8-4d3f-adf8-2c9bccda28e0.png)

## 🧑 Author

[Svilen Stoev](https://github.com/SvilenStoev)
- Facebook: [@Svilen Stoev](https://www.facebook.com/svilen.stoev.3)
- LinkedIn: [@svilenstoev](https://www.linkedin.com/in/svilenstoev/?fbclid=IwAR3__rQn3sR4rxJKEL6FK4QV1aR9tnF6vnOwMWsBghXz3xZPx-lYOc66gtU)

## :v: Your opition is important for me

Give a :star: if you like this project!

## 💵 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details
