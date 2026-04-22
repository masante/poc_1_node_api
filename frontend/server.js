const express = require('express');
const axios = require('axios');

const app = express();
const PORT = process.env.PORT || 3000;
const BACKEND_URL = process.env.BACKEND_URL || 'http://backend:5000';

app.use(express.json());

// GET all persons
app.get('/api/persons', async (req, res) => {
  try {
    const response = await axios.get(`${BACKEND_URL}/api/persons`);
    res.json(response.data);
  } catch (error) {
    console.error('Error fetching persons:', error.message);
    res.status(500).json({ error: 'Failed to fetch persons' });
  }
});

// GET person by ID
app.get('/api/persons/:id', async (req, res) => {
  try {
    const response = await axios.get(`${BACKEND_URL}/api/persons/${req.params.id}`);
    res.json(response.data);
  } catch (error) {
    console.error('Error fetching person:', error.message);
    res.status(500).json({ error: 'Failed to fetch person' });
  }
});

// POST new person
app.post('/api/persons', async (req, res) => {
  try {
    const response = await axios.post(`${BACKEND_URL}/api/persons`, req.body);
    res.status(201).json(response.data);
  } catch (error) {
    console.error('Error creating person:', error.message);
    res.status(500).json({ error: 'Failed to create person' });
  }
});

// PUT update person
app.put('/api/persons/:id', async (req, res) => {
  try {
    const response = await axios.put(`${BACKEND_URL}/api/persons/${req.params.id}`, req.body);
    res.json(response.data);
  } catch (error) {
    console.error('Error updating person:', error.message);
    res.status(500).json({ error: 'Failed to update person' });
  }
});

// DELETE person
app.delete('/api/persons/:id', async (req, res) => {
  try {
    await axios.delete(`${BACKEND_URL}/api/persons/${req.params.id}`);
    res.status(204).send();
  } catch (error) {
    console.error('Error deleting person:', error.message);
    res.status(500).json({ error: 'Failed to delete person' });
  }
});

// ============= ORGANISATIONS ROUTES =============
// GET all organisations
app.get('/api/organisations', async (req, res) => {
  try {
    const response = await axios.get(`${BACKEND_URL}/api/organisations`);
    res.json(response.data);
  } catch (error) {
    console.error('Error fetching organisations:', error.message);
    res.status(500).json({ error: 'Failed to fetch organisations' });
  }
});

// GET organisation by ID
app.get('/api/organisations/:id', async (req, res) => {
  try {
    const response = await axios.get(`${BACKEND_URL}/api/organisations/${req.params.id}`);
    res.json(response.data);
  } catch (error) {
    console.error('Error fetching organisation:', error.message);
    res.status(500).json({ error: 'Failed to fetch organisation' });
  }
});

// GET organisation by OrgId
app.get('/api/organisations/byorgid/:orgId', async (req, res) => {
  try {
    const response = await axios.get(`${BACKEND_URL}/api/organisations/byorgid/${req.params.orgId}`);
    res.json(response.data);
  } catch (error) {
    console.error('Error fetching organisation by OrgId:', error.message);
    res.status(500).json({ error: 'Failed to fetch organisation' });
  }
});

// GET organisations by business type
app.get('/api/organisations/bytype/:businessType', async (req, res) => {
  try {
    const response = await axios.get(`${BACKEND_URL}/api/organisations/bytype/${req.params.businessType}`);
    res.json(response.data);
  } catch (error) {
    console.error('Error fetching organisations by type:', error.message);
    res.status(500).json({ error: 'Failed to fetch organisations' });
  }
});

// POST new organisation
app.post('/api/organisations', async (req, res) => {
  try {
    const response = await axios.post(`${BACKEND_URL}/api/organisations`, req.body);
    res.status(201).json(response.data);
  } catch (error) {
    console.error('Error creating organisation:', error.message);
    res.status(500).json({ error: 'Failed to create organisation' });
  }
});

// PUT update organisation
app.put('/api/organisations/:id', async (req, res) => {
  try {
    const response = await axios.put(`${BACKEND_URL}/api/organisations/${req.params.id}`, req.body);
    res.json(response.data);
  } catch (error) {
    console.error('Error updating organisation:', error.message);
    res.status(500).json({ error: 'Failed to update organisation' });
  }
});

// DELETE organisation
app.delete('/api/organisations/:id', async (req, res) => {
  try {
    await axios.delete(`${BACKEND_URL}/api/organisations/${req.params.id}`);
    res.status(204).send();
  } catch (error) {
    console.error('Error deleting organisation:', error.message);
    res.status(500).json({ error: 'Failed to delete organisation' });
  }
});

// ============= REGISTRATIONS ROUTES =============
// GET all registrations
app.get('/api/registrations', async (req, res) => {
  try {
    const response = await axios.get(`${BACKEND_URL}/api/registrations`);
    res.json(response.data);
  } catch (error) {
    console.error('Error fetching registrations:', error.message);
    res.status(500).json({ error: 'Failed to fetch registrations' });
  }
});

// GET registration by ID
app.get('/api/registrations/:id', async (req, res) => {
  try {
    const response = await axios.get(`${BACKEND_URL}/api/registrations/${req.params.id}`);
    res.json(response.data);
  } catch (error) {
    console.error('Error fetching registration:', error.message);
    res.status(500).json({ error: 'Failed to fetch registration' });
  }
});

