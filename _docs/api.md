# Microservices Training .NET

## Indices

* [Clients API](#clients-api)
  
  * [List](#1-list)
  * [Get](#3-get)
  * [Create](#1-create)
  * [Update](#5-update)

## Clients API



### 1. Create



***Endpoint:***

```bash
Method: POST
Type: RAW
URL: {{ClientsApiEndpoint}}/api/clients
```


***Headers:***

| Key | Value | Description |
| --- | ------|-------------|
| Content-Type | application/json |  |



***Body:***

```js        
{
    "_id": "1123",
    "Name": "test",
    "Email": "testEmail@gmail.com"
}
```



### 2. Delete (not available yet)



***Endpoint:***

```bash
Method: DELETE
Type: 
URL: {{ClientsApiEndpoint}}/delete/5e11269570c0f10b912c572d
```



### 3. Get



***Endpoint:***

```bash
Method: GET
Type: 
URL: {{ClientsApiEndpoint}}/api/clients/12
```



### 4. List



***Endpoint:***

```bash
Method: GET
Type: 
URL: {{ClientsApiEndpoint}}/api/clients
```



### 5. Update



***Endpoint:***

```bash
Method: PUT
Type: RAW
URL: {{ClientsApiEndpoint}}/api/clients/9
```


***Headers:***

| Key | Value | Description |
| --- | ------|-------------|
| Content-Type | application/json |  |



***Body:***

```js        
{
    "_id": "1123",
    "Name": "test",
    "Email": "testEmail@gmail.com"
}