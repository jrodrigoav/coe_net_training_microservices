# Microservices Training .NET

## Indices

* [Clients API](#clients-api)
  
  * [List](#1-list)

  * [Inventory API](#inventory-api)

  * [List by Resource](#1-list-by-resource)
  * [List by Resource Unavailable (not available)](#2-list-by-resource-unavailable)
  * [Register](#3-register)
  * [Set Availability](#4-set-availability)
  * [Summary](#5-summary)

  ## Inventory API



### 1. List by Resource



***Endpoint:***

```bash
Method: GET
Type: 
URL: {{InventoryApiEndpoint}}api/inventory/list
```


***Query params:***

| Key | Value | Description |
| --- | ------|-------------|
|  |  |  |

***Body:***

```js        
{
	  "Id": "120",
    "IsAvailable": true
}
```



### 3. Register



***Endpoint:***

```bash
Method: POST
Type: RAW
URL: {{InventoryApiEndpoint}}api/inventory
```


***Headers:***

| Key | Value | Description |
| --- | ------|-------------|
| Content-Type | application/json |  |



***Body:***

```js        
{
	  "Id": "143",
    "Name": "Inventory one"
}
```



### 4. Set Availability



***Endpoint:***

```bash
Method: PUT
Type: RAW
URL: {{InventoryApiEndpoint}}/api/inventory
```


***Headers:***

| Key | Value | Description |
| --- | ------|-------------|
| Content-Type | application/json |  |



***Body:***

```js        
{
	  "Id": "120",
    "IsAvailable": false
}
```



### 5. Summary



***Endpoint:***

```bash
Method: GET
Type: 
URL: {{InventoryApiEndpoint}}/api/inventory