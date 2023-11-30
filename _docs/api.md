# Microservices Training .NET

## Indices

* [Clients API](#clients-api)
  
  * [List](#1-list)

## Resources API



### 1. Create



***Endpoint:***

```bash
Method: POST
Type: RAW
URL: {{ResourceApiEndpoint}}api/resources/create
```


***Headers:***

| Key | Value | Description |
| --- | ------|-------------|
| Content-Type | application/json |  |



***Body:***

```js        
{
	"Name": "EKS course",
	"DateOfPublication": "2020",
	"Author": "Mario Mercado",
	"Tags": [
		"aws", "kubernetes", "cloud", "devops"
	],
	"Type": "DVD",
	"Description": "This is an amazing course, I think..."
}
```



### 2. Delete



***Endpoint:***

```bash
Method: DELETE
Type: 
URL: {{ResourceApiEndpoint}}/api/resources/delete/5e0175e7618fcae0713d7543
```



### 3. Get



***Endpoint:***

```bash
Method: GET
Type: 
URL: {{ResourceApiEndpoint}}/api/resources/get/a0671dc6-3d6d-11ea-8dd9-38c98647e86c
```



### 4. List



***Endpoint:***

```bash
Method: GET
Type: 
URL: {{ResourceApiEndpoint}}/api/resources/list
```



### 5. Update



***Endpoint:***

```bash
Method: PUT
Type: RAW
URL: {{ResourceApiEndpoint}}/api/resources/update/5e0175e7618fcae0713d7543
```


***Headers:***

| Key | Value | Description |
| --- | ------|-------------|
| Content-Type | application/json |  |



***Body:***

```js        
{
	"Name": "EKS course",
	"DateOfPublication": "2020",
	"Author": "Mario Mercado",
	"Tags": [
		"aws", "kubernetes", "cloud", "devops"
	],
	"Type": "DVD",
	"Description": "This is an amazing course, I think..."
}
```