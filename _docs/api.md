# Microservices Training .NET

## Indices

* [Clients API](#clients-api)
  
  * [List](#1-list)

  ## Renting API



### 1. List



***Endpoint:***

```bash
Method: GET
Type: 
URL: {{RentingApiEndpoint}}/list
```



### 2. List By Client Id



***Endpoint:***

```bash
Method: GET
Type: 
URL: {{RentingApiEndpoint}}/list/4872e42e-b27c-4823-8f83-1383e047b5c2
```



### 3. Register



***Endpoint:***

```bash
Method: POST
Type: RAW
URL: {{RentingApiEndpoint}}/register
```


***Headers:***

| Key | Value | Description |
| --- | ------|-------------|
| Content-Type | application/json |  |



***Body:***

```js        
{
    "ResourceId": "0001",
    "ClientId": "0003",
    "RegistrationDate": "01/15/2023",
    "ReturnDate": "01/15/2023",
    "CopyId": "0001",
    "ResourceName": "Test"
}
```



### 4. Return



***Endpoint:***

```bash
Method: PUT
Type: RAW
URL: {{RentingApiEndpoint}}/return/165


***Headers:***

| Key | Value | Description |
| --- | ------|-------------|
| Content-Type | application/json |  |



***Body:***

```js        
{
	"ReturnDate": "07-02-2020"
}
```