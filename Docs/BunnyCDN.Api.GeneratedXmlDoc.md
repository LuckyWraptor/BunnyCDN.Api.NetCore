# BunnyCDN.Api #

## Type AccessKey

 AccessKey for API authorization and request handling. 



---
#### Property AccessKey.Token

 API Token required for successful REST calls 

**Value**: API Token string



---
#### Property AccessKey.Client

 HttpClient configured with Token header. 

**Value**: Preconfigured HttpClient using the token.



---
#### Method AccessKey.#ctor

 Access token provider instance 



---
#### Method AccessKey.#ctor(System.String)

 Access token provider instance 

|Name | Description |
|-----|------|
|apiKey: |Access token|


---
#### Method AccessKey.SetToken(System.String,System.Net.Http.HttpMessageHandler)

 Sets the token for the key provider 

|Name | Description |
|-----|------|
|apiKey: |API Token|
|httpMessageHandler: |The HttpMessageHandler instance to handle the token requests|


---
#### Method AccessKey.Dispose

 Dispose the AccessKey 



---
## Type AccountKey

 AccountKey for Account-specific API authorization and request handling. 



---
#### Method AccountKey.#ctor

 Account Token provider 



---
#### Method AccountKey.#ctor(System.String)

 Account Token provider 

|Name | Description |
|-----|------|
|apiKey: |API Token|


---
## Type StorageKey

 StorageKey for Storage-specific API authorization and request handling. 



---
#### Method StorageKey.#ctor

 Storage Token provider 



---
#### Method StorageKey.#ctor(System.String)

 Storage Token provider 

|Name | Description |
|-----|------|
|apiKey: |API Token|


---
## Type Account

 Account API interface 



---
#### Field Account.AccountKey

 AccountKey, used to retrieve the required HttpClient 



---
#### Method Account.#ctor(BunnyCDN.Api.AccountKey)

 Account API Interface 

|Name | Description |
|-----|------|
|accKey: |AccountKey token-provider|


---
#### Method Account.#ctor(System.String)

 Account API Interface 

|Name | Description |
|-----|------|
|accountKey: ||


---
#### Method Account.GetBillingSummary

 Retrieve the billing summary from the account. 

**Returns**: BillingSummary containing all the returned data, throws upon failure



---
#### Method Account.ApplyCoupon(System.String)

 Attempts to validate a coupon on your account. 

|Name | Description |
|-----|------|
|couponCode: |Coupon code to validate|
**Returns**: Success state or throws



---
#### Method Account.GetStatisticSummary(System.Nullable{System.DateTime},System.Nullable{System.DateTime},System.Nullable{System.Int64},System.Nullable{System.Int64})

 Retrieves the statistics from the account based of the provided criteria, last month if no datespan was provided 

|Name | Description |
|-----|------|
|dtFrom: |(Optional) From date|
|dtTo: |(Optional) To Date|
|pullZone: |(Optional) PullZone id|
|serverZoneId: |(Optional) ServerZoneId|
**Returns**: The account's statistic summary



---
#### Method Account.GetPath(System.String)

 Gets a valid API URL string. 

|Name | Description |
|-----|------|
|path: |Input path|
**Returns**: A valid URL for API calls



---
#### Method Account.GetPullZones

 Retrieve the PullZones allocated to your account. 

**Returns**: An array of the account PullZones



---
#### Method Account.AddPullZone(System.String,System.String,System.Nullable{System.Int64})

 Create a new PullZone pointing to an origin or a StorageZone 

|Name | Description |
|-----|------|
|name: |Name of the new PullZone|
|originUrl: |Origin url to pull data from|
|storageZoneId: |Storage Zone identifier (If using StorageZone)|
**Returns**: The added PullZone



---
#### Method Account.GetPullZone(System.Int64)

 Retrieve a specific PullZone from your account. 

|Name | Description |
|-----|------|
|zoneId: |PullZone identifier to retrieve|
**Returns**: The requested PullZone, throws otherwise