// GET registrations by material
app.get('/api/registrations/bymaterial/:material', async (req, res) => {
  try {
    const response = await axios.get(`${BACKEND_URL}/api/registrations/bymaterial/${req.params.material}`);
    res.json(response.data);
  } catch (error) {
    console.error('Error fetching registrations by material:', error.message);
    res.status(500).json({ error: 'Failed to fetch registrations' });
  }
});

// GET registrations by type
app.get('/api/registrations/bytype/:wasteProcessingType', async (req, res) => {
  try {
    const response = await axios.get(`${BACKEND_URL}/api/registrations/bytype/${req.params.wasteProcessingType}`);
    res.json(response.data);
  } catch (error) {
    console.error('Error fetching registrations by type:', error.message);
    res.status(500).json({ error: 'Failed to fetch registrations' });
  }
});

// GET registrations by status
app.get('/api/registrations/bystatus/:status', async (req, res) => {
  try {
    const response = await axios.get(`${BACKEND_URL}/api/registrations/bystatus/${req.params.status}`);
    res.json(response.data);
  } catch (error) {
    console.error('Error fetching registrations by status:', error.message);
    res.status(500).json({ error: 'Failed to fetch registrations' });
  }
});

// POST new registration
app.post('/api/registrations', async (req, res) => {
  try {
    const response = await axios.post(`${BACKEND_URL}/api/registrations`, req.body);
    res.status(201).json(response.data);
  } catch (error) {
    console.error('Error creating registration:', error.message);
    res.status(500).json({ error: 'Failed to create registration' });
  }
});

// PUT update registration
app.put('/api/registrations/:id', async (req, res) => {
  try {
    const response = await axios.put(`${BACKEND_URL}/api/registrations/${req.params.id}`, req.body);
    res.json(response.data);
  } catch (error) {
    console.error('Error updating registration:', error.message);
    res.status(500).json({ error: 'Failed to update registration' });
  }
});

// DELETE registration
app.delete('/api/registrations/:id', async (req, res) => {
  try {
    await axios.delete(`${BACKEND_URL}/api/registrations/${req.params.id}`);
    res.status(204).send();
  } catch (error) {
    console.error('Error deleting registration:', error.message);
    res.status(500).json({ error: 'Failed to delete registration' });
  }
});

// ============= ACCREDITATIONS ROUTES =============
// GET all accreditations
app.get('/api/accreditations', async (req, res) => {
  try {
    const response = await axios.get(`${BACKEND_URL}/api/accreditations`);
    res.json(response.data);
  } catch (error) {
    console.error('Error fetching accreditations:', error.message);
    res.status(500).json({ error: 'Failed to fetch accreditations' });
  }
});

// GET accreditation by ID
app.get('/api/accreditations/:id', async (req, res) => {
  try {
    const response = await axios.get(`${BACKEND_URL}/api/accreditations/${req.params.id}`);
    res.json(response.data);
  } catch (error) {
    console.error('Error fetching accreditation:', error.message);
    res.status(500).json({ error: 'Failed to fetch accreditation' });
  }
});

// GET accreditations by material
app.get('/api/accreditations/bymaterial/:material', async (req, res) => {
  try {
    const response = await axios.get(`${BACKEND_URL}/api/accreditations/bymaterial/${req.params.material}`);
    res.json(response.data);
  } catch (error) {
    console.error('Error fetching accreditations by material:', error.message);
    res.status(500).json({ error: 'Failed to fetch accreditations' });
  }
});

// GET accreditations by type
app.get('/api/accreditations/bytype/:wasteProcessingType', async (req, res) => {
  try {
    const response = await axios.get(`${BACKEND_URL}/api/accreditations/bytype/${req.params.wasteProcessingType}`);
    res.json(response.data);
  } catch (error) {
    console.error('Error fetching accreditations by type:', error.message);
    res.status(500).json({ error: 'Failed to fetch accreditations' });
  }
});

// GET accreditations by status
app.get('/api/accreditations/bystatus/:status', async (req, res) => {
  try {
    const response = await axios.get(`${BACKEND_URL}/api/accreditations/bystatus/${req.params.status}`);
    res.json(response.data);
  } catch (error) {
    console.error('Error fetching accreditations by status:', error.message);
    res.status(500).json({ error: 'Failed to fetch accreditations' });
  }
});

// POST new accreditation
app.post('/api/accreditations', async (req, res) => {
  try {
    const response = await axios.post(`${BACKEND_URL}/api/accreditations`, req.body);
    res.status(201).json(response.data);
  } catch (error) {
    console.error('Error creating accreditation:', error.message);
    res.status(500).json({ error: 'Failed to create accreditation' });
  }
});

// PUT update accreditation
app.put('/api/accreditations/:id', async (req, res) => {
  try {
    const response = await axios.put(`${BACKEND_URL}/api/accreditations/${req.params.id}`, req.body);
    res.json(response.data);
  } catch (error) {
    console.error('Error updating accreditation:', error.message);
    res.status(500).json({ error: 'Failed to update accreditation' });
  }
});

// DELETE accreditation
app.delete('/api/accreditations/:id', async (req, res) => {
  try {
    await axios.delete(`${BACKEND_URL}/api/accreditations/${req.params.id}`);
    res.status(204).send();
  } catch (error) {
    console.error('Error deleting accreditation:', error.message);
    res.status(500).json({ error: 'Failed to delete accreditation' });
  }
});

app.listen(PORT, () => {
  console.log(`Frontend server running on port ${PORT}`);
  console.log(`Backend URL: ${BACKEND_URL}`);
});
