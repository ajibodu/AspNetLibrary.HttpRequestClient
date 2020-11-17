# AspNetLibrary.HttpRequestClient
Easy to use .Net Http Client where you can call an API with at least 2 line of code.
_snipet below_

### Initialize client 
`HttpRequestClient<RespObj> client = new HttpRequestClient<RespObj>(requestURL);`
_Expected response object **RespObj**_ 
#

### Add Request Header
`client.Request.Headers.Add("X-ApiAuthentication", APIKey);`
#

### **GET** Request  
`var resp =  client.Get();`
_This would return response as **RespObj** _
#

### **GET** Request  
`var resp =  client.GetStrng();`
_This would return response as **string** _
#

### **POST** Request 
`var resp =  client.Post(JsonConvert.SerializeObject(requestObject), PostType.String);`
_This would return response as **RespObj** _
#

### **POST** Request  
`var resp =  client.PostString(JsonConvert.SerializeObject(request), PostType.String);`
_This would return response as **string** _
#

### Access HttpWebResponse
`HttpWebResponse response = client.Response;`