---
#### Method Account.UpdatePullZone(System.Int64,BunnyCDN.Api.PullZone)

 Update a PullZone 

|Name | Description |
|-----|------|
|zoneId: |PullZone identifier|
|pullZoneChages: |PullZone to apply|
**Returns**: Success state, throws if failed



---
#### Method Account.DeletePullZone(System.Int64)

 Deletes a PullZone (irreversable!) 

|Name | Description |
|-----|------|
|zoneId: |PullZone identifier|
**Returns**: Success state, throws if failed.



---
#### Method Account.AddEdgeRule(System.Int64,BunnyCDN.Api.EdgeRule)

 Adds an edge rule 

|Name | Description |
|-----|------|
|zoneId: |Zone identifier|
|edgeRule: |Edge rule to add|
**Returns**: Success, throws if failed



---
#### Method Account.ModifyEdgeRule(System.Int64,BunnyCDN.Api.EdgeRule)

 Modifies an edge rule 

|Name | Description |
|-----|------|
|zoneId: |Zone id to apply the rule to|
|edgeRule: |Edge rule to modify|
**Returns**: Success, throws if failed



---
#### Method Account.DeleteEdgeRule(System.Int64,System.Guid)

 Deletes an Edge rule (irreversable) 

|Name | Description |
|-----|------|
|zoneId: |Zone identifier|
|guid: |Edge Rule identifier|
**Returns**: Success, throws otherwise



---
#### Method Account.AddPullZoneHostname(System.Int64,System.String)

 Add a Hostname to a provided PullZone 

|Name | Description |
|-----|------|
|zoneId: |Zone identifier|
|hostName: |Hostname to remove|
**Returns**: Success, throws otherwise



---
#### Method Account.DeletePullZoneHostname(System.Int64,System.String)

 Removes a Hostname from a provided PullZone 

|Name | Description |
|-----|------|
|zoneId: |Zone identifier|
|hostName: |Hostname to remove|
**Returns**: Success, throws otherwise



---
#### Method Account.SetPullZoneForceSSL(System.Int64,System.String,System.Boolean)

 Set the forcing of a PullZone SSL 

|Name | Description |
|-----|------|
|zoneId: |Zone identifier|
|hostName: |Hostname|
|forcing: |Forcing state|
**Returns**: Success, throws otherwise



---
#### Method Account.LoadPullZoneCertificate(System.Int64,System.String)

 Request a Let's Encrypt certificate for a PullZone's specific hostname. 

|Name | Description |
|-----|------|
|zoneId: |Zone identifier|
|hostName: |Hostname|
**Returns**: Success, throws otherwise



---
#### Method Account.UploadPullZoneCertificate(System.Int64,System.String,System.String,System.String)

 Upload a custom certificate to for the PullZone's specific hostname 

|Name | Description |
|-----|------|
|zoneId: |Zone identifier|
|hostName: |Hostname|
|certificateBase64: |Certificate (Base64-Encoded)|
|certificateKeyBase64: |Certificate key (Base64-Encoded)|
**Returns**: Success, throws otherwise



---
#### Method Account.PullZoneBlockIP(System.Int64,System.Net.IPAddress)

 Blocks an IPAddress from the specified PullZone 

|Name | Description |
|-----|------|
|zoneId: |PullZone identifier|
|blockedIP: |IPAddress to block|
**Returns**: Succes, throws otherwise



---
#### Method Account.PullZoneBlockIP(System.Int64,System.String)

 Blocks an IP string from the specified PullZone 

|Name | Description |
|-----|------|
|zoneId: |PullZone identifier|
|blockedIP: |IP to block|
**Returns**: Succes, throws otherwise



---
#### Method Account.PullZoneUnblockIP(System.Int64,System.Net.IPAddress)

 Removes a blocked IP from the specified PullZone 

|Name | Description |
|-----|------|
|zoneId: |PullZone identifier|
|blockedIP: |IPAddress|
**Returns**: Succes, throws otherwise



