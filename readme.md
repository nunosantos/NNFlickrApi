# Flickr Data Aggregator	

This file intends to illustrate how to install the Flickr Data Aggregator application and technologies also used

## Table of contents

* Quick start
* What's included
* Documentation
* Technologies
* Test Cases
* Architectures
* Creator

## Quick start

Please ensure to have Node.js and .NET Core installed on your machine prior to deploying the application.

[Node.js installation](https://nodejs.org/en/download/)

[.NET Core Installation](https://dotnet.microsoft.com/download)

## What's included

Name | Description  
--- | ---  
`Core ` | `.NET assembly responsible for containing all the business logic, interfaces, model and services` 
`UI ` | `.NET core web api application, with React as a frontend technology stack for data rendering` 
`Tests ` | `Unit tests` 

## Documentation

### Technologies

* ASP.NET Core Web API (c#)
* React
* NodeJS
* xUnit
* Moq

### Test Cases

Name | Description  
--- | ---  
**1** | As a user if I input an empty string I should get a 400 (Bad Request) and return to the main page
**2** | As a user if I input correct input I should ge a 200 (Ok) and a list of items should be returned as per the search category


### Screenshots

In app testing - main window

![flickr1](https://user-images.githubusercontent.com/3398578/97221251-f2d8e500-17cc-11eb-82d1-29af73ef7094.png)

In app testing - search input

![flickr2](https://user-images.githubusercontent.com/3398578/97221232-ed7b9a80-17cc-11eb-9e64-e09834643637.png)

API documentation

![flickr3](https://user-images.githubusercontent.com/3398578/97221265-f8362f80-17cc-11eb-9569-79f6c136cb70.png)
## Creator

Nuno Santos
