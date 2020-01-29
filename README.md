# rws

available API

http://localhost:52012/api/getexchangerate

Request Body Sample
{
"Source": "SGD",
"Target": "PHP",
"SourceValue": "10"
}

Response Body Sample
{
    "data": {
        "Value": "38.11",
        "Source": "SGD",
        "Target": "PHP",
        "TargetValue": "381.10"
    },
    "success": true,
    "message": null
}


Database: SQL Server (Restore ExchangeRate.bak)
