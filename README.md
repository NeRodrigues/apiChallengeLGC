# apiChallengeLGC

## Overview
This project is part of the LGC recruitment technical challenge for API testing.

## Requirements
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Allure Report](https://allurereport.org/docs/install/)
- [JAVA 8 or above](https://www.java.com/en/)
- [.NET Install Tool extension for VSC](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscode-dotnet-runtime) - if using VSCode
- [C# Dev Kit extension for VSC](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit) - if using VSCode


## How To
1. Build and ensure all required packages are installed:
    ```sh
    dotnet build
    ```

1. Run the tests:
    ```sh
    dotnet test
    ```

1. Generate and view report
    ```sh
    allure generate
    allure serve
    ```

