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

app.listen(PORT, () => {
  console.log(`Frontend server running on port ${PORT}`);
  console.log(`Backend URL: ${BACKEND_URL}`);
});
