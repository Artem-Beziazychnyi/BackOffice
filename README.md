<!-- 
    Readme.md of Back Office project 
    Author: Artem Beziazychnyi
-->

Main functionality

The project uploads a catalog of brands from TSV file. After brands were uploaded user can see the sum of each brand quantity. Also, a user can add or remove inventory by changing the quantity value. And of course, the user can both add and delete a Brand.

There are to folders

server -> contains .net core api project
client -> contains Vue.js project

How to run server .net core api

! before run server make sure that Net Core SDK 3.1.101 is installed on your machine. If not please install it from there
https://dotnet.microsoft.com/download/dotnet-core/3.1

1. navigate to server -> BackOffice folder
2. open appsettings.json file and replase my local connection string settings by yours.
2. open Terminal(macOS) or CMD(Windows) in BackOffice folder. 
3. for create a data base schema run comman: dotnet ef database update 
3. run command for run server: dotnet run.

By default, the server run on 5001 port localhost e.g. https://localhost:5001. 
If sever started succesfully you will see swagger documentation.

How to run UI

1. open Terminal(macOS) or CMD(Windows) client folder
2. run: npm install
3. run: npm run serve

By default, the server will run on 8080 port localhost e.g. http://localhost:8080
if 80 port busy the server will run on 81 port e.g. http://localhost:8081

Files for upload you can find in root folder.