---
#### Method Account.PullZoneUnblockIP(System.Int64,System.String)

 Removes a blocked IP string from the specified PullZone 

|Name | Description |
|-----|------|
|zoneId: |PullZone identifier|
|blockedIP: |IP to unblock|
**Returns**: Succes, throws otherwise



---
#### Method Account.PurgeUrl(System.String)

 Purge a file based off th url provided 

|Name | Description |
|-----|------|
|urlString: |Purge url|
**Returns**: Success, throws if unauthorized/invalid



---
#### Method Account.PurgePullZone(System.Int64)

 Purges all files in a pull zone 

|Name | Description |
|-----|------|
|pullZoneId: |Pull zone id|
**Returns**: Success, throws if unauthorized



---
#### Method Account.GetStorageZones

 Retrieves all the account's storagezones 

**Returns**: An array of StorageZones



---
#### Method Account.CreateStorageZone(System.String)

 Creates a new storage zone. 

|Name | Description |
|-----|------|
|zoneName: |Storage zone name (alphabetical letters, numbers and dashes with 3-20 characters in length)|
**Returns**: The response StorageZone



---
#### Method Account.DeleteStorageZone(System.Int64)

 Delete a StorageZone 

|Name | Description |
|-----|------|
|zoneId: |Storage zone Identifier|
**Returns**: Succes, throws if unauthorized



---
## Type BillingType

 Billing Type enumerable 



---
#### Field BillingType.Bitcoin

 Bitcoin payment 

---
#### Field BillingType.CreditCard

 Credit card payment 

---
#### Field BillingType.MonthlyUsage

 Monthly usage subtraction 

---
#### Field BillingType.CouponCode

 Coupon validation 

---
## Type BunnyException

 Generic BunnyException to catch all (handled) wrapper errors. 



---
#### Method BunnyException.#ctor(System.String)

 A BunnyException wrapper around the Exception, used to catch all BunnyCDN exceptions. 

|Name | Description |
|-----|------|
|message: |Error message for base|
**Returns**: A BunnyCDN exception



---
## Type BunnyTokenException

 Token BunnyException to catch token errors. 



---
#### Property BunnyTokenException.Token

 Token (if any) causing the error. 

**Value**: Token-string or null if none



---
#### Method BunnyTokenException.#ctor(System.String)

 Token exception used for Token faults. 

|Name | Description |
|-----|------|
|message: |Error message for base|
**Returns**: A BunnyCDN token exception



---
#### Method BunnyTokenException.#ctor(System.String,System.String)

 Token exception used for Token faults. 

|Name | Description |
|-----|------|
|message: |Error message for base|
|token: |Token causing the exception|
**Returns**: A BunnyCDN token exception



---
## Type BunnyZoneException

 Zone BunnyException to catch Zone errors. 



---
## Type BunnyNotFoundException

 NotFound BunnyException to catch api 404 responses. 



---
## Type BunnyUnauthorizedException

 Unauthorized BunnyException to catch unauthorized requests. 



---
## Type BunnyBadRequestException

 BadRequest BunnyException to catch bad requests 



---
#### Property BunnyBadRequestException.Error

 Error massage response content. 



---
#### Method BunnyBadRequestException.#ctor(System.String)

 Bad Request exception used for input faults. 

|Name | Description |
|-----|------|
|message: |Error message for base|
**Returns**: A BunnyCDN BadRequest exception



---
#### Method BunnyBadRequestException.#ctor(BunnyCDN.Api.ErrorMessage)

 Error massage response content. 

|Name | Description |
|-----|------|
|error: |Error message context|


---
## Type BunnyInvalidResponseException

 InvalidResponse BunnyException to catch invalid http responses. 



---
#### Method BunnyInvalidResponseException.#ctor

 Invalid response exception used for invalid JSON returns 

**Returns**: A BunnyCDN InvalidResponse exception



