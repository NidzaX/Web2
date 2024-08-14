import axios from 'axios';

// Define the base URL for your API
const API_URL = 'https://localhost:44325/api/reviews'; // Update this URL to match your API endpoint

// Function to add a review
export const addReview = async (userEmail, driverId, rating, comment, token) => {
    try {
        const response = await axios.post(`${API_URL}`, {
            userEmail,
            driverId,
            rating,
            comment
        }, {
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json',
            },
        });

        return response.data;
    } catch (error) {
        console.error('Error adding review:', error.response ? error.response.data : error);
        throw error;
    }
};
