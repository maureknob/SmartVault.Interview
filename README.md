# Overview

The point of this brief exercise is to help us better understand your ability to work through problems, design solutions, and work in an existing codebase. It's important that the solution you provide meets all the requirements, demonstrates clean code, and is scalable.

# Code

There are 3 projects in this solution:

## SmartVault.CodeGeneration

This project is used to generate code that is used in the SmartVault.Program library.

## SmartVault.DataGeneration

This project is used to create a test sqlite database.

## SmartVault.Program

This project will be used to fulfill some of the requirements that require output.

# Requirements

1. Speed up the execution of the SmartVault.DataGeneration tool. Developers have complained that this takes a long time to create a test database.

2. All business objects should have a created on date.

3. Implement a way to output the contents of every third file of an account to a single file.

4. Implement a way to get the total file size of all files.

5. Add a new business object to support OAuth integrations (No need to implement an actual OAuth integration, just the boilerplate necessary in the given application)

6. Commit your code to a github repository and share the link back with us

# Guidelines

- There should be at least one test project

- This project uses [Source Generators](https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview) and should be run in Visual Studio 2022

- You may create any additional projects to support your application, including test projects.

- Use good judgement and keep things as simple as necessary, but do make sure the submission does not feel unfinished or thrown together

- This should take 2-4 hours to complete

# Solutions

### SmartVault.DataGeneration

To speed up the creation of the test database, I created a transaction. In this way, it was possible to create all the command parameters and perform the commit only once.

I also removed the FileInfo methods from inside the loops. This reduced the time considerably.

I also performed some modularizations, like unnesting the loops and also created some classes and methods to make the code more readable.

### SmartVault.Program

A class was created to represent the OAuth integration.

To add the "CreatedOn" property to BusinesObjects, I created an abstract class containing this property and made the other classes inherit it.

Implementing the GetAllFileSizes and WriteEveryThirdFileToFile methods was the hardest part of the challenge. This is because I couldn't connect to the database from the "SmartVault.Program" project. As the database file is in the "SmartVault.DataGeneration" project, I tried to make it a kind of data access layer and leave database operations in a single place.

So I implemented a class with CRUD operations, in "SmartVault.DataGeneration", and accessed the methods of this class in "SmartVault.Program". It happens that when I access "SmartVault.DataGeneration" running the project "SmartVault.Program", the relative path used to build the connection string is the one of the project that is running, therefore the connection string always looked for the database in the binaries of the project "SmartVault.Program" and not at "SmartVault.DataGeneration". The same problem occurs to access the text file.

So I gave up on the idea, and simply moked the data to use in the GetAllFileSizes and WriteEveryThirdFileToFile methods.

# Possible improvements

If I worked at Smart Vault and hypothetically this was one of the company's projects, I would suggest the following improvements to make the project more scalable and ensure the application's lifecycle:

I would create the "SmartVault.DAL" project to reduce the coupling of the other projects with the database. This project would implement Repositor Patern and Unity of Work to carry out operations with the database, providing an interface for other projects to consume the data.

I would also create a "SmartVault.IOC" project to perform inversion of control and use dependency injection in projects. This way we can work with abstractions instead of concrete classes. This would make the project more scalable, testable and clean.

It would also turn the "SmartVault.CodeGeneration" and "SmartVault.Data generation" projects into class libraries.