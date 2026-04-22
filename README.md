# Microservices Application - Person Management System

This is a microservices application consisting of a Node.js frontend, a .NET backend, and a MongoDB datastore.

## Architecture

- **Frontend**: Node.js Express server that acts as an API gateway, proxying requests to the backend
- **Backend**: ASP.NET Core 8 RESTful API with MongoDB integration
- **Database**: MongoDB for data persistence

## System Requirements

- Docker and Docker Compose installed
- (Optional) .NET 8 SDK for local development of the backend
- (Optional) Node.js 18+ for local development of the frontend

## Project Structure

```
.
├── frontend/
│   ├── package.json
│   ├── server.js
│   ├── Dockerfile
│   └── .dockerignore
├── backend/
│   ├── Models/
│   │   ├── Person.cs
│   │   ├── Organisation.cs
│   │   ├── Registration.cs
│   │   ├── Accreditation.cs
│   │   └── Shared.cs (User, Address models)
│   ├── Controllers/
│   │   ├── PersonsController.cs
│   │   ├── OrganisationsController.cs
│   │   ├── RegistrationsController.cs
│   │   └── AccreditationsController.cs
│   ├── Services/
│   │   ├── PersonService.cs
│   │   ├── OrganisationService.cs
│   │   ├── RegistrationService.cs
│   │   └── AccreditationService.cs
│   ├── PersonApi.csproj
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   ├── Dockerfile
│   ├── .dockerignore
│   ├── mongo-init.js (Initialization script for PersonDb)
│   ├── benchmark-write-read.js (Initialization script for epr database)
│   ├── create-collection.js
│   └── data-generators.js
├── docker-compose.yml
└── README.md
```

## Quick Start

### Using Docker Compose (Recommended)

1. Navigate to the project root directory
2. Start the application:
   ```bash
   docker-compose up -d
   ```
   This command will:
   - Build Docker images for the frontend and backend services
   - Pull the MongoDB image (7.0)
   - Create and start all three containers (mongodb, backend-api, frontend-api)
   - Create a Docker network for inter-service communication
   - Mount the initialization script to prepopulate MongoDB with 10 sample Person records (PersonDb)

3. (Optional) Populate the EPR database with organisation data:
   
   **From your local machine:**
   ```bash
   cd backend
   node benchmark-write-read.js
   ```
   
   **From Docker:**
   ```bash
   docker-compose exec backend bash -c "MONGO_URL=mongodb://mongo:27017 npm run init:epr"
   ```
   
   This populates MongoDB with:
   - **Database**: `epr`
   - **Collection**: `organisation_epr`
   - **Records**: 20 organisations with 3 registrations and 3 accreditations each

3. Verify all services are running:
   ```bash
   docker-compose ps
   ```
   You should see three containers: mongodb, backend-api, and frontend-api all in "Up" state.

4. The application will be available at:
   - **Frontend API**: http://localhost:3000 (Node.js gateway)
   - **Backend API (Swagger UI)**: http://localhost:5000/swagger (ASP.NET Core API documentation)
   - **MongoDB**: localhost:27017 (Database instance)

5. View logs from all services:
   ```bash
   docker-compose logs -f
   ```

6. To stop the application:
   ```bash
   docker-compose down
   ```

7. To stop and remove all data (including MongoDB volume):
   ```bash
   docker-compose down -v
   ```

### Local Development

#### Backend Setup
1. Navigate to the `backend` directory
2. Ensure MongoDB is running (or update the connection string in appsettings.json)
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Run the backend:
   ```bash
   dotnet run
   ```

#### Frontend Setup
1. Navigate to the `frontend` directory
2. Install dependencies:
   ```bash
   npm install
   ```
3. Update BACKEND_URL in server.js or set the environment variable
4. Run the frontend:
   ```bash
   npm start
   ```

## API Endpoints

### Frontend API (Port 3000)
The frontend proxies all requests to the backend:

```
GET    /api/persons          - List all persons
GET    /api/persons/:id      - Get person by ID
POST   /api/persons          - Create a new person
PUT    /api/persons/:id      - Update a person
DELETE /api/persons/:id      - Delete a person
```

### Backend API (Port 5000)

#### Person Endpoints

##### List all persons
```
GET /api/persons
```

Response:
```json
[
  {
    "id": "65a1234567890abcdef12345",
    "firstName": "John",
    "lastName": "Doe"
  }
]
```

##### Get person by ID
```
GET /api/persons/:id
```

##### Create a new person
```
POST /api/persons
Content-Type: application/json

{
  "firstName": "Jane",
  "lastName": "Smith"
}
```

