db.getSiblingDB("PersonDb");

db.persons.insertMany([
  {
    "firstName": "John",
    "lastName": "Doe"
  },
  {
    "firstName": "Jane",
    "lastName": "Smith"
  },
  {
    "firstName": "Michael",
    "lastName": "Johnson"
  },
  {
    "firstName": "Emily",
    "lastName": "Williams"
  },
  {
    "firstName": "Robert",
    "lastName": "Brown"
  },
  {
    "firstName": "Sarah",
    "lastName": "Davis"
  },
  {
    "firstName": "David",
    "lastName": "Miller"
  },
  {
    "firstName": "Jessica",
    "lastName": "Wilson"
  },
  {
    "firstName": "James",
    "lastName": "Moore"
  },
  {
    "firstName": "Lauren",
    "lastName": "Taylor"
  }
]);

print("✓ 10 sample Person records inserted successfully");
