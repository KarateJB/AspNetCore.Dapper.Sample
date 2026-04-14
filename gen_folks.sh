#!/usr/bin/env bash

# Create Folk
curl -k -X POST "https://localhost:63563/api/folk" \
  --include \
  -H "Content-Type: application/json" \
  -d '{
    "Name": "John Doe",
    "Birthday": "1990-01-01T00:00:00Z",
    "Address": "123 Main Street",
    "Phone": "1234567890",
    "EmploymentOn": "2020-01-01T09:00:00Z"
  }'

# Get Folk by id
curl -k "https://localhost:63563/api/folk/5B1320D3-27CB-463E-97E6-06C18CE3F253" --include;

# Update Folk
curl -k -X PUT "https://localhost:63563/api/folk/5B1320D3-27CB-463E-97E6-06C18CE3F253" \
  --include \
  -H "Content-Type: application/json" \
  -d '{
    "Id": "5B1320D3-27CB-463E-97E6-06C18CE3F253",
    "Name": "John Wick",
    "Birthday": "1990-01-01T00:00:00Z",
    "Address": "456 Updated Street",
    "Phone": "0987654321",
    "EmploymentOn": "2021-01-01T09:00:00Z"
  }'

# Delete Folk
curl -k -X DELETE "https://localhost:63563/api/folk/5B1320D3-27CB-463E-97E6-06C18CE3F253" \
  --include;