---
#### Method BunnyInvalidResponseException.#ctor(System.String)

 Invalid response exception used for invalid JSON returns 

|Name | Description |
|-----|------|
|message: |Error message for base|
**Returns**: A BunnyCDN InvalidResponse exception



---
## Type BunnyInvalidRequestException

 InvalidRequest BunnyException to catch invalid http requests. 



---
#### Method BunnyInvalidRequestException.#ctor

 Invalid response exception used for invalid JSON returns 

**Returns**: A BunnyCDN InvalidResponse exception



---
#### Method BunnyInvalidRequestException.#ctor(System.String)

 Invalid response exception used for invalid JSON returns 

|Name | Description |
|-----|------|
|message: |Error message for base|
**Returns**: A BunnyCDN InvalidResponse exception



---
## Type Internals.UriExtensions

 Url Extensions class. Credits: https://stackoverflow.com/a/19679135 



---
#### Method Internals.UriExtensions.AddParameter(System.Uri,System.String,System.String)

 Adds the specified parameter to the Query String. 

|Name | Description |
|-----|------|
|url: ||
|paramName: |Name of the parameter to add.|
|paramValue: |Value for the parameter to add.|
**Returns**: Url with added parameter.



---
## Type JsonWrapper

 A simple JSON.Net wrapper for easy async Bunny handling 



---
#### Method JsonWrapper.Deserialize``1(System.Net.Http.HttpResponseMessage)

 Deserializes an response object to the desired class, re 

|Name | Description |
|-----|------|
|httpResponse: ||
|T: ||
**Returns**: 



---
#### Method JsonWrapper.Serialize(System.Object)

 Serializes an object using the JsonConvert Serializer 

|Name | Description |
|-----|------|
|any: |Any object to serialize|
**Returns**: A JSON string of the serialized object



---
## Type BillingSummary

 Billing summary object 



---
#### Property BillingSummary.Balance

 The current account balance 



---
#### Property BillingSummary.ThisMonthCharges

 The charges for the current month 



---
#### Property BillingSummary.BillingRecords

 The billing backlog 



---
#### Property BillingSummary.MonthlyChargesStorage

 Current storage price for this month 



---
#### Property BillingSummary.MonthlyChargesEUTraffic

 Current traffic price for the European geozone 



---
#### Property BillingSummary.MonthlyChargesUSTraffic

 Current traffic price for the Nort-American geozone 



---
#### Property BillingSummary.MonthlyChargesASIATraffic

 Current traffic price for the Asian geozone 



---
#### Property BillingSummary.MonthlyChargesSATraffic

 Current traffic price for the South-African geozone 



---
## Type BillingRecord

 Billing record object 



---
#### Property BillingRecord.Id

 The unique ID of the billing record 



---
#### Property BillingRecord.Amount

 The amount referenced by the billing record 



---
#### Property BillingRecord.Payer

 The identifier of the payer who paid for the billing. 



---
#### Property BillingRecord.PaymentId

 The unique ID of the payment that the record was created for 



---
#### Property BillingRecord.Timestamp

 The timestamp when the billing record was created 



---
#### Property BillingRecord.InvoiceAvailable

 True if there is an invoice available for download for this record 



---
#### Property BillingRecord.Type

 The type of the billing record. *Bitcoin* = 1, *CreditCard* = 2, *MonthlyUsage* = 3, *CouponCode* = 5 



---
## Type EdgeRule

 Edge rule interfacing object 



---
#### Property EdgeRule.Guid

 Edge Rule identifier 



---
#### Property EdgeRule.ActionType

 The action type of the edge rule 



---
#### Property EdgeRule.ActionParameter1

 The action parameter 1 of the edge rule 



---
#### Property EdgeRule.ActionParameter2

 The action parameter 2 of the edge rule 



---
#### Property EdgeRule.Triggers

 Triggers of the Rule 



---
#### Property EdgeRule.TriggerMatchingType

 The trigger matching type 



---
#### Property EdgeRule.Description

 The description of the edge rule 



