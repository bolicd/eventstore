# EventStore Example 

## How to start

This project uses .NET Core 3.1, so make sure it is installed.

Run DbMigration project. This should create tables required. By
default it uses LocalDB default database.

Run the project by running RestAPI project. It should navigate to swagger
by default.

## Tests

There are two integration tests included. There was not much point in writing
more since this is only for demo purposes. Tests are also using in memory db
with temporary database, created each time test is run, and deleted once tests are
completed

