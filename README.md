# Search-Ranking

A simple website for checking the ranking of a company URL on Google search. Search history is saved.

There is also a bot that can be run by a task scheduler.

## Recommended IDE Setup

After cloning the repo, you will be required to attach the `Search-Ranking.mdf` database, which can be found in the `\DB\Data` folder.

The back-end is .NET 8 and will require VS2022. The source code is located in the `\Search-Ranking-backend` folder. The bot code is also in this VS solution.

The front-end is VITE/Vue.js 3 and is best opened with VScode. The source code is located in the `\Search-Ranking-frontend` folder. The back-end is exposed to the front-end via the `server.proxy` Vite configuration (vite.config.js), so it should work out of the box.

The application scraper relies on having Microsoft Edge installed. It is best to run this application on a Windows machine.

## Running the Application

Start the back-end first by running it in VS debug mode - a swagger page should appear.

Then run the front-end from the VScode terminal using the following command: `npm run dev`. Click on the URL provided on the screen.

## About the Project

The scraper is based on Selenium Edge webdriver.

The search engine configurations are stored in the database. This is to allow a system admin to update the CSS selector details without relying on a new app to be redeployed. Currently, there is only one search engine defined, and the system is hardcoded to the first entry in the search engine. This means the system can easily be enhanced to have multiple search engines, but this is out of scope for the project.

Database access is accomplished via Entity Framework Core. I am using AutoMapper to convert EF model objects to BL model objects. AutoMapper is again used on the API controllers to convert the BL model objects to DTO objects.

## Other Future Enhancements

Currently, the application has no security access control. This could easily be accomplished by adding OAuth.

The database connection string is hardcoded. Normally, for a production-ready application, I would have that defined in the configuration file.