##### Update a person
```
PUT /api/persons/:id
Content-Type: application/json

{
  "firstName": "Jane",
  "lastName": "Doe"
}
```

##### Delete a person
```
DELETE /api/persons/:id
```

#### Organisation Endpoints

##### List all organisations
```
GET /api/organisations
```

Response:
```json
[
  {
    "id": "507f1f77bcf86cd799439011",
    "orgId": "500000",
    "businessType": "individual",
    "wasteProcessingTypes": ["reprocessor", "exporter"],
    "reprocessingNations": ["england", "wales"],
    "version": 1,
    "schemaVersion": 1
  }
]
```

##### Get organisation by ID
```
GET /api/organisations/:id
```

##### Get organisation by OrgId
```
GET /api/organisations/byorgid/:orgId
```

##### Get organisations by business type
```
GET /api/organisations/bytype/:businessType
```

##### Create a new organisation
```
POST /api/organisations
Content-Type: application/json

{
  "orgId": "500001",
  "businessType": "partnership",
  "wasteProcessingTypes": ["reprocessor"],
  "reprocessingNations": ["england"],
  "schemaVersion": 1,
  "version": 1
}
```

##### Update an organisation
```
PUT /api/organisations/:id
Content-Type: application/json

{
  "businessType": "unincorporated",
  "wasteProcessingTypes": ["exporter"],
  "version": 2
}
```

##### Delete an organisation
```
DELETE /api/organisations/:id
```

#### Registration Endpoints

##### List all registrations
```
GET /api/registrations
```

Response:
```json
[
  {
    "id": "507f1f77bcf86cd799439012",
    "status": "approved",
    "material": "glass",
    "wasteProcessingType": "reprocessor",
    "wasteRegistrationNumber": "CBDU123456",
    "formSubmissionTime": "2024-01-15T10:30:00Z"
  }
]
```

##### Get registration by ID
```
GET /api/registrations/:id
```

##### Get registrations by material
```
GET /api/registrations/bymaterial/:material
```

Example materials: `glass`, `plastic`, `paper`, `steel`, `aluminium`, `fibre`, `wood`

##### Get registrations by waste processing type
```
GET /api/registrations/bytype/:wasteProcessingType
```

Example types: `reprocessor`, `exporter`

##### Get registrations by status
```
GET /api/registrations/bystatus/:status
```

Example statuses: `created`, `approved`, `archived`

##### Create a new registration
```
POST /api/registrations
Content-Type: application/json

{
  "status": "created",
  "material": "plastic",
  "wasteProcessingType": "exporter",
  "wasteRegistrationNumber": "CBDU789012",
  "formSubmissionTime": "2024-01-20T14:00:00Z",
  "contactDetails": {
    "fullName": "John Smith",
    "email": "john@example.com",
    "phone": "+441234567890",
    "role": "Manager",
    "title": "Mr"
  }
}
```

##### Update a registration
```
PUT /api/registrations/:id
Content-Type: application/json

{
  "status": "approved"
}
```

##### Delete a registration
```
DELETE /api/registrations/:id
```

#### Accreditation Endpoints

##### List all accreditations
```
GET /api/accreditations
```

Response:
```json
[
  {
    "id": "507f1f77bcf86cd799439013",
    "status": "approved",
    "material": "glass",
    "wasteProcessingType": "reprocessor",
    "formSubmissionTime": "2024-01-10T09:00:00Z"
  }
]
```

##### Get accreditation by ID
```
GET /api/accreditations/:id
```

##### Get accreditations by material
```
GET /api/accreditations/bymaterial/:material
```

##### Get accreditations by waste processing type
```
GET /api/accreditations/bytype/:wasteProcessingType
```

##### Get accreditations by status
```
GET /api/accreditations/bystatus/:status
```

##### Create a new accreditation
```
POST /api/accreditations
Content-Type: application/json

{
  "status": "created",
  "material": "aluminium",
  "wasteProcessingType": "reprocessor",
  "formSubmissionTime": "2024-01-22T11:00:00Z",
  "prnIssuance": {
    "plannedIssuance": "1000 tonnes",
    "signatories": [
      {
        "fullName": "Jane Doe",
        "email": "jane@example.com",
        "phone": "+441987654321",
        "role": "Director",
        "title": "Ms"
      }
    ]
  },
  "businessPlan": [
    {
      "description": "Infrastructure investment",
      "detailedDescription": "Investment in new processing equipment",
      "percentSpent": 25
    }
  ]
}
```

