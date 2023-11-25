# Waldening API

***Designed and Implemented By Genesis Scott***

### Description
An API that provides information on national and state parks.

### Technologies Used
- AutoMapper
- Entity Framework Core
- .NET WebAPI
- Code Generation & Controller Scaffolding
- Pagination
- Linq
- MySQL

### Setup and Installation

#### Install Tools

If you have not already, install the dotnet-ef tool by running the following command in your terminal:
```$ dotnet tool install -g dotnet-ef --version 6.0.0```


#### Setup
1. Clone this repo
2. Navigate to this project's prod directory 
3. Touch appsettings.json and appsetings.Development.json
4. Fill the following in appsettings.json and appsettings.Development.json

***Appsettings.json***
replace YOUR_USERNAME and YOUR_MYSQL_PASSWORD with the credentials for your running local instance of MySQL
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=cretaceous_api;uid=[YOUR_USERNAME];pwd=[YOUR_MYSQL_PASSWORD];"
  }
}
```
***Appsettings.Development.json***
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Trace",
      "Microsoft.AspNetCore": "Information",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```
5. Create the databse in the prod dir by running  ```dotnet ef databse update```
6. run dotnet run and navigate to http://localhost:5000/Swagger to view and test endpoints

### Usage
In this app there's one model (`Park`) and 5 endpoints related to that model. 

### GET /api/Parks?name=NAME&state=STATE&page=PAGE_NUM&pageSize=PAGE_SIZE
<table>
    <thead>
      <tr>
        <th>HTTP Verb</th>
        <th>URL</th>
        <th>Expected Behavior</th>
        <th>Response Status</th>
      </tr>
    </thead>
      <tr>
        <td>GET</td>
        <td>/api/Parks?page=1&pageSize=10</td>
        <td>Returns an collection of Park objects mapped to the page size, number, and optional name and state query parameters. NOTE: name and state are nullable but page size and number are not</td>
        <td>200: Ok</td>
      </tr>
</table>

Expected Response:
```json
{
  "pageNumber": 1,
  "pageSize": 4,
  "numberOfPages": 1,
  "numberOfResults": 4,
  "nextPageUrl": null,
  "results": [
    {
      "parkId": 1,
      "state": "Texas",
      "name": "McKinley Falls State Park",
      "website": "https://tpwd.texas.gov/state-parks/mckinney-falls",
      "isNational": false,
      "activities": [
        "Biking",
        "Hiking",
        "Camping",
        "Swimming",
        "Rock Climbing"
      ]
    },
    {
      "parkId": 2,
      "state": "Arizona",
      "name": "Grand Canyon National Park",
      "website": "https://www.nationalparks.org/explore/parks/grand-canyon-national-park",
      "isNational": true,
      "activities": [
        "Hiking",
        "Canyoneering",
        "Camping",
        "Swimming",
        "Spelunking",
        "Rock Climbing"
      ]
    },
    {
      "parkId": 3,
      "state": "Texas",
      "name": "Pedernales Falls State Park",
      "website": "https://tpwd.texas.gov/state-parks/pedernales-falls",
      "isNational": false,
      "activities": [
        "Hiking",
        "Camping",
        "Swimming"
      ]
    },
    {
      "parkId": 4,
      "state": "New Mexico",
      "name": "White Sands National Park",
      "website": "https://www.nps.gov/whsa/index.htm",
      "isNational": true,
      "activities": [
        "Dune Sledding",
        "Hiking",
        "Cabin Rental",
        "Biking"
      ]
    }
  ]
}
```

### GET /api/Parks/{id}
<table>
    <thead>
      <tr>
        <th>HTTP Verb</th>
        <th>URL</th>
        <th>URL Parameter *required</th>
        <th>Expected Behavior</th>
        <th>Response Status</th>
      </tr>
    </thead>
      <tr>
        <td>GET</td>
        <td>/api/Parks/{id}</td>
        <td>id (int)</td>
        <td>Returns a JSON object representing an Park with an "parkId" property that matches the "id" provided as a URL parameter.</td>
        <td>200: Ok</td>
      </tr>
</table>

Example Request URL: `GET /api/Parks/1`

Expected Response: 

```json
{
  "parkId": 1,
  "state": "Texas",
  "name": "McKinley Falls State Park",
  "website": "https://tpwd.texas.gov/state-parks/mckinney-falls",
  "isNational": false,
  "activities": [
    "Biking",
    "Hiking",
    "Camping",
    "Swimming",
    "Rock Climbing"
  ]
}
```

### POST /api/Parks
<table>
    <thead>
      <tr>
        <th>HTTP Verb</th>
        <th>URL</th>
        <th>Request Body *required</th>
        <th>Expected Behavior</th>
        <th>Response Status</th>
      </tr>
    </thead>
      <tr>
        <td>POST</td>
        <td>/api/Parks</td>
        <td>A JSON object containing key-value pairs for: <br> - name(string), <br> - state(string), <br> - Website(string) <br> - Activities(List(string)) <br> - parkId(int) may be included but regardless of the value provided, it's value will be set by the database when the record is saved.</td>
        <td>Creates a new Park object in the database.</td>
        <td>201: Created</td>
      </tr>
</table>

Example Request Body:
*all fields are required*
```json
{
  "state": "Utah",
  "name": "Four Courners National Monument",
  "website": "https://navajonationparks.org/navajo-tribal-parks/four-corners-monument/",
  "isNational": true,
  "activities": [
    "Native Vendors"
  ]
}
```

Expected Response:

```json
{
  "parkId": 5,
  "state": "Utah",
  "name": "Four Courners National Monument",
  "website": "https://navajonationparks.org/navajo-tribal-parks/four-corners-monument/",
  "isNational": true,
  "activities": [
    "Native Vendors"
  ]
}
```

### PUT /api/Parks/{id}
<table>
    <thead>
      <tr>
        <th>HTTP Verb</th>
        <th>URL</th>
        <th>URL Parameter *required</th>
        <th>Request Body *required</th>
        <th>Expected Response</th>
        <th>Response Status</th>
      </tr>
    </thead>
      <tr>
        <td>PUT</td>
        <td>/api/Parks/{id}</td>
        <td>id (int)</td>
        <td>A JSON object containing key-value pairs for: <br> - name(string), <br> - state(string), <br> - Website(string) <br> - Activities(List(string)) <br> ParkId must match the ParkId of the Record</td>
        <td>No content</td>
        <td>204: No Content</td>
      </tr>
</table>

Example Request Body *required:

```json
{
  "parkId": 1,
  "state": "Texas",
  "name": "Palo Duro State Park",
  "website": "https://tpwd.texas.gov/state-parks/palo-duro-canyon",
  "isNational": false,
  "activities": [
    "Hiking",
    "Camping",
    "Canyoneering"
  ]
}
```

### DELETE /api/Parks/{id}
<table>
    <thead>
      <tr>
        <th>HTTP Verb</th>
        <th>URL</th>
        <th>URL Parameter *required</th>
        <th>Expected Behavior</th>
        <th>Response Status</th>
      </tr>
    </thead>
      <tr>
        <td>DELETE</td>
        <td>/api/Parks/{id}</td>
        <td>id (int)</td>
        <td>Deletes an Park from the database.</td>
        <td>204: No Content</td>
      </tr>
</table>