---
#### Property EdgeRule.Enabled

 Enabled status of the rule 



---
## Type EdgeRuleTrigger

 Edge rule trigger object 



---
#### Property EdgeRuleTrigger.Guid

 (Read only) The GUID of the edge rule. 



---
#### Property EdgeRuleTrigger.Type

 The type of the trigger 



---
#### Property EdgeRuleTrigger.PatternMatches

 Pattern matching array 



---
#### Property EdgeRuleTrigger.PatternMatchingType

 The trigger matching type 



---
#### Property EdgeRuleTrigger.Parameter1

 The parameter 1 of the trigger 



---
## Type ErrorMessage

 Error-message response object 



---
#### Property ErrorMessage.Field

 Invalid field name 



---
#### Property ErrorMessage.ErrorKey

 ErrorKey string 



---
#### Property ErrorMessage.HttpCode

 Http StatusCode (Always the same as the HTTP response StatusCode) 



---
#### Property ErrorMessage.Message

 Error message 



---
## Type Hostname

 Hostname interfacing object 



---
#### Property Hostname.Id

 Hostname identifier 



---
#### Property Hostname.Value

 The full hostname domain value 



---
#### Property Hostname.ForceSSL

 True if the hostname will force the users to use a SSL connection 



---
#### Property Hostname.IsSystemHostname

 True if the hostname is a system hostname 



---
#### Property Hostname.HasCertificate

 True if the hostname is configured with an SSL certificate 



---
#### Method Hostname.ToString

 Returns the Hostname value 

**Returns**: 



---
## Type PullZone

 Pullzone interfacing object 



---
#### Property PullZone.Id

 The unique ID of the pull zone 



---
#### Property PullZone.Name

 The name of the pull zone 



---
#### Property PullZone.OriginUrl

 The origin URL where the pull zone files are pulled from. 



---
#### Property PullZone.UserId

 The owning account identifier 



---
#### Property PullZone.Enabled

 True if the pull zone is currently enabled and running 



---
#### Property PullZone.Hostnames

 The list of hostnames linked to the Pull Zone 



---
#### Property PullZone.StorageZoneId

 The ID of the storage zone that the pull zone is linked to. If the value is less than 1, it means the zone is not connected to a storage zone. 



---
#### Property PullZone.AllowedReferrers

 The list of referrer hostnames that are allowed to access the pull zone. Requests containing the header Referer hostname that is not on the list will be rejected. If empty, all the referrers are allowed 



---
#### Property PullZone.BlockedReferrers

 The list of Referrers that are blocked from accessing the pull zone. 



---
#### Property PullZone.BlockedIps

 The list of IPs that are blocked from accessing the pull zone. Requests coming from the following IPs will be rejected. If empty, all the IPs will be allowed 



---
#### Property PullZone.EnableGeoZoneUS

 True if the delivery from the US zone is enabled for this pull zone 



---
#### Property PullZone.EnableGeoZoneEU

 True if the delivery from the EU zone is enabled for this pull zone 



---
#### Property PullZone.EnableGeoZoneASIA

 True if the delivery from the ASIA zone is enabled for this pull zone 



---
#### Property PullZone.EnableGeoZoneSA

 True if the delivery from the South American zone is enabled for this pull zone 



---
#### Property PullZone.EnableGeoZoneAF

 True if the delivery from the African zone is enabled for this pull zone 



---
#### Property PullZone.ZoneSecurityEnabled

 True if the URL secure token authentication security is enabled 



---
#### Property PullZone.ZoneSecurityKey

 The security key used for secure URL token authentication 



---
#### Property PullZone.ZoneSecurityIncludeHashRemoteIP

 True if the zone security hash should include the remote IP 



---
#### Property PullZone.IgnoreQueryStrings

 True if the Pull Zone is ignoring query strings when serving cached objects 



---
#### Property PullZone.MonthlyBandwidthLimit

 The monthly limit of bandwidth in bytes that the pullzone is allowed to use. Set to 0 for unlimited. 



