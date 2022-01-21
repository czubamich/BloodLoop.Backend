# BloodLoop.Backend
<!-- PROJECT SHIELDS -->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]

<!-- PROJECT LOGO -->
<br />
  <p align="left">
    API for gathering donation data from multiple blood banks created as a part of Engineer's Thesis 2022. 
    The system takes into account blood donors who might access their donation history and technical users whose task is to enter data into the system mainly through software integrations with blood donation stations.
</p>

<!-- TABLE OF CONTENTS -->
<details open="open">
  <summary><h2 style="display: inline-block">Table of Contents</h2></summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#example-usage">Example usage</a></li>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
        <li><a href="#report-issues">Report Issues</a></li>
      </ul>
    </li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->
## About The Project

### Example usage
API can be used with following [React Native App](https://github.com/czubamich/BloodLoop.Mobile)

### Built With

* [ASP.NET Web API](https://docs.microsoft.com/en-us/aspnet/web-api/)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [Ardalis.Specification](https://github.com/ardalis/Specification)
* [Ardalis.GuardClauses](https://github.com/ardalis/GuardClauses)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [FluentAssertions](https://fluentassertions.com/)
* [xUnit.net](https://github.com/moq/moq4)

<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites
To build this project, you require:

* Visual Studio 2019
* IIS Express
* Microsoft SQL Server

### Installation
1. Clone the repo
   ```sh
   git clone https://github.com/czubamich/BloodLoop.Backend.git
   ```
2. Open project in Vistual Studio.
3. Setup SQL database connection string (BloodLoop.WebApi/appsettings.json)
4. Run 'app' F5.

### Report issues
If you find anything that is performing not as espected? Any feature that should be added or improved? Please feel free to check the issue tracked and create create a ticket. Please try to provide a detailed description of your problem, including the steps to reproduce it.

<!-- CONTACT -->
## Contact

Michael Czuba - czuba.mich@gmail.com

<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/czubamich/BloodLoop.Backend.svg?style=for-the-badge
[contributors-url]: https://github.com/czubamich/BloodLoop.Backend/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/czubamich/BloodLoop.Backend.svg?style=for-the-badge
[forks-url]: https://github.com/czubamich/BloodLoop.Backend/network/members
[stars-shield]: https://img.shields.io/github/stars/czubamich/BloodLoop.Backend.svg?style=for-the-badge
[stars-url]: https://github.com/czubamich/BloodLoop.Backend/stargazers
[issues-shield]: https://img.shields.io/github/issues/czubamich/BloodLoop.Backend.svg?style=for-the-badge
[issues-url]: https://github.com/czubamich/BloodLoop.Backend/issues
[license-shield]: https://img.shields.io/github/license/czubamich/BloodLoop.Backend.svg?style=for-the-badge
[license-url]: https://github.com/czubamich/BloodLoop.Backend/blob/master/LICENSE.txt

<!-- README created using the following template -->
<!-- https://github.com/othneildrew/Best-README-Template -->
