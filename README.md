# Contacts 3-Tier Architecture

A C# Console Application built using the **3-Tier Architecture** pattern.

## 📌 Project Description

This project is a Contact Management System developed using C# and SQL Server.

The project is designed using the 3-Tier Architecture to separate the application into different layers, making the code more organized, maintainable, and easier to develop.

## 🏗️ Architecture

The project consists of three main layers:

### 1. Presentation Layer
Responsible for interacting with the user through the Console Application.

- Displaying contact information
- Adding contacts
- Updating contacts
- Deleting contacts
- Finding contacts
- Listing all contacts

### 2. Business Layer
Contains the application's business logic.

- Contact management
- Validation
- Communication between the Presentation Layer and Data Access Layer

### 3. Data Access Layer
Responsible for communicating with the database.

- SQL queries
- Insert, Update, Delete and Select operations
- Database connection
- Retrieving contact and country data

## 🛠️ Technologies Used

- C#
- .NET
- SQL Server
- ADO.NET
- Visual Studio
- 3-Tier Architecture

## ✨ Features

- Find Contact
- Add New Contact
- Update Contact
- Delete Contact
- Get Contact by ID
- List All Contacts
- Manage Country Information
- Database integration using ADO.NET

## 📂 Project Structure

```text
Contacts-3-Tier-Architecture
│
├── ContactsBusinessLayer
│
├── ContactsDataAccessLayer
│
├── ContactsConsoleAppPresentationLayer
│
└── contacts-3-Tier-Architecture.sln
