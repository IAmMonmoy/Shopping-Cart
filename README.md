# Shopping-Cart
This is built on ASP.NET CORE 2.0. The front end with angular is on my repository list named **Shopping-Cart-UI**. Take a look at it if you are interested to see how I implemented front end with angular.


## Basic Requirements

   [Asp.Net Core SDK](https://www.microsoft.com/net/learn/get-started/windows/)
   
   Microsoft Sql Server    
   
   
## Run
   Clone the repository.
   
   Open terminal in the repository.
   
   type **dotnet restore** in the terminal to restore any additional packages.
   
   type **dotnet ef database update** in the terminal.
   
   type **dotnet run**.
   
   
## Api list
   * [Post] http://localhost:5000/api/register **Register User**
     * Json Data in body exmaple
      ```
      {
        "Email" : "iammonmoy@ooutlook.com",
        "UserName" : "Somthing",
        "Password" : "512345Rrm_",
        "ConfirmPassword" : "512345Xyz_"
      }
      ```
   
   * [Post] http://localhost:5000/api/Auth **User login. Get Token**
     * Json Data in body exmaple
      ```
      {
        "Email" : "iammonmoy@gmail.com",
        "Password" : "512345Xyz_"
      }
      ```
   
  
   * [Get] http://localhost:5000/api/Tags **Get All Tags (With Authorization Header)**
   
   * [Post] http://localhost:5000/api/Tags **Post Tags (With Authorization Header)**
      * Json Data in body exmaple
      ```
      {
        "TagName" : "abcd",
        "TagDescription" : "cdef"
      }
      ```
   
   * [Get] http://localhost:5000/api/Tags/{id} **Get Tag By Id (With Authorization Header)**
   
   * [Put] http://localhost:5000/api/Tags/{id} **Edit Tag By Id (With Authorization Header)*
     * Json Data in body exmaple
      ```
      {
        "TagName" : "Girl",
        "TagDescription" : "Girl"
      }
      ```
   
   * [Delete] http://localhost:5000/api/Tags/{id} **Delete Tag By Id (With Authorization Header)**
   
   
  
   * [Get] http://localhost:5000/api/products/ **Get All Products**
   
   * [Post] http://localhost:5000/api/products/ **Post Product (With Authorization Header)**
     * See Form Data Example In The ScreenShots Section
   
   * [Put] http://localhost:5000/api/products/{id} **Edit Product By Id (With Authorization Header)**
      * See Form Data Example In The ScreenShots Section
   
   * [Put] http://localhost:5000/api/products/{id}/{stock} **Update product stock (With Authorization Header)**
   
   * [Delete] http://localhost:5000/api/products/{id} **Delete Product By Id (With Authorization Header)**
   
   
  
   * [Get] http://localhost:5000/api/shipment/ **Get All Shipment (With Authorization Header)**
   
   * [Get] http://localhost:5000/api/shipment/{id} **Get All products in a shipment (With Authorization Header)**
   
   * [Post] http://localhost:5000/api/shipment/ **Post Shipment (With Authorization Header)**
      * Json Data in body exmaple
      ```
      {
          "userName": "IAmMonmoy",
          "buyerName": "abc",
          "buyerAddress": "def",
          "buyerPhone": "ghi",
          "totalCost": 13440,
          "isDelivered": false,
          "productQuantity" : [
            {"productName" : "T SHIRT", "quantity" : "3" },
            {"productName" : "Example", "quantity" : "2" }
        ]
      }
      ```
  
   **Take A look at the controllers for additional information**
   
## ScreenShots
   * Authorization Header
   ![Form Header](/ScreenshotsForReadme/formHeader.PNG)
   * Post Request With Json Data
   ![Post Request With Json Data](/ScreenshotsForReadme/JsonDataPost.PNG)
   * Product Post Request With Form Data
   ![Post Request With Form Data](/ScreenshotsForReadme/productAdd.PNG)
   * Product Put Request With Form Data
   ![Post Request With Form Data](/ScreenshotsForReadme/productEdit.PNG)
## Ask a question?

If you have any query please contact at iammonmoy@gmail.com