##### Update an accreditation
```
PUT /api/accreditations/:id
Content-Type: application/json

{
  "status": "approved"
}
```

##### Delete an accreditation
```
DELETE /api/accreditations/:id
```

## Ports

- **Frontend**: 3000
- **Backend**: 5000
- **MongoDB**: 27017

## Environment Variables

### Backend
- `ASPNETCORE_ENVIRONMENT`: Set to Development or Production
- `ASPNETCORE_URLS`: Backend server URL (default: http://+:5000)
- `ConnectionStrings__MongoDB`: MongoDB connection string (default: mongodb://mongo:27017)

### Frontend
- `NODE_ENV`: Node environment (development/production)
- `BACKEND_URL`: Backend API URL (default: http://backend:5000)
- `PORT`: Frontend server port (default: 3000)

## MongoDB Connection

The MongoDB instance runs in a Docker container with:
- Default port: 27017
- Username: admin
- Password: admin

### Databases and Collections

#### PersonDb
- **Database name**: PersonDb
- **Collections**:
  - `persons` - Simple person records (auto-initialized with 10 sample records)

#### EPR Database
- **Database name**: epr
- **Collections**:
  - `organisation_epr` - Organisation records with nested registrations and accreditations (populated via `benchmark-write-read.js`)

### Connecting to MongoDB

#### Using Docker
```bash
# From the project root
docker-compose exec mongo mongosh -u admin -p admin
```

#### From host machine
```bash
mongosh "mongodb://admin:admin@localhost:27017"
```

## Data Models

### Person
```
{
  _id: ObjectId,
  firstName: string,
  lastName: string
}
```

### Organisation (EPR Database)
```
{
  _id: ObjectId,
  orgId: string,
  systemReference: ObjectId,
  schemaVersion: integer,
  version: integer,
  wasteProcessingTypes: string[],
  reprocessingNations: string[],
  businessType: string,
  registrations: Registration[],
  accreditations: Accreditation[],
  contactDetails: User,
  formSubmissionRawDataId: string,
  createdAt: DateTime,
  partnership: Partnership
}
```

### Registration (Nested in Organisation)
```
{
  _id: ObjectId,
  formSubmissionTime: DateTime,
  status: string,
  material: string,
  wasteProcessingType: string,
  gridReference: string,
  wasteRegistrationNumber: string,
  wasteManagementPermits: WasteManagementPermit[],
  approvedPersons: User[],
  contactDetails: User,
  submitterContactDetails: User,
  samplingInspectionPlan: string[],
  formSubmissionRawDataId: string,
  siteAddress: Address,
  recyclingProcess: string[],
  yearlyMetrics: YearlyMetrics,
  plantEquipmentDetails: string,
  suppliers: string,
  exportPorts: string[],
  overseasSites: string[],
  noticeAddress: Address,
  accreditationId: string
}
```

### Accreditation (Nested in Organisation)
```
{
  _id: ObjectId,
  formSubmissionTime: DateTime,
  status: string,
  material: string,
  wasteProcessingType: string,
  prnIssuance: PrnIssuance,
  businessPlan: BusinessPlanItem[],
  contactDetails: User,
  submitterContactDetails: User,
  samplingInspectionPlan: string[],
  formSubmissionRawDataId: string,
  siteAddress: Address,
  recyclingProcess: string[],
  overseasSites: string[]
}
```

### Shared Models

#### User
```
{
  fullName: string,
  email: string,
  phone: string,
  role: string,
  title: string
}
```

#### Address
```
{
  line1: string,
  line2: string,
  town: string,
  county: string,
  country: string,
  postcode: string
}
```

## Troubleshooting

### Backend cannot connect to MongoDB
- Ensure MongoDB container is running: `docker-compose ps`
- Check the MongoDB connection string in `appsettings.json`
- Verify the network is properly configured: `docker network ls`

### Frontend cannot reach backend
- Ensure the backend container is running
- Check the BACKEND_URL environment variable
- Verify network connectivity between containers

### Port already in use
- Change the port mappings in docker-compose.yml
- Or stop other services using those ports

## Querying MongoDB Collections

### Accessing MongoDB

#### Using MongoDB Shell (mongosh)

1. Connect to the MongoDB container:
   ```bash
   docker-compose exec mongo mongosh -u admin -p admin
   ```

2. Switch to the PersonDb database:
   ```javascript
   use PersonDb
   ```

3. View all collections:
   ```javascript
   show collections
   ```

### PersonDb Queries

#### View all Person records
```javascript
use PersonDb
db.persons.find()
```

#### View all persons with formatted output
```javascript
db.persons.find().pretty()
```

#### Count total persons
```javascript
db.persons.countDocuments()
```

#### Find a person by firstName
```javascript
db.persons.find({ "firstName": "John" })
```

#### Find a person by lastName
```javascript
db.persons.find({ "lastName": "Doe" })
```

### EPR Database Queries

#### Switch to EPR database
```javascript
use epr
```

#### View all organisations
```javascript
db.organisation_epr.find().pretty()
```

#### Count organisations
```javascript
db.organisation_epr.countDocuments()
```

#### Find organisations by business type
```javascript
db.organisation_epr.find({ "businessType": "individual" }).pretty()
```

#### Find organisations by waste processing type
```javascript
db.organisation_epr.find({ "wasteProcessingTypes": "reprocessor" }).pretty()
```

#### Get a specific organisation by OrgId
```javascript
db.organisation_epr.findOne({ "orgId": "500000" })
```

#### Count registrations across all organisations
```javascript
db.organisation_epr.aggregate([
  { $unwind: "$registrations" },
  { $count: "totalRegistrations" }
])
```

#### Find registrations by status
```javascript
db.organisation_epr.find({ "registrations.status": "approved" }).pretty()
```

#### Find registrations by material type
```javascript
db.organisation_epr.find({ "registrations.material": "glass" }).pretty()
```

## Backend Services

The backend provides services for managing Person, Organisation, Registration, and Accreditation data:

### PersonService
- `GetAllPersonsAsync()` - Retrieve all persons
- `GetPersonByIdAsync(id)` - Get specific person
- `CreatePersonAsync(person)` - Create new person
- `UpdatePersonAsync(id, person)` - Update person
- `DeletePersonAsync(id)` - Delete person

### OrganisationService
- `GetAllOrganisationsAsync()` - List all organisations
- `GetOrganisationByIdAsync(id)` - Get organisation by ID
- `GetOrganisationByOrgIdAsync(orgId)` - Get organisation by OrgId
- `CreateOrganisationAsync(org)` - Create organisation
- `UpdateOrganisationAsync(id, org)` - Update organisation
- `DeleteOrganisationAsync(id)` - Delete organisation
- `GetOrganisationsByBusinessTypeAsync(type)` - Filter by business type

### RegistrationService
- `GetAllRegistrationsAsync()` - List all registrations
- `GetRegistrationByIdAsync(id)` - Get registration by ID
- `GetRegistrationsByMaterialAsync(material)` - Filter by material
- `GetRegistrationsByWasteProcessingTypeAsync(type)` - Filter by waste processing type
- `GetRegistrationsByStatusAsync(status)` - Filter by status
- `CreateRegistrationAsync(registration)` - Create registration
- `UpdateRegistrationAsync(id, registration)` - Update registration
- `DeleteRegistrationAsync(id)` - Delete registration

### AccreditationService
- `GetAllAccreditationsAsync()` - List all accreditations
- `GetAccreditationByIdAsync(id)` - Get accreditation by ID
- `GetAccreditationsByMaterialAsync(material)` - Filter by material
- `GetAccreditationsByWasteProcessingTypeAsync(type)` - Filter by waste processing type
- `GetAccreditationsByStatusAsync(status)` - Filter by status
- `CreateAccreditationAsync(accreditation)` - Create accreditation
- `UpdateAccreditationAsync(id, accreditation)` - Update accreditation
- `DeleteAccreditationAsync(id)` - Delete accreditation

## Sample Data

### PersonDb Sample Data
When the application starts, MongoDB is automatically prepopulated with 10 sample Person records:

| First Name | Last Name |
|-----------|----------|
| John | Doe |
| Jane | Smith |
| Michael | Johnson |
| Emily | Williams |
| Robert | Brown |
| Sarah | Davis |
| David | Miller |
| Jessica | Wilson |
| James | Moore |
| Lauren | Taylor |

### EPR Database Sample Data
Run `benchmark-write-read.js` to populate the EPR database with:
- **20 organisations** with various business types (individual, unincorporated, partnership)
- **3 registrations per organisation** for different materials and waste processing types
- **3 accreditations per organisation** for waste processing operations
- Sample materials: aluminium, fibre, glass, paper, plastic, steel, wood
- Waste processing types: reprocessor, exporter

## Testing the API

You can test the API using tools like Postman, curl, or the Swagger UI.

### Example using curl

#### Create a person
```bash
curl -X POST http://localhost:3000/api/persons \
  -H "Content-Type: application/json" \
  -d '{"firstName":"John","lastName":"Doe"}'
```

#### List all persons
```bash
curl http://localhost:3000/api/persons
```

#### Update a person (replace :id with actual ID)
```bash
curl -X PUT http://localhost:3000/api/persons/:id \
  -H "Content-Type: application/json" \
  -d '{"firstName":"Jane","lastName":"Doe"}'
```

#### Delete a person
```cd bench 
curl -X DELETE http://localhost:3000/api/persons/:id
```

### Testing Organisation Endpoints

#### List all organisations
```bash
curl http://localhost:3000/api/organisations
```

#### Get a specific organisation
```bash
curl http://localhost:3000/api/organisations/:id
```

#### Get organisation by OrgId
```bash
curl http://localhost:3000/api/organisations/byorgid/500000
```

#### Get organisations by business type
```bash
curl http://localhost:3000/api/organisations/bytype/individual
```

#### Create a new organisation
```bash
curl -X POST http://localhost:3000/api/organisations \
  -H "Content-Type: application/json" \
  -d '{
    "orgId": "500100",
    "businessType": "partnership",
    "wasteProcessingTypes": ["reprocessor"],
    "reprocessingNations": ["england", "wales"],
    "schemaVersion": 1,
    "version": 1
  }'
```

#### Update an organisation
```bash
curl -X PUT http://localhost:3000/api/organisations/:id \
  -H "Content-Type: application/json" \
  -d '{"businessType": "unincorporated"}'
```

#### Delete an organisation
```bash
curl -X DELETE http://localhost:3000/api/organisations/:id
```

### Testing Registration Endpoints

#### List all registrations
```bash
curl http://localhost:3000/api/registrations
```

#### Get a specific registration
```bash
curl http://localhost:3000/api/registrations/:id
```

#### Get registrations by material
```bash
curl http://localhost:3000/api/registrations/bymaterial/glass
```

#### Get registrations by waste processing type
```bash
curl http://localhost:3000/api/registrations/bytype/reprocessor
```

#### Get registrations by status
```bash
curl http://localhost:3000/api/registrations/bystatus/approved
```

#### Create a new registration
```bash
curl -X POST http://localhost:3000/api/registrations \
  -H "Content-Type: application/json" \
  -d '{
    "status": "created",
    "material": "plastic",
    "wasteProcessingType": "exporter",
    "wasteRegistrationNumber": "CBDU999888",
    "formSubmissionTime": "2024-04-22T10:00:00Z",
    "contactDetails": {
      "fullName": "Alice Johnson",
      "email": "alice@example.com",
      "phone": "+441111222333",
      "role": "Manager",
      "title": "Ms"
    }
  }'
```

#### Update a registration
```bash
curl -X PUT http://localhost:3000/api/registrations/:id \
  -H "Content-Type: application/json" \
  -d '{"status": "approved"}'
```

#### Delete a registration
```bash
curl -X DELETE http://localhost:3000/api/registrations/:id
```

### Testing Accreditation Endpoints

#### List all accreditations
```bash
curl http://localhost:3000/api/accreditations
```

#### Get a specific accreditation
```bash
curl http://localhost:3000/api/accreditations/:id
```

#### Get accreditations by material
```bash
curl http://localhost:3000/api/accreditations/bymaterial/aluminium
```

#### Get accreditations by waste processing type
```bash
curl http://localhost:3000/api/accreditations/bytype/reprocessor
```

#### Get accreditations by status
```bash
curl http://localhost:3000/api/accreditations/bystatus/approved
```

#### Create a new accreditation
```bash
curl -X POST http://localhost:3000/api/accreditations \
  -H "Content-Type: application/json" \
  -d '{
    "status": "created",
    "material": "steel",
    "wasteProcessingType": "reprocessor",
    "formSubmissionTime": "2024-04-22T14:30:00Z",
    "prnIssuance": {
      "plannedIssuance": "5000 tonnes",
      "signatories": [
        {
          "fullName": "Bob Smith",
          "email": "bob@example.com",
          "phone": "+441444555666",
          "role": "Director",
          "title": "Mr"
        }
      ]
    }
  }'
```

#### Update an accreditation
```bash
curl -X PUT http://localhost:3000/api/accreditations/:id \
  -H "Content-Type: application/json" \
  -d '{"status": "approved"}'
```

#### Delete an accreditation
```bash
curl -X DELETE http://localhost:3000/api/accreditations/:id
```

## Additional Notes

- All API responses from the frontend are proxied through to the backend
- The frontend provides a unified entry point for all API calls
- MongoDB data persists in a named volume `mongo_data`
- The application uses Docker networks for inter-service communication

## License

N/A
