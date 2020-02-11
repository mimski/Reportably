# Reportably
Web app based on ASP.NET Core MVC 3.1 for listing company reports

![image](https://user-images.githubusercontent.com/44443424/74246647-8c1ea480-4ced-11ea-849d-98a56432124f.png)

## Project Description

* **Public part** -  accessible without authentication
  - view the list of company reports
  - search reports
* **Private part** - available for registered users only
  - download reports but only when they provide valid email addresses
* **Administrative part** - available for administrators only
  - create report records and upload report documents

Each report have title, summary, publication date, author and a .pdf file associated with it.
The system persist/log all downloads of report documents. 
 