---
#### Property PullZone.MonthlyBandwidthUsed

 The amount of bandwidth in bytes that the pull zone used this month 



---
#### Property PullZone.MonthlyCharges

 The total monthly charges generated by the pull zone this month. 



---
#### Property PullZone.AddHostHeader

 If true, when making an origin pull request a host header will be added with the current hostname. 



---
#### Property PullZone.Type

 The pricing type of the pull zone. **Premium** = 0, **Volume** = 1 



---
#### Property PullZone.CustomNginxConfig

 The custom nginx configuration set for this pull zone. 



---
#### Property PullZone.AccessControlOriginHeaderExtensions

 The list of extensions that will add the access control origin header. 



---
#### Property PullZone.EnableAccessControlOriginHeader

 Set to true if the CORS headers should be enabled 



---
#### Property PullZone.DisableCookies

 If true, the cookies are disabled for the pull zone. 



---
#### Property PullZone.BudgetRedirectedCountries

 The list of country codes that will get redirected to the absolute cheapest possible datacenter. 



---
#### Property PullZone.BlockedCountries

 The list of country codes that will be blocked from accessing the pull zone. 



---
#### Property PullZone.EnableOriginShield





---
#### Property PullZone.CacheControlMaxAgeOverride





---
#### Property PullZone.BurstSize





---
#### Property PullZone.RequestLimit





---
#### Property PullZone.BlockRootPathAccess





---
#### Property PullZone.CacheQuality





---
#### Property PullZone.LimitRatePerSecond





---
#### Property PullZone.LimitRateAfter

 Set to rate-limit after threshold. 



---
#### Property PullZone.ConnectionLimitPerIPCount

 Set the connection limit per IP 



---
#### Property PullZone.PriceOverride

 Price override? 



---
#### Property PullZone.AddCanonicalHeader

 Set to true to add the Canonical header 



---
#### Property PullZone.EnableLogging

 Set to true if the logging for the zone should be enabled 



---
#### Property PullZone.IgnoreVaryHeader

 Set to true to ignore the vary header 



---
#### Property PullZone.EnableCacheSlice

 Set to true if the to enable caching slices (Stream optimization) 



---
#### Property PullZone.EdgeRules

 A list of edge rules for the pullzone 



---
#### Property PullZone.EnableWebPVary

 Set to true if the WebP vary feature should be enabled 



---
#### Property PullZone.EnableCountryCodeVary

 Set to true if the Country vary feature should be enabled 



---
#### Property PullZone.EnableMobileVary

 Set to true if the Mobile vary feature should be enabled 



---
#### Property PullZone.EnableHostnameVary

 Set to true if the Hostname vary feature should be enabled 



---
#### Property PullZone.CnameDomain

 CNAME domain 



---
## Type StatisticSummary

 StatisticSummary object 



---
#### Property StatisticSummary.TotalBandwidthUsed

 Total bandwidth used (in bytes). 



---
#### Property StatisticSummary.TotalRequestsServed

 Total reqests served 



---
#### Property StatisticSummary.CacheHitRate

 Current cache hit-rate 



---
#### Property StatisticSummary.BandwidthUsedChart

 Total bandwidth usage with timestamps 



---
#### Property StatisticSummary.BandwidthCachedChart

 Cached bandwidth usage with timestamps 



---
#### Property StatisticSummary.CacheHitRateChart

 Cache hit-rates with timestamps 



---
#### Property StatisticSummary.RequestsServedChart

 Request serve count with timestamps 



---
#### Property StatisticSummary.PullRequestsPulledChart

 Pull request count with timestamps 



---
#### Property StatisticSummary.UserBalanceHistoryChart

 User balance with timestamps 



---
#### Property StatisticSummary.UserStorageUsedChart

 User storage usage with timestamps 



---
#### Property StatisticSummary.GeoTrafficDistribution

 Geographical bandwidth usage within timespan (or last 30 days if unset) 



