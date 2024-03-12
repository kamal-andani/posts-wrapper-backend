# Posts Wrapper

This is a backend API developed in .NET Core 6.
This App aims to fetch a list of posts from external API, and do modifications like merging results, removing duplicates, and ordering results by fields in directions that are provided as query parameters.


## Run the Application Locally
Following command can be used to run the application.
```
dotnet watch
```
Once started, the application can be accessed at - https://localhost:7016


### Example API
https://localhost:7016/api/posts?tags=science,tech&sortby=likes&direction=desc