---
#### Property StatisticSummary.Error3xxChart

 HTTP 3xx error count with timestamps 



---
#### Property StatisticSummary.Error4xxChart

 HTTP 4xx error count with timestamps 



---
#### Property StatisticSummary.Error5xxChart

 HTTP 5xx error count with timestamps 



---
## Type StorageEntry

 StorageEntry interfacing object 



---
#### Property StorageEntry.FailIndex

 Use-case undocumented 



---
#### Property StorageEntry.Guid

 Storage object identification number 



---
#### Property StorageEntry.StorageZoneName

 Storage zone name 



---
#### Property StorageEntry.Path

 Object location path 



---
#### Property StorageEntry.ObjectName

 Object name 



---
#### Property StorageEntry.Length

 Content length 



---
#### Property StorageEntry.LastChanged

 Object changed timestamp (UTC) 



---
#### Property StorageEntry.IsDirectory

 Object is directory 



---
#### Property StorageEntry.ServerId

 Server identifier storing the object 



---
#### Property StorageEntry.UserId

 User identifier owning the object 



---
#### Property StorageEntry.DateCreated

 Object creation timestamp (UTC) 



---
#### Property StorageEntry.StorageZoneId

 Storage zone identification number 



---
## Type StorageZone

 StorageZone interfacing object 



---
#### Property StorageZone.Id

 The unique ID of the storage zone 



---
#### Property StorageZone.UserId

 The unique user ID that owns the storage zone 



---
#### Property StorageZone.Name

 The name of the storage zone 



---
#### Property StorageZone.Password

 The password for accessing the storage zone 



---
#### Property StorageZone.ReadOnlyPassword

 The read-only password for accessing the storage zone with read only access. 



---
#### Property StorageZone.DateModified

 Date of storage zone modification 



---
#### Property StorageZone.FilesStored

 The number of files stored in the storage zone 



---
#### Property StorageZone.StorageUsed

 he amount of storage used by the storage zone in bytes 



---
#### Property StorageZone.PullZones

 Corresponding pullzones list 

**Value**: 



---
#### Property StorageZone.Deleted

 True if the storage is deleted 



---
## Type Storage

 Storage API endpoint interface 



---
#### Property Storage.Zone

 Storagezone name set for the desired node 

**Value**: Storage zone name



---
#### Field Storage.StorageKey

 StorageKey, used to retrieve the required HttpClient 



---
#### Method Storage.#ctor(BunnyCDN.Api.StorageKey,System.String)

 Storage API interface 

|Name | Description |
|-----|------|
|sKey: |StorageKey token-provider|
|zone: |Zone name for the desired node|


---
#### Method Storage.#ctor(System.String,System.String)

 Storage API interface 

|Name | Description |
|-----|------|
|storageToken: |Token string|
|zone: |Zone name for the desired node|


---
#### Method Storage.GetFile(System.String)

 Retrieve a file from the storage API 

|Name | Description |
|-----|------|
|path: |file path (without prefixing slash)|
**Returns**: The file in a byte array, throws if failed



---
#### Method Storage.GetFolder(System.String)

 Retrieve objects inside folder from the storage API (not-recursive) 

|Name | Description |
|-----|------|
|path: |Path to retrieve objects from|
**Returns**: StorageEntry array containing the objects



---
#### Method Storage.Put(System.Byte[],System.String)

 Creates/overwrites a file (and the missing path) in your storage zone. 

|Name | Description |
|-----|------|
|fileContent: |File contents|
|path: |Path to store file in zone|
**Returns**: Success



---
#### Method Storage.Delete(System.String)

 Deletes a file/directory-path from a the storage zone. 

|Name | Description |
|-----|------|
|path: ||
**Returns**: Success



---
#### Method Storage.GetPath(System.String)

 Gets a valid API URL string. 

|Name | Description |
|-----|------|
|path: |Input path|
**Returns**: A valid URL for API calls



---


